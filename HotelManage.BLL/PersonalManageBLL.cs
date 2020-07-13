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
    /// 个人管理 逻辑处理类
    /// </summary>
    public class PersonalManageBLL
    {
        /// <summary>
        /// 修改用户的密码
        /// </summary>
        /// <param name="pwdy">原密码</param>
        /// <param name="pwdx">要修改的密码</param>
        /// <param name="pwdq">修改密码确认</param>
        /// <param name="name">当前用户名字</param>
        /// <returns></returns>
        public NewsModel EditUserPwd(string pwdy, string pwdx, string pwdq, string name)
        {
            if (string.IsNullOrWhiteSpace(pwdy) || string.IsNullOrWhiteSpace(pwdx) || string.IsNullOrWhiteSpace(pwdq))
            {
                return new NewsModel(false, "请不要留空");
            }
            if (!pwdx.Equals(pwdq))
            {
                return new NewsModel(false, "两次密码不一致");
            }
            PersonalManageDAL personalManageDAL = new PersonalManageDAL();
            if (!personalManageDAL.GetPwdByName(name).Equals(MD5Tool.MD5Encryption(pwdy)))
            {
                return new NewsModel(false, "原密码不正确");
            }
            if (personalManageDAL.EditUserPwd(MD5Tool.MD5Encryption(pwdx), name))
            {
                return new NewsModel(true, "修改完成");
            }
            return new NewsModel(false, "用户不存在");
        }

        /// <summary>
        /// 获取所有用户用户信息根据用户名字
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public StaffViewModel GetUserInByName(string name)
        {
            PersonalManageDAL personalManageDAL = new PersonalManageDAL();
            return personalManageDAL.GetUserInByName(name);
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="nation">民族</param>
        /// <param name="email">邮箱</param>
        /// <param name="sex">性别</param>
        /// <param name="name">当前用户名字</param>
        /// <returns></returns>
        public NewsModel EditUserIn(string nation, string email, string sex, string name)
        {
            bool sexDB = sex.Equals("0") ? false : true;
            PersonalManageDAL personalManageDAL = new PersonalManageDAL();
            if (personalManageDAL.EditUserIn(nation, email, sexDB, name))
            {
                return new NewsModel(true, "修改完成");
            }
            return new NewsModel(false, "修改失败");
        }
    }
}
