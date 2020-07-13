using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManage.Models
{
    /// <summary>
    /// 员工管理 Tabs 输出模型
    /// </summary>
    public class StaffViewModel2
    {
        /// <summary>
        /// 编号 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 员工名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 员工部门
        /// </summary>
        public string DeptTxt { get; set; }
        /// <summary>
        /// 员工性别 
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 员工年龄
        /// </summary>
        public string Age { get; set; }
        /// <summary>
        /// 员工民族
        /// </summary>
        public string Nation { get; set; }
        /// <summary>
        /// 员工手机
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 员工邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string EstablishDate { get; set; }
        /// <summary>
        /// 身份文本
        /// </summary>
        public string DdentityTxt { get; set; }
        /// <summary>
        /// 状态文本
        /// </summary>
        public string StateTxt { get; set; }
    }
}
