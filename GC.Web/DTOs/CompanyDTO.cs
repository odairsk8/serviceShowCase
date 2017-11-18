using System;
using System.Collections.Generic;

namespace GC.Web.DTOs
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Foundation { get; set; }
        public string History { get; set; }
        public IEnumerable<PhotoDTO> Photos { get; set; }

    }
}
