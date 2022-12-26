using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusterilerController : ControllerBase
    {
        private IMusterilerRepository musterilerRepository;

        private IWebHostEnvironment webHostEnvironment;


        public MusterilerController(IMusterilerRepository repo, IWebHostEnvironment environment)
        {
            musterilerRepository = repo;
            webHostEnvironment = environment;
        }


        [HttpGet]
        public IEnumerable<Musteriler> GetMusteriler()
        {
            return musterilerRepository.GetAllMusteriler().ToList();
        }

        [HttpGet("id")]
        public Musteriler GetMusteriById(int id)
        {
            return musterilerRepository.GetMusteriById(id); 
        }

        [HttpPost]
        public Musteriler Create([FromBody] Musteriler musteriler)
        {
            return musterilerRepository.AddMusteri(musteriler);
        }

        [HttpPut]
        public Musteriler Update([FromForm] Musteriler musteriler)
        {
            return musterilerRepository.UpdateMusteri(musteriler);
        }

        [HttpDelete]
        public void Delete(int? id) => musterilerRepository.DeleteMusteri(id);

    }
}
