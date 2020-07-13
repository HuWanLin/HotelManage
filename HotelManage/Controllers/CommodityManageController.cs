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
    /// 商品管理 控制器类
    /// </summary>
    public class CommodityManageController : Controller
    {
        #region returnView
        /// <summary>
        /// 商品情况 页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 管理商品 页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ManageCommodity()
        {
            return View();
        }

        /// <summary>
        /// 进货情况 页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddCommodityShow()
        {
            return View();
        }

        /// <summary>
        /// 商品消耗 页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ConsumeCommodity()
        {
            return View();
        }
        #endregion

        #region returnData
        /// <summary>
        /// 根据房间ID获取所有商品
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult GetCommodityByroomId(string Id)
        {
            CommodityManageBLL commodityManageBLL = new CommodityManageBLL();
            return Json(commodityManageBLL.GetCommodityByroomId(Id), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取所有的商品总表
        /// </summary>
        /// <param name="name">商品名字</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">一页多少行</param>
        /// <returns></returns>
        public ActionResult GetAllCommodity(string name, string page, string rows)
        {
            CommodityManageBLL commodityManageBLL = new CommodityManageBLL();
            return Json(commodityManageBLL.GetAllCommodity(name, page, rows), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加商品总表
        /// </summary>
        /// <param name="name">商品名称</param>
        /// <param name="unitPrice">商品单价</param>
        /// <returns></returns>
        public ActionResult AddCommodity(string name, string unitPrice)
        {
            CommodityManageBLL commodityManageBLL = new CommodityManageBLL();
            return Json(commodityManageBLL.AddCommodity(name, unitPrice), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取所有的进货情况
        /// </summary>
        /// <param name="name">商品名称</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">一页多少行</param>
        /// <returns></returns>
        public ActionResult GetAllPurchase(string name, string page, string rows)
        {
            CommodityManageBLL commodityManageBLL = new CommodityManageBLL();
            return Json(commodityManageBLL.GetAllPurchase(name, page, rows), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取所有的商品名称
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllCommodityName()
        {
            CommodityManageBLL commodityManageBLL = new CommodityManageBLL();
            return Json(commodityManageBLL.GetAllCommodityName(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 添加进货
        /// </summary>
        /// <param name="name">商品名称</param>
        /// <param name="commodityNum">进货数量</param>
        /// <returns></returns>
        public ActionResult AddCommodityP(string namecombobox, string commodityNum)
        {
            CommodityManageBLL commodityManageBLL = new CommodityManageBLL();
            return Json(commodityManageBLL.AddCommodityP(namecombobox, commodityNum), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取所有的消耗商品记录
        /// </summary>
        /// <param name="name">商品名称</param>
        /// <param name="completeNum">商品数量</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">一页多少行</param>
        /// <returns></returns>
        public ActionResult GetAllConsumeCommodity(string name, string completeNum, string page, string rows)
        {
            CommodityManageBLL commodityManageBLL = new CommodityManageBLL();
            return Json(commodityManageBLL.GetAllConsumeCommodity(name, completeNum, page, rows), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}