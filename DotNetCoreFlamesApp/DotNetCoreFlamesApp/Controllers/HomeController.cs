using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotNetCoreFlamesApp.Models;
using System.Net.Http;

namespace DotNetCoreFlamesApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Flames()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Flames(FlamesViewModel flamesViewModel)
        {
            var data ="";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost/FlamesApi/");
                client.DefaultRequestHeaders.Clear();

                HttpResponseMessage res = await client.GetAsync("api/Values?name1=" + flamesViewModel.Flames.Name1 + "&name2=" + flamesViewModel.Flames.Name2);

                if (res.IsSuccessStatusCode)
                {
                    data = res.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    data = "Error";
                }

            }
            flamesViewModel = ProcessRequest(data[1], flamesViewModel.Flames);
            return View(flamesViewModel);
        }
        [HttpGet]
        public IActionResult Clear()
        {
            return View("Flames");
        }

        private FlamesViewModel ProcessRequest(char data, FlamesModel fm)
        {
            FlamesViewModel flamesViewModel;
            string name1 = fm.Name1;
            string name2 = fm.Name2;
            switch (data)
            {
                case 'F':
                    flamesViewModel = new FlamesViewModel()
                    {
                        Result = name1 + " & " + name2 + " are Friends",
                        ImgSrc = "~/Images/Friends.jpg",
                        AltImgSrc = "Friends Img"
                    };
                    break;
                case 'L':
                    flamesViewModel = new FlamesViewModel()
                    {
                        Result = name1 + " & " + name2 + " are Lovers",
                        ImgSrc = "~/Images/Lovers.jpg",
                        AltImgSrc = "Lovers Img"
                    };
                    break;
                case 'A':
                    flamesViewModel = new FlamesViewModel()
                    {
                        Result = name1 + " has more Affection towards " + name2,
                        ImgSrc = "~/Images/affection.jpg",
                        AltImgSrc = "affection Img"
                    };
                    break;
                case 'M':
                    flamesViewModel = new FlamesViewModel()
                    {
                        Result = name1 + " & " + name2 + " will get Married",
                        ImgSrc = "~/Images/Marriage.jpg",
                        AltImgSrc = "Marriage Img"
                    };
                    break;
                case 'E':
                    flamesViewModel = new FlamesViewModel()
                    {
                        Result = name1 + " & " + name2 + " are Enemies",
                        ImgSrc = "~/Images/Enemies.jpg",
                        AltImgSrc = "Enemies Img"
                    };
                    break;

                case 'S':
                    flamesViewModel = new FlamesViewModel()
                    {
                        Result = name1 + " & " + name2 + " are Siblings",
                        ImgSrc = "~/Images/siblings.jpg",
                        AltImgSrc = "Siblings Img"
                    };
                    break;
                default:
                    flamesViewModel = new FlamesViewModel()
                    {
                        Result = "We are Sorry!! Something wrong happened.",
                        ImgSrc = "~/Images/Sorry.jpg",
                        AltImgSrc = "Sorry Img"
                    };
                    break;
            }
            flamesViewModel.Flames = fm;
            return flamesViewModel;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
