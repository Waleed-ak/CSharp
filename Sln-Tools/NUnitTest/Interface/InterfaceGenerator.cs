using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace NUnitTest.Interface
{
	public class InterfaceGenerator
	{
		#region Private Fields
		private const MethodAttributes Attribute = MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.Abstract;
		private static readonly Random Rnd = new Random();
		#endregion Private Fields

		#region Public Methods

		public static Type Generate<TClass>()
		{
			var type = typeof(TClass);
			var typeName = $"I{type.Name}_{Rnd.Next():X8}";
			var asmBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName(typeName),AssemblyBuilderAccess.Run);
			var moduleBuilder = asmBuilder.DefineDynamicModule(typeName,$"{typeName}.dll");
			// create a new interface type
			var typeBuilder = moduleBuilder.DefineType(typeName,TypeAttributes.Public | TypeAttributes.Abstract | TypeAttributes.Interface);
			// build properties
			foreach(var prop in type.GetProperties())
			{
				BuildProperty(typeBuilder,prop);
			}

			foreach(var method in type.GetMethods())
			{
				BuildMethod(typeBuilder,method);
			}
			// complete the type creation
			var resType = typeBuilder.CreateType();
			// save the result to a file
			asmBuilder.Save($"{typeName}.dll");
			return resType;
		}

		#endregion Public Methods

		#region Private Methods

		private static void BuildMethod(TypeBuilder typeBuilder,MethodInfo method)
		{
			var paramTypes = method.GetParameters().Select(c => c.ParameterType).ToArray();
			var methodBuilder = typeBuilder.DefineMethod(method.Name,method.Attributes,method.ReturnType,paramTypes);
		}

		private static void BuildProperty(TypeBuilder typeBuilder,PropertyInfo prop)
		{
			var propBuilder = typeBuilder.DefineProperty(prop.Name,prop.Attributes,prop.PropertyType,Type.EmptyTypes);

			// build the getters and setters method if a public one was available
			var getter = prop.GetGetMethod();
			var setter = prop.GetSetMethod();

			if(getter?.IsPublic == true)
			{
				propBuilder.SetGetMethod(typeBuilder.DefineMethod(getter.Name,Attribute,getter.ReturnType,getter.GetParameters().Select(p => p.ParameterType).ToArray()));
			}

			if(setter?.IsPublic == true)
			{
				propBuilder.SetSetMethod(typeBuilder.DefineMethod(setter.Name,Attribute,setter.ReturnType,setter.GetParameters().Select(p => p.ParameterType).ToArray()));
			}
		}

		#endregion Private Methods
	}
}