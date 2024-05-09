﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Demo_session_3_MVC.ViewModels
{
	public class UserViewModel
	{
		public string Id { get; set; }
		public string FName { get; set; }
		public string LName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public IEnumerable<string> Roles { get; set; }

		
	}
}
