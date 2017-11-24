using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using LASI.Core.Configuration;
using LASI.Utilities;
using static System.StringComparer;

namespace LASI.Core {
    sealed class NameProvider {
        public NameProvider() {
            var fileData = Task.Run(async () => new {
                FemaleNames = await ReadLinesAsync(Paths.Names.Female),
                MaleNames = await ReadLinesAsync(Paths.Names.Male),
                LastNames = await ReadLinesAsync(Paths.Names.Last)
            }).Result;
            FemaleNames = fileData.FemaleNames;
            MaleNames = fileData.MaleNames;
            GenderAmbiguousNames = MaleNames.Intersect(FemaleNames).WithComparer(OrdinalIgnoreCase);
            LastNames = fileData.LastNames;

            var stratified =
                from m in MaleNames.Select((name, i) => new { Rank = (double)i / MaleNames.Count, Name = name })
                join f in FemaleNames.Select((name, i) => new { Rank = (double)i / FemaleNames.Count, Name = name })
                on m.Name equals f.Name
                group f.Name by f.Rank / m.Rank > 1 ? 'F' : m.Rank / f.Rank > 1 ? 'M' : 'U';
            var byLikelyGender = stratified.ToDictionary(strata => strata.Key);

            MaleNames = MaleNames.Except(byLikelyGender['F']);
            FemaleNames = FemaleNames.Except(byLikelyGender['M']);
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

        private static async Task<ImmutableSortedSet<string>> ReadLinesAsync(string fileName) {
            using (var reader = new System.IO.StreamReader(fileName)) {
                return (await reader.ReadToEndAsync())
                    .SplitRemoveEmpty('\r', '\n')
                    .Select(s => s.Trim())
                    .ToImmutableSortedSet(OrdinalIgnoreCase);
            }
        }

        /// <summary>
        /// Gets a sequence of all known Last Names.
        /// </summary>
        public ImmutableSortedSet<string> LastNames { get; }

        /// <summary>
        /// Gets a sequence of all known Female Names.
        /// </summary>
        public ImmutableSortedSet<string> FemaleNames { get; }

        /// <summary>
        /// Gets a sequence of all known Male Names.
        /// </summary>
        public ImmutableSortedSet<string> MaleNames { get; }

        /// <summary>
        /// Gets a sequence of all known Names which are just as likely to be Female or Male.
        /// </summary>
        public ImmutableSortedSet<string> GenderAmbiguousNames { get; }

        public ImmutableSortedSet<string> AllNames {
            get {
                var builder = FemaleNames.ToBuilder();
                builder.UnionWith(MaleNames);
                builder.UnionWith(GenderAmbiguousNames);
                builder.UnionWith(LastNames);
                return builder.ToImmutable().WithComparer(OrdinalIgnoreCase);
            }
        }
    }
}
