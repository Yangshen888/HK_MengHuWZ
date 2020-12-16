using System;
using System.Collections.Generic;

namespace HaikanCMS.Api.Entities
{
    public partial class ExternalLink
    {
        public Guid ExternalLinkUuid { get; set; }
        public int? IsDeleted { get; set; }
        public int Id { get; set; }
        public string Staue { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public string Link { get; set; }
        public string Remark { get; set; }
        public string ColumnUuid { get; set; }
        public Guid? LinkTypeUuid { get; set; }
    }
}
