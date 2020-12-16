using System;
using System.Collections.Generic;

namespace HaikanCMS.Api.Entities
{
    public partial class RestsData
    {
        public int Id { get; set; }
        public Guid RestsDataUuid { get; set; }
        public string Name { get; set; }
        public string Accessory { get; set; }
        public string UpdateTime { get; set; }
        public string Type { get; set; }
        public string UpdatePeople { get; set; }
        public int? IsDeleted { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
    }
}
