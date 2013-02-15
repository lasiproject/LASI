﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    /// <summary>
    /// Defines the behavior of a Descriptive construct which can Describe an instance of any class which implements the IDescribable Inter
    /// </summary>
    /// <see cref="IDescribable"/>
    public interface IDescriber : LASI.Algorithm.ILexical
    {
        IEntity Describes {
            get;
            set;
        }
    }
}
