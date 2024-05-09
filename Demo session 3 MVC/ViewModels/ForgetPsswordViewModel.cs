using System.ComponentModel.DataAnnotations;

namespace Demo_session_3_MVC.ViewModels
{
	public class ForgetPsswordViewModel
	{
		[Required(ErrorMessage = "Email is Required")]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }
	}
}
