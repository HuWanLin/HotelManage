using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HotelManage.Models;
using HotelManage.DAL;
using HotelManage.Tool;

namespace HotelManage.BLL
{
    /// <summary>
    /// 员工管理 逻辑处理类
    /// </summary>
    public class StaffManageBLL
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginId">帐号</param>
        /// <param name="loginPwd">密码</param>
        /// <param name="valiCodeInput">输入的验证码</param>
        /// <param name="ValidateCode">生成的验证码</param>
        /// <returns></returns>
        public NewsModel StaffLogin(string loginId, string loginPwd, string valiCodeInput, string ValidateCode)
        {
            if (ValidateCode.Equals(valiCodeInput))
            {
                StaffManageDAL staffManageDAL = new StaffManageDAL();
                Staff staff = staffManageDAL.StaffLogin(loginId);
                if (Convert.IsDBNull(staff))
                {
                    return new NewsModel(false, "没有这个帐号");
                }
                else if (staff.Password.Equals(MD5Tool.MD5Encryption(loginPwd)))
                {
                    return new NewsModel(true, staff.Name + "|" + staff.Staff_DdentityId);
                }
                else
                {
                    return new NewsModel(false, "密码错误");
                }
            }
            else
            {
                return new NewsModel(false, "验证码错误");
            }
        }

        /// <summary>
        /// 获取所有员工信息
        /// </summary>
        /// <param name="name">员工名字</param>
        /// <param name="dept">员工部门</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">一页多少条</param>
        /// <returns></returns>
        public Dictionary<String, Object> GetAllStaff(string name, string dept, string state, string page, string rows)
        {
            if (dept == null && page == null)
            {
                return null;
            }
            if (dept == "" || dept == null)
            {
                dept = "0";
            }
            if (state == "" || state == null)
            {
                state = "0";
            }

            StaffManageDAL staffManageDAL = new StaffManageDAL();
            int total = 0;   //总记录数           
            List<StaffViewModel2> list = staffManageDAL.GetAllStaff(name, Convert.ToInt32(dept), Convert.ToInt32(state), out total, Convert.ToInt32(page ?? "0"), Convert.ToInt32(rows ?? "0"));
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
        /// 添加员工
        /// </summary>
        /// <param name="name">员工姓名</param>
        /// <param name="sex">员工性别</param>
        /// <param name="age">员工年龄</param>
        /// <param name="nation">员工民族</param>
        /// <param name="phone">员工手机</param>
        /// <param name="email">员工邮箱</param>
        /// <param name="DeptTxt">部门编号</param>
        /// <param name="StateTxt">状态编号</param>
        /// <returns></returns>
        public NewsModel AddStaff(string name, string sex, string age, string nation, string phone, string email, string DeptTxt, string StateTxt)
        {
            StaffManageDAL staffManageDAL = new StaffManageDAL();
            if (staffManageDAL.IsNullStaffByPhone(phone))
            {
                return new NewsModel(false, "手机号已使用，无法录入！");
            }
            Staff staff = new Staff();
            staff.Name = name;
            staff.Sex = sex.Equals("0") ? false : true;
            staff.Age = Convert.ToInt32(age);
            staff.Nation = nation;
            staff.Phone = phone;
            staff.Email = email;
            staff.DeptId = Convert.ToInt32(DeptTxt);
            staff.EstablishDate = DateTime.Now;
            staff.IsDelete = false;
            staff.Password = MD5Tool.MD5Encryption("1");
            staff.Staff_StateId = Convert.ToInt32(StateTxt);
            staff.Staff_DdentityId = new StaffManageDAL().GetDdentityIdByDeptID(staff.DeptId);

            if (staffManageDAL.AddStaff(staff))
            {
                return new NewsModel(true, "添加成功");
            }
            return new NewsModel(false, "添加失败");
        }

        /// <summary>
        /// 修改员工信息
        /// </summary>
        /// <param name="id">员工编号</param>
        /// <param name="name">员工姓名</param>
        /// <param name="sex">员工性别</param>
        /// <param name="age">员工年龄</param>
        /// <param name="nation">员工民族</param>
        /// <param name="phone">员工手机</param>
        /// <param name="email">员工邮箱</param>
        /// <param name="DeptTxt">员工部门编号</param>
        /// <param name="StateTxt">员工状态</param>
        /// <returns></returns>
        public NewsModel EditStaff(string id, string name, string sex, string age, string nation, string phone, string email, string DeptTxt, string StateTxt)
        {
            StaffManageDAL staffManageDAL = new StaffManageDAL();
            Staff staff = new Staff();
            staff.Name = name;
            staff.Sex = sex.Equals("女") ? false : true;
            staff.Age = Convert.ToInt32(age);
            staff.Nation = nation;
            staff.Phone = phone;
            staff.Email = email;
            staff.DeptId = new DeptManageDAL().GetDeptIdByDeptName(DeptTxt);
            staff.Staff_StateId = new StaffManageDAL().GetStateIdByStateTxt(StateTxt);

            if (staffManageDAL.EditStaff(Convert.ToInt32(id), staff))
            {
                return new NewsModel(true, "修改成功");
            }
            return new NewsModel(false, "修改失败");
        }

        /// <summary>
        ///删除员工
        /// </summary>
        /// <param name="id">员工编号</param>
        /// <returns></returns>
        public NewsModel DestroyStaff(int id)
        {
            StaffManageDAL staffManageDAL = new StaffManageDAL();
            if (staffManageDAL.DestroyStaff(id))
            {
                return new NewsModel(true, "删除成功");
            }
            return new NewsModel(false, "删除失败");
        }

        /// <summary>
        /// 获取所有员工状态
        /// </summary>
        /// <param name="flag">是否要加 全部状态 选项</param>
        /// <returns></returns>
        public List<Staff_DeptViewModel> GetALLStaffTxt(string flag)
        {
            return new StaffManageDAL().GetALLStaffTxt((flag.Equals("1")) ? true : false);
        }
    }
}
