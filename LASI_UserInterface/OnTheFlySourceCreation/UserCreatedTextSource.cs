﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm;

namespace LASI.UserInterface.OnTheFlySourceCreation
{
    public sealed class UserCreatedTextSource : RawTextFragment
    {
        public UserCreatedTextSource(string text, string name) : base(text, name) { }
        public UserCreatedTextSource(IEnumerable<string> lines, string name) : base(lines, name) { }

    }
}
