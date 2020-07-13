using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using HotelManage.Tool;   //导入工具类
using HotelManage.BLL;
using HotelManage.Models;

namespace HotelManage.Controllers
{
    /// <summary>
    /// 登录 控制器
    /// </summary>
    public class LoginController : Controller
    {
        #region returnView
        /// <summary>
        /// 登录页显示
        /// </summary>
        /// <returns>返回登录首页</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 登录响应方法
        /// </summary>
        /// <param name="loginId">帐号</param>
        /// <param name="loginPwd">密码</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(string loginId, string loginPwd, string valiCodeInput)
        {

            StaffManageBLL staffManageBLL = new StaffManageBLL();
            NewsModel newModel = staffManageBLL.StaffLogin(loginId, loginPwd, valiCodeInput, Session["ValidateCode"].ToString());
            string[] sArray = newModel.executeResult.Split('|');
            Session["UserName"] = sArray[0];
            if (sArray.Length == 2)
            {
                Session["DdentityId"] = sArray[1];
            }           
            return Json(newModel, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 退出系统
        /// </summary>
        /// <returns>返回首页</returns>
        public ActionResult Exit()
        {
            Session.Abandon();  //清空数据
            return Redirect("Index");   //重定向到登录页面
        }
        #endregion

        #region returnData
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns>图片数据</returns>
        public ActionResult GetValidateCode()
        {
            VerificationCode vc = new VerificationCode();
            string yanzhengma = vc.CreateVerificationText(5);   //获取随机的五位数验证码  
            Session["ValidateCode"] = yanzhengma;          //保存验证码
            byte[] bytes = vc.CreateValidateGraphic(yanzhengma);     //验证码字符串变成图片
            return File(bytes, @"image/jpeg");            //返回图片
        }
        #endregion
    }
}