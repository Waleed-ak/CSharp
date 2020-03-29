using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Tools
{
	[AttributeUsage(AttributeTargets.Method,AllowMultiple = false)]
	public sealed class OperationBehaviourAttribute:Attribute, IOperationBehavior
	{
		#region Private Fields
		private readonly static IParameterInspector ParameterInspector = Resolver.Resolve<IParameterInspector>();
		#endregion Private Fields

		#region Public Methods

		public void AddBindingParameters(OperationDescription operationDescription,BindingParameterCollection bindingParameters)
		{
		}

		public void ApplyClientBehavior(OperationDescription operationDescription,ClientOperation clientOperation) => clientOperation.ParameterInspectors.Add(ParameterInspector);

		public void ApplyDispatchBehavior(OperationDescription operationDescription,DispatchOperation dispatchOperation) => dispatchOperation.ParameterInspectors.Add(ParameterInspector);

		public void Validate(OperationDescription operationDescription)
		{
		}

		#endregion Public Methods
	}
}
