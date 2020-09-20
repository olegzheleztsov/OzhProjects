// Create By: Oleg Gelezcov                        (olegg )
// Project: BasketAPI     File: HomeController.cs    Created at 2020/09/10/11:48 PM
// All rights reserved, for personal using only
// 

using Microsoft.AspNetCore.Mvc;

namespace BasketAPI.Controllers
{
    public class HomeController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}