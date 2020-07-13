using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManage.Models
{
    /// <summary>
    /// 房间历史状态 输出模型类
    /// </summary>
    public class HistoryStateViewModel
    {
        /// <summary>
        /// 房间编号 
        /// </summary>
        public string CompleteNum { get; set; }
        /// <summary>
        /// 状态文本
        /// </summary>
        public string StateTxt { get; set; }
        /// <summary>
        /// 改变时间
        /// </summary>
        public string EstablishDate { get; set; }
    }
}
