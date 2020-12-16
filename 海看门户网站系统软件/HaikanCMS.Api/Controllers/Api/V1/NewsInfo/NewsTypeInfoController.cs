using System;
using System.Collections.Generic;
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
    public class NewsTypeInfoController : ControllerBase
    {
        private readonly haiKanChemistryContext _dbContext;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;
        /// <summary>
        /// 
        /// </summary>
        /// <param NewsTypeName="dbContext"></param>
        /// <param NewsTypeName="mapper"></param>
        /// <param NewsTypeName="hostingEnvironment"></param>
        public NewsTypeInfoController(haiKanChemistryContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }
        #region 后台管理列表
        /// <summary>
        /// 显示所有信息
        /// </summary>
        /// <param NewsTypeName="payload"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult List(NewsRequestPayload payload)
        {
            using (_dbContext)
            {
                var query = from p in _dbContext.NewsType
                            where p.IsDeleted != 1
                            orderby p.Id ascending
                            select new
                            {
                                p.NewsTypeUuid,
                                p.NewsTypeName,
                                p.IsDeleted,
                                p.Staue
                            };
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x => x.NewsTypeName.Contains(payload.Kw.Trim()));
                }
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
                var totalCount = query.Count();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(list, totalCount);
                ToLog.AddLog("查询", "成功:查询:新闻类型列表信息数据", _dbContext);
                return Ok(response);
            }
        }

        #endregion
        #region 创建
        /// <summary>
        /// 创建
        /// </summary>
        /// <param NewsTypeName="model">视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(NewsTypeViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;

            using (_dbContext)
            {
                var entity = new NewsType();
                entity.NewsTypeName = model.NewsTypeName;
                entity.NewsTypeUuid = Guid.NewGuid();
                entity.Remark = model.Remark;
                entity.IsDeleted = 0;
                entity.AddPeople = AuthContextService.CurrentUser.LoginName;
                entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd");
                _dbContext.NewsType.Add(entity);
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("添加", "成功:添加:新闻类型列表信息一条数据", _dbContext);
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
        /// <param NewsTypeName="guid">惟一编码</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult goEdit(Guid guid)
        {
            using (_dbContext)
            {
                var entity = _dbContext.NewsType.FirstOrDefault(x => x.NewsTypeUuid == guid);
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(entity);
                return Ok(response);
            }
        }
        /// <summary>
        /// 保存编辑后的信息
        /// </summary>
        /// <param NewsTypeName="model">视图实体</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Edit(NewsTypeViewModel model)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                var entity = _dbContext.NewsType.FirstOrDefault(x => x.NewsTypeUuid.ToString() == model.NewsTypeUuid);
                entity.NewsTypeName = model.NewsTypeName;
                entity.Remark = model.Remark;
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("编辑", "成功:编辑:新闻类型列表信息一条数据", _dbContext);
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
        /// <param NewsTypeName="ids">ID,多个以逗号分隔</param>
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
        /// <param NewsTypeName="command"></param>
        /// <param NewsTypeName="ids">ID,多个以逗号分隔</param>
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
        /// <param NewsTypeName="isDeleted"></param>
        /// <param NewsTypeName="ids">ID字符串,多个以逗号隔开</param>
        /// <returns></returns>
        private ResponseModel UpdateIsDelete(CommonEnum.IsDeleted isDeleted, string ids)
        {
            using (_dbContext)
            {
                var parameters = ids.Split(",").Select((id, index) => new SqlParameter(string.Format("@p{0}", index), id)).ToList();
                var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
                var sql = string.Format("UPDATE NewsType SET IsDeleted=@IsDel WHERE NewsTypeUuid IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@IsDel", (int)isDeleted));
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
                ToLog.AddLog("删除", "成功:删除:新闻类型列表信息一条数据", _dbContext);
                var response = ResponseModelFactory.CreateInstance;
                return response;
            }
        }
        #endregion
        #endregion
    }
}
