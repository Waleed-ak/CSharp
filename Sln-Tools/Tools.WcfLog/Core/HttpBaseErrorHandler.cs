using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace Tools
{
	public abstract class HttpBaseErrorHandler:IErrorHandler
	{
		#region Public Methods

		public abstract void Fault(Exception error,MessageVersion version,ref Message fault);

		public bool HandleError(Exception error) => false;

		public void ProvideFault(Exception error,MessageVersion version,ref Message fault)
		{
			if(error != null)
			{
				Fault(error,version,ref fault);
			}
		}

		#endregion Public Methods
	}
}