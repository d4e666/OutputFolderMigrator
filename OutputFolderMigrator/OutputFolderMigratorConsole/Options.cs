#region References

using System;
using CommandLine;
using CommandLine.Text;

#endregion

namespace OutputFolderMigratorConsole
{
	internal enum ProgrammingLanguage
	{
		VisualBasic,
		CSharp,
	}

	internal class Options
	{
		[Option('s', nameof(SourceDirectory), Required = false, HelpText = "Source directory to process")]
		public string SourceDirectory { get; set; }

		[Option('o', nameof(OutputDirectory), DefaultValue = @"bin\$(Configuration)\$(Platform)\",
			HelpText =
				"Directory to set as outputdirectory. Valid options are:\n- an absolute or relative path\n - a combination of MSBuild parameters like the default bin\\$(Configuration)\\$(Platform)\\."
			)]
		public string OutputDirectory { get; set; }

		[Option('l', nameof(Language), DefaultValue = ProgrammingLanguage.CSharp, HelpText = "The language for which projects are processed")]
		public ProgrammingLanguage Language { get; set; }

		[ParserState]
		public IParserState LastParserState { get; set; }

		[HelpOption]
		public string GetUsage()
		{
			return HelpText.AutoBuild(this, (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
		}
	}
}