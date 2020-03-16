using System;

namespace Tools
{
    public static class Ext
    {
        #region Public Methods

        public static T With<T>(this T @this, Action<T> action)
        {
            action(@this);
            return @this;
        }

        #endregion Public Methods
    }
}