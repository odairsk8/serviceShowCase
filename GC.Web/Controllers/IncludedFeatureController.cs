using AutoMapper;
using GC.Core.Entities;
using GC.Core.Interfaces.Services;
using GC.Web.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace GC.Web.Controllers
{
    [Route("api/Company/{companyId}/ProvidedService/{providedServiceId}/IncludedFeature")]
    public class IncludedFeatureController : Controller
    {
        private readonly IServiceBase<IncludedFeature> service;
        private readonly IMapper mapper;

        public IncludedFeatureController(IServiceBase<IncludedFeature> service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("{includedFeatureId}")]
        public async Task<IActionResult> GetById(int includedFeatureId)
        {
            var incFeat = await this.service.GetByAsync(c => c.Id == includedFeatureId, c => c.Features);
            if (incFeat == null)
                return NotFound(includedFeatureId);

            return Ok(this.mapper.Map<IncludedFeatureFormDTO>(incFeat));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]IncludedFeatureFormDTO includedFeature)
        {
            var mappedEntity = this.mapper.Map<IncludedFeature>(includedFeature);
            if (!TryValidateModel(mappedEntity))
                return BadRequest(ModelState);

            this.service.Add(mappedEntity);
            await this.service.SaveAsync();

            includedFeature = this.mapper.Map<IncludedFeatureFormDTO>(mappedEntity);

            return Ok(this.mapper.Map<IncludedFeatureFormDTO>(includedFeature));
        }

        [HttpPut]
        [Route("{includedFeatureId}")]
        public async Task<IActionResult> Update(int includedFeatureId, [FromBody]IncludedFeatureFormDTO includedFeatureDto)
        {
            var incFeatDatabase = await this.service.GetByAsync(c => c.Id == includedFeatureId, c => c.Features);
            if (incFeatDatabase == null)
                return NotFound(includedFeatureId);

            this.mapper.Map<IncludedFeatureFormDTO, IncludedFeature>(includedFeatureDto, incFeatDatabase);

            await this.service.SaveAsync();

            return Ok(this.mapper.Map<IncludedFeatureFormDTO>(incFeatDatabase));
        }

        [HttpDelete]
        [Route("{includedFeatureId}")]
        public async Task<IActionResult> Delete(int includedFeatureId)
        {
            var incFeatDatabase = await this.service.GetByIdAsync(includedFeatureId);
            if (incFeatDatabase == null)
                return NotFound(includedFeatureId);

            await this.service.DeleteAsync(incFeatDatabase);
            await this.service.SaveAsync();

            return Ok(includedFeatureId);
        }
    }
}
