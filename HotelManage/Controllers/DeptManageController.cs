using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using HotelManage.BLL;
using HotelManage.DAL;

namespace HotelManage.Controllers
{
    /// <summary>
    /// 部门管理 控制器
    /// </summary>
    public class DeptManageController : Controller
    {

        #region returnView
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region returnData
        /// <summary>
        /// 获取所有部门
        /// </summary>
        /// <param name="id">是否要加全部这个选项</param>
        /// <returns></returns>
        public ActionResult GetAllDept(int id)
        {

            DeptManageBLL deptManageBLL = new DeptManageBLL();
            return Json(deptManageBLL.GetAllDept(id), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}