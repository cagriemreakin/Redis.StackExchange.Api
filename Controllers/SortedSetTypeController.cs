using Microsoft.AspNetCore.Mvc;
using RedisExhange.Web.Api.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RedisExhange.Web.Api.Controllers
{
    public class SortedSetTypeController : Controller
    {
        private readonly IRedisService _redisService;
        private string KEY = "SortedSetNames";
        public SortedSetTypeController(IRedisService redisService)
        {
            _redisService = redisService;
        }

        public IActionResult Index()
        {
            var db = _redisService.GetDatabase(3);
            var names = new HashSet<string>();
            if (db.KeyExists(KEY))
            {
                db.SortedSetRangeByRankWithScores(KEY, order: StackExchange.Redis.Order.Ascending).ToList().ForEach(i =>
                {
                    names.Add(i.ToString());
                });

            }
            return View(names);
        }


        [HttpPost]
        public IActionResult Add(string name,int score)
        {
            var db = _redisService.GetDatabase(3);
            db.SortedSetAdd(KEY, name,score);
            db.KeyExpire(KEY, DateTime.Now.AddMinutes(5));
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteItem(string name)
        {
            var db = _redisService.GetDatabase(3);
            await db.SortedSetRemoveAsync(KEY, name);
            return RedirectToAction("Index");

        }
    }
}

