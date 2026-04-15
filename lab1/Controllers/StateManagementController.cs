using Microsoft.AspNetCore.Mvc;

namespace lab1.Controllers
{
    public class StateManagementController : Controller
    {
        public IActionResult SessionSet(string username, int age)
        {
            HttpContext.Session.SetString("UserName", username);
            HttpContext.Session.SetInt32("UserAge", age);
            return RedirectToAction("SessionGet");
        }

        public IActionResult SessionGet()
        {
            var userName = HttpContext.Session.GetString("UserName");
            var userAge = HttpContext.Session.GetInt32("UserAge");

            return Content($"[Session] UserName: {userName}, UserAge: {userAge}");
        }

        public IActionResult SessionClear()
        {
            HttpContext.Session.Clear();
            return Content("Session Cleared");
        }


        // Cookies
        public IActionResult CookieSet(string userName)
        {
            CookieOptions options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1),
                IsEssential = true
            };

            Response.Cookies.Append("UserName", userName, options);
            return RedirectToAction("CookieGet");
        }

        public IActionResult CookieGet()
        {
            var userName = Request.Cookies["UserName"];
            return Content($"[Cookie] UserName: {userName}");
        }

        public IActionResult CookieDelete()
        {
            Response.Cookies.Delete("UserName");
            return Content("Cookie Deleted");
        }

        // Query String
        public IActionResult QueryStringDemo(string name, int age)
        {
            return Content($"[QueryString] Name: {name}, Age: {age}");
        }

        // Example:
        // /StateManagement/QueryStringDemo?name=Mohamed&age=22

        
        // 4. Hidden Fields
        
        public IActionResult HiddenFieldForm()
        {
            string html = @"
                <form method='post' action='/StateManagement/HiddenFieldReceive'>
                    <input type='hidden' name='userName' value='Mohamed' />
                    <button type='submit'>Send Hidden Data</button>
                </form>";
            return Content(html, "text/html");
        }

        [HttpPost]
        public IActionResult HiddenFieldReceive(string userName)
        {
            return Content($"[HiddenField] UserName: {userName}");
        }

        // 5. Form Data
        public IActionResult FormDemo()
        {
            string html = @"
                <form method='post' action='/StateManagement/FormReceive'>
                    Name: <input type='text' name='name' /><br/>
                    Age: <input type='number' name='age' /><br/>
                    <button type='submit'>Submit</button>
                </form>";
            return Content(html, "text/html");
        }

        [HttpPost]
        public IActionResult FormReceive(string name, int age)
        {
            return Content($"[Form] Name: {name}, Age: {age}");
        }

        // ViewData
        public IActionResult ViewDataDemo()
        {
            ViewData["Message"] = "Hello from ViewData!";
            ViewData["Name"] = "Mohamed";

            return View();
        }

        // ViewBag
        public IActionResult ViewBagDemo()
        {
            ViewBag.Message = "Hello from ViewBag!";
            ViewBag.Name = "Mohamed";

            return View();
        }

        // TempData
        public IActionResult TempDataSet(string message)
        {
            TempData["Message"] = message;
            return RedirectToAction("TempDataGet");
        }

        public IActionResult TempDataGet()
        {
            return View();
        }
    }
}
