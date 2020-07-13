using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HotelManage.Models;
using System.Data.Entity;

namespace HotelManage.DAL
{
    /// <summary>
    /// 订单管理 数据访问类
    /// </summary>
    public class OrderManageDAL
    {
        /// <summary>
        /// 增加订单
        /// </summary>
        /// <param name="order">所有订单信息</param>
        /// <returns></returns>
        public bool AddOrder(Order order)
        {
            try
            {
                using (HotelManageDBEntities db = new HotelManageDBEntities())
                {
                    db.Order.Add(order);

                    Room data = db.Room.Where(s => s.Id == order.RoomId).FirstOrDefault();
                    data.Room_StateId = 3;
                    db.Entry(data).State = EntityState.Modified;

                    Room_State room_State = new Room_State();
                    room_State.EstablishDate = DateTime.Now;
                    room_State.IsDelete = false;
                    room_State.RoomId = order.RoomId;
                    room_State.StateTxt = "使用中";
                    db.Room_State.Add(room_State);

                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                //throw;
            }
            return false;
        }

        /// <summary>
        /// 获取所有订单
        /// </summary>
        /// <param name="completeNum">房间完整编号</param>
        /// <param name="name">客户名字</param>
        /// <param name="total">记录总数</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">一页多少条</param>
        ///  <param name="flag">是否是读取历史</param>
        /// <returns></returns>
        public List<OrderViewModel> GetAllRoom(string completeNum, string name, out int total, int page, int rows, bool flag)
        {
            if (page.Equals(0))
            {
                total = 0;
                return null;
            }
            using (HotelManageDBEntities db = new HotelManageDBEntities())
            {
                var data = (from o in db.Order
                            join r in db.Room on o.RoomId equals r.Id
                            where o.IsDelete == flag
                            select new
                            {
                                o.Id,
                                r.CompleteNum,
                                o.purchaseDate,
                                o.Name,
                                o.Sex,
                                o.Phone,
                                o.DeleteDate,
                                o.EstimatedTime,
                                o.LiveDays,
                                o.ActualMoney,
                                o.Deposit
                            });
                if (!string.IsNullOrWhiteSpace(completeNum))
                {
                    data = data.Where(s => s.CompleteNum.Contains(completeNum));
                }
                if (!string.IsNullOrWhiteSpace(name))
                {
                    data = data.Where(s => s.Name.Contains(name));
                }
                total = data.Count();
                data = data.OrderByDescending(s => s.purchaseDate).Skip((page - 1) * rows).Take(rows);
                List<OrderViewModel> list = new List<OrderViewModel>();
                foreach (var item in data)
                {
                    OrderViewModel orderViewModel = new OrderViewModel();
                    orderViewModel.Id = item.Id;
                    orderViewModel.CompleteNum = item.CompleteNum;
                    orderViewModel.PurchaseDate = item.purchaseDate.ToString("F");
                    orderViewModel.Name = item.Name;
                    orderViewModel.Sex = item.Sex ? "男" : "女";
                    orderViewModel.Phone = item.Phone;
                    orderViewModel.DeleteDate = item.DeleteDate.ToString("F");
                    orderViewModel.EstimatedTime = item.EstimatedTime.ToString("D");
                    orderViewModel.LiveDays = item.LiveDays.ToString();
                    orderViewModel.ActualMoney = item.ActualMoney.ToString();
                    orderViewModel.Deposit = item.Deposit.ToString();
                    list.Add(orderViewModel);
                }
                return list;
            }
        }

        /// <summary>
        /// 退房
        /// </summary>
        /// <param name="Id">房间编号</param>
        /// <param name="Amount">退还金额</param>
        /// <returns></returns>
        public bool AddSignOut(int Id, decimal Amount)
        {
            try
            {
                using (HotelManageDBEntities db = new HotelManageDBEntities())
                {
                    //写入退房表
                    CheckOut checkOut = new CheckOut();
                    checkOut.EstablishDate = DateTime.Now;
                    checkOut.IsDelete = false;
                    checkOut.OrderId = Id;
                    checkOut.CheckOutDate = DateTime.Now;
                    checkOut.refundAmount = Amount;

                    db.CheckOut.Add(checkOut);

                    //订单表标记完成
                    Order order = db.Order.Where(s => s.Id == Id).FirstOrDefault();
                    order.IsDelete = true;
                    order.DeleteDate = DateTime.Now;
                    db.Entry(order).State = EntityState.Modified;

                    //房间状态设置成待清洁
                    int? roomID = order.RoomId;
                    Room room = db.Room.Where(s => s.Id == roomID).FirstOrDefault();
                    room.Room_StateId = 7;   // 7 待清洁
                    db.Entry(room).State = EntityState.Modified;

                    Room_State room_State = new Room_State();
                    room_State.EstablishDate = DateTime.Now;
                    room_State.IsDelete = false;
                    room_State.RoomId = roomID;
                    room_State.StateTxt = "待清洁";
                    db.Room_State.Add(room_State);

                    db.SaveChanges();

                    return true;
                }               
            }
            catch (Exception)
            {
                //throw;
            }
            return false;
        }

        /// <summary>
        /// 根据房间编号获取房费
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public decimal GetActualMoneyByRommId(int roomId)
        {
            using (HotelManageDBEntities db = new HotelManageDBEntities())
            {
                var data = (from r in db.Room
                            join ra in db.Room_Amount on r.Room_AmountId equals ra.Id
                            join rd in db.Room_Discount on r.Room_DiscountId equals rd.Id
                            select new
                            {
                                ra.UnitPrice,
                                rd.DiscountNum
                            }).FirstOrDefault();

                return data.UnitPrice * data.DiscountNum * 0.01M;
            }      
        }

        /// <summary>
        /// 续住
        /// </summary>
        /// <param name="id">订单编号</param>
        /// <param name="time">续住时间</param>
        /// <returns></returns>
        public bool AddContinue(int id, DateTime time)
        {
            try
            {
                using (HotelManageDBEntities db = new HotelManageDBEntities())
                {
                    Order order = db.Order.Where(s => s.Id == id).FirstOrDefault();
                    order.EstimatedTime = time;
                    order.LiveDays = (order.EstimatedTime - order.EstablishDate).Days + 1;
                    order.ActualMoney = GetActualMoneyByRommId(order.RoomId) * (order.LiveDays ?? 0);
                    db.Entry(order).State = EntityState.Modified;
                    db.SaveChanges();

                    return true;
                }                           
            }
            catch (Exception)
            {
                //throw;
            }
            return false;

        }

        /// <summary>
        /// 根据订单编号获取房间编号
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public int GetRoomIdByOrderId(int orderId)
        {
            try
            {
                using (HotelManageDBEntities db = new HotelManageDBEntities())
                {
                    return db.Order.Where(s => s.Id == orderId).FirstOrDefault().Id;
                }                             
            }
            catch (Exception)
            {
                //throw;
            }
            return 0;
        }

        /// <summary>
        /// 添加消耗
        /// </summary>
        /// <param name="cc">消耗信息对象</param>
        /// <returns></returns>
        public bool AddConsume(Commodity_Consume cc)
        {
            try
            {
                using (HotelManageDBEntities db = new HotelManageDBEntities())
                {
                    db.Commodity_Consume.Add(cc);
                    db.SaveChanges();
                    return true;
                }                          
            }
            catch (Exception)
            {
                //throw;
            }
            return false;
        }
    }
}
