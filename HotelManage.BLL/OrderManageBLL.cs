using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HotelManage.Models;
using HotelManage.DAL;
namespace HotelManage.BLL
{
    /// <summary>
    /// 订单管理 逻辑处理类
    /// </summary>
    public class OrderManageBLL
    {
        /// <summary>
        /// 增加订单
        /// </summary>
        /// <param name="Id">房间的ID</param>
        /// <param name="name">客户名字</param>
        /// <param name="iDCard">客户身份证</param>
        /// <param name="Sex">客户性别</param>
        /// <param name="phone">客户手机</param>
        /// <param name="deposit">押金</param>
        /// <returns></returns>
        public NewsModel AddOrder(string Id, string name, string iDCard, string Sex, string phone, string deposit, string estimatedTime)
        {
            OrderManageDAL orderManageDAL = new OrderManageDAL();
            Order order = new Order();
            order.EstablishDate = DateTime.Now;
            order.IsDelete = false;
            order.RoomId = Convert.ToInt32(Id);
            order.purchaseDate = DateTime.Now;
            order.Deposit = Convert.ToDecimal(deposit);
            order.Name = name;
            order.IDCard = iDCard;
            order.Sex = Sex.Equals("0") ? true : false;
            order.Phone = phone;
            order.DeleteDate = DateTime.Now;
            order.EstimatedTime = Convert.ToDateTime(estimatedTime);
            order.LiveDays = (order.EstimatedTime - order.EstablishDate).Days + 1;
            order.ActualMoney = orderManageDAL.GetActualMoneyByRommId(order.RoomId) * (order.LiveDays ?? 0);

            if (orderManageDAL.AddOrder(order))
            {
                return new NewsModel(true, "增加成功");
            }
            return new NewsModel(false, "失败");
        }

        /// <summary>
        /// 获取所有订单
        /// </summary>
        /// <param name="completeNum">房间编号</param>
        /// <param name="name">客户名字</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">一页多少条</param>
        /// <param name="flag">1 在用    其他 历史</param>
        /// <returns></returns>
        public Dictionary<String, Object> GetAllRoom(string completeNum, string name, string page, string rows, bool flag)
        {
            OrderManageDAL orderManageDAL = new OrderManageDAL();
            int total = 0;   //总记录数
            List<OrderViewModel> list = orderManageDAL.GetAllRoom(completeNum, name, out total, Convert.ToInt32(page ?? "0"), Convert.ToInt32(rows ?? "0"), flag);
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
        /// 添加退房记录
        /// </summary>
        /// <param name="Id">订单编号</param>
        /// <param name="Amount">退还金额</param>
        /// <returns></returns>
        public NewsModel AddSignOut(string Id, string Amount)
        {
            OrderManageDAL orderManageDAL = new OrderManageDAL();
            if (orderManageDAL.AddSignOut(Convert.ToInt32(Id), Convert.ToDecimal(Amount)))
            {
                return new NewsModel(true, "退房成功");
            }
            return new NewsModel(false, "退房失败");
        }

        /// <summary>
        /// 续住办理
        /// </summary>
        /// <param name="Id">订单编号</param>
        /// <param name="time">续住到的时间</param>
        /// <returns></returns>
        public NewsModel AddContinue(string Id, string time)
        {
            OrderManageDAL orderManageDAL = new OrderManageDAL();
            if (orderManageDAL.AddContinue(Convert.ToInt32(Id), Convert.ToDateTime(time)))
            {
                return new NewsModel(true, "续住办理成功");
            }
            return new NewsModel(false, "续住办理失败");
        }

        /// <summary>
        /// 添加消耗
        /// </summary>
        /// <param name="Id">房间编号</param>
        /// <param name="name">商品名称</param>
        /// <param name="commodityNum">消耗数量</param>
        /// <returns></returns>
        public NewsModel AddConsume(string Id, string name, string commodityNum)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return new NewsModel(false, "请选择商品名称!");
            }
            Commodity_Consume cc = new Commodity_Consume();
            RoomManageDAL romm = new RoomManageDAL();
            cc.EstablishDate = DateTime.Now;
            cc.IsDelete = false;
            cc.OrderId = Convert.ToInt32(Id);
            int roomId = new OrderManageDAL().GetRoomIdByOrderId(cc.OrderId ?? 0);
            Room_Commodity rc = romm.GetRoomCommodityIdByRoomIdAndCommodityId(roomId, Convert.ToInt32(name));
            cc.CommodityNum = Convert.ToInt32(commodityNum);
            cc.CommodityId = Convert.ToInt32(name);
            if (rc.CommodityNum >= Convert.ToInt32(commodityNum))
            {
                OrderManageDAL orderManageDAL = new OrderManageDAL();
                if (orderManageDAL.AddConsume(cc))
                {
                    if (romm.ReduceCommodity(rc.Id, rc.CommodityNum - Convert.ToInt32(commodityNum) ?? 0))
                    {
                        if (new CommodityManageDAL().ReduceCommodity(Convert.ToInt32(name), Convert.ToInt32(commodityNum)))
                        {
                            return new NewsModel(true, "增加成功");
                        }
                    }
                }
            }
            return new NewsModel(false, "增加失败,可分配不足或者其它");
        }
    }
}
