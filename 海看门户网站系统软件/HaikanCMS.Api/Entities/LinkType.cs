using System;
using System.Collections.Generic;

namespace HaikanCMS.Api.Entities
{
    public partial class LinkType
    {
        public int Id { get; set; }
        public Guid LinkTypeUuid { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public int? IsDelete { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
    }
}
