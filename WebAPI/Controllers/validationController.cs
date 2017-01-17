using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using WebAPI.Data;

namespace WebAPI.Controllers
{
    public class validationController : ApiController
    {

        private SubCategoryRepository _repo;
        private ProductRepository _repo1;

        public validationController()
        {
            Initilization = InitializeAsync();
            Initilization1 = InitializeAsync1();
        }
        public Task Initilization { get; private set; }
        public Task Initilization1 { get; private set; }

        private async Task InitializeAsync()
        {
            _repo = new SubCategoryRepository();
            await _repo.Initilization;
        }
        private async Task InitializeAsync1()
        {
            _repo1 = new ProductRepository();
            await _repo.Initilization;
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
                var response = await _repo.CheckCategoryExistAsyn(id);
                if (response != null)
                    return Ok(response);
                return NotFound();
            }
        }
        [Route("api/Validation/GetSubCategory/{value:alpha}")]
        public async Task<IHttpActionResult> GetSubCategory(string value)
        {
            if (value == null)
            {

                return BadRequest();
            }
            else
            {
                await Initilization;
                var response = await _repo1.CheckSubCategoryAsync(value);
                if (response != null)
                    return Ok(response);
                return NotFound();
            }
        }
    }
}
