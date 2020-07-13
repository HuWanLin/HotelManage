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
    /// 员工管理 数据访问类
    /// </summary>
    public class StaffManageDAL
    {
        /// <summary>
        /// 根据手机号获取帐号信息
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public Staff StaffLogin(string loginId)
        {
            using (HotelManageDBEntities db = new HotelManageDBEntities())
            {
                return db.Staff.Where(s => s.Phone == loginId).FirstOrDefault();
            }
        }

        /// <summary>
        /// 获取 Id 根据 名字
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<StaffId> GetIdByName(string name)
        {
            using (HotelManageDBEntities db = new HotelManageDBEntities())
            {
                List<StaffId> list = db.Staff.Where(k => k.Name.Contains(name)).Select(s => new StaffId { Id = s.Id }).ToList();
                return list;
            }           
        }

        /// <summary>
        /// 根据名字获取部门ID
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetDeptIdByName(string name)
        {
            using (HotelManageDBEntities db = new HotelManageDBEntities())
            {
                return db.Staff.Where(s => s.Name == name).FirstOrDefault().DeptId;
            }          
        }

        /// <summary>
        /// 获取所有员工
        /// </summary>
        /// <param name="name">员工名字</param>
        /// <param name="dept">员工部门ID</param>
        /// <param name="total">记录总数</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">一页多少条</param>
        /// <returns></returns>
        public List<StaffViewModel2> GetAllStaff(string name, int dept, int state, out int total, int page, int rows)
        {
            if (page.Equals(0))
            {
                total = 0;
                return null;
            }
            using (HotelManageDBEntities db = new HotelManageDBEntities())
            {
                var data = from s in db.Staff
                           join d in db.Staff_Dept on s.DeptId equals d.Id
                           join ss in db.Staff_State on s.Staff_StateId equals ss.Id
                           where s.IsDelete == false
                           select new
                           {
                               s.Id,
                               s.Name,
                               s.DeptId,
                               d.DeptTxt,
                               s.Sex,
                               s.Age,
                               s.Nation,
                               s.Phone,
                               s.Email,
                               s.EstablishDate,
                               ss.StateTxt,
                               s.Staff_StateId
                           };
                if (!string.IsNullOrWhiteSpace(name))
                {
                    data = data.Where(s => s.Name.Contains(name));
                }
                if (dept != 0)
                {
                    data = data.Where(s => s.DeptId == dept);
                }
                if (state != 0)
                {
                    data = data.Where(s => s.Staff_StateId == state);
                }
                total = data.Count();
                data = data.OrderBy(s => s.Id).Skip((page - 1) * rows).Take(rows);
                List<StaffViewModel2> list = new List<StaffViewModel2>();
                foreach (var item in data)
                {
                    StaffViewModel2 staffViewModel2 = new StaffViewModel2();
                    staffViewModel2.Id = item.Id;
                    staffViewModel2.Name = item.Name;
                    staffViewModel2.DeptTxt = item.DeptTxt;
                    staffViewModel2.Sex = item.Sex ? "男" : "女";
                    staffViewModel2.Age = item.Age.ToString();
                    staffViewModel2.Nation = item.Nation;
                    staffViewModel2.Phone = item.Phone;
                    staffViewModel2.Email = item.Email;
                    staffViewModel2.EstablishDate = item.EstablishDate.ToString("D");
                    staffViewModel2.StateTxt = item.StateTxt;
                    list.Add(staffViewModel2);
                };
                return list;
            }        
        }

        /// <summary>
        /// 添加员工
        /// </summary>
        /// <param name="staff">员工信息对象</param>
        /// <returns></returns>
        public bool AddStaff(Staff staff)
        {
            try
            {
                using (HotelManageDBEntities db = new HotelManageDBEntities())
                {
                    db.Staff.Add(staff);
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
        /// 修改员工信息
        /// </summary>
        /// <param name="id">员工编号</param>
        /// <param name="staff">修改后的员工信息</param>
        /// <returns></returns>
        public bool EditStaff(int id, Staff staff)
        {
            try
            {
                using (HotelManageDBEntities db = new HotelManageDBEntities())
                {
                    Staff st = db.Staff.Where(s => s.Id == id).FirstOrDefault();
                    st.Name = staff.Name;
                    st.Sex = staff.Sex;
                    st.Age = staff.Age;
                    st.Nation = staff.Nation;
                    st.Phone = staff.Phone;
                    st.Email = staff.Email;
                    st.DeptId = staff.DeptId;
                    st.Staff_StateId = staff.Staff_StateId;
                    db.Entry(st).State = EntityState.Modified;
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
        /// 判断手机号是还使用过
        /// </summary>
        /// <param name="phone">要使用的手机号</param>
        /// <returns></returns>
        public bool IsNullStaffByPhone(string phone)
        {
            using (HotelManageDBEntities db = new HotelManageDBEntities())
            {
                var data = db.Staff.Where(s => s.Phone == phone);
                if (data.Count() != 0)
                {
                    return true;
                }
            }             
            return false;
        }

        /// <summary>
        /// 删除员工
        /// </summary>
        /// <param name="id">员工编号</param>
        /// <returns></returns>
        public bool DestroyStaff(int id)
        {
            try
            {
                using (HotelManageDBEntities db = new HotelManageDBEntities())
                {
                    Staff staff = db.Staff.Where(s => s.Id == id).FirstOrDefault();
                    staff.IsDelete = true;
                    staff.DeleteDate = DateTime.Now;
                    db.Entry(staff).State = EntityState.Modified;
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
        /// 获取所有员工状态文本
        /// </summary>
        /// <param name="flag">是否加入 全部状态选择</param>
        /// <returns></returns>
        public List<Staff_DeptViewModel> GetALLStaffTxt(bool flag)
        {
            using (HotelManageDBEntities db = new HotelManageDBEntities())
            {
                var data = from s in db.Staff_State
                           where s.IsDelete == false
                           select new
                           {
                               s.Id,
                               s.StateTxt
                           };
                List<Staff_DeptViewModel> list = new List<Staff_DeptViewModel>();
                if (flag)
                {
                    Staff_DeptViewModel s = new Staff_DeptViewModel();
                    s.Id = 0;
                    s.StateTxt = "全部状态";
                    list.Add(s);
                }
                foreach (var item in data)
                {
                    Staff_DeptViewModel s = new Staff_DeptViewModel();
                    s.Id = item.Id;
                    s.StateTxt = item.StateTxt;

                    list.Add(s);
                }
                return list;
            }           
        }

        /// <summary>
        /// 根据状态文本获取状态编号
        /// </summary>
        /// <param name="stateTxt"></param>
        /// <returns></returns>
        public int GetStateIdByStateTxt(string stateTxt)
        {
            try
            {
                using (HotelManageDBEntities db = new HotelManageDBEntities())
                {
                    return db.Staff_State.Where(s => s.StateTxt.Contains(stateTxt)).FirstOrDefault().Id;
                }               
            }
            catch (Exception)
            {
                //   throw;
            }
            return 1;
        }

        /// <summary>
        /// 根据部门编号获取身份编号
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        public int GetDdentityIdByDeptID(int deptId)
        {
            int id = 0;
            switch (deptId)
            {
                case 1:
                    id = 4;
                    break;
                case 2:
                    id = 5;
                    break;
                case 3:
                    id = 7;
                    break;
                case 4:
                    id = 2;
                    break;
                case 5:
                    id = 2;
                    break;
                case 6:
                    id = 2;
                    break;
                case 7:
                    id = 3;
                    break;
                case 8:
                    id = 2;
                    break;
                case 9:
                    id = 2;
                    break;
                default:
                    break;
            }
            return id;
        }
    }
}
