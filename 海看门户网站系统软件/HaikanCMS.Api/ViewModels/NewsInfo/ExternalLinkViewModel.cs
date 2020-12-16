using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanCMS.Api.ViewModels.NewsInfo
{
    public class ExternalLinkViewModel
    {
        public string ExternalLinkUuid { get; set; }
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
