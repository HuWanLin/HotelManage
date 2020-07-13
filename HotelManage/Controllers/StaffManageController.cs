using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using HotelManage.BLL;

namespace HotelManage.Controllers
{
    /// <summary>
    /// 员工管理 控制器
    /// </summary>
    public class StaffManageController : Controller
    {
        #region returnView
        /// <summary>
        /// 查看员工
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 员工管理 
        /// </summary>
        /// <returns></returns>
        public ActionResult ManageStaff()
        {
            return View();
        }
        #endregion

        #region returnData
        /// <summary>
        /// 获取所有的员工
        /// </summary>
        /// <param name="name">员工名字</param>
        /// <param name="dept">员工部门</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">一页多少行</param>
        /// <returns></returns>
        public ActionResult GetAllStaff(string name, string dept, string state, string page, string rows)
        {
            StaffManageBLL staffManageBLL = new StaffManageBLL();
            return Json(staffManageBLL.GetAllStaff(name, dept, state, page, rows), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加员工
        /// </summary>
        /// <param name="name">员工姓名</param>
        /// <param name="sex">员工性别</param>
        /// <param name="age">员工年龄</param>
        /// <param name="nation">员工民族</param>
        /// <param name="phone">员工手机</param>
        /// <param name="email">员工邮箱</param>
        /// <param name="DeptTxt">部门编号</param>
        /// <param name="StateTxt">员工状态</param>
        /// <returns></returns>
        public ActionResult AddStaff(string name, string sex, string age, string nation, string phone, string email, string DeptTxt, string StateTxt)
        {
            StaffManageBLL staffManageBLL = new StaffManageBLL();
            return Json(staffManageBLL.AddStaff(name, sex, age, nation, phone, email, DeptTxt, StateTxt), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改员工信息
        /// </summary>
        /// <param name="id">员工编号</param>
        /// <param name="name">员工姓名</param>
        /// <param name="sex">员工性别</param>
        /// <param name="age">员工年龄</param>
        /// <param name="nation">员工民族</param>
        /// <param name="phone">员工手机</param>
        /// <param name="email">员工邮箱</param>
        /// <param name="DeptTxt">部门编号</param>
        /// <param name="StateTxt">员工状态</param>
        /// <returns></returns>
        public ActionResult EditStaff(string id, string name, string sex, string age, string nation, string phone, string email, string DeptTxt, string StateTxt)
        {
            StaffManageBLL staffManageBLL = new StaffManageBLL();
            return Json(staffManageBLL.EditStaff(id, name, sex, age, nation, phone, email, DeptTxt, StateTxt), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 删除员工
        /// </summary>
        /// <param name="id">员工编号</param>
        /// <returns></returns>
        public ActionResult DestroyStaff(string id)
        {
            StaffManageBLL staffManageBLL = new StaffManageBLL();
            return Json(staffManageBLL.DestroyStaff(Convert.ToInt32(id)), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取所有员工状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetALLStaffTxt(string id)
        {
            return Json(new StaffManageBLL().GetALLStaffTxt(id), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}