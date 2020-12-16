using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HaikanCMS.Api.Entities;
using HaikanCMS.Api.Entities.Enums;
using HaikanCMS.Api.Extensions;
using HaikanCMS.Api.Extensions.AuthContext;
using HaikanCMS.Api.logs.TolLog;
using HaikanCMS.Api.Models.LinkType;
using HaikanCMS.Api.Models.Response;
using HaikanCMS.Api.RequestPayload.News;
using HaikanCMS.Api.ViewModels.Experiment;
using HaikanCMS.Api.ViewModels.Rbac.Column;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace HaikanCMS.Api.Controllers.Api.V1.Experiment
{
    [Route("api/v1/Experiment/[controller]/[action]")]
    [ApiController]
    public class ExperimentController : ControllerBase
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
        public ExperimentController(haiKanChemistryContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        #region 后台管理列表
        /// <summary>
        /// 显示所有信息
        /// </summary>
        /// <param NewsTitle="payload"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult List(NewsRequestPayload payload)
        {
            using (_dbContext)
            {
                var query = _dbContext.Content.Where(x=>x.ColumnModel== "列表栏目" && x.SuperiorUuid != null && x.SuperiorMenu!=1);

                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x => x.ColumnTitle.Contains(payload.Kw.Trim()));
                }
                if (!string.IsNullOrEmpty(payload.Kw1))
                {
                    query = query.Where(x => x.SuperiorUuid==Guid.Parse(payload.Kw1.Trim()));
                }
                if (payload.Staue > -1)
                {
                    query = query.Where(x => x.Staue == payload.Staue.ToString());
                }
                if (payload.IsDeleted > CommonEnum.IsDeleted.All)
                {
                    query = query.Where(x => x.IsDeleted == Convert.ToInt32(((CommonEnum.IsDeleted)payload.IsDeleted)));
                }
                query = query.OrderByDescending(x => x.IsStick).ThenByDescending(x=>x.Id);
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
                var totalCount = query.Count();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(list, totalCount);
                ToLog.AddLog("查询", "成功:查询:内容管理信息数据", _dbContext);
                return Ok(response);
            }
        }

        /// <summary>
        /// 获取全部栏目下拉框
        /// </summary>
        /// <param>惟一编码</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult getNewsType()
        {
            using (_dbContext)
            {
                var query = (from p in _dbContext.Column
                             where p.IsDeleted!=1 &&
                             p.ColumnModel=="列表栏目" &&
                             p.SuperiorMenu==1 
                             select new
                            {
                                value = p.ColumnUuid,
                                label = p.ColumnTitle,
                            }).ToList();
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(query);
                return Ok(response);
            }
        }
        #endregion

        #region 创建
        /// <summary>
        /// 创建
        /// </summary>
        /// <param NewsTitle="model">视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(ColumnViewModel vmodel)
        {
            var response = ResponseModelFactory.CreateInstance;

            using (_dbContext)
            {
                if (_dbContext.Content.Any(x => x.ColumnTitle == vmodel.ColumnTitle && x.IsDeleted == 0))
                {
                    response.SetFailed("已存在该名称的栏目");
                    return Ok(response);
                }

                var query = _dbContext.Column.FirstOrDefault(x => x.ColumnUuid == vmodel.SuperiorUuid);
                if (query==null)
                {
                    response.SetFailed("请填写上级栏目");
                    return Ok(response);
                }
                if (vmodel.IssueTime == "")
                {
                    response.SetFailed("请选择发布时间");
                    return Ok(response);
                }
                var entity = new Content();
                entity.ColumnUuid = Guid.NewGuid();
                entity.IsDeleted = 0;
                entity.ColumnTitle = vmodel.ColumnTitle;
                entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                entity.AddPeople = AuthContextService.CurrentUser.LoginName;

                entity.SuperiorUuid = vmodel.SuperiorUuid;
                entity.SuperiorMenu = query.SuperiorMenu+1;
                entity.ColumnTitleText = query.ColumnTitle;
                entity.ColumnModel = "列表栏目";
                entity.ColumnContent = vmodel.ColumnContent;
                entity.Staue = vmodel.Staue;
                entity.SortId = vmodel.SortId;
                entity.IsStick = vmodel.IsStick;
                if (!string.IsNullOrEmpty(vmodel.IssueTime))
                {
                    entity.IssueTime = Convert.ToDateTime(vmodel.IssueTime).ToString("yyyy-MM-dd");
                }
                _dbContext.Content.Add(entity);
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("添加", "成功:添加:内容管理信息一条数据", _dbContext);
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
        /// <param NewsTitle="guid">惟一编码</param>
        /// <returns></returns>
        [HttpGet("{guid}")]
        [ProducesResponseType(200)]
        public IActionResult Edit(Guid guid)
        {
            using (_dbContext)
            {
                var entity = _dbContext.Content.FirstOrDefault(x => x.ColumnUuid == guid);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(entity);
                return Ok(response);
            }
        }

        /// <summary>
        /// 保存编辑后的信息
        /// </summary>
        /// <param NewsTitle="model">视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Edit(ColumnViewModel vmodel)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                var entity = _dbContext.Content.FirstOrDefault(x => x.ColumnUuid == vmodel.ColumnUuid);
                if (entity.ColumnTitle != vmodel.ColumnTitle)
                {
                    if (_dbContext.Content.Any(x => x.ColumnTitle == vmodel.ColumnTitle && x.IsDeleted == 0))
                    {
                        response.SetFailed("已存在该名称的栏目");
                        return Ok(response);
                    }
                }

                var query = _dbContext.Column.FirstOrDefault(x => x.ColumnUuid == vmodel.SuperiorUuid);

                entity.ColumnTitle = vmodel.ColumnTitle;
                entity.SuperiorUuid = vmodel.SuperiorUuid;
                entity.SuperiorMenu = query.SuperiorMenu+1;
                entity.ColumnContent = vmodel.ColumnContent;
                entity.SortId = vmodel.SortId;
                entity.IsStick = vmodel.IsStick;
                if (!string.IsNullOrEmpty(vmodel.IssueTime))
                {
                    entity.IssueTime = Convert.ToDateTime(vmodel.IssueTime).ToString("yyyy-MM-dd");
                }
                entity.Staue = vmodel.Staue;
                entity.ColumnTitleText = query.ColumnTitle;
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("编辑", "成功:编辑:内容管理信息一条数据", _dbContext);
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
        /// <param NewsTitle="ids">ID,多个以逗号分隔</param>
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
        /// <param NewsTitle="command"></param>
        /// <param NewsTitle="ids">ID,多个以逗号分隔</param>
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
        /// <param NewsTitle="isDeleted"></param>
        /// <param NewsTitle="ids">ID字符串,多个以逗号隔开</param>
        /// <returns></returns>
        private ResponseModel UpdateIsDelete(CommonEnum.IsDeleted isDeleted, string ids)
        {
            using (_dbContext)
            {
                var parameters = ids.Split(",").Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
                var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
                var sql = string.Format("UPDATE [Content] SET IsDeleted=@IsDel WHERE ColumnUuid IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@IsDel", (int)isDeleted));
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
                ToLog.AddLog("删除", "成功:删除:内容管理信息一条数据", _dbContext);
                var response = ResponseModelFactory.CreateInstance;
                return response;
            }
        }
        #endregion
        #endregion

       
    }
}
