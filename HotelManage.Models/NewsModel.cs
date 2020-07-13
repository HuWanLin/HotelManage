using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManage.Models
{
    /// <summary>
    /// 消息 模型类
    /// </summary>
    public class NewsModel
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool success { get; set; }

        /// <summary>
        /// 处理结果
        /// </summary>
        public string executeResult { get; set; }

        /// <summary>
        /// 消息 模型构造函数
        /// </summary>
        /// <param name="success">是否成功</param>
        /// <param name="executeResult">消息</param>
        public NewsModel(bool success, string executeResult)
        {
            this.success = success;
            this.executeResult = executeResult;
        }
    }
}
