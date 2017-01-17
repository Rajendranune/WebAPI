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
    public class ProductController : ApiController
    {
        private ProductRepository _repo;

        public ProductController()
        {
            Initilization = InitializeAsync();
        }

        public Task Initilization { get; private set; }

        private async Task InitializeAsync()
        {
            _repo = new ProductRepository();
            await _repo.Initilization;
        }

        //api/people/
        public async Task<IHttpActionResult> Get()
        {
            await Initilization;
            var response = await _repo.GetProductAsync();
            return Ok(response);
        }

        public async Task<IHttpActionResult> Get(string id)
        {
            if (id == null) { return BadRequest(); }
            await Initilization;
            var response = await _repo.GetProductAsync(id);
            if (response != null)
                return Ok(response);
            return NotFound();
        }

        public async Task<IHttpActionResult> Post(ProductModel model)
        {
            await Initilization;
            model.Id = Guid.NewGuid().ToString();
            var response = await _repo.CreateProduct(model);
            return Ok(response.Resource);
        }

        public async Task<IHttpActionResult> Put(ProductModel model)
        {
            await Initilization;
            var response = await _repo.UpdateProductAsync(model);
            return Ok(response.Resource);
        }

        public async Task<IHttpActionResult> Delete(string id)
        {
            if (id == null) { return BadRequest(); }
            await Initilization;
            var response = await _repo.DeleteProductAsync(id);
            return Ok();
        }
    }
}
