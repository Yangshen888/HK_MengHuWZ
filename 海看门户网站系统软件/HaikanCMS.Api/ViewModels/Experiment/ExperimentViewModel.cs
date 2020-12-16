﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanCMS.Api.ViewModels.Experiment
{
    public class ExperimentViewModel
    {
        public Guid NewsUuid { get; set; }
        public int? IsDeleted { get; set; }
        public int Id { get; set; }
        public string NewSubhead { get; set; }
        public string NewsTitle { get; set; }
        public string NewsContent { get; set; }
        public Guid? NewsTypeUuid { get; set; }
        public string NewsPic { get; set; }
        public string NewsUrl { get; set; }
        public int? Staue { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public int SortId { get; set; }
        public string Video { get; set; }
        public string File { get; set; }
        public string Type { get; set; }
        public string IssueTime { get; set; }
    }
}
