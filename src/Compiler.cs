using System;
using System.CodeDom.Compiler;

namespace Game1
{
	public class Compiler
	{
		CodeDomProvider CSCP;	
		CompilerParameters CP;

		public Compiler()
		{
			CSCP = CodeDomProvider.CreateProvider("CSharp");///new CSharpCodeProvider();
			CP = new System.CodeDom.Compiler.CompilerParameters
			{
				GenerateExecutable = false,
				GenerateInMemory = true
			};
			CP.ReferencedAssemblies.Add("System.dll");

			var results = CSCP.CompileAssemblyFromSource(CP, new string[]
			{@" using System;

    			class MyClass
    			{
        			public void Message(string message)
        			{
            			Console.Write(message);
        			}               
    			}"
			});
			if (results.Errors.Count == 0)
			{
    			var myClass = results.CompiledAssembly.CreateInstance("MyClass");
				myClass.GetType().GetMethod("Message").Invoke(myClass, new []{ "Hello World!" });
			}
		}
	}
}
