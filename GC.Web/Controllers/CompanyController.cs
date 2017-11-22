using AutoMapper;
using GC.Core.Entities;
using GC.Core.Interfaces.Services;
using GC.Core.Querying;
using GC.Web.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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
        public async Task<IActionResult> GetAsync(int id)
        {
            Company company = await this.service.GetByIdAsync(id, includePaths: new[] { "Photos"});
            return Ok(this.mapper.Map<CompanyDTO>(company));
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]CompanyDTO companyDto)
        {
            var mappedObj = this.mapper.Map<Company>(companyDto);

            if (!TryValidateModel(mappedObj))
                return BadRequest(ModelState);

            this.service.Add(mappedObj);
            this.service.SaveAsync();
            var resultObj = this.mapper.Map<CompanyDTO>(mappedObj);

            return Ok(resultObj);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]CompanyDTO companyDto)
        {
            var company = await this.service.GetByIdAsync(id);
            if (company == null)
                return NotFound(id);

            companyDto.Id = id;
            mapper.Map<CompanyDTO, Company>(companyDto, company);
            if (!TryValidateModel(company))
                BadRequest(ModelState);

            await this.service.SaveAsync();

            var result = await this.service.GetByIdAsync(id);
            return Ok(this.mapper.Map<CompanyDTO>(result));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var company = await this.service.GetByIdAsync(id);
            if (company == null)
                return NotFound(id);

            await this.service.DeleteAsync(company);

            return Ok(id);
        }

        
    }

    
}
