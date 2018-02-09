using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LASI.Core.Configuration;
using LASI.Utilities;
using static System.StringComparer;

namespace LASI.Core.Heuristics
{
    sealed class NameProvider
    {
        public async Task InitializeAsync()
        {
            (FemaleNames, MaleNames, LastNames) = (await ReadLinesAsync(Paths.Names.Female), await ReadLinesAsync(Paths.Names.Male), await ReadLinesAsync(Paths.Names.Last));
            GenderAmbiguousNames = MaleNames.WithComparer(OrdinalIgnoreCase).Intersect(FemaleNames);

            var stratified =
                from name in MaleNames.WithIndices()
                let rankedMale = (rank: (double)name.index / MaleNames.Count, name: name.element)
                join rankedFemale in from name in FemaleNames.WithIndices()
                                     select (rank: (double)name.index / FemaleNames.Count, name: name.element)
                on rankedMale.name equals rankedFemale.name
                let gender = rankedFemale.rank / rankedMale.rank > 1 ? Gender.Female : rankedMale.rank / rankedFemale.rank > 1 ? Gender.Male : default
                group rankedFemale.name by gender;

            var byLikelyGender = stratified.ToDictionary(strata => strata.Key);

            MaleNames = MaleNames.Except(byLikelyGender[Gender.Female]);
            FemaleNames = FemaleNames.Except(byLikelyGender[Gender.Male]);
        }
        /// <summary>
        /// Determines if provided text is in the set of Female or Male first names.
        /// </summary>
        /// <param name="text">The text to check.</param>
        /// <returns>
        /// <c>true</c> if the provided text is in the set of Female or Male first names;
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool IsFirstName(string text) => FemaleNames.Count > MaleNames.Count ?
            MaleNames.Contains(text) || FemaleNames.Contains(text) :
            FemaleNames.Contains(text) || MaleNames.Contains(text);

        /// <summary>
        /// Returns a value indicating whether the provided string corresponds to a common last
        /// name in the English language. Lookups are performed in a case insensitive manner and
        /// currently do not respect plurality.
        /// </summary>
        /// <param name="text">The Name to lookup</param>
        /// <returns>
        /// <c>true</c> if the provided string corresponds to a common last name in the English
        /// language; otherwise, <c>false</c>.
        /// </returns>
        public bool IsLastName(string text) => LastNames.Contains(text);

        /// <summary>
        /// Returns a value indicating whether the provided string corresponds to a common
        /// female name in the English language. Lookups are performed in a case insensitive
        /// manner and currently do not respect plurality.
        /// </summary>
        /// <param name="text">The Name to lookup</param>
        /// <returns>
        /// <c>true</c> if the provided string corresponds to a common female name in the
        /// English language; otherwise, <c>false</c>.
        /// </returns>
        public bool IsFemaleFirst(string text) => FemaleNames.Contains(text);

        /// <summary>
        /// Returns a value indicating whether the provided string corresponds to a common male
        /// name in the English language. Lookups are performed in a case insensitive manner and
        /// currently do not respect plurality.
        /// </summary>
        /// <param name="text">The Name to lookup</param>
        /// <returns>
        /// <c>true</c> if the provided string corresponds to a common male name in the English
        /// language; otherwise, <c>false</c>.
        /// </returns>
        public bool IsMaleFirst(string text) => MaleNames.Contains(text);

        private static async Task<ImmutableSortedSet<string>> ReadLinesAsync(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            {
                return (await reader.ReadToEndAsync())
                    .SplitRemoveEmpty('\r', '\n')
                    .Select(s => s.Trim())
                    .ToImmutableSortedSet(OrdinalIgnoreCase);
            }
        }

        /// <summary>
        /// Gets a sequence of all known Last Names.
        /// </summary>
        public ImmutableSortedSet<string> LastNames { get; private set; }

        /// <summary>
        /// Gets a sequence of all known Female Names.
        /// </summary>
        public ImmutableSortedSet<string> FemaleNames { get; private set; }

        /// <summary>
        /// Gets a sequence of all known Male Names.
        /// </summary>
        public ImmutableSortedSet<string> MaleNames { get; private set; }

        /// <summary>
        /// Gets a sequence of all known Names which are just as likely to be Female or Male.
        /// </summary>
        public ImmutableSortedSet<string> GenderAmbiguousNames { get; private set; }


        public ImmutableSortedSet<string> AllNames
        {
            get
            {
                return allNames = allNames ?? buildNames();

                ImmutableSortedSet<string> buildNames()
                {
                    var builder = FemaleNames.ToBuilder();
                    builder.UnionWith(MaleNames);
                    builder.UnionWith(GenderAmbiguousNames);
                    builder.UnionWith(LastNames);
                    return builder.ToImmutable().WithComparer(OrdinalIgnoreCase);
                }
            }
        }

        private ImmutableSortedSet<string> allNames;

    }
}
