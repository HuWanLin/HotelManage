using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManage.Models
{
    /// <summary>
    /// 房间  模型类
    /// </summary>
    public class RoomViewModel
    {
        /// <summary>
        /// 房间的编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 房间的完整编号 
        /// </summary>
        public string CompleteNum { get; set; }
        /// <summary>
        /// 房间的状态
        /// </summary>
        public string StateTxt { get; set; }
        /// <summary>
        /// 房间的折扣
        /// </summary>
        public string DiscountNum { get; set; }
        /// <summary>
        /// 房间的单价
        /// </summary>
        public string UnitPrice { get; set; }
        /// <summary>
        /// 房间的类型文本
        /// </summary>
        public string TypeTxt { get; set; }
        /// <summary>
        /// 房间的实际金额
        /// </summary>
        public string ActualMoney { get; set; }
    }
}
