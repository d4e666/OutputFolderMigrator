#region References

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml.Linq;
using CommandLine;

#endregion

namespace OutputFolderMigratorConsole
{
	class Program
	{
		private const string xmlns = "http://schemas.microsoft.com/developer/msbuild/2003";

		static void Main(string[] args)
		{
			Options options = new Options();
			if (!Parser.Default.ParseArguments(args, options)) return;
			if (string.IsNullOrWhiteSpace(options.SourceDirectory)) options.SourceDirectory = Environment.CurrentDirectory;
			foreach (
				string projectFile in Directory.EnumerateFiles(options.SourceDirectory, GetExtention(options.Language), SearchOption.AllDirectories))
			{
				ProcessProjectFile(projectFile, options.OutputDirectory);
			}
		}

		private static string GetExtention(ProgrammingLanguage language)
		{
			switch (language)
			{
				case ProgrammingLanguage.CSharp:
					return "*.csproj";
				case ProgrammingLanguage.VisualBasic:
					return "*.vbproj";
			}
			throw new InvalidEnumArgumentException();
		}

		private static void ProcessProjectFile(string projectFile, string outputDirectory)
		{
			XDocument doc = XDocument.Load(projectFile);

			IEnumerable<XElement> paths = doc.Descendants(XName.Get("OutputPath", xmlns));
			foreach (XElement path in paths)
			{
				path.Value = outputDirectory;
			}
			doc.Save(projectFile);
		}
	}
}