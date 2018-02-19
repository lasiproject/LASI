using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LASI.WebService.Data.Models
{
    public interface IDeletable
    {
        bool Deleted { get; }
    }
}
