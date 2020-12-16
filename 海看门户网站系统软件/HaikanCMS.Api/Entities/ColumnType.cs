using System;
using System.Collections.Generic;

namespace HaikanCMS.Api.Entities
{
    public partial class ColumnType
    {
        public Guid ColumnTypeUuid { get; set; }
        public string TypeName { get; set; }
        public int Id { get; set; }
    }
}
