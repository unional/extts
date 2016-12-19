using System;
using System.Linq;
using System.IO;
using System.Diagnostics;

namespace c2.tools.ExtTS
{
    class program
    {
        static void Main(string[] args)
        {
            var libs = args.Length <= 0 ? path.ExtLibs : path.ExtLibs.Select(l => args.Any(a => String.Compare(l, a, true) == 0) ? l : null).ToArray();
            for (var i = 0; i < libs.Length; i++)
                if (libs[i] != null)
            {
                try
                {
                    Console.WriteLine($"[{i + 1}/{libs.Length}. {libs[i]}] GENERATE JsDuck Documents --------------------------------------------------------------");
                    GenerateDocs(path._0_tools_jsduck, path._1_src_all[i], path._2_docs_all[i]);

                    Console.WriteLine($"[{i + 1}/{libs.Length}. {libs[i]}] GENERATE TypeScript Definitions --------------------------------------------------------------");
                    GenerateTS(path._2_docs_all[i], path._3_out_all[i]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($@"[{i + 1}/{libs.Length}. {libs[i]}] {ex.ToString().Replace(Environment.NewLine, "|")}");
                }
                Console.WriteLine();
                Console.WriteLine("Generation completed. Press any key to close.");
                Console.ReadLine();
            }
        }

        public static void GenerateDocs(string jsduckPath, string srcPath, string docPath)
        {
            if (Directory.EnumerateFiles(docPath, "*.*", SearchOption.TopDirectoryOnly).Any())
                Console.WriteLine($@"[Warning] JsDuck DOCS already generated: {docPath}, please empty the folder to re-generate the ExtJS docs from ExtJS source");
            else
            {
                Console.WriteLine($@"[JsDuck] NOTES: You may need to change SystemLocale to En(US)~ if any error");

                var process = new Process()
                {
                    StartInfo = new ProcessStartInfo(jsduckPath, $@"""{srcPath}"" --output ""{docPath}""")
                    {
                        UseShellExecute = false
                    }
                };
                process.Start();
                process.WaitForExit();
            }
        }


        static void GenerateTS(string docPath, string tsPath)
        {
            var outputPath = Path.Combine(docPath, "output");
            if (!Directory.Exists(outputPath))
                throw new DirectoryNotFoundException(outputPath);
            if (!Directory.EnumerateFiles(outputPath, "*.js", SearchOption.TopDirectoryOnly).Any())
                throw new FileNotFoundException(Path.Combine(outputPath, "*.js"));

            var sourcePath = Path.Combine(docPath, "source");
            if (!Directory.Exists(sourcePath))
                throw new DirectoryNotFoundException(sourcePath);
            if (!Directory.EnumerateFiles(sourcePath, "*.html", SearchOption.TopDirectoryOnly).Any())
                throw new FileNotFoundException(Path.Combine(sourcePath, "*.html"));

            try
            {
                jsduck.JsDoc.UnknownExtTypes.Clear();
                Console.WriteLine($"GENERATING:\n    - From OUTPUT: {outputPath}\\*.js\n    - From SOURCE: {sourcePath}\\*.html");
                var tsFile = new model.Builder(outputPath, sourcePath).Build();
                if (tsFile == null)
                    Console.WriteLine($@"Modules: not found");
                else
                {
                    Console.WriteLine($@"Modules: {tsFile.Classes.Count}");
                    var tmp = tsPath + ".~tmp";
                    try
                    {
                        tsFile.Write(tmp);
                        if (!File.Exists(tsPath))
                            File.Move(tmp, tsPath);
                        else if (!model.Builder.IsSameTS(tmp, tsPath))
                        {
                            File.Delete(tsPath);
                            File.Move(tmp, tsPath);
                        }
                        else
                            Console.WriteLine($@"WARNING: {tsPath} has not changed");
                    }
                    finally
                    {
                        if (File.Exists(tmp))
                            File.Delete(tmp);
                    }
                    Console.WriteLine($@"GENERATED: {tsPath}");
                }
                if (jsduck.JsDoc.UnknownExtTypes.Count > 0)
                    Console.WriteLine($@"UNKNOWN TYPES[{jsduck.JsDoc.UnknownExtTypes.Count}]: {String.Join(", ", jsduck.JsDoc.UnknownExtTypes)}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($@"GENERATE ERROR: {tsPath}. {ex}");
            }
            Console.WriteLine();
        }
    }
}
