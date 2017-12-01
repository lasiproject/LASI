using Microsoft.EntityFrameworkCore;

namespace LASI.WebService
{
    public class InteractiveChoicesContext : DbContext
    {
        public InteractiveChoicesContext(DbContextOptions options) : base(options)
        {
        }

        public InteractiveChoicesContext()
        {
        }
    }
}
