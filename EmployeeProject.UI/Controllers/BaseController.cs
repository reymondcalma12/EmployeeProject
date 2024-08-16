using Microsoft.AspNetCore.Mvc;

namespace EmployeeProject.UI.Controllers
{
    public class BaseController<T> : Controller where T : class
    {
        private ILogger<T> _logger { get; set; }

        public ILogger<T> Logger => _logger ?? HttpContext.RequestServices.GetRequiredService<ILogger<T>>();
    }
}
