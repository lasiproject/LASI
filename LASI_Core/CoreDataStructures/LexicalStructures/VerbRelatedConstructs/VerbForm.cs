using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.Core
{
    /// <summary>
    /// Defines the Six Tenses of Verbs
    /// </summary>
    public enum VerbForm : byte
    {
        /// <summary>
        /// The base tense
        /// </summary>
        Base,
        /// <summary>
        /// The past tense
        /// </summary>
        Past,
        /// <summary>
        /// The past participle tense
        /// </summary>
        PastParticiple,
        /// <summary>
        /// The present participle tense
        /// </summary>
        PresentParticiple,
        /// <summary>
        /// The singular present tense
        /// </summary>
        SingularPresent,
        /// <summary>
        /// the third person singular present tense
        /// </summary>
        ThirdPersonSingularPresent

    }

}