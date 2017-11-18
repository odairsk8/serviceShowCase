using AutoMapper;
using GC.Core.Entities;
using GC.Core.Interfaces.Services;
using GC.Web.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GC.Web.Controllers
{
    [Route("api/[controller]")]
    public class PhotoController : Controller
    {
        private readonly ICompanyService companyService;
        private readonly IHostingEnvironment host;
        private readonly IMapper mapper;
        private readonly PhotoSettings photoSettings;

        public PhotoController(
            ICompanyService companyService,
            IOptionsSnapshot<PhotoSettings> options, 
            IHostingEnvironment host,
            IMapper mapper)
        {
            this.companyService = companyService;
            this.host = host;
            this.mapper = mapper;
            this.photoSettings = options.Value;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        [Route("company/{companyId}")]
        public async Task<IActionResult> UploadCompanyPhoto(int companyId, IFormFile file)
        {
            var company = await this.companyService.GetByIdAsync(companyId);
            if (company == null)
                return NotFound(companyId);

            if (file == null) return BadRequest("Null file");
            if (file.Length == 0) return BadRequest("Empty file");
            if (file.Length > this.photoSettings.MaxBytes) return BadRequest("Maximum file size is 10mb.");
            if (!this.photoSettings.IsSupported(file.FileName)) return BadRequest("Invalid file type.");

            var uploadFolderPath = Path.Combine(this.host.WebRootPath + "\\uploads");
            if (!Directory.Exists(uploadFolderPath))
                Directory.CreateDirectory(uploadFolderPath);

            var filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadFolderPath, filename);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var photo = new Photo() { FileName = filename };
            company.Photos.Add(photo);
            await this.companyService.SaveAsync();

            return Ok(this.mapper.Map<PhotoDTO>(photo));
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
