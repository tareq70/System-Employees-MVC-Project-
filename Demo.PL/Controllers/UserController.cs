using AutoMapper;
using DAL.Models;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IMapper _mapper { get; }

        public UserController(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string SearchValue)
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                var Users = await _userManager.Users.Select(U => new UserViewModel()
                {
                    Id = U.Id,
                    FName = U.Fname,
                    LName = U.Lname,
                    Email = U.Email,
                    PhoneNumber = U.PhoneNumber,
                    Roles = _userManager.GetRolesAsync(U).Result
                }).ToListAsync();
                return View(Users);
            }
            else
            {
                var User = await _userManager.FindByEmailAsync(SearchValue);
                var MappedUser = new UserViewModel()
                {
                    Id = User.Id,
                    FName = User.Fname,
                    LName = User.Lname,
                    Email = User.Email,
                    PhoneNumber = User.PhoneNumber,
                    Roles = _userManager.GetRolesAsync(User).Result
                };
                return View(new List<UserViewModel> { MappedUser });
            }
        }


        public async Task<IActionResult> Detalis(string id, string ViewName = "Detalis")
        {
            if (id is null)
                return BadRequest();
            var User = await _userManager.FindByIdAsync(id);
            if (User is null)
                return NotFound();
            var MappedUser = _mapper.Map<ApplicationUser, UserViewModel>(User);
            return View(ViewName, MappedUser);

        }

        public async Task<IActionResult> Edit(string id)
        {
            return await (Detalis(id, "Edit"));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel model, [FromRoute] string id)
        {
            if (id != model.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(id);
                    user.PhoneNumber = model.PhoneNumber;
                    user.Fname = model.FName;
                    user.Lname = model.LName;
                    await _userManager.UpdateAsync(user);
                    return RedirectToAction(nameof(Index));


                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(model);

        }


        public async Task<IActionResult> Delete(string id)
        {
            return await Detalis(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> DDelete(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                await _userManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Error", "Home");
            }


        }


    }
}
