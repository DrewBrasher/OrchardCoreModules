using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrewBrasher.OrchardCore.ContentWarning.Controllers;
public class HomeController : Controller
{
    public ActionResult Index()
    {
        return View();
    }
}
