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
    /// 通知管理 数据访问类
    /// </summary>
    public class RegulationsManageDAL
    {
        /// <summary>
        /// 获取通知数据
        /// </summary>
        /// <param name="name">发布人</param>
        /// <param name="title">标题</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">行</param>
        /// <param name="total">总数</param>
        /// <param name="deptId">部门ID</param>
        /// <returns></returns>
        public List<RegulationsViewModel> GetAllRegulations(string name, string title, int page, int rows, out int total, int deptId)
        {
            if (page.Equals(0))
            {
                total = 0;
                return null;
            }
            using (HotelManageDBEntities db = new HotelManageDBEntities())
            {
                var data = db.Regulations.Where(s => s.DeptIdList.Contains(deptId.ToString()) && s.IsDelete == false);
                if (!string.IsNullOrWhiteSpace(name))
                {
                    data = data.Where(s => s.StaffName.Contains(name));
                }
                if (!string.IsNullOrWhiteSpace(title))
                {
                    data = data.Where(s => s.Title.Contains(title));
                }

                total = data.Count();
                data = data.OrderByDescending(s => s.EstablishDate).Skip((page - 1) * rows).Take(rows);
                List<RegulationsViewModel> listV = new List<RegulationsViewModel>();
                foreach (Regulations item in data.ToList())
                {
                    RegulationsViewModel r = new RegulationsViewModel();
                    r.Id = item.Id;
                    r.StaffName = item.StaffName;
                    r.Title = item.Title;
                    r.RegulationsContent = item.RegulationsContent.Count() > 5 ? item.RegulationsContent.Substring(0, 5) + "......" : item.RegulationsContent;
                    r.EstablishDate = item.EstablishDate.ToString("D");
                    listV.Add(r);
                }
                return listV;
            }
        }

        /// <summary>
        /// 添加通知 
        /// </summary>
        /// <param name="regulations">要添加的所有数据</param>
        /// <returns></returns>
        public bool AddRegulations(Regulations regulations)
        {
            try
            {
                using (HotelManageDBEntities db = new HotelManageDBEntities())
                {
                    db.Regulations.Add(regulations);
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
        /// 删除通知
        /// </summary>
        /// <param name="id">通知编号</param>
        /// <returns></returns>
        public bool DestroyRegulations(int id)
        {
            try
            {
                using (HotelManageDBEntities db = new HotelManageDBEntities())
                {
                    Regulations date = db.Regulations.Where(s => s.Id == id).FirstOrDefault();
                    date.IsDelete = true;
                    date.DeleteDate = DateTime.Now;
                    db.Entry(date).State = EntityState.Modified;
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
        /// 根据通知编号获取通知对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RegulationsViewModel GetRegulationsById(int id)
        {
            using (HotelManageDBEntities db = new HotelManageDBEntities())
            {
                var data = db.Regulations.Where(s => s.Id == id);
                RegulationsViewModel r = new RegulationsViewModel();
                foreach (var item in data)
                {
                    r.Title = item.Title;
                    r.RegulationsContent = item.RegulationsContent;
                    r.StaffName = item.StaffName;
                    r.EstablishDate = item.EstablishDate.ToString("F");
                }
                return r;
            }
        }
    }
}
