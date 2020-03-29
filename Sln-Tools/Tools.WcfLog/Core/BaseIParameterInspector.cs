using System.ServiceModel.Dispatcher;

namespace Tools
{
	public abstract class BaseIParameterInspector:IParameterInspector
	{
		#region Public Methods

		public abstract void After(string operationName,object returnValue);

		public void AfterCall(string operationName,object[] outputs,object returnValue,object correlationState) => After(operationName,returnValue);

		public abstract void Before(string operationName,object[] inputs);

		public object BeforeCall(string operationName,object[] inputs)
		{
			Before(operationName,inputs);
			return null;
		}

		#endregion Public Methods
	}
}
