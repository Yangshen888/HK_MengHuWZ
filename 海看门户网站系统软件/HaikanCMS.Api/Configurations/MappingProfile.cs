using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HaikanCMS.Api.ViewModels.Rbac.Department;
using HaikanCMS.Api.ViewModels.Rbac.SystemMenu;
using HaikanCMS.Api.ViewModels.Rbac.SystemPermission;
using HaikanCMS.Api.ViewModels.Rbac.SystemRole;
using HaikanCMS.Api.ViewModels.Rbac.SystemUser;
using HaikanCMS.Api.Entities;
//using HaikanCMS.Api.ViewModels.Person;
//using HaikanCMS.Api.ViewModels.Base;
//using HaikanCMS.Api.ViewModels.Score;

namespace HaikanCMS.Api.Configurations
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            #region SystemUser
            CreateMap<SystemUser, UserJsonModel>();
            CreateMap<UserCreateViewModel, SystemUser>();
            CreateMap<UserEditViewModel, SystemUser>();
            CreateMap<SystemUser, UserEditViewModel>();
            #endregion
            #region SystemRole
            CreateMap<SystemRole, RoleJsonModel>();
            CreateMap<RoleCreateViewModel, SystemRole>();
            CreateMap<SystemRole, RoleCreateViewModel>();
            #endregion

            #region SystemMenu
            CreateMap<SystemMenu, MenuJsonModel>();
            CreateMap<MenuCreateViewModel, SystemMenu>();
            CreateMap<MenuEditViewModel, SystemMenu>();
            CreateMap<SystemMenu, MenuEditViewModel>();
            #endregion

            #region SystemPermission
            CreateMap<SystemPermission, PermissionJsonModel>()
                .ForMember(d => d.MenuName, s => s.MapFrom(x => x.SystemMenuUu.Name))
                .ForMember(d => d.PermissionTypeText, s => s.MapFrom(x => x.Type.ToString()));
            CreateMap<PermissionCreateViewModel, SystemPermission>();
            CreateMap<PermissionEditViewModel, SystemPermission>();
            CreateMap<SystemPermission, PermissionEditViewModel>();
            #endregion



        }
    }
}
