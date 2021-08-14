using System;
using System.Net;

namespace Authentication.CrossCutting.Exceptions
{
	public class ExceptionBase : Exception
	{
		private readonly HttpStatusCode _httpStatusCode;
		public ExceptionBase(HttpStatusCode httpStatusCode, string message) : base(message)
		{
			_httpStatusCode = httpStatusCode;
		}

		public HttpStatusCode StatusCode => _httpStatusCode;
	}
}
