using System;
using HaikanCMS.Api.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HaikanCMS.Api.Entities
{
    public partial class haiKanChemistryContext : DbContext
    {
        public haiKanChemistryContext()
        {
        }

        public haiKanChemistryContext(DbContextOptions<haiKanChemistryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cabinet> Cabinet { get; set; }
        public virtual DbSet<Column> Column { get; set; }
        public virtual DbSet<Content> Content { get; set; }
        public virtual DbSet<ExternalLink> ExternalLink { get; set; }
        public virtual DbSet<LinkType> LinkType { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<NewsType> NewsType { get; set; }
        public virtual DbSet<Proclamation> Proclamation { get; set; }
        public virtual DbSet<RestsData> RestsData { get; set; }
        public virtual DbSet<SystemIcon> SystemIcon { get; set; }
        public virtual DbSet<SystemLog> SystemLog { get; set; }
        public virtual DbSet<SystemMenu> SystemMenu { get; set; }
        public virtual DbSet<SystemPermission> SystemPermission { get; set; }
        public virtual DbSet<SystemRole> SystemRole { get; set; }
        public virtual DbSet<SystemRolePermissionMapping> SystemRolePermissionMapping { get; set; }
        public virtual DbSet<SystemSetting> SystemSetting { get; set; }
        public virtual DbSet<SystemUser> SystemUser { get; set; }
        public virtual DbSet<SystemUserRoleMapping> SystemUserRoleMapping { get; set; }
        public virtual DbSet<ViewMenu> ViewMenu { get; set; }
        public virtual DbSet<ViewSystemPermissionWithMenu> ViewSystemPermissionWithMenu { get; set; }
        public virtual DbSet<ViewSystemPermissionWithMenu2> ViewSystemPermissionWithMenu2 { get; set; }
        public virtual DbSet<Visit> Visit { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseSqlServer("Data Source=192.168.0.147;Initial Catalog=xingxing;Persist Security Info=True;User ID=sa;Password=root");
                var conncectstr = ConfigurationManager.ConnectionStrings.DefaultConnection;
                optionsBuilder.UseSqlServer(conncectstr);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cabinet>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Cabinet");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Column>(entity =>
            {
                entity.HasKey(e => e.ColumnUuid)
                    .IsClustered(false);

                entity.HasComment("栏目菜单表");

                entity.HasIndex(e => e.Id)
                    .HasName("IX_Column")
                    .IsClustered();

                entity.Property(e => e.ColumnUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.ColumnAudio).HasComment("音频");

                entity.Property(e => e.ColumnContent).HasComment("栏目内容");

                entity.Property(e => e.ColumnFile).HasComment("其他文件");

                entity.Property(e => e.ColumnModel).HasComment("栏目模型");

                entity.Property(e => e.ColumnNum).HasComment("栏目编号");

                entity.Property(e => e.ColumnPic).HasComment("图片");

                entity.Property(e => e.ColumnTitle)
                    .HasMaxLength(255)
                    .HasComment("文档标题");

                entity.Property(e => e.ColumnTitleText)
                    .HasMaxLength(255)
                    .HasComment("所属文档标题");

                entity.Property(e => e.ColumnType)
                    .HasMaxLength(255)
                    .HasComment("栏目类型");

                entity.Property(e => e.ColumnUrl).HasComment("外部链接");

                entity.Property(e => e.ColumnVideo).HasComment("视频");

                entity.Property(e => e.ColumnWord)
                    .HasMaxLength(255)
                    .HasComment("文档");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDeleted).HasComment("0正常 1删除");

                entity.Property(e => e.IsStick)
                    .HasDefaultValueSql("((0))")
                    .HasComment("是否置顶");

                entity.Property(e => e.IssueTime)
                    .HasMaxLength(255)
                    .HasComment("发布时间");

                entity.Property(e => e.SortId)
                    .HasColumnName("SortID")
                    .HasDefaultValueSql("((0))")
                    .HasComment("排序编号");

                entity.Property(e => e.Staue).HasMaxLength(255);

                entity.Property(e => e.SuperiorMenu).HasComment("栏目级别:0.一级");

                entity.Property(e => e.SuperiorUuid).HasComment("上级栏目Uuid");
            });

            modelBuilder.Entity<Content>(entity =>
            {
                entity.HasKey(e => e.ColumnUuid)
                    .IsClustered(false);

                entity.HasComment("内容表");

                entity.HasIndex(e => e.Id)
                    .HasName("IX_Content")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.ColumnUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople).HasMaxLength(255);

                entity.Property(e => e.AddTime).HasMaxLength(255);

                entity.Property(e => e.ColumnTitle).HasMaxLength(255);

                entity.Property(e => e.ColumnTitleText).HasMaxLength(255);

                entity.Property(e => e.ColumnType).HasMaxLength(255);

                entity.Property(e => e.ColumnWord).HasMaxLength(255);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsStick)
                    .HasDefaultValueSql("((0))")
                    .HasComment("是否置顶");

                entity.Property(e => e.IssueTime).HasMaxLength(255);

                entity.Property(e => e.SortId).HasColumnName("SortID");

                entity.Property(e => e.Staue).HasMaxLength(255);
            });

            modelBuilder.Entity<ExternalLink>(entity =>
            {
                entity.HasKey(e => e.ExternalLinkUuid)
                    .IsClustered(false);

                entity.HasComment("链接管理表");

                entity.HasIndex(e => e.Id)
                    .HasName("IX_ExternalLink")
                    .IsClustered();

                entity.Property(e => e.ExternalLinkUuid)
                    .HasComment("新闻类别")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDeleted).HasComment("0正常 1删除");

                entity.Property(e => e.Link)
                    .HasMaxLength(255)
                    .HasComment("链接");

                entity.Property(e => e.LinkTypeUuid)
                    .HasColumnName("LinkTypeUUID")
                    .HasComment("链接类别");

                entity.Property(e => e.Remark).HasComment("备注");

                entity.Property(e => e.Staue).HasMaxLength(255);
            });

            modelBuilder.Entity<LinkType>(entity =>
            {
                entity.HasKey(e => e.LinkTypeUuid)
                    .IsClustered(false);

                entity.HasComment("链接类别表");

                entity.HasIndex(e => e.Id)
                    .HasName("IX_LinkType")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.LinkTypeUuid)
                    .HasColumnName("LinkTypeUUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddPeople).HasMaxLength(255);

                entity.Property(e => e.AddTime).HasMaxLength(255);

                entity.Property(e => e.Class).HasMaxLength(255);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.HasKey(e => e.NewsUuid)
                    .HasName("PK_新闻表")
                    .IsClustered(false);

                entity.HasComment("新闻表");

                entity.HasIndex(e => e.Id)
                    .HasName("IX_新闻表")
                    .IsClustered();

                entity.Property(e => e.NewsUuid).ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.File).HasComment("文件");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDeleted).HasComment("0正常 1删除");

                entity.Property(e => e.IsStick)
                    .HasDefaultValueSql("((0))")
                    .HasComment("是否置顶");

                entity.Property(e => e.NewSubhead).HasComment("新闻副标题");

                entity.Property(e => e.NewsContent).HasComment("新闻内容");

                entity.Property(e => e.NewsPic).HasComment("图片");

                entity.Property(e => e.NewsTitle).HasComment("新闻标题");

                entity.Property(e => e.NewsTypeUuid).HasComment("新闻类别");

                entity.Property(e => e.NewsUrl).HasComment("链接");

                entity.Property(e => e.SortId)
                    .HasColumnName("SortID")
                    .HasComment("排序编号");

                entity.Property(e => e.Staue).HasComment("发布状态:0保存1发布");

                entity.Property(e => e.Video)
                    .HasColumnName("video")
                    .HasComment("视频");
            });

            modelBuilder.Entity<NewsType>(entity =>
            {
                entity.HasKey(e => e.NewsTypeUuid)
                    .IsClustered(false);

                entity.HasComment("新闻类别表");

                entity.HasIndex(e => e.Id)
                    .HasName("IX_NewsType")
                    .IsClustered();

                entity.Property(e => e.NewsTypeUuid)
                    .HasComment("新闻类别")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDeleted).HasComment("0正常 1删除");

                entity.Property(e => e.NewsTypeName)
                    .HasMaxLength(255)
                    .HasComment("新闻类别名称");

                entity.Property(e => e.Remark).HasComment("备注");

                entity.Property(e => e.SortId)
                    .HasColumnName("SortID")
                    .HasComment("排序编号");

                entity.Property(e => e.Staue).HasMaxLength(255);
            });

            modelBuilder.Entity<Proclamation>(entity =>
            {
                entity.HasKey(e => e.ProclamationUuid)
                    .IsClustered(false);

                entity.HasComment("通知公告表");

                entity.HasIndex(e => e.Id)
                    .HasName("IX_Proclamation")
                    .IsClustered();

                entity.Property(e => e.ProclamationUuid)
                    .HasComment("")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDeleted).HasComment("0正常 1删除");

                entity.Property(e => e.IsStick)
                    .HasDefaultValueSql("((0))")
                    .HasComment("是否置顶");

                entity.Property(e => e.ProclamationContent).HasComment("通知公告内容");

                entity.Property(e => e.ProclamationTitle)
                    .HasMaxLength(255)
                    .HasComment("通知公告标题");

                entity.Property(e => e.Remark).HasComment("备注");

                entity.Property(e => e.SortId)
                    .HasColumnName("SortID")
                    .HasComment("排序编号");

                entity.Property(e => e.Staue).HasMaxLength(255);
            });

            modelBuilder.Entity<RestsData>(entity =>
            {
                entity.HasKey(e => e.RestsDataUuid);

                entity.HasComment("其他数据表");

                entity.HasIndex(e => e.RestsDataUuid)
                    .HasName("IX_RestsData")
                    .IsUnique();

                entity.Property(e => e.RestsDataUuid)
                    .HasColumnName("RestsDataUUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Accessory).HasComment("附件");

                entity.Property(e => e.AddPeople).HasMaxLength(255);

                entity.Property(e => e.AddTime).HasMaxLength(255);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasComment("名称");

                entity.Property(e => e.Type)
                    .HasMaxLength(255)
                    .HasComment("类型");

                entity.Property(e => e.UpdatePeople)
                    .HasMaxLength(255)
                    .HasComment("更新人");

                entity.Property(e => e.UpdateTime)
                    .HasMaxLength(255)
                    .HasComment("更新时间");
            });

            modelBuilder.Entity<SystemIcon>(entity =>
            {
                entity.HasKey(e => e.SystemIconUuid)
                    .HasName("PK__SystemIc__540CED9CA9C5EF3A")
                    .IsClustered(false);

                entity.HasComment("图标");

                entity.Property(e => e.SystemIconUuid)
                    .HasColumnName("SystemIconUUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Color).HasMaxLength(50);

                entity.Property(e => e.Custom).HasMaxLength(60);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Size).HasMaxLength(20);
            });

            modelBuilder.Entity<SystemLog>(entity =>
            {
                entity.HasKey(e => e.SystemLogUuid)
                    .HasName("PK__SystemLo__EC98D064570938F6")
                    .IsClustered(false);

                entity.HasComment("系统日志表");

                entity.Property(e => e.SystemLogUuid)
                    .HasColumnName("SystemLogUUID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasColumnType("datetime")
                    .HasComment("添加时间");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Ipaddress)
                    .HasColumnName("IPAddress")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("IP地址");

                entity.Property(e => e.IsDelete).HasComment("标记删除1，未删除2，已删除");

                entity.Property(e => e.OperationContent)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasComment("操作内容");

                entity.Property(e => e.OperationTime)
                    .HasColumnType("datetime")
                    .HasComment("操作时间");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("操作类型(增删改查)");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("操作用户ID");

                entity.Property(e => e.UserIdtype)
                    .HasColumnName("UserIDType")
                    .HasComment("用户名和类型");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("操作用户用户名");
            });

            modelBuilder.Entity<SystemMenu>(entity =>
            {
                entity.HasKey(e => e.SystemMenuUuid)
                    .HasName("PK__SystemMe__859EB749BD38D1AA")
                    .IsClustered(false);

                entity.HasComment("菜单表");

                entity.Property(e => e.SystemMenuUuid)
                    .HasColumnName("SystemMenuUUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Alias).HasMaxLength(255);

                entity.Property(e => e.BeforeCloseFun).HasMaxLength(255);

                entity.Property(e => e.Component).HasMaxLength(255);

                entity.Property(e => e.CreatedByUserName).HasMaxLength(255);

                entity.Property(e => e.CreatedOn).HasMaxLength(255);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Icon).HasMaxLength(255);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedByUserName).HasMaxLength(255);

                entity.Property(e => e.ModifiedOn).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.ParentName).HasMaxLength(255);

                entity.Property(e => e.Url).HasMaxLength(255);
            });

            modelBuilder.Entity<SystemPermission>(entity =>
            {
                entity.HasKey(e => e.SystemPermissionUuid)
                    .HasName("PK__SystemPe__44671AD2FBD363BB")
                    .IsClustered(false);

                entity.HasComment("权限表");

                entity.Property(e => e.SystemPermissionUuid)
                    .HasColumnName("SystemPermissionUUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ActionCode).HasMaxLength(255);

                entity.Property(e => e.CaPower).HasMaxLength(255);

                entity.Property(e => e.CreatedByUserName).HasMaxLength(255);

                entity.Property(e => e.CreatedOn).HasMaxLength(255);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Icon).HasMaxLength(255);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedByUserName).HasMaxLength(255);

                entity.Property(e => e.ModifiedOn).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.SystemMenuUuid).HasColumnName("SystemMenuUUID");

                entity.HasOne(d => d.SystemMenuUu)
                    .WithMany(p => p.SystemPermission)
                    .HasForeignKey(d => d.SystemMenuUuid)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__SystemPer__Syste__0CDAE408");
            });

            modelBuilder.Entity<SystemRole>(entity =>
            {
                entity.HasKey(e => e.SystemRoleUuid)
                    .HasName("PK__SystemRo__68ACD1AFF45C10EA")
                    .IsClustered(false);

                entity.HasComment("角色表");

                entity.Property(e => e.SystemRoleUuid)
                    .HasColumnName("SystemRoleUUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddPeople).HasMaxLength(255);

                entity.Property(e => e.AddTime).HasMaxLength(255);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsCreEdu)
                    .HasDefaultValueSql("((0))")
                    .HasComment("是否由管理者创建");

                entity.Property(e => e.IsFixation)
                    .HasDefaultValueSql("((0))")
                    .HasComment("是否内置  0否,1是");

                entity.Property(e => e.RoleName).HasMaxLength(255);

                entity.Property(e => e.SchoolUuid).HasColumnName("SchoolUUID");
            });

            modelBuilder.Entity<SystemRolePermissionMapping>(entity =>
            {
                entity.HasKey(e => new { e.SystemRoleUuid, e.SystemPermissionUuid })
                    .HasName("PK__SystemRo__4CEAA00283521FC9")
                    .IsClustered(false);

                entity.HasComment("角色权限关系");

                entity.Property(e => e.SystemRoleUuid).HasColumnName("SystemRoleUUID");

                entity.Property(e => e.SystemPermissionUuid).HasColumnName("SystemPermissionUUID");

                entity.Property(e => e.AddPeople).HasMaxLength(255);

                entity.Property(e => e.AddTime).HasMaxLength(255);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<SystemSetting>(entity =>
            {
                entity.HasComment("全局配置");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AppPeople)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ClobalName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CountTime).HasMaxLength(255);

                entity.Property(e => e.GlobalLogo)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Globalurl)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            });

            modelBuilder.Entity<SystemUser>(entity =>
            {
                entity.HasKey(e => e.SystemUserUuid)
                    .HasName("PK__SystemUs__0BD86B9580F5647F")
                    .IsClustered(false);

                entity.HasComment("系统用户");

                entity.HasIndex(e => e.Id)
                    .HasName("IX_SystemUser")
                    .IsClustered();

                entity.Property(e => e.SystemUserUuid)
                    .HasColumnName("SystemUserUUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddPeople)
                    .HasMaxLength(255)
                    .HasComment("添加人");

                entity.Property(e => e.AddTime)
                    .HasMaxLength(255)
                    .HasComment("注册时间");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("地址");

                entity.Property(e => e.Age)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("年龄");

                entity.Property(e => e.Content)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("邮箱");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.InTime)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("入职时间");

                entity.Property(e => e.IsCreEdu)
                    .HasDefaultValueSql("((0))")
                    .HasComment("是否由管理级别创建  0否   1是");

                entity.Property(e => e.IsDeleted).HasComment("0正常 1删除");

                entity.Property(e => e.Job)
                    .HasMaxLength(255)
                    .HasComment("职务");

                entity.Property(e => e.LoginName)
                    .HasMaxLength(255)
                    .HasComment("登录名");

                entity.Property(e => e.ManageDepartment).HasColumnType("text");

                entity.Property(e => e.Nickname)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("用户昵称");

                entity.Property(e => e.PassWord)
                    .HasMaxLength(255)
                    .HasComment("密码");

                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("电话");

                entity.Property(e => e.Picture)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("头像");

                entity.Property(e => e.Placeofresidence)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("居住地");

                entity.Property(e => e.RealName)
                    .HasMaxLength(255)
                    .HasComment("真实姓名");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("备注");

                entity.Property(e => e.SchoolUuid)
                    .HasColumnName("SchoolUUID")
                    .HasComment("校区UUID");

                entity.Property(e => e.Sex)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("性别");

                entity.Property(e => e.SystemRoleUuid)
                    .HasColumnName("SystemRoleUUid")
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("角色id,用逗号分隔");

                entity.Property(e => e.Types)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("部门");

                entity.Property(e => e.UserIdCard)
                    .HasMaxLength(255)
                    .HasComment("身份证");

                entity.Property(e => e.UserType).HasComment("1 超管 2其他");

                entity.Property(e => e.Wechat)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("微信");

                entity.Property(e => e.ZaiGang)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("状态");
            });

            modelBuilder.Entity<SystemUserRoleMapping>(entity =>
            {
                entity.HasKey(e => new { e.SystemUserUuid, e.SystemRoleUuid })
                    .HasName("PK__SystemUs__FD52A68F37AD580B")
                    .IsClustered(false);

                entity.HasComment("用户角色关系");

                entity.Property(e => e.SystemUserUuid).HasColumnName("SystemUserUUID");

                entity.Property(e => e.SystemRoleUuid).HasColumnName("SystemRoleUUID");

                entity.Property(e => e.AddPeople).HasMaxLength(255);

                entity.Property(e => e.AddTime)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<ViewMenu>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Menu");

                entity.Property(e => e.Alias).HasMaxLength(255);

                entity.Property(e => e.BeforeCloseFun).HasMaxLength(255);

                entity.Property(e => e.Component).HasMaxLength(255);

                entity.Property(e => e.CreatedByUserName).HasMaxLength(255);

                entity.Property(e => e.CreatedOn).HasMaxLength(255);

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Icon).HasMaxLength(255);

                entity.Property(e => e.ModifiedByUserName).HasMaxLength(255);

                entity.Property(e => e.ModifiedOn).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.ParentName).HasMaxLength(255);

                entity.Property(e => e.Pd).HasColumnName("pd");

                entity.Property(e => e.Ps).HasColumnName("ps");

                entity.Property(e => e.Pt).HasColumnName("pt");

                entity.Property(e => e.SystemMenuUuid).HasColumnName("SystemMenuUUID");

                entity.Property(e => e.SystemRoleUuid).HasColumnName("SystemRoleUUID");

                entity.Property(e => e.Url).HasMaxLength(255);
            });

            modelBuilder.Entity<ViewSystemPermissionWithMenu>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_SystemPermissionWithMenu");

                entity.Property(e => e.MenuAlias).HasMaxLength(255);

                entity.Property(e => e.MenuName).HasMaxLength(255);

                entity.Property(e => e.Pd).HasColumnName("pd");

                entity.Property(e => e.PermissionActionCode).HasMaxLength(255);

                entity.Property(e => e.PermissionName).HasMaxLength(255);

                entity.Property(e => e.Ps).HasColumnName("ps");

                entity.Property(e => e.SystemRoleUuid).HasColumnName("SystemRoleUUID");
            });

            modelBuilder.Entity<ViewSystemPermissionWithMenu2>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_SystemPermissionWithMenu2");

                entity.Property(e => e.MenuAlias).HasMaxLength(255);

                entity.Property(e => e.MenuName).HasMaxLength(255);

                entity.Property(e => e.Pd).HasColumnName("pd");

                entity.Property(e => e.PermissionActionCode).HasMaxLength(255);

                entity.Property(e => e.PermissionName).HasMaxLength(255);

                entity.Property(e => e.Ps).HasColumnName("ps");
            });

            modelBuilder.Entity<Visit>(entity =>
            {
                entity.HasKey(e => e.VisitUuid)
                    .IsClustered(false);

                entity.HasComment("访问量表");

                entity.HasIndex(e => e.Id)
                    .HasName("IX_Visit")
                    .IsUnique()
                    .IsClustered();

                entity.Property(e => e.VisitUuid)
                    .HasColumnName("VisitUUID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AddTime).HasColumnType("datetime");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
