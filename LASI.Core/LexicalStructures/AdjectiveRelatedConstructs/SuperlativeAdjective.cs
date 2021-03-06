﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Core
{
    /// <summary>
    /// <para>A specialization of the Adjective class, SuperlativeAdjective represents adjectives such as "greenest" and "best".</para>
    /// </summary>
    public class SuperlativeAdjective : Adjective
    {
        /// <summary>
        /// Initializes a new instance of the SuperalitiveAdjective class
        /// </summary>
        /// <param name="text">The text content of the Adjective.</param>
        public SuperlativeAdjective(string text) : base(text) { }
    }
}