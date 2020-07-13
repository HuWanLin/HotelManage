using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManage.Models
{
    /// <summary>
    /// 员工状态输出模型类
    /// </summary>
    public class Staff_DeptViewModel
    {
        /// <summary>
        /// 状态编号 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 状态文本
        /// </summary>
        public string StateTxt { get; set; }
    }
}
