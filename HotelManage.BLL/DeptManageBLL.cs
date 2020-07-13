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
    /// 部门管理 逻辑处理类
    /// </summary>
    public class DeptManageBLL
    {
        /// <summary>
        /// 获取所有部门
        /// </summary>
        /// <returns></returns>
        public List<DeptViewModel> GetAllDept(int flag)
        {
            DeptManageDAL deptManageDAL = new DeptManageDAL();
            return deptManageDAL.GetAllDept(flag == 1 ? true : false);
        }
    }
}
