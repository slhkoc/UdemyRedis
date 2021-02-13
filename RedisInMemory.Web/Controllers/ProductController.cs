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

               
                options.AbsoluteExpiration = DateTime.Now.AddSeconds(10); //--> AbsoluteExpiration
                options.SlidingExpiration = TimeSpan.FromSeconds(10); //--> Sliding Expiration

                options.Priority = CacheItemPriority.High;

                options.RegisterPostEvictionCallback((key, value, reason, state) =>
                {
                    _memoryCache.Set("callback", $"{key}->{value}=>sebep:{reason}");
                });

                _memoryCache.Set<string>("tarih", DateTime.Now.ToString(), options);
            }
            return View();
        }

        public IActionResult Show()
        {
            _memoryCache.TryGetValue("tarih", out string tarihcache);
            _memoryCache.TryGetValue("callback", out string callback);
            ViewBag.zaman = tarihcache;
            ViewBag.callback = callback;

            return View();
        }
    }
}
