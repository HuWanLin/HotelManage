using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using HotelManage.BLL;
using HotelManage.Models;

namespace HotelManage.Controllers
{
    /// <summary>
    /// 通知管理 控制器
    /// </summary>
    public class RegulationsManageController : Controller
    {

        #region returnView      
        /// <summary>
        /// 查看通知 页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowRegulations()
        {
            return View();
        }

        /// <summary>
        /// 通知管理 页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ManageRegulations()
        {
            return View();
        }

        /// <summary>
        /// 通知详细页面
        /// </summary>
        /// <returns></returns>
        public ActionResult DetailedRegulations(string id)
        {
            RegulationsViewModel r = new RegulationsManageBLL().GetRegulationsById(id);
            ViewData["Title"] = r.Title;
            ViewData["Content"] = r.RegulationsContent;
            ViewData["EstablishDate"] = r.EstablishDate;
            ViewData["StaffName"] = r.StaffName;
            return View();
        }
        #endregion

        #region returnData
        /// <summary>
        /// 获取所有的规定数据
        /// </summary>
        /// <param name="num">搜索参数：编号</param>
        /// <param name="name">搜索参数：姓名</param>
        /// <param name="age">搜索参数：年龄</param>
        /// <param name="page">分页参数：第几页</param>
        /// <param name="rows">分页参数：一页多少行</param>
        /// <returns>Json数据</returns>
        public JsonResult GetAllRegulations(string name, string title, string page, string rows)
        {
            RegulationsManageBLL regulationsManageBLL = new RegulationsManageBLL();
            return Json(regulationsManageBLL.GetAllRegulations(name, title, page, rows, Session["UserName"].ToString()), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加通知 
        /// </summary>
        /// <param name="title">通知标题</param>
        /// <param name="tegulationsContent">通知内容</param>
        /// <param name="DeptList">接收部门</param>
        /// <returns></returns>
        public JsonResult AddRegulations(string title, string tegulationsContent, string[] DeptList)
        {
            RegulationsManageBLL regulationsManageBLL = new RegulationsManageBLL();
            NewsModel newModel = regulationsManageBLL.AddRegulations(title, tegulationsContent, DeptList, Session["UserName"].ToString());
            return Json(newModel, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除通知
        /// </summary>
        /// <param name="id">通知编号</param>
        /// <returns></returns>
        public JsonResult DestroyRegulations(string id)
        {
            return Json(new RegulationsManageBLL().DestroyRegulations(id), JsonRequestBehavior.AllowGet);
        }
        #endregion        
    }
}