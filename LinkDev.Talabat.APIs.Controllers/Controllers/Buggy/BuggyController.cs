using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using LinkDev.Talabat.APIs.Controllers.Controllers.Errors;
using LinkDev.Talabat.APIs.Controllers.Exceptions;
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
			throw new NotFoundException();
		 //return NotFound(new ApiResponse(404));
		}

		[HttpGet("badrequest")]
		public IActionResult GetBadRequest()
		{
			return BadRequest(new ApiResponse(400));
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
			return Unauthorized(new ApiResponse(401));
		}



	}
}
