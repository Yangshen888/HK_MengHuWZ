using System;
using System.Collections.Generic;

namespace HaikanCMS.Api.Entities
{
    public partial class ClassVideo
    {
        public Guid ClassVideoUuid { get; set; }
        public int? IsDeleted { get; set; }
        public int Id { get; set; }
        public string Staue { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public string ClassVideoName { get; set; }
        public int? SortId { get; set; }
        public string Remark { get; set; }
        public string Type { get; set; }
        public string Lecturer { get; set; }
        public string ClassVideoContent { get; set; }
        public string Accessory { get; set; }
        public string CourseTime { get; set; }
        public string Picture { get; set; }
        public string Video { get; set; }
    }
}
