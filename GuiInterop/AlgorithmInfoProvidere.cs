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
    public interface IResultProvider
    {
        IEnumerable<ILexical> this[Type T] {
            get;
            set;
        }
    }
    public class results : IResultProvider
    {
        private KeyedByTypeCollection<IEnumerable<ILexical>> myItems = new KeyedByTypeCollection<IEnumerable<ILexical>>();
        public IEnumerable<ILexical> this[Type T] {
            get {
                return myItems[T];
            }
            set {
                throw new NotImplementedException();
            }
        }
    }

}

