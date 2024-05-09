using Demoo.BLL.Interfaces;
using Demoo.BLL.Repositories;
using Demoo.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Demo_session_3_MVC.Controllers
{
    // inheritance : departmentcontroller is a controller
    // aggregation : departmentcontroller has a departmentRepository
    public class DepartmentController : Controller
    {
        //private  readonly IDepartmentRepository _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork) //Ask CLR for creating object  From Class implementing interface "IDepartmentRepository"

        {
            //_departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
        }



        //Department/Index
        public async Task< IActionResult >Index()
        {
            //1.ViewData is a Dictionary Object(introduced in ASP.Net Framework 3.5)
            //=>It helps us to transfer data from controller to view 

            //ViewData["Message"] = "Hello View Data";

            //2. ViewBag is a Dynamic property (introduced in ASP.Net Framework 4  based on dynamic feature)
            //=>It helps us to transfer data from controller to view 

            //ViewBag.Message = "Hello View Bag";


            var departments = await _unitOfWork.DepartmentRepository.GetAll();

            return View(departments);
        }
        // /Department/Create

        // [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task< IActionResult> Create(Department department)
        {
            if (ModelState.IsValid) //Server side Validation 
            {
               int Result =  _unitOfWork.DepartmentRepository.Add(department);
                
                 if(Result > 0)
                {
                    TempData["Massage"] = "New Department is Created";
                }
                return RedirectToAction(nameof(Index));


                //var count = _unitOfWork.Completa();
                //if (count > 0)
                //  return RedirectToAction(nameof(Index));  
            }
            return View(department); 
        }

        // /Department/Details/1

        [HttpGet]
        public async Task< IActionResult >Details(int? id, string ViewName = "Details")
        {
            //Defensive Code

            if (id is null)
                return BadRequest(); //400

            var department = await _unitOfWork.DepartmentRepository.GetbyId(id.Value);

            if (department is null)
                return NotFound(); //404

            //Will Execute it if pass at all Validations 
            return View(ViewName, department);
        }


        // /Department/Edit/1
        // /Department/Edit
        public async Task<IActionResult> Edit(int? id)
        {

            ///if (id is null)
            ///    return BadRequest(); //400
            ///var department = _departmentRepository.Get(id.Value);
            ///if (department is null)
            ///    return NotFound(); //404
            ///Will Execute it if pass at all Validations 
            ///return View(department);
            ///

            return await Details(id, "Edit");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task< IActionResult >Edit([FromRoute] int id, Department department)
        {
            if (id != department.ID)
                return BadRequest();

            if (ModelState.IsValid) //Server side Validation 
            {
                try
                {
                     _unitOfWork.DepartmentRepository.Update(department);
                    _unitOfWork.Completa();


                }
                catch (Exception ex)
                {
                    // 1.  Log Exception
                    // 2.  Friendly Message

                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(department);
        }

        // /Department/Delete/1
        // /Department/Delete/
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, Department department)
        {
            if (id != department.ID)
                return BadRequest();
            try
            {
                _unitOfWork.DepartmentRepository.Delete(department);
                _unitOfWork.Completa();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                // 1.  Log Exception
                // 2.  Friendly Message

                ModelState.AddModelError(string.Empty, ex.Message);

                return View(department);

            }
        }
    }
}
