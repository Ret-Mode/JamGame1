using System;
using System.CodeDom.Compiler;

namespace Game1
{
	public class Compiler
	{


		public Compiler(String FileName, bool InMemory)
		{
			CodeDomProvider CSCP;
			CompilerParameters CP;
			CSCP = CodeDomProvider.CreateProvider("CSharp");
			CP = new System.CodeDom.Compiler.CompilerParameters
			{
				GenerateExecutable = !InMemory,
				GenerateInMemory = InMemory
			};
			CP.ReferencedAssemblies.Add("System.dll");

			///CompilerResults results = CSCP.CompileAssemblyFromSource(CP, new string[]
			CompilerResults results = CSCP.CompileAssemblyFromFile(CP, FileName);
			if (results.Errors.Count == 0)
			{
    			object myClass = results.CompiledAssembly.CreateInstance("MyClass");
				myClass.GetType().GetMethod("Message").Invoke(myClass, new []{ "Hello World!" });
				myClass = null;
			}
			CP = null;
			CSCP = null;
		}
	}
}
