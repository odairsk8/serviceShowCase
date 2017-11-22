
using AutoMapper;
using GC.Core.Entities;
using GC.Core.Interfaces.Services;
using GC.Core.Querying;
using GC.Web.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        [Route("{providedServiceId}/Details")]
        public async Task<IActionResult> GetDetails(int providedServiceId)
        {
            var providedService = await this.service.GetFullEntity(providedServiceId);
            if (service == null)
                return NotFound(providedServiceId);

            return Ok(this.mapper.Map<ProvidedServiceDetailsDTO>(providedService));
        }

        [HttpGet]
        [Route("{providedServiceId}")]
        public async Task<IActionResult> GetById(int providedServiceId)
        {
            var providedService = await this.service.GetFullEntity(providedServiceId);
            if (service == null)
                return NotFound(providedServiceId);

            return Ok(this.mapper.Map<ProvidedServiceFormDTO>(providedService));
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
            var providedService = await this.service.GetByAsync(c => c.Id == providedServiceId, includes: d => d.CoverImage);
            if (providedService == null)
                return NotFound(companyId);

            if (file == null) return BadRequest("Null file");
            if (file.Length == 0) return BadRequest("Empty file");
            if (file.Length > this.photoSettings.MaxBytes) return BadRequest("Maximum file size is 10mb.");
            if (!this.photoSettings.IsSupported(file.FileName)) return BadRequest("Invalid file type.");


            var uploadsFolderPath = Path.Combine(this.host.WebRootPath + "\\uploads");

            var photo = await this.service.UploadCoverlPicture(providedService, file, uploadsFolderPath);

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

        [HttpPost]
        [Route("{providedServiceId}/Thumbnail")]
        public async Task<IActionResult> UploadThumbnailPicture(int companyId, int providedServiceId, IFormFile file)
        {
            var providedService = await this.service.GetByAsync(c => c.Id == providedServiceId, includes: d => d.ThumbnailPicture);
            if (providedService == null)
                return NotFound(companyId);

            if (file == null) return BadRequest("Null file");
            if (file.Length == 0) return BadRequest("Empty file");
            if (file.Length > this.photoSettings.MaxBytes) return BadRequest("Maximum file size is 10mb.");
            if (!this.photoSettings.IsSupported(file.FileName)) return BadRequest("Invalid file type.");


            var uploadsFolderPath = Path.Combine(this.host.WebRootPath + "\\uploads");

            var photo = await this.service.UploadThumbnaillPicture(providedService, file, uploadsFolderPath);

            return Ok(this.mapper.Map<PhotoDTO>(photo));
        }

        [HttpDelete]
        [Route("{providedServiceId}/Thumbnail")]
        public async Task<IActionResult> DeleteThumbnailPicture(int providedServiceId)
        {
            var providedService = await this.service.GetByAsync(c => c.Id == providedServiceId, includes: d => d.ThumbnailPicture);
            if (providedService == null || providedService.ThumbnailPicture == null)
                return NotFound(providedService.Id);

            var uploadsFolderPath = Path.Combine(this.host.WebRootPath + "\\uploads");
            await this.service.RemoveThumbnailPicture(providedService, uploadsFolderPath);

            return Ok(providedService.Id);
        }

        [HttpGet]
        [Route("{providedServiceId}/Features")]
        public async Task<IActionResult> GetFeatures(int providedServiceId)
        {
            var features = await this.service.GetFeatures(providedServiceId);
            if (features == null || features.Count() == 0)
                return NotFound(providedServiceId);

            var result = this.mapper.Map<IEnumerable<IncludedFeatureFormDTO>>(features);
            return Ok(result);
        }

        [HttpPost]
        [Route("{providedServiceId}/Features")]
        public async Task<IActionResult> SaveFeatures(int providedServiceId, [FromBody]ICollection<IncludedFeatureFormDTO> featuresDto)
        {
            var mappedFeatures = this.mapper.Map<IEnumerable<IncludedFeature>>(featuresDto);
            await this.service.SaveFeatures(providedServiceId, mappedFeatures);

            await this.service.SaveAsync();
            return Ok();
        }
    }
}
