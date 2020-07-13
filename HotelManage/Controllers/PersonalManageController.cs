using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using HotelManage.BLL;
using HotelManage.Models;

namespace HotelManage.Controllers
{
    /// <summary>
    /// 个人管理  控制器
    /// </summary>
    public class PersonalManageController : Controller
    {
        #region returnView       
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region returnData
        /// <summary>
        /// 修改自己的密码
        /// </summary>
        /// <param name="pwdy">原密码</param>
        /// <param name="pwdx">修改后的密码</param>
        /// <param name="pwdq">确认密码</param>
        /// <returns></returns>
        public ActionResult EditUserPwd(string pwdy, string pwdx, string pwdq)
        {
            PersonalManageBLL personalManageBLL = new PersonalManageBLL();
            NewsModel newsModel = personalManageBLL.EditUserPwd(pwdy, pwdx, pwdq, Session["UserName"].ToString());
            return Json(newsModel, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取用户信息根据名字
        /// </summary>
        /// <returns></returns>
        public ActionResult GetUserInByName()
        {
            PersonalManageBLL personalManageBLL = new PersonalManageBLL();
            return Json(personalManageBLL.GetUserInByName(Session["UserName"].ToString()), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 修改自己的个人信息
        /// </summary>
        /// <param name="nation">民族</param>
        /// <param name="email">邮箱</param>
        /// <param name="sex">性别</param>
        /// <returns></returns>
        public ActionResult EditUserIn(string nation, string email, string sex)
        {
            PersonalManageBLL personalManageBLL = new PersonalManageBLL();
            return Json(personalManageBLL.EditUserIn(nation, email, sex, Session["UserName"].ToString()), JsonRequestBehavior.AllowGet); ;
        }

        /// <summary>
        /// 获取民族
        /// </summary>
        /// <returns></returns>
        public ActionResult GetNation()
        {
            string[] nationString = { "汉族", "蒙古族", "回族", "藏族", "维吾尔族", "苗族", "彝族", "壮族", "布依族", "朝鲜族", "满族", "侗族", "瑶族", "白族", "土家族", "哈尼族", "哈萨克族", "傣族", "黎族", "僳僳族", "佤族", "畲族", "高山族", "拉祜族", "水族", "东乡族", "纳西族", "景颇族", "柯尔克孜族", "土族", "达斡尔族", "仫佬族", "羌族", "布朗族", "撒拉族", "毛南族", "仡佬族", "锡伯族", "阿昌族", "普米族", "塔吉克族", "怒族", "乌孜别克族", "俄罗斯族", "鄂温克族", "德昂族", "保安族", "裕固族", "京族", "塔塔尔族", "独龙族", "鄂伦春族", "赫哲族", "门巴族", "珞巴族", "基诺族" };
            List<NationViewModel> list = new List<NationViewModel>();

            foreach (string item in nationString)
            {
                NationViewModel na = new NationViewModel();
                na.text = item;
                list.Add(na);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取性别列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSex()
        {
            List<SexViewModel> list = new List<SexViewModel>();
            string[] st = { "女", "男" };
            for (int i = 0; i < 2; i++)
            {
                SexViewModel s = new SexViewModel();
                s.Id = i;
                s.Sex = st[i];
                list.Add(s);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}