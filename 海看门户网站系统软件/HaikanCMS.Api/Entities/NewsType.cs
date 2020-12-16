using System;
using System.Collections.Generic;

namespace HaikanCMS.Api.Entities
{
    public partial class NewsType
    {
        public Guid NewsTypeUuid { get; set; }
        public int? IsDeleted { get; set; }
        public int Id { get; set; }
        public string Staue { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public string NewsTypeName { get; set; }
        public int? SortId { get; set; }
        public string Remark { get; set; }
    }
}
