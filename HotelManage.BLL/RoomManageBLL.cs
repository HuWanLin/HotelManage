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
    /// 房间管理 逻辑处理类
    /// </summary>
    public class RoomManageBLL
    {
        /// <summary>
        /// 获取所有的房间信息
        /// </summary>
        /// <param name="floorNum">楼层</param>
        /// <param name="completeNum">编号</param>
        /// <param name="typeId">房间类型</param>
        /// <param name="stateId">房间状态</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">一页多少条</param>
        /// <returns></returns>
        public Dictionary<String, Object> GetAllRoom(string floorNum, string completeNum, string typeId, string stateId, string page, string rows)
        {
            RoomManageDAL roomManageDAL = new RoomManageDAL();
            int total = 0;   //总记录数           
            List<RoomViewModel> list = roomManageDAL.GetAllRoom(floorNum, completeNum, string.IsNullOrWhiteSpace(typeId) ? 0 : Convert.ToInt32(typeId), string.IsNullOrWhiteSpace(stateId) ? 0 : Convert.ToInt32(stateId), out total, Convert.ToInt32(page ?? "0"), Convert.ToInt32(rows ?? "0"));
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
        /// 获取所有房间历史状态
        /// </summary>
        /// <param name="completeNum">房间编号</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">一页多少行</param>
        /// <returns></returns>
        public Dictionary<String, Object> GetAllHistoryState(string completeNum, string page, string rows)
        {
            RoomManageDAL roomManageDAL = new RoomManageDAL();
            int total = 0;   //总记录数   

            List<HistoryStateViewModel> list = roomManageDAL.GetAllHistoryState(completeNum, out total, Convert.ToInt32(page ?? "0"), Convert.ToInt32(rows ?? "0"));
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
        /// 获取所有历史金额
        /// </summary>
        /// <param name="completeNum">房间编号</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">一页多少行</param>
        /// <returns></returns>
        public Dictionary<String, Object> GetAllHistoryAmount(string completeNum, string page, string rows)
        {
            RoomManageDAL roomManageDAL = new RoomManageDAL();
            int total = 0;   //总记录数   

            List<HistoryAmountViewModel> list = roomManageDAL.GetAllHistoryAmount(completeNum, out total, Convert.ToInt32(page ?? "0"), Convert.ToInt32(rows ?? "0"));
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
        /// 获取所有历史折扣
        /// </summary>
        /// <param name="completeNum">房间编号</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">一页多少行</param>
        /// <returns></returns>
        public Dictionary<String, Object> GetAllHistoryDiscount(string completeNum, string page, string rows)
        {
            RoomManageDAL roomManageDAL = new RoomManageDAL();
            int total = 0;   //总记录数   

            List<HistoryDiscountViewModel> list = roomManageDAL.GetAllHistoryDiscount(completeNum, out total, Convert.ToInt32(page ?? "0"), Convert.ToInt32(rows ?? "0"));
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
        /// 获取所有房间类型
        /// </summary>
        /// <returns></returns>
        public List<SexViewModel> GetTypeTxt()
        {
            return new RoomManageDAL().GetTypeTxt();
        }

        /// <summary>
        /// 获取所有房间状态
        /// </summary>
        /// <returns></returns>
        public List<SexViewModel> GetStateTxt()
        {
            return new RoomManageDAL().GetStateTxt();
        }

        /// <summary>
        /// 修改房间状态
        /// </summary>
        /// <param name="id">房间编号</param>
        /// <param name="dentityId">状态编号</param>
        /// <returns></returns>
        public NewsModel ModifyState(int id, string dentityId)
        {
            RoomManageDAL roomManageDAL = new RoomManageDAL();
            if (roomManageDAL.ModifyState(id, Convert.ToInt32(dentityId)))
            {
                return new NewsModel(true, "修改成功");
            }
            return new NewsModel(false, "请等待其它部门完成工作，或修改失败");
        }

        /// <summary>
        /// 修改折扣
        /// </summary>
        /// <param name="Id">房间编号</param>
        /// <param name="DiscountNum">折扣数</param>
        /// <returns></returns>
        public NewsModel EditDiscount(string Id, string DiscountNum)
        {
            RoomManageDAL roomManageDAL = new RoomManageDAL();
            if (roomManageDAL.EditDiscount(Convert.ToInt32(Id), Convert.ToInt32(DiscountNum)))
            {
                return new NewsModel(true, "修改成功");
            }
            return new NewsModel(false, "修改失败");
        }

        /// <summary>
        /// 修改金额
        /// </summary>
        /// <param name="Id">房间编号</param>
        /// <param name="UnitPrice">金额</param>
        /// <returns></returns>
        public NewsModel EditAmount(string Id, string UnitPrice)
        {
            RoomManageDAL roomManageDAL = new RoomManageDAL();
            if (roomManageDAL.EditAmount(Convert.ToInt32(Id), Convert.ToDecimal(UnitPrice)))
            {
                return new NewsModel(true, "修改成功");
            }
            return new NewsModel(false, "修改失败");
        }

        /// <summary>
        /// 增加分配
        /// </summary>
        /// <param name="Id">房间Id</param>
        /// <param name="name">要增加的商品ID</param>
        /// <param name="commodityNum">增加数量</param>
        /// <returns></returns>
        public NewsModel IncreaseCommodityC(string Id, string name, string commodityNum)
        {
            int num = new CommodityManageDAL().GetNumById(Convert.ToInt32(name));  //可分配数
            if (num == -1 || num < Convert.ToInt32(commodityNum))
            {
                return new NewsModel(false, "此商品可分配数不足");
            }
            RoomManageDAL roomManageDAL = new RoomManageDAL();
            if (roomManageDAL.IsNoNullRoomCommodity(Convert.ToInt32(Id), Convert.ToInt32(name)))  //有记录
            {
                if (roomManageDAL.IncreaseCommodityC(Convert.ToInt32(Id), Convert.ToInt32(name), Convert.ToInt32(commodityNum)))
                {
                    return new NewsModel(true, "分配成功");
                }
            }
            else  //无记录
            {
                Room_Commodity rc = new Room_Commodity();
                rc.EstablishDate = DateTime.Now;
                rc.IsDelete = false;
                rc.RoomId = Convert.ToInt32(Id);
                rc.CommodityId = Convert.ToInt32(name);
                rc.CommodityNum = Convert.ToInt32(commodityNum);
                if (roomManageDAL.IncreaseCommodityCW(rc))
                {
                    return new NewsModel(true, "分配成功");
                }
            }
            return new NewsModel(true, "分配失败");
        }
    }
}
