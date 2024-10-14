using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Buggy
{
	public class BuggyController : BaseApiController
	{
		[HttpGet("notfound")]
		public IActionResult GetNotFoundRequest()
		{ 
		 return NotFound(new { StatusCode = 404, Messaage = "Not Found" });
		}

		[HttpGet("badrequest")]
		public IActionResult GetBadRequest()
		{
			return BadRequest(new {StatusCode=400 , Messaage="Bad Request" });
		}

		[HttpGet("badrequest/{id}")]
		public IActionResult GetValidation(int id)
		{
			return Ok();
		}

		[HttpGet("servererror")]
		public IActionResult GetServerError()
		{
			throw new Exception();
		}

		[HttpGet("unauthorized")]
		public IActionResult GetUnauthorizedError()
		{
			return Unauthorized(new { StatusCode = 401, Messaage = "Unauthorized" });
		}



	}
}
