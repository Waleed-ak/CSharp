using System;
using System.Collections.Generic;

namespace Tools
{
  public static class Cls<T>
  {
    #region Private Fields
    private static readonly Dictionary<object,Func<T>> _DicFuncObj = new();
    private static readonly Type _Type = typeof(T);
    #endregion Private Fields

    #region Public Methods

    public static void Register(Func<T> instanceCreator,object key = null)
    {
      if(_DicFuncObj.TryGetValue(key,out var _))
        throw new Exception($"The Type {_Type.Name} for this key [{key}] is already registered");

      _DicFuncObj[key] = instanceCreator;
    }

    public static void RegisterSingleton(Func<T> instanceCreator,object key = null)
    {
      var lazy = new ItemLazy(instanceCreator);
      Register(() => lazy.Value,key);
    }

    public static T Resolve(object key = null)
    {
      if(_DicFuncObj.TryGetValue(key,out var func))
        return func();
      throw new TypeAccessException($"Please Register the Type {_Type.Name} : {_Type.FullName}");
    }

    #endregion Public Methods

    #region Private Classes

    private class ItemLazy
    {
      #region Private Fields
      private readonly Func<T> _Func;
      private readonly object _Lock = new();
      private bool _HasValue;
      private T _Value;
      #endregion Private Fields

      #region Public Constructors

      public ItemLazy(Func<T> func) => _Func = func;

      #endregion Public Constructors

      #region Public Properties
      public T Value => Create();
      #endregion Public Properties

      #region Private Methods

      private T Create()
      {
        if(_HasValue)
          return _Value;
        lock(_Lock)
        {
          if(!_HasValue)
          {
            try
            {
              _Value = _Func();
              _HasValue = true;
            }
            catch(Exception ex)
            {
              _HasValue = false;
              throw new($"{typeof(T).Name}: Has Error On CacheMem",ex);
            }
          }
        }
        return _Value;
      }

      #endregion Private Methods
    }
    #endregion Private Classes
  }
}
