using System.Collections.Generic;

namespace Tools
{
  public class RedisConfig:IRedisConfig
  {
    #region Public Properties
    public string AppName { get; set; } = "App";
    public int DefaultDatabase { get; set; }
    public List<EndPoint> EndPoints { get; set; } = new List<EndPoint>();
    public string Password { get; set; }
    public bool Sentinel { get; set; }
    #endregion Public Properties
  }
}
