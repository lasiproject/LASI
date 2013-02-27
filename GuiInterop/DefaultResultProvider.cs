using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ServiceModel;
using LASI.Algorithm;
namespace LASI.GuiInterop
{

    public class DefaultResultProvider : IResultProvider
    {
        private KeyedByTypeCollection<IEnumerable<ILexical>> lexicalItems = new KeyedByTypeCollection<IEnumerable<ILexical>>();
        public IEnumerable<ILexical> this[Type T] {
            get {
                return lexicalItems[T];
            }
        }
    }

}

