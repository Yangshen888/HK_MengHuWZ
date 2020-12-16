﻿using HaikanCMS.Api.Entities;
using HaikanCMS.Api.Extensions.AuthContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaikanCMS.Api.logs.TolLog
{
    public class ToLog
    {
        public static string AddLog(string Typem, string Content, haiKanChemistryContext _dbContext)
        {
            string messange = "";
            var entity = new HaikanCMS.Api.Entities.SystemLog();
            entity.SystemLogUuid = Guid.NewGuid();
            entity.UserId = AuthContextService.CurrentUser.Guid.ToString();
            entity.UserName = AuthContextService.CurrentUser.LoginName;
            entity.Type = Typem;
            entity.OperationContent = Content;
            entity.OperationTime = DateTime.Now;
            entity.AddTime = DateTime.Now;
            entity.AddPeople = "系统日志";
            entity.IsDelete = 0;
            _dbContext.SystemLog.Add(entity);
            _dbContext.SaveChanges();
            return messange;
        }
    }
}
