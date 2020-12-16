using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanCMS.Api.ViewModels.NewsInfo
{
    public class NewsTypeViewModel
    {
        public string NewsTypeUuid { get; set; }
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
