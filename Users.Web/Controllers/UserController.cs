using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Users.Web.Models;
using System.Data.SqlClient;
using System.Data;
using BusinessLogic;
using DataAccess;

namespace Users.Web.Controllers
{
    public class UserController : Controller
    {
        IUserRepository userRepository = new UserRepository();
        private List<UserViewModel> GetData()
        {
            List<UserViewModel> userViewModelList = new List<UserViewModel>();
            foreach(var u in userRepository.GettAllUsers())
            {
                userViewModelList.Add(
                    new UserViewModel
                    {
                        Id = u.Id,
                        UserName = u.UserName,
                        Description = u.Description,
                        Email = u.Email,
                        City = u.City,
                        Street = u.Street
                    });
            }

            return userViewModelList;
        }

        public ActionResult Index()
        {
            return View(GetData());
        }
    }



}