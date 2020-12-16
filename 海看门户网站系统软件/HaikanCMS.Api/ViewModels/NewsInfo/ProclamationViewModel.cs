using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanCMS.Api.ViewModels.NewsInfo
{
    public class ProclamationViewModel
    {
        public string ProclamationUuid { get; set; }
        public int? IsDeleted { get; set; }
        public int Id { get; set; }
        public string Staue { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public string ProclamationTitle { get; set; }
        public int SortId { get; set; }
        public string Remark { get; set; }
        public string ProclamationContent { get; set; }
        public int IsStick { get; set; }
    }
}
