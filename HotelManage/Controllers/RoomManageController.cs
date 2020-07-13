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
    /// 房间管理 控制器
    /// </summary>
    public class RoomManageController : Controller
    {
        #region returnView
        /// <summary>
        /// 查看房间 页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 房间管理 页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ManageRoom()
        {
            return View();
        }

        /// <summary>
        /// 房间历史状态 页面
        /// </summary>
        /// <returns></returns>
        public ActionResult HistoryState()
        {
            return View();
        }

        /// <summary>
        /// 房间历史金额 页面
        /// </summary>
        /// <returns></returns>
        public ActionResult HistoryAmount()
        {
            return View();
        }

        /// <summary>
        /// 房间历史折扣 页面
        /// </summary>
        /// <returns></returns>
        public ActionResult HistoryDiscount()
        {
            return View();
        }
        #endregion

        #region returnData       
        /// <summary>
        /// 获取所有的房间
        /// </summary>
        /// <param name="floorNum">楼层</param>
        /// <param name="completeNum">完整编号</param>
        /// <param name="typeTxt">类型</param>
        /// <param name="stateTxt">状态</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">一页多少条</param>
        /// <returns></returns>
        public JsonResult GetAllRoom(string floorNum, string completeNum, string typeTxt, string stateTxt, string page, string rows)
        {
            RoomManageBLL roomManageBLL = new RoomManageBLL();
            return Json(roomManageBLL.GetAllRoom(floorNum, completeNum, typeTxt, stateTxt, page, rows), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取所有的楼层
        /// </summary>
        /// <returns></returns>
        public JsonResult GetFloorNum()
        {
            List<SexViewModel> listStaffId = new List<SexViewModel>();
            string[] list = { "全部楼层", "一 楼", "二 楼", "三 楼", "四 楼", "五 楼" };
            for (int i = 0; i < 6; i++)
            {
                SexViewModel sexViewModel = new SexViewModel();
                sexViewModel.Id = i;
                sexViewModel.Sex = list[i];
                listStaffId.Add(sexViewModel);
            }
            return Json(listStaffId, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取所有的房间类型
        /// </summary>
        /// <returns></returns>
        public JsonResult GetTypeTxt()
        {
            return Json(new RoomManageBLL().GetTypeTxt(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取所有的房间状态
        /// </summary>
        /// <returns></returns>
        public JsonResult GetStateTxt()
        {
            return Json(new RoomManageBLL().GetStateTxt(), JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 获取所有的历史状态
        /// </summary>
        /// <param name="completeNum"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        public JsonResult GetAllHistoryState(string completeNum, string page, string rows)
        {
            RoomManageBLL roomManageBLL = new RoomManageBLL();
            return Json(roomManageBLL.GetAllHistoryState(completeNum, page, rows), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取所有的历史金额
        /// </summary>
        /// <param name="completeNum">房间编号</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">一页多少行</param>
        /// <returns></returns>
        public JsonResult GetAllHistoryAmount(string completeNum, string page, string rows)
        {
            RoomManageBLL roomManageBLL = new RoomManageBLL();
            return Json(roomManageBLL.GetAllHistoryAmount(completeNum, page, rows), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取所有历史折扣
        /// </summary>
        /// <param name="completeNum">房间编号</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">一页多少行</param>
        /// <returns></returns>
        public JsonResult GetAllHistoryDiscount(string completeNum, string page, string rows)
        {
            RoomManageBLL roomManageBLL = new RoomManageBLL();
            return Json(roomManageBLL.GetAllHistoryDiscount(completeNum, page, rows), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改房间状态
        /// </summary>
        /// <param name="id">房间编号</param>
        /// <returns></returns>
        public JsonResult ModifyState(string id)
        {
            RoomManageBLL roomManageBLL = new RoomManageBLL();
            return Json(roomManageBLL.ModifyState(Convert.ToInt32(id ?? "0"), Session["DdentityId"].ToString()), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改折扣
        /// </summary>
        /// <param name="Id">房间编号</param>
        /// <param name="DiscountNum">折扣数</param>
        /// <returns></returns>
        public JsonResult EditDiscount(string Id, string DiscountNum)
        {
            RoomManageBLL roomManageBLL = new RoomManageBLL();
            return Json(roomManageBLL.EditDiscount(Id, DiscountNum), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改金额
        /// </summary>
        /// <param name="Id">房间编号</param>
        /// <param name="UnitPrice">修改金额</param>
        /// <returns></returns>
        public JsonResult EditAmount(string Id, string UnitPrice)
        {
            RoomManageBLL roomManageBLL = new RoomManageBLL();
            return Json(roomManageBLL.EditAmount(Id, UnitPrice), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 增加消耗
        /// </summary>
        /// <param name="Id">房间编号</param>
        /// <param name="name">商品名称</param>
        /// <param name="commodityNum">消耗数量</param>
        /// <returns></returns>
        public ActionResult IncreaseCommodityC(string Id, string namecombobox, string commodityNum)
        {
            RoomManageBLL roomManageBLL = new RoomManageBLL();
            return Json(roomManageBLL.IncreaseCommodityC(Id, namecombobox, commodityNum), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}