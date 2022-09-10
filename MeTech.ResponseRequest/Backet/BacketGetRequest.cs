using System;
using MediatR;

namespace MeTech.ResponseRequest.Backet
{
	public class BacketGetRequest:IRequest<BacketGetResponse>
	{
		public int Id { get; set; }
	}
}

