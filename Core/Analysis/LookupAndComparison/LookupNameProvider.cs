using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using LASI.Utilities;

namespace LASI.Core.Heuristics
{
    public static partial class Lexicon
    {
        private sealed class NameProvider
        {
            public void Load()
            {
                Task.WaitAll(new[] {
                    Task.Run(async () => femaleNames = await ReadLinesAsync(Paths.Names.Female)),
                    Task.Run(async () => maleNames = await ReadLinesAsync(Paths.Names.Male)),
                    Task.Run(async () => lastNames = await ReadLinesAsync(Paths.Names.Last))
                });
                genderAmbiguousNames = maleNames.Intersect(femaleNames);

                var stratified =
                    from m in maleNames.Select((name, index) => new { Rank = (double)index / maleNames.Count, Name = name })
                    join f in femaleNames.Select((name, i) => new { Rank = (double)i / femaleNames.Count, Name = name })
                    on m.Name equals f.Name
                    group f.Name by f.Rank / m.Rank > 1 ? 'F' : m.Rank / f.Rank > 1 ? 'M' : 'U';
                var byLikelyGender = stratified.ToDictionary(strata => strata.Key);

                maleNames = maleNames.Except(byLikelyGender['F']);
                femaleNames = femaleNames.Except(byLikelyGender['M']);
            }

            /// <summary>
            /// Determines if provided text is in the set of Female or Male first names.
            /// </summary>
            /// <param name="text">The text to check.</param>
            /// <returns> <c>true</c> if the provided text is in the set of Female or Male first names; otherwise, <c>false</c>.</returns>
            public bool IsFirstName(string text)
            {
                return femaleNames.Count > maleNames.Count ?
                    maleNames.Contains(text) || femaleNames.Contains(text) :
                    femaleNames.Contains(text) || maleNames.Contains(text);
            }
            /// <summary>
            /// Returns a value indicating whether the provided string corresponds to a common last name in the English language. 
            /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
            /// </summary>
            /// <param name="text">The Name to lookup</param>
            /// <returns> <c>true</c> if the provided string corresponds to a common last name in the English language; otherwise, <c>false</c>.</returns>
            public bool IsLastName(string text)
            {
                return lastNames.Contains(text);
            }
            /// <summary>
            /// Returns a value indicating whether the provided string corresponds to a common female name in the English language. 
            /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
            /// </summary>
            /// <param name="text">The Name to lookup</param>
            /// <returns>
            /// <c>true</c> if the provided string corresponds to a common female name in the english language; otherwise, <c>false</c>.
            /// </returns>
            public bool IsFemaleFirst(string text)
            {
                return femaleNames.Contains(text);
            }
            /// <summary>
            /// Returns a value indicating whether the provided string corresponds to a common male name in the english language. 
            /// Lookups are performed in a case insensitive manner and currently do not respect plurality.
            /// </summary>
            /// <param name="text">The Name to lookup</param>
            /// <returns>
            /// <c>true</c> if the provided string corresponds to a common male name in the English language; otherwise, <c>false</c>.
            /// </returns>
            public bool IsMaleFirst(string text)
            {
                return maleNames.Contains(text);
            }

            private static async Task<ImmutableSortedSet<string>> ReadLinesAsync(string fileName)
            {
                using (var reader = new System.IO.StreamReader(fileName))
                {
                    var data = await reader.ReadToEndAsync();
                    return data.SplitRemoveEmpty('\r', '\n')
                        .Select(s => s.Trim())
                        .ToImmutableSortedSet(IgnoreCase);
                }
            }

            /// <summary>
            /// Gets a sequence of all known Last Names.
            /// </summary>
            public IReadOnlyCollection<string> LastNames
            {
                get { return lastNames.ToList().ToImmutableList(); }
            }
            /// <summary>
            /// Gets a sequence of all known Female Names.
            /// </summary>
            public IReadOnlyCollection<string> FemaleNames
            {
                get
                {
                    return femaleNames.ToList().AsReadOnly();
                }
            }
            /// <summary>
            /// Gets a sequence of all known Male Names.
            /// </summary>
            public IReadOnlyCollection<string> MaleNames
            {
                get
                {
                    return maleNames.ToList().AsReadOnly();
                }
            }
            /// <summary>
            /// Gets a sequence of all known Names which are just as likely to be Female or Male.
            /// </summary>
            public IReadOnlyCollection<string> GenderAmbiguousNames
            {
                get
                {
                    return genderAmbiguousNames.ToList().AsReadOnly();
                }
            }

            public IImmutableSet<string> AllNames => new[] { lastNames, maleNames, femaleNames, genderAmbiguousNames }
                .Aggregate((fold, current) => fold.Union(current))
                .WithComparer(IgnoreCase);

            #region Fields

            // Name Data Sets
            private ImmutableSortedSet<string> lastNames;
            private ImmutableSortedSet<string> maleNames;
            private ImmutableSortedSet<string> femaleNames;
            private ImmutableSortedSet<string> genderAmbiguousNames;

            #endregion

        }
        static StringComparer IgnoreCase => StringComparer.OrdinalIgnoreCase;
    }
}
