using BLL.Interfaces;
using BLL.Repository;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IunitOfWork _unitOfWork;

        public DepartmentController(IunitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var department = await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View(department);
        } 
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Create(Department department)
        {
            if (ModelState.IsValid)
            {
                 await _unitOfWork.DepartmentRepository.AddAsync(department);
                int result = await _unitOfWork.CompleteAsync();
                if (result > 0)  
                {
                    TempData["message"] = "Department is created";
                }
                return RedirectToAction(nameof(Index));
            }
            else
                return View(department);
        }
        public async Task<IActionResult> Detalis(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id.Value);
            if (department is null)
                return NotFound();
            return View(department);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if(id is null)
                return BadRequest();
            var department =await _unitOfWork.DepartmentRepository.GetByIdAsync(id.Value);
            if (department is null)
                return NotFound();
            return View(department);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Department department ,[FromRoute] int id)
        {
            if(id!=department.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.DepartmentRepository.Update(department);
                   await _unitOfWork.CompleteAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                } 
            }
                return View(department);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return BadRequest();
            var department =await _unitOfWork.DepartmentRepository.GetByIdAsync(id.Value);
            if (department is null)
                return NotFound();
            return View(department);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Department department, [FromRoute] int id)
        {
            if (id != department.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.DepartmentRepository.Delete(department);
                    int result =await _unitOfWork.CompleteAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(department);
        }
    }
}
