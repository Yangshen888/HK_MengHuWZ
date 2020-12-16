using System;
using System.Collections.Generic;

namespace HaikanCMS.Api.Entities
{
    public partial class Column
    {
        public Guid ColumnUuid { get; set; }
        public int? IsDeleted { get; set; }
        public int Id { get; set; }
        public string ColumnTitleText { get; set; }
        public string ColumnTitle { get; set; }
        public string ColumnNum { get; set; }
        public string AddTime { get; set; }
        public string AddPeople { get; set; }
        public Guid? SuperiorUuid { get; set; }
        public int? SuperiorMenu { get; set; }
        public string ColumnType { get; set; }
        public string ColumnModel { get; set; }
        public string ColumnUrl { get; set; }
        public string ColumnPic { get; set; }
        public string Staue { get; set; }
        public string ColumnWord { get; set; }
        public string ColumnContent { get; set; }
        public string ColumnVideo { get; set; }
        public string ColumnAudio { get; set; }
        public string ColumnFile { get; set; }
        public int? SortId { get; set; }
        public string IssueTime { get; set; }
        public int? IsStick { get; set; }
    }
}
