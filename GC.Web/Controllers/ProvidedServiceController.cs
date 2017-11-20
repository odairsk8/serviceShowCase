
using AutoMapper;
using GC.Core.Entities;
using GC.Core.Interfaces.Services;
using GC.Core.Querying;
using GC.Web.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GC.Web.Controllers
{
    [Route("api/Company/{companyId}/ProvidedService")]
    public class ProvidedServiceController : Controller
    {
        private readonly IMapper mapper;
        private readonly IProvidedServiceService service;
        private readonly IHostingEnvironment host;
        private readonly PhotoSettings photoSettings; 
        private readonly ICompanyService companyService;

        public ProvidedServiceController(IMapper mapper, 
            IProvidedServiceService service,
            IHostingEnvironment host,
            IOptionsSnapshot<PhotoSettings> options,
            ICompanyService companyService)
        {
            this.mapper = mapper;
            this.service = service;
            this.host = host;
            this.photoSettings = options.Value;
            this.companyService = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(ProvidedServiceQueryDTO queryDto)
        {
            var query = this.mapper.Map<ProvidedServiceQuery>(queryDto);
            var services = await this.service.GetByQueryAsync(query);
            var result = this.mapper.Map<QueryResultDTO<ProvidedServiceListItemDTO>>(services);
            return Ok(result);
        }

        [HttpGet]
        [Route("{provideServiceId}")]
        public async Task<IActionResult> GetById(int provideServiceId)
        {
            var providedService = await this.service.GetByAsync(c => c.Id == provideServiceId, includes: d => d.CoverImage);
            if (service == null)
                return NotFound(provideServiceId);

            return Ok(this.mapper.Map<ProvidedServiceDetailsDTO>(providedService));
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]ProvidedServiceFormDTO formDto)
        {
            var company = await this.companyService.GetByIdAsync(formDto.CompanyId);
            if (company == null)
                return NotFound(formDto.CompanyId);

            var providedService = this.mapper.Map<ProvidedService>(formDto);
            if (!TryValidateModel(providedService))
                return BadRequest(ModelState);

            company.ProvidedServices.Add(providedService);
            await this.service.SaveAsync();

            var result = this.mapper.Map<ProvidedServiceFormDTO>(providedService);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]ProvidedServiceFormDTO formDto)
        {
            var company = await this.companyService.GetByIdAsync(formDto.CompanyId);
            if (company == null)
                return NotFound(formDto.CompanyId);

            var providedService = await this.service.GetByIdAsync(formDto.Id);
            if (providedService == null)
                return NotFound(formDto.Id);

            providedService = mapper.Map<ProvidedServiceFormDTO, ProvidedService>(formDto, providedService);
            await this.service.SaveAsync();

            var result = this.mapper.Map<ProvidedServiceFormDTO>(providedService);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{provideServiceId}")]
        public async Task<IActionResult> Delete(int provideServiceId)
        {
            var providedService = await this.service.GetByIdAsync(provideServiceId);
            if (providedService == null)
                return NotFound(provideServiceId);

            await this.service.DeleteAsync(providedService);
            await this.service.SaveAsync();

            var result = this.mapper.Map<ProvidedServiceFormDTO>(providedService);
            return Ok(result);
        }

        [HttpPost]
        [Route("{providedServiceId}/CoverPhoto")]
        public async Task<IActionResult> UploadCoverPicture(int companyId, int providedServiceId, IFormFile file)
        {
            var providedService = await this.service.GetByAsync(c => c.Id == providedServiceId, includes: d => d.CoverImage );
            if (providedService == null)
                return NotFound(companyId);

            if (file == null) return BadRequest("Null file");
            if (file.Length == 0) return BadRequest("Empty file");
            if (file.Length > this.photoSettings.MaxBytes) return BadRequest("Maximum file size is 10mb.");
            if (!this.photoSettings.IsSupported(file.FileName)) return BadRequest("Invalid file type.");


            var uploadsFolderPath = Path.Combine(this.host.WebRootPath + "\\uploads");
            
            var photo = await this.service.UploadCoverPicture(providedService, file, uploadsFolderPath);

            return Ok(this.mapper.Map<PhotoDTO>(photo));
        }

        [HttpDelete]
        [Route("{providedServiceId}/CoverPhoto")]
        public async Task<IActionResult> DeleteCoverPhoto(int providedServiceId)
        {
            var providedService = await this.service.GetByAsync(c => c.Id == providedServiceId, includes: d => d.CoverImage);
            if (providedService == null || providedService.CoverImage == null)
                return NotFound(providedService.Id);

            var uploadsFolderPath = Path.Combine(this.host.WebRootPath + "\\uploads");
            await this.service.RemoveCoverPicture(providedService, uploadsFolderPath);

            return Ok(providedService.Id);
        }
    }
}
