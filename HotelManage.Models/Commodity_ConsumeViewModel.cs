using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManage.Models
{
    /// <summary>
    /// 消耗商品 输出模型
    /// </summary>
    public class Commodity_ConsumeViewModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 房间编号
        /// </summary>
        public string CompleteNum { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 消耗数量
        /// </summary>
        public string CommodityNum { get; set; }
        /// <summary>
        /// 消耗时间
        /// </summary>
        public string EstablishDate { get; set; }      
    }
}
