using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace LASI.WebApp.Old.Models
{
    public abstract class TextualModel<T> : IViewModel<T>
    {
        public TextualModel(T modelFor) {
            Id = System.Threading.Interlocked.Increment(ref IdGenerator);
            ModelFor = modelFor;
        }
        public int Id { get; }
        public T ModelFor { get; }
        public abstract string Text { get; }
        public abstract Style Style { get; }
        private static int IdGenerator;
    }
}