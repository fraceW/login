using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		
		BbsContextModel bc = new BbsContextModel();

		public ActionResult Register()
		{
			return View();
		}

		public ActionResult About()
		{
			return View();
		}

        RoleContext rc = new RoleContext();
        public ActionResult Contact()
		{
			return View();
		}

		public ActionResult Check()
		{
			return View();
		}

		public ActionResult Course()
		{
			return View();
		}




		[HttpGet]
		public ActionResult InsertPerson(string jobNum, string jobName, int jobAge, string job)
		{
			if (ModelState.IsValid)
			{
				bc.OpenConnection();
				string sql = "insert into person values('" + jobNum + "','" + jobName + "'," + jobAge + ",'" + job + "')";
				int result = bc.InsertData(sql);
				if (result > 0)
				{
					ModelState.AddModelError("success", "成功");
				}
				else
				{
					ModelState.AddModelError("error", "失败");
				}
				bc.CloseConnection();
				return Content("保存成功");
			}
			else
			{
				return View();
			}
		}

        UsersContext us = new UsersContext();
        [HttpGet]
		public ActionResult UserList(int pageNo, int pageSize)
		{
			List<UserPerson> user = new List<UserPerson>();
			string userName = Session["userName"] as string;
			double count = Math.Ceiling(Convert.ToDouble(us.UserPerson.Count())/5);
			IQueryable<UserPerson> data = us.UserPerson.OrderBy(t => t.ID).Skip(pageNo * pageSize).Take(pageSize);
			user = data.ToList();
			return Json(new { UserList = user, name = userName , pageTotal = count }, JsonRequestBehavior.AllowGet);
        }

		[HttpGet]
		public int Verify(string name)
		{
            int role = rc.Role.Where(t => t.firstname == name).ToList().Count;
            return role;
		}
		
		[HttpGet]
		public int Login(string name, string pwd)
		{
			Session.Remove("userName");
			Session.Add("userName", name);
			int count = rc.Role.Where(t => t.firstname == name && t.Password == pwd).ToList().Count;
            return count;
        }

		[HttpGet]
		public ActionResult Registe(string name, string newPwd, string roleType)
		{
			if (ModelState.IsValid)
			{
				bc.OpenConnection();
				string sql = "insert into Teacher values((select MAX(ID) from Teacher) + 1,'" + name + "','" + newPwd + "','" + roleType + "')";
				int result = bc.InsertData(sql);
				if (result > 0)
				{
					ModelState.AddModelError("success", "成功");
				}
				else
				{
					ModelState.AddModelError("error", "失败");
				}
				bc.CloseConnection();
				return Content("保存成功！");
			}
			else
			{
				return View();
			}
		}

		[HttpGet]
		public ActionResult DelectPreson(string id)
		{
			bc.OpenConnection();
			string sql = string.Format(@"delete from person where id = '{0}'", id);
			int result = bc.Delete(sql);
			bc.CloseConnection();
			return Content("删除成功！");
		}

		[HttpGet]
		public ActionResult UpdatePreson(string id, string name, int age, string job)
		{
			bc.OpenConnection();
			string sql = string.Format(@"delete from person where id = '{0}'", id);
			int res = bc.Delete(sql);

			string insertSql = "insert into person values('" + id + "','" + name + "'," + age + ",'" + job + "')";
			int result = bc.InsertData(insertSql);

			bc.CloseConnection();
			return Content("保存成功");
		}

		[HttpGet]
		public ActionResult GetCheckData(int pageNo, int pageSize, string textContext)
		{
			CheckContext cc = new CheckContext();
			IQueryable<CheckProject> IQuery = cc.CheckProject;

			if (!string.IsNullOrWhiteSpace(textContext))
				IQuery = IQuery.Where(t => t.Code.Contains(textContext.Trim()) || t.Name.Contains(textContext.Trim()));
			double count = Math.Ceiling(Convert.ToDouble(IQuery.Count()) / 5);
			List<CheckProject> check = IQuery.OrderBy(t => t.Code).Skip(pageNo * pageSize).Take(pageSize).ToList();

			return Json(new { checkData = check, totalCount = count }, JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public ActionResult Menu()
		{
			MenuContext mc = new MenuContext();

			List<MenuData> menu = mc.menuData.ToList();
			return Json(new { menuData = menu }, JsonRequestBehavior.AllowGet);
		}
	}
}