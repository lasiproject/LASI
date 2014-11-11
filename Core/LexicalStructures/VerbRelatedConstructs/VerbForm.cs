using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LASI.Core
{
    /// <summary>
    /// Defines common conjugations of Verbs
    /// </summary>
    public enum VerbForm : byte
    {
        /// <summary>
        /// The base tense
        /// </summary>
        Base = 0,
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
        /// The third person singular present tense
        /// </summary>
        ThirdPersonSingularPresent

    }

}