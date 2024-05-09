using AutoMapper;
using Demoo.BLL.Interfaces;
using Demo_session_3_MVC.Helpers;
using Demo_session_3_MVC.ViewModels;
using Demoo.BLL.Interfaces;
using Demoo.DAL;
using Demoo.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace Demo_session_3_MVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IEmployeeRepository _employeeRepository;
        //private readonly IDepartmentRepository _DepartmentRepository;

        public EmployeeController(IMapper mapper, IUnitOfWork unitOfWork   ) //Ask CLR for creating object  From Class implementing interface "IEmployeeRepository"

        {
            //_employeeRepository = employeeRepository;
            //_DepartmentRepository = departmentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
          
        }



        //Employee/Index
        public async Task< IActionResult> Index(string searchInp)
        {
            var employees = Enumerable.Empty<Employee>();
            if (string.IsNullOrEmpty(searchInp))
                employees = await _unitOfWork.EmployeeRepository.GetAll();

            else
                employees =  _unitOfWork.EmployeeRepository.searchByName(searchInp.ToLower());

            var mappedEmps =  _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(mappedEmps);
        }
        // /Employee/Create

        // [HttpGet]
        public IActionResult Add()
        {
            //ViewBag.Departments =_DepartmentRepository.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Add(EmployeeViewModel employeeVM)
        {

            if (ModelState.IsValid) //Server side Validation 
            {
                employeeVM.ImageName = DocumentSetting.UploadFile(employeeVM.Image, "images");
             
                var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

              int Result=  _unitOfWork.EmployeeRepository.Add(employee);
                if(Result >0)
                {
                    TempData["Massage"] = "Employee is Created";
                }
                _unitOfWork.Completa();
                return RedirectToAction(nameof(Index));  
            }
            return View(employeeVM); 
        }

        // /Employee/Details/1

        [HttpGet]
        public async Task <IActionResult>Details(int? id, string ViewName = "Details")
        {
            //Defensive Code

            if (id is null)
                return BadRequest(); //400

            var employee = await _unitOfWork.EmployeeRepository.GetbyId(id.Value);
            var mappedEmp =   _mapper.Map<Employee, EmployeeViewModel>(employee);

            if (employee is null)
                return NotFound(); //404

            //Will Execute it if pass at all Validations 
            return  View(ViewName, mappedEmp);
        }


        // /Employee/Edit/1
        // /Employee/Edit
        public async Task<IActionResult> Edit(int? id)
        {

            ///if (id is null)
            ///    return BadRequest(); //400
            ///var Employee = _EmployeeRepository.Get(id.Value);
            ///if (Employee is null)
            ///    return NotFound(); //404
            ///Will Execute it if pass at all Validations 
            ///return View(Employee);
            ///

            return await Details(id, "Edit");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, EmployeeViewModel EmployeeVM)
        {
            if (id != EmployeeVM.Id)
                return BadRequest();

            if (ModelState.IsValid) //Server side Validation 
            {
                try
                {
                    var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(EmployeeVM);

                    _unitOfWork.EmployeeRepository.Update(mappedEmp);
                    _unitOfWork.Completa();
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    // 1.  Log Exception
                    // 2.  Friendly Message

                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return  View(EmployeeVM);
        }

        // /Employee/Delete/1
        // /Employee/Delete/
        public Task<IActionResult> Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, EmployeeViewModel EmployeeVM)
        {
            if (id != EmployeeVM.Id)
                return BadRequest();
            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(EmployeeVM);
                _unitOfWork.EmployeeRepository.Delete(mappedEmp);
                _unitOfWork.Completa();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                // 1.  Log Exception
                // 2.  Friendly Message

                ModelState.AddModelError(string.Empty, ex.Message);

                return View(EmployeeVM);

            }
        }
    }
}
