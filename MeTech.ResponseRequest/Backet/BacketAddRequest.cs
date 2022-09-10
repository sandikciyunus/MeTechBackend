using System;
using MediatR;
using MeTech.Model.Backet;

namespace MeTech.ResponseRequest.Backet
{
	public class BacketAddRequest:IRequest<BacketAddResponse>
	{
		public BacketAddModel Backet { get; set; }

    }
}

