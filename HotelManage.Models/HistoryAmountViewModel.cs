using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManage.Models
{
    /// <summary>
    /// 历史金额 输出模型
    /// </summary>
    public class HistoryAmountViewModel
    {
        /// <summary>
        /// 房间编号
        /// </summary>
        public string CompleteNum { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// 改变时间
        /// </summary>
        public string EstablishDate { get; set; }
    }
}
