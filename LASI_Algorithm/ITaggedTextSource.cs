using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LASI.Algorithm
{
    public interface ITaggedTextSource
    {
        string GetText();
        Task<string> GetTextAsync();
        string DataName { get; }
    }
}
