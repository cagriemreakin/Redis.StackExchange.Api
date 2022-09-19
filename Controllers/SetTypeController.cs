using Microsoft.AspNetCore.Mvc;
using RedisExhange.Web.Api.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RedisExhange.Web.Api.Controllers
{
    public class SetTypeController : Controller
    {
        // GET: /<controller>/
        private readonly IRedisService _redisService;
        private string KEY = "SetNames";
        public SetTypeController(IRedisService redisService)
        {
            _redisService = redisService;
        }

        public IActionResult Index()
        {
            var db = _redisService.GetDatabase(2);
            var names = new HashSet<string>();
            if (db.KeyExists(KEY))
            {
                db.SetMembers(KEY).ToList().ForEach(i =>
                {
                    names.Add(i.ToString());
                });
            }
            return View(names);
        }


        [HttpPost]
        public IActionResult Add(string name)
        {
            var db = _redisService.GetDatabase(2);
            db.KeyExpire(KEY, DateTime.Now.AddMinutes(5));
            db.SetAddAsync(KEY, name).Wait();
            return RedirectToAction("Index");
        }

        public  async Task<IActionResult> DeleteItem(string name)
        {
            var db = _redisService.GetDatabase(2);
            await db.SetRemoveAsync(KEY, name);
            return RedirectToAction("Index");

        }
    }
}

