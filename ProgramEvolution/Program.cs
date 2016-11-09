using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProgramEvolution
{
	class Program
	{
		public static Dictionary<string, List<string>> CommandGramma = new Dictionary<string, List<string>>();
		public static string ProgramHeader = "using System;\n" +
											 "using System.Collections.Generic;\n" +
											 "using System.Linq;\n" +
											 "using System.Text;\n" +
											 "using System.Text.RegularExpressions;\n" +
											 "using System.Threading.Tasks;\n" +
											 "namespace ProgramEvolution\n" +
												"{\n" +
													"class Program\n" +
													"{\n" +
														"static void Main(string[] args)" +
														"{";
		public static string ProgramTail = "}\n}\n}\n";


		static string GenerateCommand(string startCommand)
		{
			var rnd = new Random();
			while(true)
			{
				var commandBeforeReplacements = startCommand;
				foreach (var key in CommandGramma.Keys)
				{
					var randomIndex = rnd.Next(0, CommandGramma[key].Count);

					var regex = new Regex(Regex.Escape(key));
					startCommand = regex.Replace(startCommand, CommandGramma[key][randomIndex], 1);
				}

				if (startCommand.Equals(commandBeforeReplacements))
					break;
			}

			return startCommand + ";";
		}


		static void Main(string[] args)
		{
			

			CommandGramma.Add("COMMAND", new List<string> { "DEF_VAR", "ASSIGN", "CALL_FUNC" });

			CommandGramma.Add("DEF_VAR", new List<string> { "var VARIABLE_NAME = new CLASS_NAME(VARIABLES)",  "var VARIABLE_NAME = VARIABLE_NAME", "var VARIABLE_NAME = CONSTANT", "var VARIABLE_NAME = CALL_FUNC" });
			CommandGramma.Add("ASSIGN", new List<string> { "VARIABLE_NAME = VARIABLE_NAME", "VARIABLE_NAME = CONSTANT", "VARIABLE_NAME = CALL_FUNC" });
			CommandGramma.Add("CALL_FUNC", new List<string> { "FUNCTION_NAME(VARIABLES)", "VARIABLE_NAME.FUNCTION_NAME(VARIABLES)" });

			CommandGramma.Add("CLASS_NAME", new List<string> { "Random" });
			CommandGramma.Add("VARIABLES", new List<string> { "VARIABLE_NAME, VARIABLES", "VARIABLE_NAME" });
			CommandGramma.Add("VARIABLE_NAME", new List<string> { "var1", "var2", "var3" });
			CommandGramma.Add("CONSTANT", new List<string> { "42", "\"жиза\"" });
			CommandGramma.Add("FUNCTION_NAME", new List<string> { "Next" });
			//CommandGramma.Add("", new List<string> { });

			var program = "";
			for (var i = 0; i < 10; i++)
			{
				program += GenerateCommand("COMMAND") + "\n";
			}
			Console.WriteLine(program);
		}
	}
}
