using System.Web.UI.WebControls;

namespace LASI.WebApp.Models.Lexical
{
    interface IViewModel<T>
    {
        T ModelFor { get; }
        int Id { get; }
        Style Style { get; }
    }
}