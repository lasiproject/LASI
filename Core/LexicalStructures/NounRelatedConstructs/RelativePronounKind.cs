using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LASI.Core
{
    /// <summary>
    /// Defines the various kinds of Relative Pronouns
    /// </summary>
    public enum RelativePronounKind : byte
    {
        /// <summary>
        /// UNDEFINED
        /// </summary>
        UNDEFINED = 0,
        /// <summary>
        /// SubjectRolePersonal
        /// </summary>
        SubjectRolePersonal,
        /// <summary>
        /// ObjectRoleEntity
        /// </summary>
        ObjectRoleEntity,
        /// <summary>
        /// ObjectRoleLocational
        /// </summary>
        ObjectRoleLocational,
        /// <summary>
        /// ObjectRoleTemporal
        /// </summary>
        ObjectRoleTemporal,
        /// <summary>
        /// ObjectRoleExpository
        /// </summary>
        ObjectRoleExpository,
    }
}
