using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManage.Models
{
    /// <summary>
    ///商品 模型类
    /// </summary>
    public class CommodityViewModel
    {
        /// <summary>
        /// 商品编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 商品名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int CommodityNum { get; set; }
        /// <summary>
        /// 商品单价
        /// </summary>
        public decimal UnitPrice { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string EstablishDate { get; set; }
        /// <summary>
        /// 可分配数量
        /// </summary>
        public string SeparableNum { get; set; }
    }
}
