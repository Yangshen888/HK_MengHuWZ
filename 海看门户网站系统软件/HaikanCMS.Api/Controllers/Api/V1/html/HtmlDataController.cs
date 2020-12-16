using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HaikanCMS.Api.Entities;
using HaikanCMS.Api.Extensions;
using HaikanCMS.Api.logs.TolLog;
using HaikanCMS.Api.Models.Html;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HaikanCMS.Api.Controllers.Api.V1.html
{
    [Route("api/v1/html/[controller]/[action]")]
    [ApiController]
    public class HtmlDataController : ControllerBase
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
        public HtmlDataController(haiKanChemistryContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        #region 首页绑定
        /// <summary>
        /// 首页栏目
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult IndexCatalog(string value)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var list = _dbContext.Column.Where(x => x.IsDeleted != 1 && x.SuperiorUuid == null && x.SuperiorMenu == 0 && x.ColumnTitle!="首页").OrderByDescending(x => x.IsStick).ThenByDescending(x => x.SortId);
                if (!string.IsNullOrEmpty(value))
                {
                    var entity = _dbContext.Column.Where(x => x.ColumnUuid.ToString() == value && x.IsDeleted != 1).OrderByDescending(x => x.IsStick).ThenByDescending(x => x.SortId).ToList();
                    response.SetData(entity);
                    return Ok(response);
                }
                var datalist = list.ToList();
                response.SetData(datalist);
                return Ok(response);
            }
        }

        /// <summary>
        /// 获取栏目表数据
        /// 预约共享，国家级精品课程，国家级虚拟仿真实验教学中心，省级精品课程，中心概况等
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AllColumnList(string type, string value)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                if (type=="实验进程" || type == "实验教案"|| type == "国家级精品课程" || type == "国家级虚拟仿真实验教学中心" || type == "省级精品课程")
                {
                    var list = _dbContext.Content.Where(x => x.IsDeleted != 1 && x.ColumnTitleText == type);
                    if (list.Any())
                    {
                        list = list.OrderByDescending(x => x.IsStick).ThenByDescending(x => x.SortId);
                        if (!string.IsNullOrEmpty(value))
                        {
                            var entity = _dbContext.Content.Where(x => x.ColumnUuid == Guid.Parse(value) && x.IsDeleted != 1).OrderByDescending(x => x.IsStick).ThenByDescending(x => x.AddTime).ToList();
                            response.SetData(entity);
                            return Ok(response);
                        }
                        var datalist = list.ToList();
                        response.SetData(datalist);
                        return Ok(response);
                    }

                    response.SetFailed("暂无数据");
                    return Ok(response);

                }
                else
                {
                    var query = _dbContext.Column.FirstOrDefault(x => x.IsDeleted != 1 && x.ColumnTitle == type);
                    if (query != null)
                    {
                        var list = _dbContext.Column.Where(x => x.IsDeleted != 1 && x.SuperiorUuid == query.ColumnUuid);
                        if (list.Any())
                        {
                            list = list.OrderByDescending(x => x.IsStick).ThenByDescending(x => x.SortId);
                            if (!string.IsNullOrEmpty(value))
                            {
                                var entity = _dbContext.Column.Where(x => x.ColumnUuid == Guid.Parse(value) && x.IsDeleted != 1).OrderByDescending(x => x.IsStick).ThenByDescending(x => x.AddTime).ToList();
                                response.SetData(entity);
                                return Ok(response);
                            }
                        }
                        var datalist = list.ToList();
                        response.SetData(datalist);
                        return Ok(response);
                    }

                    response.SetFailed("暂无数据");
                    return Ok(response);
                }
            }
        }


        /// <summary>
        /// 获取新闻动态数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult NewsTrendsList(string value)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var list = _dbContext.News.Where(x => x.IsDeleted != 1);
                if (list.Any())
                {
                    list = list.OrderByDescending(x => x.IsStick).ThenByDescending(x => x.SortId);
                    if (!string.IsNullOrEmpty(value))
                    {
                        var entity = _dbContext.News.Where(x => x.NewsUuid == Guid.Parse(value) && x.IsDeleted != 1).OrderByDescending(x => x.IsStick).ThenByDescending(x => x.AddTime).ToList();
                        response.SetData(entity);
                        return Ok(response);
                    }
                    var datalist = list.ToList();
                    response.SetData(datalist);
                    return Ok(response);
                }

                response.SetFailed("暂无数据");
                return Ok(response);
            }
        }

        /// <summary>
        /// 获取新闻公告数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult NewsNoticeList(string value)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var list = _dbContext.Proclamation.Where(x => x.IsDeleted != 1);
                if (list.Any())
                {
                    list = list.OrderByDescending(x => x.IsStick).ThenByDescending(x => x.AddTime);
                    if (!string.IsNullOrEmpty(value))
                    {
                        var entity = _dbContext.Proclamation.Where(x => x.ProclamationUuid == Guid.Parse(value) && x.IsDeleted != 1).OrderByDescending(x => x.IsStick).ThenByDescending(x => x.AddTime).ToList();
                        response.SetData(entity);
                        return Ok(response);
                    }
                    var datalist = list.ToList();
                    response.SetData(datalist);
                    return Ok(response);
                }

                response.SetFailed("暂无数据");
                return Ok(response);
            }
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetSearch(string value)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                List<HtmlSearch> model = new List<HtmlSearch>();
                var Columnlist = _dbContext.Column.Where(x => x.ColumnTitle.Contains(value) && x.ColumnContent!="").OrderByDescending(x =>x.IsStick).ThenByDescending(x=>x.AddTime).Select(x=>new HtmlSearch{
                    Title=x.ColumnTitle,
                    Guuid=x.ColumnUuid,
                    Content=x.ColumnContent
                }).ToList();
                model.AddRange(Columnlist);
                var Contentlist = _dbContext.Content.Where(x => x.ColumnTitle.Contains(value) && x.ColumnContent != "").OrderByDescending(x => x.IsStick).ThenByDescending(x => x.AddTime).Select(x => new HtmlSearch
                {
                    Title = x.ColumnTitle,
                    Guuid = x.ColumnUuid,
                    Content = x.ColumnContent
                }).ToList();
                model.AddRange(Contentlist);
                var Newslist = _dbContext.News.Where(x => x.NewsTitle.Contains(value) && x.NewsContent!="").OrderByDescending(x => x.IsStick).ThenByDescending(x => x.AddTime).Select(x => new HtmlSearch
                {
                    Title = x.NewsTitle,
                    Guuid = x.NewsUuid,
                    Content = x.NewsContent
                }).ToList();
                model.AddRange(Newslist);
                var Proclalist = _dbContext.Proclamation.Where(x => x.ProclamationTitle.Contains(value) && x.ProclamationContent != "").OrderByDescending(x => x.IsStick).ThenByDescending(x => x.AddTime).Select(x => new HtmlSearch
                {
                    Title = x.ProclamationTitle,
                    Guuid = x.ProclamationUuid,
                    Content = x.ProclamationContent
                }).ToList();
                model.AddRange(Proclalist);
                
                response.SetData(model);
                return Ok(response);
                
            }
        }


        /// <summary>
        /// 获取友情连接数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ExternalList()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var list = _dbContext.ExternalLink.Where(x => x.IsDeleted != 1);
                if (list.Any())
                {
                    list = list.OrderByDescending(x => x.AddTime);
                    
                    var datalist = list.ToList();
                    response.SetData(datalist);
                    return Ok(response);
                }

                response.SetFailed("暂无数据");
                return Ok(response);
            }
        }

        /// <summary>
        /// 获取轮播图或者底部信息数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult RestsDataList(string value,string type)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var list = _dbContext.RestsData.Where(x => x.IsDeleted != 1);
                if (list.Any())
                {
                    var query = list.FirstOrDefault(x => x.Name==value && x.Type==type);

                    response.SetData(query);
                    return Ok(response);
                }

                response.SetFailed("暂无数据");
                return Ok(response);
            }
        }

        /// <summary>
        /// 获取下拉链接数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult LinkTypeList()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var list = _dbContext.ExternalLink.Where(x => x.IsDeleted != 1);
                if (list.Any())
                {
                    var firstType = list.Where(x => x.LinkTypeUuid == GetGuid("1")).ToList();
                    var secondType = list.Where(x => x.LinkTypeUuid == GetGuid("2")).ToList();
                    var thirdType = list.Where(x => x.LinkTypeUuid == GetGuid("3")).ToList();
                    var fourthType = list.Where(x => x.LinkTypeUuid == GetGuid("4")).ToList();

                    response.SetData(new { firstType , secondType , thirdType , fourthType });
                    return Ok(response);
                }

                response.SetFailed("暂无数据");
                return Ok(response);
            }
        }

        /// <summary>
        /// 获取链接类别
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Guid GetGuid(string type)
        {
            using (haiKanChemistryContext hcc = new haiKanChemistryContext())
            {
                var guid = Guid.Empty;
                var query= hcc.LinkType.FirstOrDefault(x => x.Class == type);
                if (query!=null)
                {
                    guid = query.LinkTypeUuid;
                }
                return guid;
            }
        }
        #endregion

        #region 访问量
        /// <summary>
        /// 获取访问量
        /// </summary>
        /// <returns></returns>
        public IActionResult SetVisit()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                
                Visit model = new Visit();
                model.VisitUuid = Guid.NewGuid();
                model.AddTime = DateTime.Now;
                _dbContext.Visit.Add(model);
                _dbContext.SaveChanges();
                var sysset = _dbContext.SystemSetting.First();
                var ctime = Convert.ToDateTime(sysset.CountTime);
                var date = ctime.ToString("yyyy年MM月dd日");
                var datalist = _dbContext.Visit.Count();
                if (datalist > 0)
                {
                    datalist = _dbContext.Visit.Where(x => x.AddTime >= ctime).Count();
                }
                response.SetData(new { date , datalist });
                return Ok(response);
            }
        }


        #endregion

    }
}
