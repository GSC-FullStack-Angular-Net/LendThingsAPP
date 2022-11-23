using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LendThingsMVC.Controllers
{
    public abstract class BaseCRUDController : Controller
    {
        protected IActionResult ManageServiceErrorResponse(HttpResponseMessage response)
        {
            switch (response.StatusCode
)
            {
                case System.Net.HttpStatusCode.NotFound:
                    return NotFound();
                default:
                    return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
