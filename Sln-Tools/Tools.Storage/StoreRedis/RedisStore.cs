using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Tools
{
  public class RedisStore:ICacheStore, IDisposable
  {
    #region Private Fields
    private readonly IDatabase _Client;
    private IConnectionMultiplexer _IConnectionMultiplexer;
    #endregion Private Fields

    #region Public Constructors

    public RedisStore(IRedisConfig redisConfig)
    {
      _Client = InitConnection(redisConfig).GetDatabase();
    }

    public RedisStore(Action<IRedisConfig> action)
    {
      var redisConfig = new RedisConfig();
      action(redisConfig);
      _Client = InitConnection(redisConfig).GetDatabase();
    }

    #endregion Public Constructors

    #region Public Properties
    public string AppName { get; set; } = "App";

    public TimeSpan CacheDuration { get; set; }
		public Func<string> Token { get; set; }
		#endregion Public Properties

		#region Public Methods

		public void Dispose() => _IConnectionMultiplexer.Dispose();

    public bool Exists<T>(string key)
    {
      try
      {
        return _Client.KeyExists(this.GetKey<T>(key));
      }
      catch(Exception)
      {
        throw;
      }
    }

    public T Get<T>(string key)
    {
      var tempKey = this.GetKey<T>(key);
      try
      {
        if(key.CheckKey())
        {
          return default;
        }
        T obj = default;
        try
        {
          obj = JsonCache.Deserialize<T>(_Client.StringGet(tempKey).ToString());
        }
        catch
        {
        }
        if(obj is null)
        {
          return default;
        }
        Task.Run(() => _Client.KeyExpire(key: tempKey,expiry: CacheDuration));
        return obj;
      }
      catch(Exception)
      {
        throw;
      }
    }

    public bool Remove<T>(string key)
    {
      try
      {
        return _Client.KeyDelete(this.GetKey<T>(key));
      }
      catch(Exception)
      {
        throw;
      }
    }

    public void Set<T>(string key,T value)
    {
      try
      {
        _Client.StringSet(this.GetKey<T>(key),JsonCache.Serialize(value),expiry: CacheDuration,when: When.Always,flags: CommandFlags.None);
      }
      catch(Exception)
      {
        throw;
      }
    }

    public void Set<T>(string key,T value,TimeSpan timeSpan)
    {
      try
      {
        _Client.StringSet(this.GetKey<T>(key),JsonCache.Serialize(value),expiry: timeSpan,when: When.Always,flags: CommandFlags.None);
      }
      catch(Exception)
      {
        throw;
      }
    }


		#endregion Public Methods

		#region Private Methods

		private IConnectionMultiplexer InitConnection(IRedisConfig redisConfig)
    {
      try
      {
        var config = new ConfigurationOptions
        {
          KeepAlive = 180,
          SyncTimeout = 5000,
          DefaultDatabase = redisConfig.DefaultDatabase,
        };
        if(redisConfig.Sentinel)
        {
          config.CommandMap = CommandMap.Sentinel;
          config.ServiceName = "mymaster";
        }
        else
        {
          config.Password = redisConfig.Password;
        }
        foreach(var item in redisConfig.EndPoints)
        {
          config.EndPoints.Add(Dns.GetHostAddresses(item.Host).First(),item.Port);
        }
        AppName = redisConfig.AppName;
        _IConnectionMultiplexer = ConnectionMultiplexer.Connect(config);
        return _IConnectionMultiplexer;
      }
      catch(Exception)
      {
        throw;
      }
    }

    #endregion Private Methods
  }
}
