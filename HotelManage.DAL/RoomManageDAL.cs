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
    /// 房间管理 数据访问类
    /// </summary>
    public class RoomManageDAL
    {
        /// <summary>
        /// 获取所有房间
        /// </summary>
        /// <param name="floorNum">楼层</param>
        /// <param name="completeNum">编号</param>
        /// <param name="typeTxt">类型</param>
        /// <param name="stateTxt">状态</param>
        /// <param name="total">记录总数</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">一页多少条</param>
        /// <returns></returns>
        public List<RoomViewModel> GetAllRoom(string floorNum, string completeNum, int typeTxt, int stateTxt, out int total, int page, int rows)
        {
            if (page.Equals(0))
            {
                total = 0;
                return null;
            }
            using (HotelManageDBEntities db = new HotelManageDBEntities())
            {
                var data = from r in db.Room
                           join rt in db.Room_Type on r.Room_TypeId equals rt.Id
                           join rs in db.Room_State on r.Room_StateId equals rs.Id
                           join rd in db.Room_Discount on r.Room_DiscountId equals rd.Id
                           join ra in db.Room_Amount on r.Room_AmountId equals ra.Id
                           where rs.IsDelete == false
                           select new
                           {
                               r.Id,
                               r.CompleteNum,
                               rs.StateTxt,
                               rd.DiscountNum,
                               ra.UnitPrice,
                               rt.RoomName,
                               r.FloorNum,
                               r.Room_TypeId,
                               r.Room_StateId
                           };

                if (!string.IsNullOrWhiteSpace(floorNum) && !floorNum.Equals("0"))
                {
                    data = data.Where(s => s.FloorNum.Contains(floorNum));
                }
                if (!string.IsNullOrWhiteSpace(completeNum))
                {
                    data = data.Where(s => s.CompleteNum.Contains(completeNum));
                }
                if (typeTxt != 0)
                {
                    data = data.Where(s => s.Room_TypeId == typeTxt);
                }
                if (stateTxt != 0)
                {
                    data = data.Where(s => s.Room_StateId == stateTxt);
                }
                total = data.Count();
                data = data.OrderBy(s => s.Room_StateId).Skip((page - 1) * rows).Take(rows);
                List<RoomViewModel> listV = new List<RoomViewModel>();
                decimal d = 0.01M;
                foreach (var item in data)
                {
                    RoomViewModel roomViewModel = new RoomViewModel();
                    roomViewModel.Id = item.Id;
                    roomViewModel.CompleteNum = item.CompleteNum;
                    roomViewModel.StateTxt = item.StateTxt;
                    roomViewModel.DiscountNum = item.DiscountNum.ToString().Substring(0, 2) + "折";
                    roomViewModel.UnitPrice = item.UnitPrice.ToString();
                    roomViewModel.TypeTxt = item.RoomName;
                    roomViewModel.ActualMoney = (item.DiscountNum * item.UnitPrice * d).ToString().Substring(0, 5);
                    listV.Add(roomViewModel);
                }
                return listV;
            }
        }

        /// <summary>
        /// 获取所有历史房间状态
        /// </summary>
        /// <param name="completeNum">房间编号</param>
        /// <param name="total">总数</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">一页多少条记录</param>
        /// <returns></returns>
        public List<HistoryStateViewModel> GetAllHistoryState(string completeNum, out int total, int page, int rows)
        {
            if (page.Equals(0))
            {
                total = 0;
                return null;
            }
            using (HotelManageDBEntities db = new HotelManageDBEntities())
            {
                var data = from rs in db.Room_State
                           join r in db.Room on rs.RoomId equals r.Id
                           where rs.RoomId != null
                           select new
                           {
                               rs.StateTxt,
                               r.CompleteNum,
                               rs.EstablishDate
                           };
                if (!string.IsNullOrWhiteSpace(completeNum))
                {
                    data = data.Where(s => s.CompleteNum.Contains(completeNum));
                }
                total = data.Count();
                data = data.OrderBy(s => s.CompleteNum).Skip((page - 1) * rows).Take(rows);
                List<HistoryStateViewModel> listV = new List<HistoryStateViewModel>();
                foreach (var item in data)
                {
                    HistoryStateViewModel h = new HistoryStateViewModel();
                    h.StateTxt = item.StateTxt;
                    h.CompleteNum = item.CompleteNum;
                    h.EstablishDate = item.EstablishDate.ToString("F");

                    listV.Add(h);
                }

                return listV;
            }
        }

        /// <summary>
        /// 获取所有房间历史金额
        /// </summary>
        /// <param name="completeNum">房间编号</param>
        /// <param name="total">总数</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">一页多少条</param>
        /// <returns></returns>
        public List<HistoryAmountViewModel> GetAllHistoryAmount(string completeNum, out int total, int page, int rows)
        {
            if (page.Equals(0))
            {
                total = 0;
                return null;
            }
            using (HotelManageDBEntities db = new HotelManageDBEntities())
            {
                var data = from ra in db.Room_Amount
                           join r in db.Room on ra.RoomId equals r.Id
                           where ra.RoomId != null
                           select new
                           {
                               ra.UnitPrice,
                               r.CompleteNum,
                               ra.EstablishDate
                           };
                if (!string.IsNullOrWhiteSpace(completeNum))
                {
                    data = data.Where(s => s.CompleteNum.Contains(completeNum));
                }
                total = data.Count();
                data = data.OrderBy(s => s.CompleteNum).Skip((page - 1) * rows).Take(rows);
                List<HistoryAmountViewModel> listV = new List<HistoryAmountViewModel>();
                foreach (var item in data)
                {
                    HistoryAmountViewModel h = new HistoryAmountViewModel();
                    h.UnitPrice = item.UnitPrice;
                    h.CompleteNum = item.CompleteNum;
                    h.EstablishDate = item.EstablishDate.ToString("F");

                    listV.Add(h);
                }

                return listV;
            }
        }

        /// <summary>
        /// 获取所有房间历史折扣
        /// </summary>
        /// <param name="completeNum">房间编号</param>
        /// <param name="total">总数</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">一页多少条</param>
        /// <returns></returns>
        public List<HistoryDiscountViewModel> GetAllHistoryDiscount(string completeNum, out int total, int page, int rows)
        {
            if (page.Equals(0))
            {
                total = 0;
                return null;
            }
            using (HotelManageDBEntities db = new HotelManageDBEntities())
            {
                var data = from rd in db.Room_Discount
                           join r in db.Room on rd.RoomId equals r.Id
                           where rd.RoomId != null
                           select new
                           {
                               rd.DiscountNum,
                               r.CompleteNum,
                               rd.EstablishDate
                           };
                if (!string.IsNullOrWhiteSpace(completeNum))
                {
                    data = data.Where(s => s.CompleteNum.Contains(completeNum));
                }
                total = data.Count();
                data = data.OrderBy(s => s.CompleteNum).Skip((page - 1) * rows).Take(rows);
                List<HistoryDiscountViewModel> listV = new List<HistoryDiscountViewModel>();
                foreach (var item in data)
                {
                    HistoryDiscountViewModel h = new HistoryDiscountViewModel();
                    h.DiscountNum = item.DiscountNum.ToString().Substring(0, 2) + "折";
                    h.CompleteNum = item.CompleteNum;
                    h.EstablishDate = item.EstablishDate.ToString("F");

                    listV.Add(h);
                }

                return listV;
            }
        }

        /// <summary>
        /// 根据房间类型文本获取房间类型编号
        /// </summary>
        /// <param name="typeName">房间类型文本</param>
        /// <returns></returns>
        public int GetTypeIdByName(string typeName)
        {
            using (HotelManageDBEntities db = new HotelManageDBEntities())
            {
                return db.Room_Type.Where(s => s.TypeTxt == typeName).FirstOrDefault().Id;
            }
        }

        /// <summary>
        /// 根据房间状态文本获取房间状态编号
        /// </summary>
        /// <param name="stateName">房间状态文本</param>
        /// <returns></returns>
        public int GetStateIdByName(string stateName)
        {
            using (HotelManageDBEntities db = new HotelManageDBEntities())
            {
                return db.Room_State.Where(s => s.StateTxt == stateName).FirstOrDefault().Id;
            }
        }

        /// <summary>
        /// 获取所有房间类型
        /// </summary>
        /// <returns></returns>
        public List<SexViewModel> GetTypeTxt()
        {
            using (HotelManageDBEntities db = new HotelManageDBEntities())
            {
                var data = from r in db.Room_Type
                           where r.RoomId == null
                           select new
                           {
                               r.Id,
                               r.RoomName
                           };
                List<SexViewModel> list = new List<SexViewModel>();
                SexViewModel sex = new SexViewModel();
                sex.Id = 0;
                sex.Sex = "全部类型";
                list.Add(sex);
                foreach (var item in data)
                {
                    SexViewModel s = new SexViewModel();
                    s.Id = item.Id;
                    s.Sex = item.RoomName;

                    list.Add(s);
                }

                return list;
            }
        }

        /// <summary>
        /// 获取所有房间状态
        /// </summary>
        /// <returns></returns>
        public List<SexViewModel> GetStateTxt()
        {
            using (HotelManageDBEntities db = new HotelManageDBEntities())
            {
                var data = from r in db.Room_State
                           where r.RoomId == null
                           select new
                           {
                               r.Id,
                               r.StateTxt
                           };
                List<SexViewModel> list = new List<SexViewModel>();
                SexViewModel sex = new SexViewModel();
                sex.Id = 0;
                sex.Sex = "全部类型";
                list.Add(sex);
                foreach (var item in data)
                {
                    SexViewModel s = new SexViewModel();
                    s.Id = item.Id;
                    s.Sex = item.StateTxt;

                    list.Add(s);
                }
                return list;
            }
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="id">房间编号</param>
        /// <param name="dentityId">身份编号</param>
        /// <returns></returns>
        public bool ModifyState(int id, int dentityId)
        {
            try
            {
                using (HotelManageDBEntities db = new HotelManageDBEntities())
                {
                    Room room = db.Room.Where(s => s.Id == id).FirstOrDefault();

                    Room_State room_State = new Room_State();
                    room_State.EstablishDate = DateTime.Now;
                    room_State.IsDelete = false;
                    room_State.RoomId = id;

                    bool flag = true;
                    if (dentityId == 5)   //清洁部门 状态 待清洁
                    {
                        if (room.Room_StateId == 7)
                        {
                            room.Room_StateId = 4;
                            room_State.StateTxt = "在清洁";
                            flag = false;
                        }
                        else if (room.Room_StateId == 4)
                        {
                            room.Room_StateId = 1;
                            room_State.StateTxt = "可使用";
                            flag = false;
                        }

                    }
                    if (dentityId == 6)  //维修部门  状态 待清洁
                    {
                        if (room.Room_StateId == 7 || room.Room_StateId == 1)
                        {
                            room.Room_StateId = 5;
                            room_State.StateTxt = "维修中";
                            flag = false;
                        }
                        else if (room.Room_StateId == 5)
                        {
                            room.Room_StateId = 7;
                            room_State.StateTxt = "待清洁";
                            flag = false;
                        }
                    }
                    if (flag)
                    {
                        return false;
                    }
                    db.Entry(room).State = EntityState.Modified;
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
        /// 修改折扣
        /// </summary>
        /// <param name="Id">房间编号</param>
        /// <param name="DiscountNum">折扣数</param>
        /// <returns></returns>
        public bool EditDiscount(int Id, int DiscountNum)
        {
            try
            {
                using (HotelManageDBEntities db = new HotelManageDBEntities())
                {
                    Room_Discount room_Discount = new Room_Discount();
                    room_Discount.EstablishDate = DateTime.Now;
                    room_Discount.IsDelete = false;
                    room_Discount.DiscountNum = DiscountNum;
                    db.Room_Discount.Add(room_Discount);
                    db.SaveChanges();

                    Room room = db.Room.Where(s => s.Id == Id).FirstOrDefault();

                    Room_Discount rd = db.Room_Discount.Where(s => s.Id == room.Room_DiscountId).FirstOrDefault();
                    rd.IsDelete = true;
                    rd.DeleteDate = DateTime.Now;
                    rd.RoomId = Id;
                    db.Entry(rd).State = EntityState.Modified;


                    room.Room_DiscountId = room_Discount.Id;
                    db.Entry(room).State = EntityState.Modified;
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
        /// 修改房间金额
        /// </summary>
        /// <param name="Id">房间编号</param>
        /// <param name="UnitPrice">金额</param>
        /// <returns></returns>
        public bool EditAmount(int Id, decimal UnitPrice)
        {
            try
            {
                using (HotelManageDBEntities db = new HotelManageDBEntities())
                {
                    Room_Amount room_Amount = new Room_Amount();
                    room_Amount.EstablishDate = DateTime.Now;
                    room_Amount.IsDelete = false;
                    room_Amount.UnitPrice = UnitPrice;
                    db.Room_Amount.Add(room_Amount);
                    db.SaveChanges();

                    Room room = db.Room.Where(s => s.Id == Id).FirstOrDefault();

                    Room_Amount ra = db.Room_Amount.Where(s => s.Id == room.Room_AmountId).FirstOrDefault();
                    ra.IsDelete = true;
                    ra.DeleteDate = DateTime.Now;
                    ra.RoomId = room.Id;
                    db.Entry(ra).State = EntityState.Modified;


                    room.Room_AmountId = room_Amount.Id;
                    db.Entry(room).State = EntityState.Modified;

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
        /// 根据房间编号和商品编号获取房间有商品贪睡 
        /// </summary>
        /// <param name="roomId">房间编号</param>
        /// <param name="comId">商品编号</param>
        /// <returns></returns>
        public Room_Commodity GetRoomCommodityIdByRoomIdAndCommodityId(int roomId, int comId)
        {
            try
            {
                using (HotelManageDBEntities db = new HotelManageDBEntities())
                {
                    return db.Room_Commodity.Where(s => s.RoomId == roomId && s.CommodityId == comId).FirstOrDefault();
                }             
            }
            catch (Exception)
            {
                // throw;
            }
            return null;
        }

        /// <summary>
        /// 减少库存
        /// </summary>
        /// <param name="id">房间有商品表编号</param>
        /// <param name="num">减少数量</param>
        /// <returns></returns>
        public bool ReduceCommodity(int id, int num)
        {
            try
            {
                using (HotelManageDBEntities db = new HotelManageDBEntities())
                {
                    Room_Commodity rc = db.Room_Commodity.Where(s => s.Id == id).FirstOrDefault();
                    rc.CommodityNum = num;
                    db.Entry(rc).State = EntityState.Modified;
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
        /// 判断房间有商品表是还存在记录
        /// </summary>
        /// <param name="roomID"></param>
        /// <param name="commodityID"></param>
        /// <returns></returns>
        public bool IsNoNullRoomCommodity(int roomID, int commodityID)
        {
            try
            {
                using (HotelManageDBEntities db = new HotelManageDBEntities())
                {
                    var data = db.Room_Commodity.Where(s => s.RoomId == roomID && s.CommodityId == commodityID);
                    if (data.Count() != 0)
                    {
                        return true;
                    }
                }               
            }
            catch (Exception)
            {
                // throw;
            }
            return false;
        }

        /// <summary>
        /// 添加房间商品分配
        /// </summary>
        /// <param name="Id">房间编号</param>
        /// <param name="name">商品编号</param>
        /// <param name="commodityNum">添加数量</param>
        /// <returns></returns>
        public bool IncreaseCommodityC(int Id, int name, int commodityNum)
        {
            try
            {
                using (HotelManageDBEntities db = new HotelManageDBEntities())
                {
                    Room_Commodity rc = db.Room_Commodity.Where(s => s.RoomId == Id && s.CommodityId == name).FirstOrDefault();
                    rc.CommodityNum = rc.CommodityNum + commodityNum;
                    db.Entry(rc).State = EntityState.Modified;

                    Commodity c = db.Commodity.Where(s => s.Id == name).FirstOrDefault();
                    c.SeparableNum = c.SeparableNum - commodityNum;
                    db.Entry(c).State = EntityState.Modified;

                    db.SaveChanges();
                    return true;
                }                         
            }
            catch (Exception)
            {
                //   throw;
            }
            return false;
        }

        /// <summary>
        /// 添加房间有商品表
        /// </summary>
        /// <param name="rc">房间有商品表对象</param>
        /// <returns></returns>
        public bool IncreaseCommodityCW(Room_Commodity rc)
        {
            try
            {
                using (HotelManageDBEntities db = new HotelManageDBEntities())
                {
                    db.Room_Commodity.Add(rc);

                    Commodity c = db.Commodity.Where(s => s.Id == rc.CommodityId).FirstOrDefault();
                    c.SeparableNum = c.SeparableNum - rc.CommodityNum;
                    db.Entry(c).State = EntityState.Modified;

                    db.SaveChanges();
                    return true;
                }              
            }
            catch (Exception)
            {
                // throw;
            }
            return false;
        }
    }
}
