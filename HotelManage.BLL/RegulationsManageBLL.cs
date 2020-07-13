using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HotelManage.DAL;
using HotelManage.Models;

namespace HotelManage.BLL
{
    /// <summary>
    /// 规定管理 逻辑处理类
    /// </summary>
    public class RegulationsManageBLL
    {
        /// <summary>
        /// 处理分页数据
        /// </summary>
        /// <param name="name">发布人</param>
        /// <param name="title">标题</param>
        /// <param name="page">第几页</param>
        /// <param name="rows">一页多少行</param>
        /// <param name="nameD">当前用户</param>
        /// <returns>返回表格要求的格式数据</returns>
        public Dictionary<String, Object> GetAllRegulations(string name, string title, string page, string rows, string nameD)
        {
            RegulationsManageDAL regulationsManageDAL = new RegulationsManageDAL();
            StaffManageDAL staffManageDAL = new StaffManageDAL();
            int total = 0;   //总记录数
            List<RegulationsViewModel> list = regulationsManageDAL.GetAllRegulations(name, title, Convert.ToInt32(page ?? "0"), Convert.ToInt32(rows ?? "0"), out total, staffManageDAL.GetDeptIdByName(nameD));
            Dictionary<String, Object> map = new Dictionary<String, Object>();
            if (total != 0 && !rows.Equals(null))
            {
                map.Add("total", total);
                map.Add("pages", total / Convert.ToInt32(rows));
                map.Add("rows", list);
            }
            else
            {
                map.Add("total", 0);
                map.Add("pages", 1);
                map.Add("rows", new Regulations());
            }
            return map;
        }

        /// <summary>
        /// 增加通知
        /// </summary>
        /// <param name="title">通知标题</param>
        /// <param name="tegulationsContent">通知内容</param>
        /// <param name="DeptList">接收部门</param>
        /// <param name="name">当前用户名字</param>
        /// <returns></returns>
        public NewsModel AddRegulations(string title, string tegulationsContent, string[] DeptList, string name)
        {
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(tegulationsContent) || DeptList == null || string.IsNullOrWhiteSpace(name))
            {
                return new NewsModel(false, "标题、内容、部门为空或者错误登陆！");
            }
            Regulations regulations = new Regulations();
            regulations.EstablishDate = DateTime.Now;
            regulations.IsDelete = false;
            regulations.StaffName = name;
            regulations.RegulationsContent = tegulationsContent;
            regulations.Title = title;

            for (int i = 0; i < DeptList.Count(); i++)
            {
                regulations.DeptIdList += DeptList[i];
                if (i != DeptList.Count() - 1)
                {
                    regulations.DeptIdList += ",";
                }
            }

            RegulationsManageDAL regulationsManageDAL = new RegulationsManageDAL();
            if (regulationsManageDAL.AddRegulations(regulations))
            {
                return new NewsModel(true, "增加成功");
            }
            return new NewsModel(false, "增加失败");
        }

        /// <summary>
        /// 删除通知
        /// </summary>
        /// <param name="id">通知编号</param>
        /// <returns></returns>
        public NewsModel DestroyRegulations(string id)
        {
            RegulationsManageDAL regulationsManageDAL = new RegulationsManageDAL();
            if (regulationsManageDAL.DestroyRegulations(Convert.ToInt32(id)))
            {
                return new NewsModel(true, "删除成功");
            }
            return new NewsModel(false, "删除失败");
        }

        /// <summary>
        /// 根据通知编号获取通知详细
        /// </summary>
        /// <param name="id">通知编号</param>
        /// <returns></returns>
        public RegulationsViewModel GetRegulationsById(string id)
        {
            return new RegulationsManageDAL().GetRegulationsById(Convert.ToInt32(id ?? "0"));
        }
    }
}
