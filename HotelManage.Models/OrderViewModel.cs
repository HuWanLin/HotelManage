using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManage.Models
{
    /// <summary>
    /// 订单 模型类
    /// </summary>
    public class OrderViewModel
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 订单的完整房间叼
        /// </summary>
        public string CompleteNum { get; set; }
        /// <summary>
        /// 订单创建时间
        /// </summary>
        public string PurchaseDate { get; set; }
        /// <summary>
        /// 订单客户名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 订单客户性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 订单客户手机
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 退房时间
        /// </summary>
        public string DeleteDate { get; set; }
        /// <summary>
        /// 预计离店时间
        /// </summary>
        public string EstimatedTime { get; set; }
        /// <summary>
        /// 居住天数
        /// </summary>
        public string LiveDays { get; set; }
        /// <summary>
        /// 房费
        /// </summary>
        public string ActualMoney { get; set; }
        /// <summary>
        /// 押金
        /// </summary>
        public string Deposit { get; set; }
    }
}
