using Consul;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebTest.Models;

namespace WebTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            #region 无网关配置
            //var url = "";
            //ConsulClient client = new ConsulClient(c =>
            //{
            //    c.Address = new Uri("http://106.75.217.26:21080/");//consul地址
            //});
            //var servicesA = client.Catalog.Service("ServerA").Result.Response;
            //if (servicesA != null && servicesA.Any())
            //{
            //    var service = servicesA.ElementAt(new Random().Next(servicesA.Count()));
            //    url = $"http://{service.ServiceAddress}:{service.ServicePort}/Tset/index";
            //}
            //string contentA = this.InvokeApi(url);
            //base.ViewBag.ServerA = JsonConvert.DeserializeObject<TestObj>(contentA);

            //var servicesB = client.Catalog.Service("ServerB").Result.Response;
            //if (servicesB != null && servicesB.Any())
            //{
            //    var service = servicesB.ElementAt(new Random().Next(servicesB.Count()));
            //    url = $"http://{service.ServiceAddress}:{service.ServicePort}/Tset/index";
            //}
            //string contentB = this.InvokeApi(url);
            //base.ViewBag.ServerB = JsonConvert.DeserializeObject<TestObj>(contentB);

            //var servicesC = client.Catalog.Service("ServerC").Result.Response;
            //if (servicesC != null && servicesC.Any())
            //{
            //    var service = servicesC.ElementAt(new Random().Next(servicesC.Count()));
            //    url = $"http://{service.ServiceAddress}:{service.ServicePort}/Tset/index";
            //}
            //string contentC = this.InvokeApi(url);
            //base.ViewBag.ServerC = JsonConvert.DeserializeObject<TestObj>(contentC);
            //return View();
            #endregion

            #region 有网关配置
            var urlA = "http://111.71.27.26/ServerA/index";
            var urlB = "http://111.71.27.26/ServerB/index";
            var urlC = "http://111.71.27.26/ServerC/index";

            //服务A
            string contentA = this.InvokeApi(urlA);
            try
            {
                base.ViewBag.ServerA = JsonConvert.DeserializeObject<TestObj>(contentA);
            }
            catch (Exception)
            {
                base.ViewBag.ServerA = new TestObj
                {
                    ip = "服务器繁忙",
                    port = "服务器繁忙",
                    name = "ServerA"
                };
            }
            //服务B
            string contentB = this.InvokeApi(urlB);
            try
            {
                base.ViewBag.ServerB = JsonConvert.DeserializeObject<TestObj>(contentB);
            }
            catch (Exception)
            {
                base.ViewBag.ServerB = new TestObj
                {
                    ip = "服务器繁忙",
                    port = "服务器繁忙",
                    name = "ServerB"
                };
            }
            //服务C
            string contentC = this.InvokeApi(urlC);
            try
            {
                base.ViewBag.ServerC = JsonConvert.DeserializeObject<TestObj>(contentC);
            }
            catch (Exception)
            {
                base.ViewBag.ServerC = new TestObj
                {
                    ip = "服务器繁忙",
                    port = "服务器繁忙",
                    name = "ServerC"
                };
            }

            return View();
            #endregion
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

        #region Private Method
        /// <summary>
        /// HttpGet请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string InvokeApi(string url)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage message = new HttpRequestMessage();
                message.Method = HttpMethod.Get;
                message.RequestUri = new Uri(url);
                var result = httpClient.SendAsync(message).Result;
                string content = result.Content.ReadAsStringAsync().Result;
                return content;
            }
        }
        #endregion

        public class TestObj
        {
            public string ip { get; set; }
            public string port { get; set; }
            public string name { get; set; }
        }
    }
}
