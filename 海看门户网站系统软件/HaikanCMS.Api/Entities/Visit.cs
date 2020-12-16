using System;
using System.Collections.Generic;

namespace HaikanCMS.Api.Entities
{
    public partial class Visit
    {
        public int Id { get; set; }
        public Guid VisitUuid { get; set; }
        public DateTime? AddTime { get; set; }
    }
}
