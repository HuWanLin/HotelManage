using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HotelManage.DAL;
using HotelManage.Models;

namespace HotelManage.BLL
{
    /// <summary>
    /// 商品管理 逻辑处理类
    /// </summary>
    public class CommodityManageBLL
    {
        /// <summary>
        /// 获取所有商品根据房间的ID
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public List<CommodityViewModel> GetCommodityByroomId(string roomId)
        {
            CommodityManageDAL commodityManageDAL = new CommodityManageDAL();
            if (roomId.Contains("F"))
            {
                roomId = roomId.Replace('F', ' ').Trim();
                int id = new OrderManageDAL().GetRoomIdByOrderId(Convert.ToInt32(roomId));
                return commodityManageDAL.GetCommodityByroomId(Convert.ToInt32(id));
            }

            return commodityManageDAL.GetCommodityByroomId(Convert.ToInt32(roomId));
        }

        /// <summary>
        /// 获取所有商品
        /// </summary>
        /// <param name="name">商品名称</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">一页多少行</param>
        /// <returns></returns>
        public Dictionary<String, Object> GetAllCommodity(string name, string page, string rows)
        {
            CommodityManageDAL commodityManageDAL = new CommodityManageDAL();
            int total = 0;   //总记录数           
            List<CommodityViewModel> list = commodityManageDAL.GetAllCommodity(name, out total, Convert.ToInt32(page ?? "0"), Convert.ToInt32(rows ?? "0"));
            Dictionary<String, Object> map = new Dictionary<String, Object>();
            if (total != 0 && !rows.Equals(null))
            {
                map.Add("total", total);
                map.Add("pages", total / Convert.ToInt32(rows));
                map.Add("rows", list);
            }
            else
            {
                map.Add("total", 0);
                map.Add("pages", 1);
                map.Add("rows", new Regulations());
            }
            return map;
        }

        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="name">商品名称</param>
        /// <param name="unitPrice">商品单价</param>
        /// <returns></returns>
        public NewsModel AddCommodity(string name, string unitPrice)
        {
            CommodityManageDAL commodityManageDAL = new CommodityManageDAL();
            Commodity commodity = new Commodity();
            commodity.EstablishDate = DateTime.Now;
            commodity.IsDelete = false;
            commodity.Name = name;
            commodity.UnitPrice = Convert.ToDecimal(unitPrice);
            commodity.Stock = 0;
            commodity.SeparableNum = 0;
            if (commodityManageDAL.AddCommodity(commodity))
            {
                return new NewsModel(true, "增加成功");
            }
            return new NewsModel(false, "增失败");
        }

        /// <summary>
        /// 获取所有进货表
        /// </summary>
        /// <param name="name">商品名称</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">一行多少行</param>
        /// <returns></returns>
        public Dictionary<String, Object> GetAllPurchase(string name, string page, string rows)
        {
            CommodityManageDAL commodityManageDAL = new CommodityManageDAL();
            int total = 0;   //总记录数           
            List<CommodityViewModel> list = commodityManageDAL.GetAllPurchase(name, out total, Convert.ToInt32(page ?? "0"), Convert.ToInt32(rows ?? "0"));
            Dictionary<String, Object> map = new Dictionary<String, Object>();
            if (total != 0 && !rows.Equals(null))
            {
                map.Add("total", total);
                map.Add("pages", total / Convert.ToInt32(rows));
                map.Add("rows", list);
            }
            else
            {
                map.Add("total", 0);
                map.Add("pages", 1);
                map.Add("rows", new Regulations());
            }
            return map;
        }

        /// <summary>
        /// 获取所有商品名称
        /// </summary>
        /// <returns></returns>
        public List<SexViewModel> GetAllCommodityName()
        {
            CommodityManageDAL commodityManageDAL = new CommodityManageDAL();
            return commodityManageDAL.GetAllCommodityName();
        }

        /// <summary>
        /// 添加进货
        /// </summary>
        /// <param name="name">商品编号</param>
        /// <param name="commodityNum">添加数量</param>
        /// <returns></returns>
        public NewsModel AddCommodityP(string name, string commodityNum)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return new NewsModel(false, "添加失败");
            }
            Commodity_Purchase cp = new Commodity_Purchase();
            cp.EstablishDate = DateTime.Now;
            cp.IsDelete = false;
            cp.CommodityId = Convert.ToInt32(name);
            cp.CommodityNum = Convert.ToInt32(commodityNum);

            CommodityManageDAL commodityManageDAL = new CommodityManageDAL();
            if (commodityManageDAL.AddCommodityP(cp))
            {
                return new NewsModel(true, "添加成功");
            }
            return new NewsModel(false, "添加失败");
        }

        /// <summary>
        /// 获取所有消耗
        /// </summary>
        /// <param name="name">商品编号</param>
        /// <param name="completeNum">消耗数量</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">一页多少行</param>
        /// <returns></returns>
        public Dictionary<String, Object> GetAllConsumeCommodity(string name, string completeNum, string page, string rows)
        {
            CommodityManageDAL commodityManageDAL = new CommodityManageDAL();
            int total = 0;   //总记录数           
            List<Commodity_ConsumeViewModel> list = commodityManageDAL.GetAllConsumeCommodity(name, completeNum, out total, Convert.ToInt32(page ?? "0"), Convert.ToInt32(rows ?? "0"));
            Dictionary<String, Object> map = new Dictionary<String, Object>();
            if (total != 0 && !rows.Equals(null))
            {
                map.Add("total", total);
                map.Add("pages", total / Convert.ToInt32(rows));
                map.Add("rows", list);
            }
            else
            {
                map.Add("total", 0);
                map.Add("pages", 1);
                map.Add("rows", new Regulations());
            }
            return map;
        }
    }
}
