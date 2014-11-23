using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experimentation
{
    class Class1
    {
        string ClassName => nameof(Class1) + Id(new Class1()?.ClassName);
        public static T Id<T>(T text) => text;
    }
}
