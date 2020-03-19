using System.Threading.Tasks;

namespace Tools
{
	public interface IClientApi
	{
		#region Public Methods

		Res GetJson<Res>(string uri);

		Task<Res> GetJsonAsync<Res>(string uri);

		Res PostJson<Res>(string uri,object reqObject);

		Task<Res> PostJsonAsync<Res>(string uri,object reqObject);

		#endregion Public Methods
	}
}
