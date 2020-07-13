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
    /// 商品管理 数据访问类
    /// </summary>
    public class CommodityManageDAL
    {
        /// <summary>
        /// 获取所有商品信息根据房间的编号
        /// </summary>
        /// <param name="roomId">房间的编号</param>
        /// <returns></returns>
        public List<CommodityViewModel> GetCommodityByroomId(int roomId)
        {
            using (HotelManageDBEntities db = new HotelManageDBEntities())
            {
                var data = from r in db.Room_Commodity
                           join c in db.Commodity on r.CommodityId equals c.Id
                           where r.RoomId == roomId
                           select new CommodityViewModel
                           {
                               Id = r.Id,
                               Name = c.Name,
                               CommodityNum = r.CommodityNum ?? 0,
                               UnitPrice = c.UnitPrice ?? 0
                           };
                int a = data.Count();
                return data.ToList();
            }
        }

        /// <summary>
        /// 获取所有商品
        /// </summary>
        /// <param name="name">商品名称</param>
        /// <param name="total">总数</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">一页多少行</param>
        /// <returns></returns>
        public List<CommodityViewModel> GetAllCommodity(string name, out int total, int page, int rows)
        {
            if (page.Equals(0))
            {
                total = 0;
                return null;
            }
            using (HotelManageDBEntities db = new HotelManageDBEntities())
            {
                var data = from c in db.Commodity
                           select new
                           {
                               c.Id,
                               c.Name,
                               c.UnitPrice,
                               c.Stock,
                               c.SeparableNum
                           };
                if (!string.IsNullOrWhiteSpace(name))
                {
                    data = data.Where(s => s.Name.Contains(name));
                }
                total = data.Count();
                data = data.OrderBy(s => s.Id).Skip((page - 1) * rows).Take(rows);
                List<CommodityViewModel> listV = new List<CommodityViewModel>();
                foreach (var item in data)
                {
                    CommodityViewModel commodityViewModel = new CommodityViewModel();
                    commodityViewModel.Id = item.Id;
                    commodityViewModel.Name = item.Name;
                    commodityViewModel.UnitPrice = item.UnitPrice ?? 0;
                    commodityViewModel.CommodityNum = item.Stock ?? 0;
                    commodityViewModel.SeparableNum = item.SeparableNum.ToString();

                    listV.Add(commodityViewModel);
                }
                return listV;
            }
        }

        /// <summary>
        /// 获取所有进货记录
        /// </summary>
        /// <param name="name">商品名称</param>
        /// <param name="total">总数</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">一页多少行</param>
        /// <returns></returns>
        public List<CommodityViewModel> GetAllPurchase(string name, out int total, int page, int rows)
        {
            if (page.Equals(0))
            {
                total = 0;
                return null;
            }
            using (HotelManageDBEntities db = new HotelManageDBEntities())
            {               
                var data = from cp in db.Commodity_Purchase
                           join c in db.Commodity on cp.CommodityId equals c.Id
                           select new
                           {
                               cp.Id,
                               c.Name,
                               cp.CommodityNum,
                               cp.EstablishDate
                           };
                if (!string.IsNullOrWhiteSpace(name))
                {
                    data = data.Where(s => s.Name.Contains(name));
                }
                total = data.Count();
                data = data.OrderBy(s => s.Id).Skip((page - 1) * rows).Take(rows);
                List<CommodityViewModel> listV = new List<CommodityViewModel>();
                foreach (var item in data)
                {
                    CommodityViewModel commodityViewModel = new CommodityViewModel();
                    commodityViewModel.Id = item.Id;
                    commodityViewModel.Name = item.Name;
                    commodityViewModel.CommodityNum = item.CommodityNum ?? 0;
                    commodityViewModel.EstablishDate = item.EstablishDate.ToString("F");

                    listV.Add(commodityViewModel);
                }
                return listV;
            }            
        }

        /// <summary>
        /// 添加商品种类
        /// </summary>
        /// <param name="commodity">商品对象</param>
        /// <returns></returns>
        public bool AddCommodity(Commodity commodity)
        {
            try
            {
                using (HotelManageDBEntities db = new HotelManageDBEntities())
                {                   
                    db.Commodity.Add(commodity);
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
        /// 获取所有商品名称
        /// </summary>
        /// <returns></returns>
        public List<SexViewModel> GetAllCommodityName()
        {
            using (HotelManageDBEntities db = new HotelManageDBEntities())
            {
               
                var data = from c in db.Commodity
                           select new
                           {
                               c.Id,
                               c.Name
                           };
                List<SexViewModel> list = new List<SexViewModel>();
                foreach (var item in data)
                {
                    SexViewModel s = new SexViewModel();
                    s.Id = item.Id;
                    s.Sex = item.Name;

                    list.Add(s);
                }
                return list;
            }           
        }

        /// <summary>
        /// 添加进货
        /// </summary>
        /// <param name="cp">进货信息对象</param>
        /// <returns></returns>
        public bool AddCommodityP(Commodity_Purchase cp)
        {
            try
            {
                using (HotelManageDBEntities db = new HotelManageDBEntities())
                {                   
                    db.Commodity_Purchase.Add(cp);

                    Commodity commodity = db.Commodity.Where(s => s.Id == cp.CommodityId).FirstOrDefault();
                    commodity.Stock = commodity.Stock + cp.CommodityNum;
                    commodity.SeparableNum = commodity.SeparableNum + cp.CommodityNum;

                    db.Entry(commodity).State = EntityState.Modified;
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
        /// 获取所有消耗记录
        /// </summary>
        /// <param name="name">商品名称</param>
        /// <param name="completeNum">房间编号</param>
        /// <param name="total">总数</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">一行多少条</param>
        /// <returns></returns>
        public List<Commodity_ConsumeViewModel> GetAllConsumeCommodity(string name, string completeNum, out int total, int page, int rows)
        {
            if (page.Equals(0))
            {
                total = 0;
                return null;
            }
            using (HotelManageDBEntities db = new HotelManageDBEntities())
            {                
                var data = from cc in db.Commodity_Consume
                           join o in db.Order on cc.OrderId equals o.Id
                           join r in db.Room on o.RoomId equals r.Id
                           join c in db.Commodity on cc.CommodityId equals c.Id
                           select new
                           {
                               cc.Id,
                               r.CompleteNum,
                               c.Name,
                               cc.CommodityNum,
                               cc.EstablishDate
                           };
                if (!string.IsNullOrWhiteSpace(name))
                {
                    data = data.Where(s => s.Name.Contains(name));
                }
                if (!string.IsNullOrWhiteSpace(completeNum))
                {
                    data = data.Where(s => s.CompleteNum.Contains(completeNum));
                }
                total = data.Count();
                data = data.OrderBy(s => s.Id).Skip((page - 1) * rows).Take(rows);
                List<Commodity_ConsumeViewModel> listV = new List<Commodity_ConsumeViewModel>();
                foreach (var item in data)
                {
                    Commodity_ConsumeViewModel cc = new Commodity_ConsumeViewModel();
                    cc.Id = item.Id;
                    cc.CompleteNum = item.CompleteNum;
                    cc.Name = item.Name;
                    cc.CommodityNum = item.CommodityNum.ToString();
                    cc.EstablishDate = item.EstablishDate.ToString("F");

                    listV.Add(cc);
                }
                return listV;
            }            
        }

        /// <summary>
        /// 减少库存
        /// </summary>
        /// <param name="id">商品编号</param>
        /// <param name="num">减少数量</param>
        /// <returns></returns>
        public bool ReduceCommodity(int id, int num)
        {
            try
            {
                using (HotelManageDBEntities db = new HotelManageDBEntities())
                {                   
                    Commodity c = db.Commodity.Where(s => s.Id == id).FirstOrDefault();
                    c.Stock = c.Stock - num;
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

        /// <summary>
        /// 根据商品编号获取商品可分配数量
        /// </summary>
        /// <param name="id">商品编号</param>
        /// <returns></returns>
        public int GetNumById(int id)
        {
            try
            {
                using (HotelManageDBEntities db = new HotelManageDBEntities())
                {
                    return db.Commodity.Where(s => s.Id == id).FirstOrDefault().SeparableNum ?? -1;
                }                         
            }
            catch (Exception)
            {
                //   throw;
            }
            return -1;
        }
    }
}
