using BloodBank.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BloodBank.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login(Account acc)
        {
            var context = new BloodBankContext();
            Account acc1 = new Account();
            if (acc == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                acc1 = context.Accounts.FirstOrDefault(x => x.Username.Equals(acc.Username) && x.Password.Equals(acc.Password));
                if (acc1 == null)
                {
                    return RedirectToAction("Index");
                }
                var username = JsonConvert.SerializeObject(acc1.Username);
                var role = JsonConvert.SerializeObject(acc1.RoleId);
                var act = Newtonsoft.Json.JsonConvert.SerializeObject(acc1);
                HttpContext.Session.SetString("username", username);
                HttpContext.Session.SetString("role", role);
                HttpContext.Session.SetString("act", act);
                if (acc1.RoleId == 1)
                {
                    return RedirectToAction("HomeAdmin");
                }
                if (acc1.RoleId == 2)
                {
                    return RedirectToAction("HomeUser");
                }
                return RedirectToAction("Index");
            }
        }

        public IActionResult SignOut()
        {
            HttpContext.Session.Remove("act");
            HttpContext.Session.Remove("role");
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index");
        }

        public IActionResult HomeUser()
        {
            return View();
        }

        public IActionResult HomeAdmin()
        {
            return View();
        }

        public IActionResult DonorRegistrator()
        {
            var context = new BloodBankContext();
            string? account = HttpContext.Session.GetString("act");
            var acc = JsonConvert.DeserializeObject<Account>(account);
            ViewBag.Id = acc.Id;
            var blood = (from a in context.BloodGroups
                         select a).ToList();
            ViewBag.Result = blood;
            return View(blood);
        }

        public IActionResult BloodTransaction()
        {
            return View();
        }

        public IActionResult Transaction(Transaction transaction)
        {
            using (var context = new BloodBankContext())
            {
                context.SaveChanges();
            }
            return RedirectToAction("BloodTransaction"); ;
        }

        public IActionResult RegisterDonor(RegisterDonor donor)
        {
            using (var context = new BloodBankContext())
            {
                donor.AccountId = 2;
                context.RegisterDonors.Add(donor);
                context.SaveChanges();
            }
            return RedirectToAction("DonorRegistrator");
        }

        public IActionResult SignUp(Account acc)
        {
            using (var context = new BloodBankContext())
            {
                acc.RoleId = 2;
                context.Accounts.Add(acc);
                context.SaveChanges();
            }
            return RedirectToAction("DonorRegistrator");
        }
        public IActionResult RegisterAcc()
        {
            return View();
        }

        public IActionResult DonorSearch()
        {
            var context = new BloodBankContext();
            var registerD = (from r in context.RegisterDonors
                             join b in context.BloodGroups on r.BloodGroupId equals b.Id
                             select new
                             {
                                 FullName = r.FullName,
                                 ContactNo = r.ContactNo,
                                 Address = r.Address,
                                 Image = r.Image,
                                 Bloodname = b.Bloodname
                             });

            List<FullInfor> temp = new List<FullInfor>();

            foreach (var i in registerD.ToList())
            {
                temp.Add(new FullInfor(i.FullName, i.ContactNo, i.Address, i.Image, i.Bloodname));
            }

            ViewBag.registerD = temp;

            var blood = (from a in context.BloodGroups
                         select a).ToList();
            ViewBag.Result = blood;
            return View(blood);
        }

        public IActionResult SearchDonorDetail(BloodGroup bloodGroup)
        {
            var context = new BloodBankContext();
            var blood = (from a in context.BloodGroups
                         select a).ToList();
            if (Convert.ToString(bloodGroup.Id) != null)
            {
                var registerD = (from r in context.RegisterDonors
                                 join b in context.BloodGroups on r.BloodGroupId equals b.Id
                                 where r.BloodGroupId == bloodGroup.Id
                                 select new
                                 {
                                     FullName = r.FullName,
                                     ContactNo = r.ContactNo,
                                     Address = r.Address,
                                     Image = r.Image,
                                     Bloodname = b.Bloodname
                                 });

                List<FullInfor> temp = new List<FullInfor>();

                foreach (var i in registerD.ToList())
                {
                    temp.Add(new FullInfor(i.FullName, i.ContactNo, i.Address, i.Image, i.Bloodname));
                }

                ViewBag.registerX = temp;
            }
            return View(blood);
        }

        public IActionResult ViewEnquiry()
        {
            var context = new BloodBankContext();
            var registerD = (from r in context.RegisterDonors
                             join b in context.BloodGroups on r.BloodGroupId equals b.Id
                             select new
                             {
                                 FullName = r.FullName,
                                 ContactNo = r.ContactNo,
                                 Address = r.Address,
                                 Image = r.Image,
                                 Bloodname = b.Bloodname
                             });

            List<FullInfor> temp = new List<FullInfor>();

            foreach (var i in registerD.ToList())
            {
                temp.Add(new FullInfor(i.FullName, i.ContactNo, i.Address, i.Image, i.Bloodname));
            }

            ViewBag.registerX = temp;
            return View(temp);
        }

        public IActionResult BloodBank()
        {
            int i = 0;
            List<GroupBloodBank> list = new List<GroupBloodBank>();
            var context = new BloodBankContext();
            var query =
            from post in context.RegisterDonors
            join meta in context.BloodGroups on post.BloodGroupId equals meta.Id
            select new { BloodGroupId = meta.Id, BloodName = meta.Bloodname };

            foreach (var line in query.GroupBy(info => info.BloodName)
                         .Select(group => new
                         {
                             IdE = group.Key,
                             Count = group.Count()
                         })
                         .OrderBy(x => x.IdE))
            {
                Console.WriteLine("{0} {1}", line.IdE, line.Count);
                list.Add(new GroupBloodBank(i, line.IdE, line.Count));
                i++;
            }

            ViewBag.Data = list;
            var blood = (from a in context.BloodGroups
                         select a).ToList();
            ViewBag.Result = blood;
            return View(blood);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
