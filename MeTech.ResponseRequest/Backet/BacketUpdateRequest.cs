using System;
using MediatR;
using MeTech.Model.Backet;

namespace MeTech.ResponseRequest.Backet
{
	public class BacketUpdateRequest:IRequest<BacketUpdateResponse>
	{
		public BacketUpdateModel Backet { get; set; }
	}
}

