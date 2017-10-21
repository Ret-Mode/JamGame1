using System;
using System.CodeDom.Compiler;

namespace Game1
{
	public class Compiler
	{

		struct ScriptType
		{
			public String Name;
			public CompilerResults ScriptInternal;
			public object ScriptInstance;

		};

		ScriptType[] ScriptArray = new ScriptType[1]; //TODO (R-M): one for now
		uint Scripts = 0;
		uint MaxScripts = 1;
		CodeDomProvider CSCP;

		public Compiler()
		{
			CSCP = CodeDomProvider.CreateProvider("CSharp");


			//CSCP = null;
		}

		~Compiler()
		{
			for (uint i = 0; i < MaxScripts; ++i)
			{
				ScriptArray[i].Name = null;
				ScriptArray[i].ScriptInternal = null;
				ScriptArray[i].ScriptInstance = null;

			}
			CSCP = null;
		}

		public void ReadScript(String ScriptName)
		{
			if (Scripts < MaxScripts)
			{
				CompilerParameters CP = new System.CodeDom.Compiler.CompilerParameters
				{
					GenerateExecutable = false,
					GenerateInMemory = true
				};
				CP.ReferencedAssemblies.Add("System.dll");

				CompilerResults results = CSCP.CompileAssemblyFromFile(CP, "Scripts\\" + ScriptName + ".cs");
				if (results.Errors.Count == 0)
				{

					ScriptArray[Scripts].ScriptInstance = results.CompiledAssembly.CreateInstance("MyClass");
					ScriptArray[Scripts].Name = ScriptName;
					ScriptArray[Scripts].ScriptInternal = results;
					++Scripts;
				}
				CP = null;
			}
		}

		public void RunScript(String ScriptName, String Method)
		{
			uint Index;
			String[] S = { "a" };
			for (Index = 0; Index < MaxScripts; ++Index)
			{
				if (ScriptArray[Index].Name == ScriptName) break;
			}

			if (Index == MaxScripts) return;

			ScriptArray[Index].ScriptInstance.GetType().GetMethod(Method).Invoke(ScriptArray[Index].ScriptInstance, S);
		}



	}
}
