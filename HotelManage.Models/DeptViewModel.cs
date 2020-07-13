using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManage.Models
{
    /// <summary>
    /// 部门输出 模型
    /// </summary>
    public class DeptViewModel
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 部门名字
        /// </summary>
        public string DeptTxt { get; set; }
    }
}
