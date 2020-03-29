using System;
using System.ServiceModel.Dispatcher;

namespace Tools
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method,AllowMultiple = false)]
	public sealed class ServiceOperationAttribute:Attribute, IParameterInspector
	{
		#region Public Methods

		public void AfterCall(string operationName,object[] outputs,object returnValue,object correlationState)
		{
		}

		public object BeforeCall(string operationName,object[] inputs) => null;

		#endregion Public Methods
	}
}
