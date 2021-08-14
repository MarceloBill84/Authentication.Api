using System;
using System.Net;

namespace Authentication.CrossCutting.Exceptions
{
	[Serializable]
	public class ValidationException : ExceptionBase
	{
		public ValidationException(string message) : base(HttpStatusCode.BadRequest, message)
		{

		}
	}
}
