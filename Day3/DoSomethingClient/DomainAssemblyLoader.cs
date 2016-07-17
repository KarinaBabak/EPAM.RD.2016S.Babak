﻿using System;
using System.Reflection;
using System.Linq;
using MyInterfaces;

namespace DoSomethingClient
{
    public class DomainAssemblyLoader : MarshalByRefObject
    {
        // Before making this call make sure that MyInterface assembly is signed with mykey.snk file. See Signing tab in MyInterface project properties editor.
        // Usage:
        // result = loader.Load("MyLibrary, Version=1.2.3.4, Culture=neutral, PublicKeyToken=f46a87b3d9a80705", input)
        public Result Load(string assemblyString, Input data)
        {
            // LoadFile() doesn't bind through Fusion at all - the loader just goes ahead and loads exactly what the caller requested.
            // It doesn't use either the Load or the LoadFrom context.
            // LoadFile() has a catch. Since it doesn't use a binding context, its dependencies aren't automatically found in its directory. 

            var assembly = Assembly.Load(assemblyString);
            var types = assembly.GetTypes();

            // TODO: Find first type that has DoSomething attribute and implements IDoSomething.
            var type = FindTypeDoSomethingAttributeImplementsIDoSomething(types);            
            // TODO: Create an instance of this type.
            var instance = Activator.CreateInstance(type);
            // TODO Save instance to variable.
            IDoSomething doSomethingService = instance as IDoSomething; 
            return doSomethingService.DoSomething(data);
        }

        // Usage:
        // var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"MyDomain\MyLibrary.dll");
        // result = loader.Load(path, input);
        public Result LoadFile(string path, Input data)
        {
            // LoadFrom() goes through Fusion and can be redirected to another assembly at a different path
            // but with that same identity if one is already loaded in the LoadFrom context.

            var assembly = Assembly.LoadFile(path);
            var types = assembly.GetTypes();

            Type type = FindDoSomethingAttribute(types); // TODO: Find first type that has DoSomething attribute and don't implement IDoSomething.
            // TODO: MethodInfo mi = type.GetMethod("DoSomething");
            MethodInfo mi = type.GetMethod("DoSomething");
            // TODO: result = mi.Invoke();
            Result result = mi.Invoke(Activator.CreateInstance(type), new object[] {data}) as Result;            
            return result;
        }

        // More details: http://stackoverflow.com/questions/1477843/difference-between-loadfile-and-loadfrom-with-net-assemblies
        public Result LoadFrom(string fileName, Input data)
        {
            var assembly = Assembly.LoadFrom(fileName);
            var types = assembly.GetTypes();

            // TODO: Find first type that has DoSomething attribute and implements IDoSomething.
            var type = FindTypeDoSomethingAttributeImplementsIDoSomething(types);  
            // TODO: Create an instance of this type.
            var instance = Activator.CreateInstance(type);
            IDoSomething doSomethingService = instance as IDoSomething; // TODO Save instance to variable.
            return doSomethingService.DoSomething(data);
        }


        private Type FindTypeDoSomethingAttributeImplementsIDoSomething(Type[] types)
        {            
            foreach (var type in types)
            {                
                if(Attribute.IsDefined(type, typeof(DoSomethingAttribute)))
                {
                    if(type.GetInterfaces().Contains(typeof(IDoSomething)))
                    {
                        return type;
                    }
                }                
            }
            return null;
        }

        private Type FindDoSomethingAttribute(Type[] types)
        {
            foreach (var type in types)
            {
                if (Attribute.IsDefined(type, typeof(DoSomethingAttribute)))
                {
                    return type;
                }
            }
            return null;
        }
    }
}
