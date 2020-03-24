using System.Collections.Generic;

namespace Tools
{
  public interface IRedisConfig
  {
    #region Public Properties
    bool Sentinel { get; set; }
    string AppName { get; set; }
    int DefaultDatabase { get; set; }
    List<EndPoint> EndPoints { get; set; }
    string Password { get; set; }
    #endregion Public Properties
  }
}
