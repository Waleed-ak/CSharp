using System;
using System.Collections.Generic;
using System.Reflection;

namespace Tools
{
	public static class CopyExt
	{
		#region Private Fields
		private const BindingFlags BindingFlagsFilter = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

		private static readonly Type DelegateType = typeof(Delegate);

		private static readonly MethodInfo MemberwiseClone_MethodInfo = typeof(object).GetMethod("MemberwiseClone",BindingFlags.NonPublic | BindingFlags.Instance);
		#endregion Private Fields

		#region Public Methods

		public static T DeepClone<T>(this T original) => (T)Copy(original,new DicObject());

		#endregion Public Methods

		#region Private Methods

		private static object Copy(object obj,DicObject dic)
		{
			if(obj == null)
				return null;
			Type type = obj.GetType();

			if(type.IsPrimitive())
				return obj;
			if(type.IsDelegate())
				return null;

			if(dic.TryGetValue(obj,out var res))
				return res;
			if(type.IsArray)
				return Copy_Array(obj,dic,type);

			return Copy_NonArray(obj,dic,type);
		}

		private static object Copy_Array(object obj,DicObject dic,Type type)
		{
			var elementType = type.GetElementType();
			if(elementType.IsPrimitive())
			{
				return dic[obj] = MemberwiseClone(obj);
			}
			var array = obj as Array;
			var copied = Array.CreateInstance(elementType,array.Length);
			for(var i = 0;i < array.Length;i++)
			{
				copied.SetValue(Copy(array.GetValue(i),dic),i);
			}
			return dic[obj] = Convert.ChangeType(copied,obj.GetType());
		}

		private static object Copy_NonArray(object obj,DicObject dic,Type type)
		{
			var toret = MemberwiseClone(obj);
			//var fields = Array.FindAll(type.GetFields(BindingFlagsFilter), FieldFilter);
			var fields = type.GetFields(BindingFlagsFilter);
			foreach(FieldInfo field in fields)
			{
				var fieldValue = field.GetValue(obj);
				if(fieldValue == null)
					continue;
				field.SetValue(toret,Copy(fieldValue,dic));
			}
			return dic[obj] = toret;
		}

		//private static bool FieldFilter(this FieldInfo fieldInfo) => !fieldInfo.FieldType.IsPrimitive();

		private static bool IsDelegate(this Type type) => DelegateType.IsAssignableFrom(type);

		private static bool IsPrimitive(this Type type) => (type == typeof(string)) || (type.IsValueType && type.IsPrimitive) || type.IsEnum;

		private static object MemberwiseClone(object obj) => MemberwiseClone_MethodInfo.Invoke(obj,null);

		#endregion Private Methods

		#region Private Classes

		private class DicObject:Dictionary<object,object>
		{
			#region Private Fields
			private static readonly IEqualityComparer<object> ObjComparer = new DicObjectComparer();
			#endregion Private Fields

			#region Public Constructors

			public DicObject() : base(comparer: ObjComparer)
			{
			}

			#endregion Public Constructors
		}

		private class DicObjectComparer:IEqualityComparer<object>
		{
			#region Public Methods

			public new bool Equals(object x,object y) => ReferenceEquals(x,y);

			public int GetHashCode(object obj) => obj?.GetHashCode() ?? 0;

			#endregion Public Methods
		}
		#endregion Private Classes
	}
}
