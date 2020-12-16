using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HaikanCMS.Api.Entities;
using HaikanCMS.Api.Entities.Enums;
using HaikanCMS.Api.Extensions;
using HaikanCMS.Api.Extensions.AuthContext;
using HaikanCMS.Api.Extensions.CustomException;
using HaikanCMS.Api.logs.TolLog;
using HaikanCMS.Api.Models.LinkType;
using HaikanCMS.Api.Models.Response;
using HaikanCMS.Api.RequestPayload.News;
using HaikanCMS.Api.ViewModels;
using HaikanCMS.Api.ViewModels.NewsInfo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace HaikanCMS.Api.Controllers.Api.V1.NewsInfo
{
    [Route("api/v1/NewsInfo/[controller]/[action]")]
    [ApiController]

    public class ExternalLinkInfoController : ControllerBase
    {
        private readonly haiKanChemistryContext _dbContext;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;
        /// <summary>
        /// 
        /// </summary>
        /// <param Link="dbContext"></param>
        /// <param Link="mapper"></param>
        /// <param Link="hostingEnvironment"></param>
        public ExternalLinkInfoController(haiKanChemistryContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }
        #region 后台管理列表
        /// <summary>
        /// 显示所有信息
        /// </summary>
        /// <param Link="payload"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetList(NewsRequestPayload payload)
        {
            using (_dbContext)
            {
                var query = from p in _dbContext.ExternalLink
                            where p.IsDeleted!=1
                            orderby p.Id ascending
                            select new
                            {
                                p.ExternalLinkUuid,
                                p.Link,
                                p.IsDeleted,
                                p.Staue,
                                p.Remark
                            };
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x => x.Link.Contains(payload.Kw.Trim()));
                }
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
                var totalCount = query.Count();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(list, totalCount);
                ToLog.AddLog("查询", "成功:查询:外部链接列表信息数据", _dbContext);
                return Ok(response);
            }
        }
        #endregion
        #region 创建
        /// <summary>
        /// 创建
        /// </summary>
        /// <param Link="model">视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult GetCreate(ExternalLinkViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                var entity = new ExternalLink();
                entity.ExternalLinkUuid = Guid.NewGuid();
                entity.Link = model.Link;
                entity.Remark = model.Remark;
                entity.IsDeleted = 0;
                entity.LinkTypeUuid = model.LinkTypeUuid;
                entity.AddPeople = AuthContextService.CurrentUser.LoginName;
                entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                _dbContext.ExternalLink.Add(entity);
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("添加", "成功:添加:外部链接列表信息一条数据", _dbContext);
                }
                response.SetSuccess();
                return Ok(response);
            }
        }
        #endregion
        #region 编辑
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param Link="guid">惟一编码</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult GetfoGet(Guid guid)
        {
            using (_dbContext)
            {
                var entity = _dbContext.ExternalLink.FirstOrDefault(x => x.ExternalLinkUuid == guid);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(entity);
                return Ok(response);
            }
        }
        /// <summary>
        /// 保存编辑后的信息
        /// </summary>
        /// <param Link="model">视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult GetEdit(ExternalLinkViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                var entity = _dbContext.ExternalLink.FirstOrDefault(x => x.ExternalLinkUuid.ToString() == model.ExternalLinkUuid);
                entity.Link = model.Link;
                entity.Remark = model.Remark;
                entity.LinkTypeUuid = model.LinkTypeUuid;
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("编辑", "成功:编辑:外部链接列表信息一条数据", _dbContext);
                }
                response = ResponseModelFactory.CreateInstance;
                return Ok(response);
            }
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param Link="ids">ID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet("{ids}")]
        [ProducesResponseType(200)]
        public IActionResult Delete(string ids)
        {
            var response = ResponseModelFactory.CreateInstance;
            response = UpdateIsDelete(CommonEnum.IsDeleted.Yes, ids);
            return Ok(response);
        }
        /// <summary>
        /// 批量操作
        /// </summary>
        /// <param Link="command"></param>
        /// <param Link="ids">ID,多个以逗号分隔</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Batch(string command, string ids)
        {
            var response = ResponseModelFactory.CreateInstance;
            switch (command)
            {
                case "delete":
                    response = UpdateIsDelete(CommonEnum.IsDeleted.Yes, ids);
                    break;
                case "recover":
                    response = UpdateIsDelete(CommonEnum.IsDeleted.No, ids);
                    break;

                default:
                    break;
            }
            return Ok(response);
        }
        #region 私有方法

        /// <summary>
        /// 删除
        /// </summary>
        /// <param Link="isDeleted"></param>
        /// <param Link="ids">ID字符串,多个以逗号隔开</param>
        /// <returns></returns>
        private ResponseModel UpdateIsDelete(CommonEnum.IsDeleted isDeleted, string ids)
        {
            using (_dbContext)
            {
                var parameters = ids.Split(",").Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
                var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
                var sql = string.Format("UPDATE ExternalLink SET IsDeleted=@IsDel WHERE ExternalLinkUuid IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@IsDel", (int)isDeleted));
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
                ToLog.AddLog("删除", "成功:删除:外部链接列表信息一条数据", _dbContext);
                var response = ResponseModelFactory.CreateInstance;
                return response;
            }
        }
        #endregion
        #endregion

        #region 链接类别

        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult getType()
        {
            using (_dbContext)
            {
                var query = (from p in _dbContext.LinkType
                    where p.IsDelete != 1 
                    select new
                    {
                        value = p.LinkTypeUuid,
                        label = p.Name,
                    }).ToList();
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(query);
                return Ok(response);
            }
        }

        /// <summary>
        /// 获取类别
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetLinkType()
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var FirstType = "";
                var SecondType = "";
                var ThirdType = "";
                var FourthType = "";
                var query1 = _dbContext.LinkType.FirstOrDefault(x => x.IsDelete != 1 && x.Class=="1");
                if (query1!=null)
                {
                    FirstType = query1.Name;
                }
                var query2 = _dbContext.LinkType.FirstOrDefault(x => x.IsDelete != 1 && x.Class == "2");
                if (query2 != null)
                {
                    SecondType = query2.Name;
                }
                var query3 = _dbContext.LinkType.FirstOrDefault(x => x.IsDelete != 1 && x.Class == "3");
                if (query3 != null)
                {
                    ThirdType = query3.Name;
                }
                var query4 = _dbContext.LinkType.FirstOrDefault(x => x.IsDelete != 1 && x.Class == "4");
                if (query4 != null)
                {
                    FourthType = query4.Name;
                }
                response.SetData(new{ FirstType , SecondType , ThirdType , FourthType });
                return Ok(response);
            }
        }



        /// <summary>
        /// 修改类别
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SetLinkType(LinkTypeModel model)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                if (!string.IsNullOrEmpty(model.FirstType) )
                {
                    var query1 = _dbContext.LinkType.FirstOrDefault(x => x.IsDelete != 1 && x.Class == "1");
                    var isquery = _dbContext.LinkType.FirstOrDefault(x => x.IsDelete != 1 && x.Name == model.FirstType && x.Class != "1");
                    if (isquery == null)
                    {
                        if (query1 == null)
                        {
                            var entity = new LinkType();
                            entity.LinkTypeUuid = Guid.NewGuid();
                            entity.Name = model.FirstType;
                            entity.Class = "1";
                            entity.IsDelete = 0;
                            entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                            entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                            _dbContext.LinkType.Add(entity);
                        }
                        else
                        {
                            query1.Name = model.FirstType;
                        }
                    }
                    else
                    {
                        response.SetFailed("第一类别名称已存在");
                        return Ok(response);
                    }
                }

                if (!string.IsNullOrEmpty(model.SecondType))
                {
                    var query1 = _dbContext.LinkType.FirstOrDefault(x => x.IsDelete != 1 && x.Class == "2");
                    var isquery = _dbContext.LinkType.FirstOrDefault(x => x.IsDelete != 1 && x.Name == model.SecondType && x.Class != "2");
                    if (isquery == null)
                    {
                        if (query1 == null)
                        {
                            var entity = new LinkType();
                            entity.LinkTypeUuid = Guid.NewGuid();
                            entity.Name = model.SecondType;
                            entity.Class = "2";
                            entity.IsDelete = 0;
                            entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                            entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                            _dbContext.LinkType.Add(entity);
                        }
                        else
                        {
                            query1.Name = model.SecondType;
                        }
                    }
                    else
                    {
                        response.SetFailed("第二类别名称已存在");
                        return Ok(response);
                    }
                }

                if (!string.IsNullOrEmpty(model.ThirdType))
                {
                    var query1 = _dbContext.LinkType.FirstOrDefault(x => x.IsDelete != 1 && x.Class == "3");
                    var isquery = _dbContext.LinkType.FirstOrDefault(x => x.IsDelete != 1 && x.Name == model.ThirdType && x.Class != "3");
                    if (isquery == null)
                    {
                        if (query1 == null)
                        {
                            var entity = new LinkType();
                            entity.LinkTypeUuid = Guid.NewGuid();
                            entity.Name = model.ThirdType;
                            entity.Class = "3";
                            entity.IsDelete = 0;
                            entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                            entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                            _dbContext.LinkType.Add(entity);
                        }
                        else
                        {
                            query1.Name = model.ThirdType;
                        }
                    }
                    else
                    {
                        response.SetFailed("第三类别名称已存在");
                        return Ok(response);
                    }
                }
                if (!string.IsNullOrEmpty(model.FourthType))
                {
                    var query1 = _dbContext.LinkType.FirstOrDefault(x => x.IsDelete != 1 && x.Class == "4");
                    var isquery = _dbContext.LinkType.FirstOrDefault(x => x.IsDelete != 1 && x.Name == model.FourthType && x.Class != "4");
                    if (isquery == null)
                    {
                        if (query1 == null)
                        {
                            var entity = new LinkType();
                            entity.LinkTypeUuid = Guid.NewGuid();
                            entity.Name = model.FourthType;
                            entity.Class = "4";
                            entity.IsDelete = 0;
                            entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                            entity.AddPeople = AuthContextService.CurrentUser.DisplayName;
                            _dbContext.LinkType.Add(entity);
                        }
                        else
                        {
                            query1.Name = model.FourthType;
                        }
                    }
                    else
                    {
                        response.SetFailed("第四类别名称已存在");
                        return Ok(response);
                    }
                }
                

                _dbContext.SaveChanges();
                response.SetSuccess("保存成功");
                return Ok(response);
            }
        }
        #endregion
    }
}
