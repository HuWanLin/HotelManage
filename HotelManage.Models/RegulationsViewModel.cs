using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManage.Models
{
    /// <summary>
    /// 通知输出 模型类
    /// </summary>
    public class RegulationsViewModel
    {
        /// <summary>
        /// 通知Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 创建用户名字
        /// </summary>
        public string StaffName { get; set; }
        /// <summary>
        /// 通知标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 通知内容
        /// </summary>
        public string RegulationsContent { get; set; }
        /// <summary>
        /// 通知时间
        /// </summary>
        public string EstablishDate { get; set; }
    }
}
