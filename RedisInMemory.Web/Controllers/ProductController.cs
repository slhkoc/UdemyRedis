using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisInMemory.Web.Controllers
{
    public class ProductController : Controller
    {
        private IMemoryCache _memoryCache;
        public ProductController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public IActionResult Index()
        {

            if (!_memoryCache.TryGetValue("tarih", out string tarihcache))
            {
                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();

               
                options.AbsoluteExpiration = DateTime.Now.AddMinutes(1); //--> AbsoluteExpiration
                options.SlidingExpiration = TimeSpan.FromSeconds(10); //--> Sliding Expiration

                _memoryCache.Set<string>("tarih", DateTime.Now.ToString(), options);
            }
            return View();
        }

        public IActionResult Show()
        {
            _memoryCache.TryGetValue("tarih", out string tarihcache);

            ViewBag.zaman = tarihcache;

            return View();
        }
    }
}
