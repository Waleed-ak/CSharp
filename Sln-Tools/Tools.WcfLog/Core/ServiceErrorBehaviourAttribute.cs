using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Tools
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface,AllowMultiple = false)]
	public sealed class ServiceErrorBehaviourAttribute:Attribute, IServiceBehavior
	{
		#region Private Fields
		private readonly static IErrorHandler ErrorHandler = Resolver.Resolve<IErrorHandler>();
		#endregion Private Fields

		#region Public Methods

		public void AddBindingParameters(ServiceDescription serviceDescription,ServiceHostBase serviceHostBase,Collection<ServiceEndpoint> endpoints,BindingParameterCollection bindingParameters)
		{
		}

		public void ApplyDispatchBehavior(ServiceDescription description,ServiceHostBase serviceHostBase)
		{
			foreach(ChannelDispatcherBase channelDispatcherBase in serviceHostBase.ChannelDispatchers)
			{
				(channelDispatcherBase as ChannelDispatcher)?.ErrorHandlers.Add(ErrorHandler);
			}
		}

		public void Validate(ServiceDescription description,ServiceHostBase serviceHostBase)
		{
		}

		#endregion Public Methods
	}
}
