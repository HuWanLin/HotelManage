using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using HotelManage.BLL;
namespace HotelManage.Controllers
{
    /// <summary>
    /// 订单管理 控制器
    /// </summary>
    public class OrderManageController : Controller
    {

        #region returnView
        /// <summary>
        /// 查看在用订单 页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 订单管理 页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ManageOrder()
        {
            return View();
        }

        /// <summary>
        /// 查看历史订单 页面
        /// </summary>
        /// <returns></returns>
        public ActionResult HistoryOrder()
        {
            return View();
        }

        #endregion

        #region returnData
        /// <summary>
        ///  添加订单
        /// </summary>
        /// <param name="Id">房间ID</param>
        /// <param name="name">客户名字</param>
        /// <param name="iDCard">客户身份证</param>
        /// <param name="Sex">性别</param>
        /// <param name="phone">手机</param>
        /// <param name="deposit">押金</param>
        /// <returns></returns>
        public ActionResult AddOrder(string Id, string name, string iDCard, string Sex, string phone, string deposit, string estimatedTime)
        {
            OrderManageBLL orderManageBLL = new OrderManageBLL();
            return Json(orderManageBLL.AddOrder(Id, name, iDCard, Sex, phone, deposit, estimatedTime), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取所有的在用订单
        /// </summary>
        /// <param name="completeNum">房间编号</param>
        /// <param name="name">客户名字</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">一行多少页</param>
        /// <returns></returns>
        public ActionResult GetAllOrderX(string completeNum, string name, string page, string rows)
        {
            OrderManageBLL orderManageBLL = new OrderManageBLL();
            return Json(orderManageBLL.GetAllRoom(completeNum, name, page, rows, false), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取所有的历史订单
        /// </summary>
        /// <param name="completeNum">房间编号</param>
        /// <param name="name">客户名字</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">一行多少页</param>
        /// <returns></returns>
        public ActionResult GetAllOrderL(string completeNum, string name, string page, string rows)
        {
            OrderManageBLL orderManageBLL = new OrderManageBLL();
            return Json(orderManageBLL.GetAllRoom(completeNum, name, page, rows, true), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加退房记录
        /// </summary>
        /// <param name="Id">订单编号</param>
        /// <param name="Amount">退还的押金</param>
        /// <returns></returns>
        public ActionResult AddSignOut(string Id, string Amount)
        {
            OrderManageBLL orderManageBLL = new OrderManageBLL();
            return Json(orderManageBLL.AddSignOut(Id, Amount), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加续住
        /// </summary>
        /// <param name="Id">订单编号</param>
        /// <param name="EstimatedTime">续住时间</param>
        /// <returns></returns>
        public ActionResult AddContinue(string Id, string EstimatedTime)
        {
            OrderManageBLL orderManageBLL = new OrderManageBLL();
            return Json(orderManageBLL.AddContinue(Id, EstimatedTime), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加消耗
        /// </summary>
        /// <param name="Id">房间编号</param>
        /// <param name="name">商品名称</param>
        /// <param name="commodityNum">消耗数量</param>
        /// <returns></returns>
        public ActionResult AddConsume(string Id, string name, string commodityNum)
        {
            OrderManageBLL orderManageBLL = new OrderManageBLL();
            return Json(orderManageBLL.AddConsume(Id, name, commodityNum), JsonRequestBehavior.AllowGet);
        }       
        #endregion
    }
}