using AutoMapper;
using Demo_session_3_MVC.ViewModels;
using Demoo.DAL;
using Demoo.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_session_3_MVC.Controllers
{

    [Authorize]
    public class UserController : Controller
	{


		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IMapper _mapper;


		public UserController(UserManager<ApplicationUser> userManager, IMapper mapper)
		{
			_userManager = userManager;
			_mapper = mapper;
		}
		public async Task<IActionResult> Index(string SearchValue)
		{


			if (string.IsNullOrEmpty(SearchValue))
			{
				var Users = await _userManager.Users.Select(
					U => new UserViewModel()
					{
						Id = U.Id,
						FName = U.FName,
						LName = U.LName,
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
					FName = User.FName,
					LName = User.LName,
					Email = User.Email,
					PhoneNumber = User.PhoneNumber,
					Roles = _userManager.GetRolesAsync(User).Result


				};
				return View(new List<UserViewModel> { MappedUser });

			}
		}

		public async Task<IActionResult> Details(string Id, string ViewName = "Details")
		{
			if (Id is null)

				return BadRequest();
			var User = await _userManager.FindByIdAsync(Id);

			if (User is null)
				return NotFound();

			var MappedUser = _mapper.Map<ApplicationUser, UserViewModel>(User);


			return View(ViewName, MappedUser);

		}
		public async Task<IActionResult> Edit(string Id)
		{
			return await Details(Id, "Edit");
		}


		[HttpPost]
		public async Task<IActionResult> Edit(UserViewModel User, [FromRoute] string id)
		
		{
			if(id !=User.Id)
				return BadRequest();
			if(ModelState.IsValid) 
			{
				try
				{
					var user =await _userManager.FindByIdAsync(id);
					user.PhoneNumber = User.PhoneNumber; 
					user.FName = User.FName;
					user.LName = User.LName;
                  
                    await _userManager.UpdateAsync(user);
                    return RedirectToAction(nameof(Index));
                }catch(Exception ex) 
				{ 
					ModelState.AddModelError(string.Empty, ex.Message);	 

				}

			}
			return View(User);	
		}
		public	async Task<IActionResult> Delete(string id) 
		{
	     return await Details(id,"Delete");
		
		}
		public async Task<IActionResult>ConfirmDelete(string id)
		{
			try
			{
				var user =await _userManager.FindByIdAsync(id);
				await _userManager.DeleteAsync(user);
				return RedirectToAction(nameof(Index));

			}
			catch(Exception ex) 
			{
				ModelState.AddModelError(string.Empty, ex.Message);
			    return RedirectToAction("Error", "Home");

			}
		}



    }

}