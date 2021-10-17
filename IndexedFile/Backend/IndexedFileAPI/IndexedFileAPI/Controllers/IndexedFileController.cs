using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IndexedFile;

namespace IndexedFileAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndexedFileController : Controller
    {
        private readonly IIndexedRepository _repo;

        public IndexedFileController(IIndexedRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            string[] allData = _repo.GetAllData();
            string[] allIndexes = _repo.GetAllIndexes();

            return new JsonResult(new
            {
                data = allData,
                indexes = allIndexes
            });
        }

        [HttpPost]
        public IActionResult Add([FromForm] string value)
        {
            _repo.Add(value);

            string[] allData = _repo.GetAllData();
            string[] allIndexes = _repo.GetAllIndexes();

            return new JsonResult(new
            {
                data = allData,
                indexes = allIndexes
            });
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            _repo.Remove(id);

            string[] allData = _repo.GetAllData();
            string[] allIndexes = _repo.GetAllIndexes();

            return new JsonResult(new
            {
                data = allData,
                indexes = allIndexes
            });
        }

        [HttpGet("{id}")]
        public IActionResult Find(int id)
        {
            int index = _repo.Find(id);

            return new JsonResult(new
            {
                lineId = index
            });
        } 
    }
}
