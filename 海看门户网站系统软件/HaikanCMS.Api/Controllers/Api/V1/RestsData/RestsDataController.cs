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
using HaikanCMS.Api.Models.Response;
using HaikanCMS.Api.RequestPayload.News;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace HaikanCMS.Api.Controllers.Api.V1.RestsData
{
    [Route("api/v1/RestsData/[controller]/[action]")]
    [ApiController]
    public class RestsDataController : ControllerBase
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
        public RestsDataController(haiKanChemistryContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        #region 后台列表

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
                var query = _dbContext.RestsData.Where(x=>x.IsDeleted!=1);
                if (!string.IsNullOrEmpty(payload.Kw))
                {
                    query = query.Where(x => x.Name.Contains(payload.Kw.Trim()));
                }
                var list = query.Paged(payload.CurrentPage, payload.PageSize).ToList();
                var totalCount = query.Count();
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(list, totalCount);
                ToLog.AddLog("查询", "成功:查询:其他数据信息数据", _dbContext);
                return Ok(response);
            }
        }

        #endregion

        #region 创建
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        public IActionResult Create(Entities.RestsData model)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var query = _dbContext.RestsData.FirstOrDefault(x => x.Name == model.Name && x.Type == model.Type);
                if (query!=null)
                {
                    response.SetFailed("该类别名称已存在");
                    return Ok(response);
                }
                var entity = new Entities.RestsData
                {
                    RestsDataUuid = Guid.NewGuid(),
                    Name = model.Name,
                    Accessory = model.Accessory,
                    Type = model.Type,
                    UpdatePeople = AuthContextService.CurrentUser.LoginName,
                    UpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    AddPeople = AuthContextService.CurrentUser.LoginName,
                    AddTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };
                _dbContext.RestsData.Add(entity);
                var res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("添加", "成功:添加:其他数据信息一条数据", _dbContext);
                }
                response.SetSuccess();
                return Ok(response);
            }
        }
        #endregion

        #region 编辑
        /// <summary>
        /// 根据UUID获取数据
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Show(Guid guid)
        {
            using (_dbContext)
            {
                var query = _dbContext.RestsData.FirstOrDefault(x => x.RestsDataUuid == guid);
                var response = ResponseModelFactory.CreateResultInstance;
                response.SetData(query);
                return Ok(response);
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(Entities.RestsData model)
        {
            using (_dbContext)
            {
                var response = ResponseModelFactory.CreateResultInstance;
                var query = _dbContext.RestsData.FirstOrDefault(x => x.Name == model.Name && x.Type == model.Type && x.RestsDataUuid!=model.RestsDataUuid);
                if (query != null)
                {
                    response.SetFailed("该类别名称已存在");
                    return Ok(response);
                }
                var entity = _dbContext.RestsData.FirstOrDefault(x => x.RestsDataUuid == model.RestsDataUuid);
                entity.Name = model.Name;
                entity.Accessory = model.Accessory;
                entity.Type = model.Type;
                entity.UpdatePeople = AuthContextService.CurrentUser.LoginName;
                entity.UpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("编辑", "成功:编辑:其他数据信息一条数据", _dbContext);
                }
                response.SetSuccess();
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
                var sql = string.Format("UPDATE [RestsData] SET IsDeleted=@IsDel WHERE RestsDataUuid IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@IsDel", (int)isDeleted));
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
                ToLog.AddLog("删除", "成功:删除:其他数据信息一条数据", _dbContext);
                var response = ResponseModelFactory.CreateInstance;
                return response;
            }
        }
        #endregion
        #endregion
    }
}
