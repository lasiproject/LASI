using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LASI.Algorithm;
namespace LASI.GuiInterop
{
    public interface AlgorithmResultProvider<T> where T : ILexical, IEnumerable<T>
    {

        IEnumerator<T> GetEnumerator();


    }
}

