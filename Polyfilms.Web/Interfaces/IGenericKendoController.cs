using Microsoft.AspNetCore.Mvc;
using Kendo.Mvc.UI;

namespace PolyFilms.Web.Interfaces
{
    public interface IGenericKendoController<in T> where T : class 
    {
        IActionResult Index();

        IActionResult KendoRead([DataSourceRequest] DataSourceRequest request);

        IActionResult KendoSave([DataSourceRequest] DataSourceRequest request, T model);

        IActionResult KendoDestroy([DataSourceRequest] DataSourceRequest request, T model);

    }
}