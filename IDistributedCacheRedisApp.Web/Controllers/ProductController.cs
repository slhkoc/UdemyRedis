using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IDistributedCacheRedisApp.Web.Controllers
{
    public class ProductController : Controller
    {
        private IDistributedCache _distributedCache;
        public ProductController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
            
        }
        public async Task<IActionResult> Index()
        {
            DistributedCacheEntryOptions cacheEntryOptions = new DistributedCacheEntryOptions();
            cacheEntryOptions.AbsoluteExpiration = DateTime.Now.AddMinutes(1);

            _distributedCache.SetString("name", "salih", cacheEntryOptions);
           await _distributedCache.SetStringAsync("surname", "koc", cacheEntryOptions);
            return View();
        }

        public IActionResult Show()
        {
            string name = _distributedCache.GetString("name");
            ViewBag.name = name;
            return View();
        }

        public IActionResult Remove()
        {
            _distributedCache.Remove("name");

            return View();
        }
    }
}
