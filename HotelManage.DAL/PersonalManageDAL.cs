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
    /// 个人管理 数据访问类
    /// </summary>
    public class PersonalManageDAL
    {
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="pwdx">要修改的密码</param>
        /// <param name="name">当前用户</param>
        /// <returns></returns>
        public bool EditUserPwd(string pwdx, string name)
        {
            try
            {
                using (HotelManageDBEntities db = new HotelManageDBEntities())
                {
                    var data = db.Staff.Where(s => s.Name == name).FirstOrDefault();
                    data.Password = pwdx;
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
        /// 获取用户密码 根据当前用户名字
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetPwdByName(string name)
        {
            try
            {
                using (HotelManageDBEntities db = new HotelManageDBEntities())
                {
                    var data = db.Staff.Where(s => s.Name == name).FirstOrDefault();
                    return data.Password;
                }                          
            }
            catch (Exception)
            {
                //throw;
            }
            return null;
        }

        /// <summary>
        /// 获取用户信息根据当前用户信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public StaffViewModel GetUserInByName(string name)
        {
            try
            {
                using (HotelManageDBEntities db = new HotelManageDBEntities())
                {
                    var data = db.Staff.Where(s => s.Name == name).FirstOrDefault();
                    StaffViewModel st = new StaffViewModel();
                    st.Email = data.Email;
                    st.Nation = data.Nation;
                    st.Sex = data.Sex ? "男" : "女";

                    return st;
                }                             
            }
            catch (Exception)
            {
                //throw;
            }
            return null;
        }

        /// <summary>
        /// 修改用户的信息
        /// </summary>
        /// <param name="nation">民族</param>
        /// <param name="email">邮箱</param>
        /// <param name="sex">性别</param>
        /// <param name="name">当前用户名字</param>
        /// <returns></returns>
        public bool EditUserIn(string nation, string email, bool sex, string name)
        {
            try
            {
                using (HotelManageDBEntities db = new HotelManageDBEntities())
                {
                    Staff st = db.Staff.Where(s => s.Name == name).FirstOrDefault();
                    st.Nation = nation;
                    st.Email = email;
                    st.Sex = sex;
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
    }
}
