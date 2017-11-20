using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GC.Core.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Foundation { get; set; }
        public string History { get; set; }

        public ICollection<Photo> Photos { get; set; }
        public ICollection<ProvidedService> ProvidedServices { get; set; }

        public Company()
        {
            this.Photos = new Collection<Photo>();
            this.ProvidedServices = new Collection<ProvidedService>();
        }
    }
}