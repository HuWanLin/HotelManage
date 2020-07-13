using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManage.Models
{
    /// <summary>
    /// 历史折扣
    /// </summary>
    public class HistoryDiscountViewModel
    {
        /// <summary>
        /// 房间编号 
        /// </summary>
        public string CompleteNum { get; set; }
        /// <summary>
        /// 折扣数
        /// </summary>
        public string DiscountNum { get; set; }
        /// <summary>
        /// 改变时间
        /// </summary>
        public string EstablishDate { get; set; }
    }
}
