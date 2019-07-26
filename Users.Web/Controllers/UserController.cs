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
        public List<UserViewModel> GetData()
        {
            List<UserViewModel> userViewModelList = new List<UserViewModel>();
            foreach (var user in userRepository.GetAllUsers())
            {
                userViewModelList.Add(
                    new UserViewModel
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Description = user.Description,
                        Email = user.Email,
                        City = user.City,
                        Street = user.Street
                    });
            }
            return userViewModelList;
        }

        private List<UserViewModel> GetData(int start)
        {
            List<UserViewModel> userViewModelList = new List<UserViewModel>();
            foreach (var user in userRepository.GetAllUsers(start))
            {
                userViewModelList.Add(
                    new UserViewModel
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Description = user.Description,
                        Email = user.Email,
                        City = user.City,
                        Street = user.Street
                    });
            }
            return userViewModelList;
        }

        private UserViewModel ParseSingleUser(User useR)
        {
            UserViewModel user = new UserViewModel();
            user.Id = useR.Id;
            user.UserName = useR.UserName;
            user.Email = useR.Email;
            user.Description = useR.Description;
            user.City = useR.City;
            user.Street = useR.Street;

            return user;
        }

        public ActionResult Index(int id = 1)
        {
            ViewBag.NumberOfPages = userRepository.GetLastUserId();
            return View(GetData(id));
        }

        [HttpGet]
        public ActionResult Add()
        {
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.Id = userRepository.GetAllUsers().Count + 1;
            return View(userViewModel);
        }

        [HttpPost]
        public ActionResult Add(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                userRepository.AddUser(userViewModel.UserName, userViewModel.UserName, userViewModel.City, userViewModel.Description, userViewModel.Street);
                return RedirectToAction("Index");
            }
            else return View("Add", userViewModel);
        }

        [HttpGet]
        public ActionResult Edit(int? id = null)
        {
            if (id == null || id == 0) return RedirectToAction("Index");

            UserViewModel userViewModel = ParseSingleUser(userRepository.GetUserById((int)id));

            return View(userViewModel);
        }

        [HttpPost]
        public ActionResult Edit(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                userRepository.EditUser(userViewModel.Id, userViewModel.UserName, userViewModel.Email, userViewModel.Description, userViewModel.City, userViewModel.Street);
                return RedirectToAction("Index");
            }
            return View(userViewModel);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return RedirectToAction("Index");

            UserViewModel useR = new UserViewModel();
            useR.Id = (int)id;
            useR = ParseSingleUser(userRepository.GetUserById(useR.Id));
            return View(useR);
        }

        [HttpPost]
        public ActionResult Delete(UserViewModel useR)
        {
            userRepository.DeleteUser(useR.Id);
            return RedirectToAction("Index");
        }
    }
}