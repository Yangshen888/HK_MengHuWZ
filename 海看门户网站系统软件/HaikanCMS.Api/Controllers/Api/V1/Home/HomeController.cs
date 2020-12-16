using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HaikanCMS.Api.Entities;
using HaikanCMS.Api.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HaikanCMS.Api.Controllers.Api.V1.Home
{
    [Route("api/v1/Home/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly haiKanChemistryContext _dbContext;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;
        /// <summary>
        /// 
        /// </summary>
        /// <param NewsTitle="dbContext"></param>
        /// <param NewsTitle="mapper"></param>
        /// <param NewsTitle="hostingEnvironment"></param>
        public HomeController(haiKanChemistryContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        #region 数据统计
        /// <summary>
        /// 数据统计
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Number()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                //新闻条目
                var NewNum = _dbContext.News.Where(x => x.IsDeleted != 1).Count();
                //栏目个数
                var ColumnNum = _dbContext.Column.Where(x => x.IsDeleted != 1).GroupBy(x=>x.ColumnTitle).Select(x=> x.Key).Count();
                //累计访问次数
                var VisitAllNum = _dbContext.Visit.Count();
                //今年访问次数
                var years = DateTime.Now.Year.ToString();
                var VisitYearNum = _dbContext.Visit.Where(x=>x.AddTime.ToString().Contains(years)).Count();

                response.SetData(new { NewNum,ColumnNum,VisitAllNum,VisitYearNum });
                return Ok(response);
            }
        }
        #endregion

        #region 统计图表
        /// <summary>
        /// 数据统计
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Chart()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;

                //新闻来源
                var NewChart = _dbContext.NewsType.Where(x => x.IsDeleted != 1).Select(x => new
                {
                    Counts = NewCount(x.NewsTypeUuid),
                    x.NewsTypeName
                }).ToList();

                //每周访问量活跃度
                var monDate = GetThisWeekMonday();
                List<int> VisitChart = new List<int>();
                for (int i = 0; i < 7; i++)
                {
                    DateTime Date1 = Convert.ToDateTime(monDate+" 00:00:00");
                    DateTime Date2 = Convert.ToDateTime(monDate + " 23:59:59");
                    if (i!=0)
                    {
                        Date1 = Convert.ToDateTime(monDate + " 00:00:00").AddDays(i);
                        Date2 = Convert.ToDateTime(monDate + " 23:59:59").AddDays(i);
                    }
                    var ss = _dbContext.Visit.ToList();
                    var vcount= _dbContext.Visit.Where(x => x.AddTime >= Date1 && x.AddTime <= Date2).Count();
                    VisitChart.Add(vcount);
                }
                response.SetData(new { NewChart, VisitChart });
                return Ok(response);
            }
        }

        /// <summary>
        /// 新闻来源数量
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        private static int NewCount(Guid guid)
        {
            using (haiKanChemistryContext db=new haiKanChemistryContext())
            {
                var count = 0;
                count = db.News.Where(x => x.NewsTypeUuid == guid).Count();
                return count;
            }
        }

        #region 获取本周周一的日期
        /// <summary>
        /// 获取本周的周一日期
        /// </summary>
        /// <returns></returns>
        public static string GetThisWeekMonday()
        {
            DateTime date = DateTime.Now;
            DateTime firstDate = System.DateTime.Now;
            switch (date.DayOfWeek)
            {
                case System.DayOfWeek.Monday:
                    firstDate = date;
                    break;
                case System.DayOfWeek.Tuesday:
                    firstDate = date.AddDays(-1);
                    break;
                case System.DayOfWeek.Wednesday:
                    firstDate = date.AddDays(-2);
                    break;
                case System.DayOfWeek.Thursday:
                    firstDate = date.AddDays(-3);
                    break;
                case System.DayOfWeek.Friday:
                    firstDate = date.AddDays(-4);
                    break;
                case System.DayOfWeek.Saturday:
                    firstDate = date.AddDays(-5);
                    break;
                case System.DayOfWeek.Sunday:
                    firstDate = date.AddDays(-6);
                    break;
            }
            return firstDate.ToString("yyyy/MM/dd");
        }
        #endregion
        #endregion
    }
}
