using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class RoutesController : Controller
    {
        private IActionDescriptorCollectionProvider _provider;

        public RoutesController(IActionDescriptorCollectionProvider provider) {
            _provider = provider;
        }

        public IActionResult Index()
        {
            var urls = _provider.ActionDescriptors.Items
                    .Select(descriptor => '/' + string.Join('/', descriptor.RouteValues.Values
                                                                                    .Where(v => v != null)
                                                                                    .Select(c => c.ToLower())
                                                                                    .Reverse()))
                    .Distinct()
                    .ToList();

            return Ok(urls);
        }
    }
}
