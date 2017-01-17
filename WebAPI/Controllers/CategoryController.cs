using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Data;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Data;

namespace WebAPI.Controllers
{
    public class CategoryController : ApiController
    {
        private CategoryRepository _repo;
        public CategoryController()
        {
            Initilization = InitializeAsync();
        }

        public Task Initilization { get; private set; }

        private async Task InitializeAsync()
        {
            _repo = new CategoryRepository();
            await _repo.Initilization;
        }

        //api/people/
        public async Task<IHttpActionResult> Get()
        {
            await Initilization;
            var people = await _repo.GetCategoryAsync();
            return Ok(people);
        }

        public async Task<IHttpActionResult> Get(string id)
        {
            if (id == null) { return BadRequest(); }
            await Initilization;
            var response = await _repo.GetCategoryAsync(id);
            if (response != null)
                return Ok(response);
            return NotFound();
        }

        public async Task<IHttpActionResult> Post(CategoryModel model)
        {
            await Initilization;
            model.Id = Guid.NewGuid().ToString();
            var response = await _repo.CreateCategory(model);
            return Ok(response.Resource);
        }

        public async Task<IHttpActionResult> Put(CategoryModel model)
        {
            await Initilization;
            var response = await _repo.UpdateCategoryAsync(model);
            return Ok(response.Resource);
        }

        public async Task<IHttpActionResult> Delete(string id)
        {
            if (id == null) { return BadRequest(); }
            await Initilization;
            var response = await _repo.DeleteCategoryAsync(id);
            return Ok();
        }
    }
}
