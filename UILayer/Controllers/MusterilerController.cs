using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UILayer.Models;

namespace UILayer.Controllers
{
    public class MusterilerController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<Musteriler> musterilist = new List<Musteriler>();
            using (var httpClient =new HttpClient())
            {
                using(var responce =await httpClient.GetAsync("https://localhost:44334/api/Musteriler"))
                {
                    string apiResponce=await responce.Content.ReadAsStringAsync();
                    musterilist=JsonConvert.DeserializeObject<List<Musteriler>>(apiResponce);
                }
            }
            return View(musterilist);   
        }

        //get kısmımızı bu şekilde yazabiliyoruz.
        public ViewResult GetMusteriler()=>View();

        //post bu şekilde yazıldı.
        [HttpPost]
        public async Task<IActionResult> GetMusteriler(int id)
        {
            Musteriler musteriler= new Musteriler();
            using (var httpClient = new HttpClient())
            {
                using (var responce = await httpClient.GetAsync("https://localhost:44334/api/Musteriler"+id))
                {
                    string apiResponce = await responce.Content.ReadAsStringAsync();
                    musteriler = JsonConvert.DeserializeObject<Musteriler>(apiResponce);
                }
            }
            return View(musteriler);
        }


        [HttpGet]
        public ViewResult AddMusteriler() => View();

        [HttpPost]
        public async Task<IActionResult> AddMusteriler(Musteriler musteriler)
        {
            if (ModelState.IsValid)
            {
                using(var httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(musteriler),Encoding.UTF8, "application/json");
                    using (var responce = await httpClient.PostAsync("https://localhost:44334/api/Musteriler", content))
                    {
                        string apiResponce = await responce.Content.ReadAsStringAsync();
                        musteriler = JsonConvert.DeserializeObject<Musteriler>(apiResponce);
                    }

                }
                return View(musteriler);
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateMusteriler(int id)
        {
            Musteriler musteriler = new Musteriler();
            using (var httpClient = new HttpClient())
            {
                using (var responce = await httpClient.GetAsync("https://localhost:44334/api/Musteriler" + id))
                {
                    string apiResponce = await responce.Content.ReadAsStringAsync();
                    musteriler = JsonConvert.DeserializeObject<Musteriler>(apiResponce);
                }
            }
            return View(musteriler);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMusteriler(Musteriler musteriler)
        {
            Musteriler mus1 = new Musteriler();
            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(musteriler.Id.ToString()),"Id");
                    content.Add(new StringContent(musteriler.AdSoyad.ToString()), "AdSoyad");
                    content.Add(new StringContent(musteriler.Adres.ToString()), "Adres");
                    content.Add(new StringContent(musteriler.Telefon.ToString()), "Telefon");
                    content.Add(new StringContent(musteriler.Email.ToString()), "Email");

                    using (var responce = await httpClient.PutAsync("https://localhost:44334/api/Musteriler", content))
                    {
                        string apiResponce = await responce.Content.ReadAsStringAsync();
                        ViewBag.Result = "Success";
                        mus1 = JsonConvert.DeserializeObject<Musteriler>(apiResponce);  
                    }
                }
               
            }
            return View(mus1);
        }


        [HttpPost]

        public async Task<IActionResult> DeleteMusteriler(int musteriId)
        {
            using (var httpClient = new HttpClient())
            {
                using (var responce = await httpClient.DeleteAsync("https://localhost:44334/api/Musteriler"+ musteriId))
                {
                    string apiResponce = await responce.Content.ReadAsStringAsync();
                    
                }
            }
            return RedirectToAction("Index");
        }



    }
}
