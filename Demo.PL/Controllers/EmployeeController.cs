using AutoMapper;
using BLL.Interfaces;
using BLL.Repositories;
using BLL.Repository;
using DAL.Models;
using Demo.PL.Helper;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
	[Authorize]

	public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        public IDepartmentRepository _departmentRepository { get; }
        public IunitOfWork _unitOfWork { get; }

        public EmployeeController(IunitOfWork unitOfWork, IMapper mapper)
        {
            
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            IEnumerable<Employee> employees;

            if (string.IsNullOrEmpty(SearchValue))   
                employees =await _unitOfWork.EmployeeRepository.GetAllAsync();
            else
                employees = _unitOfWork.EmployeeRepository.GetEmployeeByName(SearchValue);

            var MappedEmployee = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(MappedEmployee);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Departments =await _unitOfWork.DepartmentRepository.GetAllAsync();
            

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {

            if (ModelState.IsValid)
            {
                employeeVM.ImageName = DocumentSetting.UploadFile(employeeVM.Image, "Images");

                //if (result > 0 )
                //    TempData["message"] = "Employee is created";
                var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                await _unitOfWork.EmployeeRepository.AddAsync(MappedEmployee);
                await _unitOfWork.CompleteAsync();

                return RedirectToAction(nameof(Index));
            }
            else
                return View(employeeVM);
        }
        public async Task<IActionResult> Detalis(int? id)
        {
            if (id is null)
                return BadRequest();
            var employee =await _unitOfWork.EmployeeRepository.GetByIdAsync(id.Value);
            if (employee is null)
                return NotFound();
            var MappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(employee);

            return View(MappedEmployee);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Departments = _unitOfWork.DepartmentRepository.GetAllAsync();
            if (id is null)
                return BadRequest();
            var employee =await _unitOfWork.EmployeeRepository.GetByIdAsync(id.Value);
            if (employee is null)
                return NotFound();
            var MappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(employee);

            return View(MappedEmployee);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeViewModel employeeVM, [FromRoute] int id)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    if (employeeVM.Image is not null)
                    {
                        employeeVM.ImageName = DocumentSetting.UploadFile(employeeVM.Image, "Images");
                    }
                    var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    _unitOfWork.EmployeeRepository.Update(MappedEmployee);
                    await _unitOfWork.CompleteAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employeeVM);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
                return BadRequest();
            var employee =await _unitOfWork.EmployeeRepository.GetByIdAsync(id.Value);
            var MappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(employee);

            if (employee is null)
                return NotFound();
            return View(MappedEmployee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EmployeeViewModel employeeVM, [FromRoute] int id)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {

                    var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    _unitOfWork.EmployeeRepository.Delete(MappedEmployee);

                    var result =await _unitOfWork.CompleteAsync();
                    if(result>0 && employeeVM.ImageName is not null)
                    {
                        DocumentSetting.DeleteFile(employeeVM.ImageName, "Images");
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employeeVM);
        }
    }
}
