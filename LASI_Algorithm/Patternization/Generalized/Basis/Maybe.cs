using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm.Patternization.Generalized.Basis
{
    public abstract class Maybe<T>
    {
        public static bool operator true(Maybe<T> maybe) { return maybe is Some<T>; }
        public static bool operator false(Maybe<T> maybe) { return !(maybe is Some<T>); }
    }
    public class None<T> : Maybe<T> { }
    public class Some<T> : Maybe<T>
    {
        public Some(T value) { if (value == null) { throw new ArgumentNullException("value"); } _value = value; }
        public T Value {
            get { return _value; }
        }
        private T _value;
    }
}
