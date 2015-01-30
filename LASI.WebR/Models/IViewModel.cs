using System.Web.UI.WebControls;

namespace LASI.WebR.Models
{
    interface IViewModel<out T>
    {
        T ModelFor { get; }
        int Id { get; }
        Style Style { get; }
    }
}