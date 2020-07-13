using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelManage.Models;

namespace HotelManage.DAL
{
    /// <summary>
    /// 部门管理 数据访问类
    /// </summary>
    public class DeptManageDAL
    {
        /// <summary>
        /// 获取所有部门
        /// </summary>
        /// <param name="flag">是否增加全部这个选择</param>
        /// <returns></returns>
        public List<DeptViewModel> GetAllDept(bool flag)
        {
            using (HotelManageDBEntities db = new HotelManageDBEntities())
            {
                var data = db.Staff_Dept.Select(s => s).ToList();
                List<DeptViewModel> list = new List<DeptViewModel>();
                if (flag)
                {
                    DeptViewModel d = new DeptViewModel();
                    d.Id = 0;
                    d.DeptTxt = "全部部门";
                    list.Add(d);
                }
                foreach (var item in data)
                {
                    DeptViewModel d = new DeptViewModel();
                    d.Id = item.Id;
                    d.DeptTxt = item.DeptTxt;
                    list.Add(d);
                }
                return list;
            }                  
        }

        /// <summary>
        /// 根据部门文本获取部门编号
        /// </summary>
        /// <param name="deptName">部门文本</param>
        /// <returns></returns>
        public int GetDeptIdByDeptName(string deptName)
        {
            try
            {
                using (HotelManageDBEntities db = new HotelManageDBEntities())
                {
                    return db.Staff_Dept.Where(s => s.DeptTxt.Contains(deptName)).FirstOrDefault().Id;
                }                            
            }
            catch (Exception)
            {
                //   throw;
            }
            return 1;
        }        
    }
}
