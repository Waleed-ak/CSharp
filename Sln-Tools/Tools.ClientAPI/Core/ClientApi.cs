using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Tools
{
	public class ClientApi:IClientApi
	{
		#region Private Fields
		private static readonly JsonMediaTypeFormatter _JsonFormatter = new JsonMediaTypeFormatter { SerializerSettings = { NullValueHandling = NullValueHandling.Ignore,DefaultValueHandling = DefaultValueHandling.Ignore } };
		private static readonly TimeSpan Timeout = TimeSpan.Parse("00:30:00");
		private readonly Uri BaseAddress;
		#endregion Private Fields

		#region Public Constructors

		public ClientApi(string _BaseAddress)
		{
			BaseAddress = new Uri(_BaseAddress);
		}

		#endregion Public Constructors

		#region Public Methods

		public Res GetJson<Res>(string uri) => GetJsonAsync<Res>(uri).GetAwaiter().GetResult();

		public async Task<Res> GetJsonAsync<Res>(string uri)
		{
			var result = default(Res);
			try
			{
				using(var Client = GetClient())
				{
					var response = await Client.GetAsync(uri).ConfigureAwait(false);
					if(response.IsSuccessStatusCode)
					{
						result = await response.Content.ReadAsAsync<Res>().ConfigureAwait(false);
					}
					else
					{
						throw new Exception($"GetJsonAsync:  \n{response.StatusCode}\n{response.ReasonPhrase}");
					}
				}
			}
			catch
			{
				throw;
			}
			return result;
		}

		public Res PostJson<Res>(string uri,object reqObject) => PostJsonAsync<Res>(uri,reqObject).GetAwaiter().GetResult();

		public async Task<Res> PostJsonAsync<Res>(string uri,object reqObject)
		{
			try
			{
				using(var Client = GetClient())
				{
					var response = await Client.PostAsync(uri,reqObject,_JsonFormatter).ConfigureAwait(false);
					if(response.IsSuccessStatusCode)
						return await response.Content.ReadAsAsync<Res>().ConfigureAwait(false);
					else
						throw new Exception($"PostJsonAsync:  \n{response.StatusCode}\n{response.ReasonPhrase}");
				}
			}
			catch
			{
				throw;
			}
		}

		#endregion Public Methods

		#region Private Methods

		private HttpClient GetClient()
			=> new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip })
			{
				BaseAddress = BaseAddress,
				Timeout = Timeout
			};

		#endregion Private Methods
	}
}
