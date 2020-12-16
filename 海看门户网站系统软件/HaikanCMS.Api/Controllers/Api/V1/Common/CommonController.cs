using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HaikanCMS.Api.Entities;
using HaikanCMS.Api.Extensions;
using HaikanCMS.Api.Extensions.CustomException;
using HaikanCMS.Api.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HaikanCMS.Api.Controllers.Api.V1.Common
{
    [Route("api/v1/common/[controller]/[action]")]
    [ApiController]
    [CustomAuthorize]
    public class CommonController : ControllerBase
    {
        private readonly haiKanChemistryContext _dbContext;
        private readonly IMapper _mapper;
        private IWebHostEnvironment _hostingEnvironment;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="mapper"></param>
        /// <param name="hostingEnvironment"></param>
        public CommonController(haiKanChemistryContext dbContext, IMapper mapper, IWebHostEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }
        #region 文件上传删除操作
        /// <summary>
        /// 图片上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> UpLoadPictureAsync([FromForm] FileCommon fc)
        {
            var response = ResponseModelFactory.CreateInstance;
            //var check = System.IO.File.Exists(filename);

            var files = Request.Form.Files;
            if (files == null || files.Count() <= 0)
            {
                response.SetFailed("请上图片");
                return Ok(response);
            }
            //var paths = new List<string>();
            //var dataPaths = new List<string>();
            var allowType = new string[] { "image/jpeg", "image/jpg", "image/png" };
            var message = "";
            try
            {
                if (files.Any(c => allowType.Contains(c.ContentType)))
                {
                    if (files[0].Length > 1024 * 1024 * 5)
                    {
                        message += files[0].FileName + "大小超过5M";
                        response.SetFailed(message);
                        return Ok(response);
                    }
                    int index = files[0].FileName.LastIndexOf('.');
                    string fname = DateTime.Now.ToString("yyyyMMdd_HHmmssfff") + "_" + Guid.NewGuid().ToString() + files[0].FileName.Substring(index);
                    string strpath = Path.Combine("UploadFiles/NesFile/Picture/" + fc.FileSavePath, fname);
                    string path = Path.Combine(_hostingEnvironment.WebRootPath, strpath);
                    //System.IO.File.Create(path);
                    var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    await files[0].CopyToAsync(stream);
                    string i = Request.HttpContext.Connection.LocalIpAddress.MapToIPv4().ToString();
                    stream.Close();
                    //response.SetData(new { strpath, fname });
                    response.SetData("http://192.168.0.226:4062/" + strpath);
                    //response.SetData("http://localhost:54321/" + strpath);
                    //response.SetData("https://zgdhx.api.dodokon.com/" + strpath);
                }
                if (message.Length > 0)
                {
                    response.SetFailed(message);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.SetFailed(ex.Message);
                return Ok(response);
            }

        }

        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> UpLoadPicture2Async([FromForm] FileCommon fc)
        {
            var response = ResponseModelFactory.CreateInstance;
            //var check = System.IO.File.Exists(filename);

            var files = Request.Form.Files;
            if (files == null || files.Count() <= 0)
            {
                response.SetFailed("请上图片");
                return Ok(response);
            }
            //var paths = new List<string>();
            //var dataPaths = new List<string>();
            var allowType = new string[] { "image/jpeg", "image/jpg", "image/png" };
            var message = "";
            try
            {
                if (files.Any(c => allowType.Contains(c.ContentType)))
                {
                    if (files[0].Length > 1024 * 1024 * 5)
                    {
                        message += files[0].FileName + "大小超过5M";
                        response.SetFailed(message);
                        return Ok(response);
                    }
                    int index = files[0].FileName.LastIndexOf('.');
                    string fname = DateTime.Now.ToString("yyyyMMdd_HHmmssfff") + "_" + Guid.NewGuid().ToString() + files[0].FileName.Substring(index);
                    string strpath = Path.Combine("UploadFiles/" + fc.FileSavePath, fname);
                    string path = Path.Combine(_hostingEnvironment.WebRootPath, strpath);
                    //System.IO.File.Create(path);
                    var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    await files[0].CopyToAsync(stream);
                    string i = Request.HttpContext.Connection.LocalIpAddress.MapToIPv4().ToString();
                    stream.Close();
                    response.SetData(new { strpath, fname });
                    //response.SetData("http://192.168.0.226:4062/" + strpath);
                    //response.SetData("http://localhost:54321/" + strpath);
                }
                if (message.Length > 0)
                {
                    response.SetFailed(message);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.SetFailed(ex.Message);
                return Ok(response);
            }

        }


        /// <summary>
        /// 文件上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> UpLoadFileAsync([FromForm] FileCommon fc)
        {
            var response = ResponseModelFactory.CreateInstance;

            var files = Request.Form.Files;
            if (files == null || files.Count() <= 0)
            {
                response.SetFailed("请上传文件");
                return Ok(response);
            }
            try
            {
                int index = files[0].FileName.LastIndexOf('.');
                string fname = DateTime.Now.ToString("yyyyMMdd_HHmmssfff") + "_" + Guid.NewGuid().ToString() + files[0].FileName.Substring(index);
                string strpath = Path.Combine("UploadFiles/" + fc.FileSavePath, fname);
                string path = Path.Combine(_hostingEnvironment.WebRootPath, strpath);
                var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                await files[0].CopyToAsync(stream);

                stream.Close();

                response.SetData(new { strpath, fname });
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.SetFailed(ex.Message);
                return Ok(response);
            }


        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult DeleteFile(FileCommon fc)
        {
            var response = ResponseModelFactory.CreateInstance;
            var path = Path.Combine(_hostingEnvironment.WebRootPath + "/", fc.FilePath);
            try
            {
                if (System.IO.File.Exists(path))
                {
                    FileInfo info = new FileInfo(path);
                    if (info.Attributes != FileAttributes.ReadOnly)
                    {
                        System.IO.File.Delete(path);
                        response.SetSuccess("删除成功");
                    }
                    else
                    {
                        response.SetFailed();
                    }
                }
                else
                {
                    response.SetFailed("文件不存在");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.SetFailed(ex.Message);
                return Ok(response);
            }
        }
        #endregion
    }
}
