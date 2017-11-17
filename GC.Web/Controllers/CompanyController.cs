using AutoMapper;
using GC.Core.Entities;
using GC.Core.Interfaces.Services;
using GC.Core.Querying;
using GC.Web.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GC.Web.Controllers
{
    [Route("api/[controller]")]
    public class CompanyController : Controller
    {
        private readonly ICompanyService service;
        private readonly IMapper mapper;

        public CompanyController(ICompanyService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get(CompanyQueryDTO query)
        {
            var mappedQuery = this.mapper.Map<CompanyQuery>(query);
            var companies = await this.service.GetByQueryAsync(mappedQuery);
            var result = this.mapper.Map<QueryResultDTO<CompanyDTO>>(companies);
            return Ok(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]CompanyDTO companyDto)
        {
            var mappedObj = this.mapper.Map<Company>(companyDto);

            if (!TryValidateModel(mappedObj)) {
                var state = ModelState;
                return BadRequest(ModelState);
            }

            this.service.Add(mappedObj);
            var resultObj = this.mapper.Map<CompanyDTO>(mappedObj);

            return Ok(resultObj);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
