using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TICKETBOX.Models;
using TICKETBOX.Models.Tables;


namespace TICKETBOX.UserController
{


    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        public IActionResult UserProfile()
        {
            var userID = User.Identity.Name;


            using (var db = new FastticketContext())
            {
                var user = db.Users.FirstOrDefault(u => u.UserName == userID);


                if (user == null || user.Role == "Admin")
                {
                    return RedirectToAction("Management", "Admin");
                }

                return View(user);
            }
        }
        [Authorize(Roles = "User")]
        public IActionResult UserDetailProfile(int id)
        {
            var userID = User.Identity.Name;
            using (var db = new FastticketContext())
            {
                var user = db.Users.FirstOrDefault(u => u.UserId == id);
                if (user == null || user.UserName != userID)
                {
                    return RedirectToAction("AccessDenied", "Home");
                }
                var viewModel = new UpdateUserModel
                {
                    UserId = user.UserId,
                    UserFullname = user.FullName,
                    UserPhoneNumber = user.UserPhoneNumber,
                    UserSex = user.Sex,
                    DoB = user.DoB,
                    UserAddress = user.UserAddress,
                    UserEmail = user.UserEmail

                };

                return View(viewModel);
            }
        }
        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult UpdateUser(UpdateUserModel viewModel)
        {
            var userID = User.Identity.Name;
            if (ModelState.IsValid)
            {
                using (var db = new FastticketContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.UserId == viewModel.UserId);
                    if (user != null && user.Role != "Admin")
                    {
                        user.FullName = viewModel.UserFullname;
                        user.UserPhoneNumber = viewModel.UserPhoneNumber;
                        user.Sex = viewModel.UserSex;
                        user.DoB = viewModel.DoB;
                        user.UserAddress = viewModel.UserAddress;
                        user.UserEmail = viewModel.UserEmail;

                        db.SaveChanges();

                        return RedirectToAction("UserDetailProfile", new { id = user.UserId });
                    }
                }
            }
            return RedirectToAction("UserDetailProfile", new { id = viewModel.UserId });
        }
        [Authorize(Roles = "User")]
        public IActionResult ChangePassword(int id)
        {
            // var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            // if (currentUserId != id.ToString())
            // {
            //     return RedirectToAction("AccessDenied", "Home");
            // }

            var model = new ChangePasswordModel
            {
                UserId = id
            };
            return View(model);
        }
        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordModel model)
        {
            var userID = User.Identity.Name;
            if (ModelState.IsValid)
            {
                using (var db = new FastticketContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.UserId == model.UserId);
                    if (user != null && user.Role != "Admin")
                    {
                        user.UserPassword = model.NewPassword;
                        db.SaveChanges();

                        return RedirectToAction("UserDetailProfile", new { id = model.UserId });
                    }
                }
            }

            return View(model);
        }
    }
}


