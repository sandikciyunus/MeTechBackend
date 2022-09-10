using System;
using MeTech.Model.Backet;
using MeTech.ResponseRequest.Base;

namespace MeTech.ResponseRequest.Backet
{
	public class BacketGetResponse:BaseResponse
	{
		public BacketGetModel Backet { get; set; }
	}
}

