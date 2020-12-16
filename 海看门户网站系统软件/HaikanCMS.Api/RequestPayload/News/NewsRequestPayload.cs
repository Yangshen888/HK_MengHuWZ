using HaikanCMS.Api.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanCMS.Api.RequestPayload.News
{
    public class NewsRequestPayload : RequestPayload
    {
        /// <summary>
        /// 是否已被删除
        /// </summary>
        public CommonEnum.IsDeleted IsDeleted { get; set; }

        ///  /// <summary>
        /// 状态
        /// </summary>
        public int Staue { get; set; }
        public string Kw1 { get; set; }
        public string Kw2 { get; set; }
    }
}
