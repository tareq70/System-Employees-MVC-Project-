using AutoMapper;
using DAL.Models;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class RoleController : Controller
    {
        public RoleManager<IdentityRole> _roleManager { get; }
        public IMapper _mapper { get; }

        public RoleController(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index(string SearchValue)
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
                var Roles = await _roleManager.Roles.ToListAsync();
                var MappedRole = _mapper.Map<IEnumerable<RoleViewModel>>(Roles);
                return View(MappedRole);

            }
            else
            {
                var Role = await _roleManager.FindByNameAsync(SearchValue);
                var MappedRole = _mapper.Map<IdentityRole, RoleViewModel>(Role);
                return View(new List<RoleViewModel>() { MappedRole });
            }
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mappedRole = _mapper.Map<RoleViewModel, IdentityRole>(model);
                await _roleManager.CreateAsync(mappedRole);
                return RedirectToAction("Index");
            }
            return View(model);
        }



        public async Task<IActionResult> Detalis(string id, string ViewName = "Detalis")
        {
            if (id is null)
                return BadRequest();
            var Role = await _roleManager.FindByIdAsync(id);
            if (Role is null)
                return NotFound();
            var MappedUser = _mapper.Map<IdentityRole, RoleViewModel>(Role);
            return View(ViewName, MappedUser);

        }

        public async Task<IActionResult> Edit(string id)
        {
            return await (Detalis(id, "Edit"));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModel model, [FromRoute] string id)
        {
            if (id != model.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var Role = await _roleManager.FindByIdAsync(id);
                    Role.Name = model.RoleName;
                    await _roleManager.UpdateAsync(Role);
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
                var user = await _roleManager.FindByIdAsync(id);
                await _roleManager.DeleteAsync(user);
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
