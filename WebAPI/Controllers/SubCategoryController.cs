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
    public class SubCategoryController : ApiController
    {
        private SubCategoryRepository _repo;

        public SubCategoryController()
        {
            Initilization = InitializeAsync();
        }

        public Task Initilization { get; private set; }

        private async Task InitializeAsync()
        {
            _repo = new SubCategoryRepository();
            await _repo.Initilization;
        }

        //api/people/
        public async Task<IHttpActionResult> Get()
        {
            await Initilization;
            var people = await _repo.GetSubCategoryAsync();
            return Ok(people);
        }

        public async Task<IHttpActionResult> Get(string id)
        {
            if (id == null)
            {

                return BadRequest();
            }
            else
            {
                await Initilization;
                var response = await _repo.GetSubCategoryAsync(id);
                if (response != null)
                    return Ok(response);
                return NotFound();
            }
        }
       

        public async Task<IHttpActionResult> Post(SubCategoryModel m)
        {
            await Initilization;
            m.Id = Guid.NewGuid().ToString();
            var response = await _repo.SubCreateCategory(m);
            return Ok(response.Resource);
        }

        public async Task<IHttpActionResult> Put(SubCategoryModel model)
        {
            await Initilization;
            var response = await _repo.UpdateSubCategoryAsync(model);
            return Ok(response.Resource);
        }

        public async Task<IHttpActionResult> Delete(string id)
        {
            if (id == null) {
                return BadRequest();
            }
            await Initilization;
            var response = await _repo.DeleteSubCategoryAsync(id);
            return Ok();
        }
       
    }
}
