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
using HaikanCMS.Api.RequestPayload.TreeMenu;
using HaikanCMS.Api.ViewModels.Rbac.Column;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace HaikanCMS.Api.Controllers.Api.V1.MenuTree
{
    [Route("api/v1/MenuTree/[controller]/[action]")]
    [ApiController]
    public class MenuTreeController : ControllerBase
    {
        private readonly haiKanChemistryContext _dbContext;
        private readonly IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        public MenuTreeController(haiKanChemistryContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// 栏目结构树
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetList(TreeMenu tree)
        {
            using (_dbContext)
            {
                var query = _dbContext.Column.Where(x => x.IsDeleted != 1&&x.SuperiorUuid==null);
                if (!string.IsNullOrEmpty(tree.Kw))
                {
                    query = query.Where(x => x.ColumnTitle.Contains(tree.Kw.Trim()));
                }
                query = query.OrderByDescending(x => x.IsStick).ThenByDescending(x=>x.SortId);
                var data = query.Paged(tree.CurrentPage, tree.PageSize).ToList();
                var totalCount = query.Count();
                                                                                                                                                                                                                                                                                                  //拼接json字符串
                var returndata = "[";
                for (int i = 0; i < data.Count; i++)
                {
                    int shuju = _dbContext.Column.Count(x => x.SuperiorUuid == data[i].ColumnUuid);


                    if (data[i].SuperiorMenu == 0)
                    {
                        //var cplist = data[i].cpName.Split(',');
                        returndata = returndata + "{columnUuid:'" + data[i].ColumnUuid + "',columnTitle:'" + data[i].ColumnTitle + "',sortID:'" + data[i].SortId + "',addTime:'" + data[i].AddTime + "',isStick:'" + data[i].IsStick + "',children:" + (shuju > 0 ? ChildrenData(data[i].ColumnUuid) : "[]") + "},";
                    }
                }

                returndata = returndata.Trim(',') + "]";
                var obj = JsonConvert.DeserializeObject(returndata);
                return Ok(new { obj, totalCount });
            }
        }


        /// <summary>
        /// 获取子级方法(拼接到json字符串)
        /// </summary>
        /// <param name="clientuuid"></param>
        /// <returns></returns>
        public string ChildrenData(Guid clientuuid)
        {
            var returndata = "[";
            if (_dbContext.Column.Any(x => x.SuperiorUuid == clientuuid && x.IsDeleted == 0))
            {
                var data = _dbContext.Column.Where(x => x.SuperiorUuid == clientuuid && x.IsDeleted == 0).OrderByDescending(x => x.IsStick).ThenByDescending(x=>x.SortId).ToList();
                var totalCount = data.Count;
                
                for (int i = 0; i < data.Count; i++)
                {
                    int shuju = _dbContext.Column.Count(x => x.SuperiorUuid == data[i].ColumnUuid);
                    

                    if (data[i] != null)
                    {
                        //var cplist = data[i].cpName.Split(',');
                        returndata = returndata + "{columnUuid:'" + data[i].ColumnUuid + "',columnTitle:'" + data[i].ColumnTitle + "',sortID:'" + data[i].SortId + "',addTime:'" + data[i].AddTime + "',isStick:'" + data[i].IsStick + "',children:" +(shuju>0?ChildrenData(data[i].ColumnUuid):"[]") + "},";
                    }
                }

                returndata = returndata.Trim(',') + "]";
                return returndata;
            }

            return returndata.Trim(',') + "]";
        }

        /// <summary>
        /// 获取全部栏目下拉框
        /// </summary>
        /// <param>惟一编码</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult getcolumnList()
        {
            using (_dbContext)
            {
                var query = from p in _dbContext.Column
                            select new
                            {
                                value = p.ColumnUuid,
                                label = p.ColumnTitle,
                            };
                var response = ResponseModelFactory.CreateInstance;
                response.SetData(query.ToList());
                return Ok(response);
            }
        }


        /// <summary>
        /// 添加栏目节点
        /// </summary>
        /// <param name="tree"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(ColumnViewModel vmodel)
        {
            var response = ResponseModelFactory.CreateInstance;
            using (_dbContext)
            {
                if(_dbContext.Column.Any(x => x.ColumnTitle == vmodel.ColumnTitle&&x.IsDeleted==0 && x.SuperiorUuid==vmodel.SuperiorUuid))
                {
                    response.SetFailed("已存在该名称的栏目");
                    return Ok(response);
                }
                var entity = new Column();

                entity.ColumnUuid = Guid.NewGuid();
                entity.IsDeleted = 0;
                entity.ColumnTitle = vmodel.ColumnTitle;
                entity.AddTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                entity.AddPeople = AuthContextService.CurrentUser.LoginName;
                //if (vmodel.SuperiorUuid == null || vmodel.SuperiorUuid == Guid.Empty)
                //{
                //    entity.SuperiorUuid = Guid.Empty;
                //    entity.SuperiorMenu = 0;
                //}
                //else
                //{
                //    Guid? newuuid = vmodel.SuperiorUuid;
                //    var supentity = _dbContext.Column.FirstOrDefault(x => x.ColumnUuid == newuuid);
                //    entity.SuperiorMenu = supentity.SuperiorMenu + 1;
                //    entity.SuperiorUuid = newuuid;
                //}
                entity.SuperiorUuid = vmodel.SuperiorUuid;
                entity.SuperiorMenu = vmodel.SuperiorUuid == null ? 0 : 1;
                entity.ColumnModel = vmodel.ColumnModel;
                entity.IsStick = vmodel.IsStick;
                entity.ColumnContent = vmodel.ColumnContent;
                entity.SortId = vmodel.SortId;
                _dbContext.Column.Add(entity);
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("添加", "成功:添加:栏目列表信息一条数据", _dbContext);
                }
                response.SetSuccess("操作成功");
                return Ok(response);
            }
            
        }



        /// <summary>
        /// 编辑
        /// </summary>
        /// <param NewsTitle="guid">惟一编码</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult loadEditData(Guid guid)
        {
            using (_dbContext)
            {
                var entity = _dbContext.Column.FirstOrDefault(x => x.ColumnUuid == guid);
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
                var entity = _dbContext.Column.FirstOrDefault(x => x.ColumnUuid == vmodel.ColumnUuid);
                if (entity.ColumnTitle != vmodel.ColumnTitle) {
                    if (_dbContext.Column.Any(x => x.ColumnTitle == vmodel.ColumnTitle && x.IsDeleted == 0))
                    {
                        response.SetFailed("已存在该名称的栏目");
                        return Ok(response);
                    }
                }
                entity.ColumnTitle = vmodel.ColumnTitle;
                entity.SuperiorUuid = vmodel.SuperiorUuid;
                entity.SuperiorMenu = vmodel.SuperiorUuid == null ? 0 : 1;
                entity.ColumnModel = vmodel.ColumnModel;
                entity.ColumnContent = vmodel.ColumnContent;
                entity.SortId = vmodel.SortId;
                entity.IsStick = vmodel.IsStick;
                int res = _dbContext.SaveChanges();
                if (res > 0)
                {
                    ToLog.AddLog("编辑", "成功:编辑:栏目列表信息一条数据", _dbContext);
                }
                response = ResponseModelFactory.CreateInstance;
                return Ok(response);
            }
        }

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
                var sql = string.Format("UPDATE [Column] SET IsDeleted=@IsDel WHERE ColumnUuid IN ({0})", parameterNames);
                parameters.Add(new SqlParameter("@IsDel", (int)isDeleted));
                _dbContext.Database.ExecuteSqlRaw(sql, parameters);
                ToLog.AddLog("删除", "成功:删除:栏目列表信息一条数据", _dbContext);
                var response = ResponseModelFactory.CreateInstance;
                return response;
            }
        }
        #endregion









    }
}
