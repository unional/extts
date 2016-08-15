using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c2.tools.ExtTS
{
    static class path
    {
        private readonly static string ExeFolderPath = System.IO.Path.GetDirectoryName(new Uri(typeof(path).Assembly.CodeBase).LocalPath);
#if DEBUG
        private readonly static string HomePath = ExeFolderPath.EndsWith(@"\bin\Debug") ? Path.GetFullPath(Path.Combine(ExeFolderPath, @"..\..\")) : ExeFolderPath;
#else
        private readonly static string HomePath = ExeFolderPath.EndsWith(@"\bin\Release") ? Path.GetFullPath(Path.Combine(ExeFolderPath, @"..\..\")) : ExeFolderPath;
#endif
        private readonly static string ParentPath = Path.GetFullPath(Path.Combine(HomePath, @"..\"));

        public readonly static string[] ExtLibs;

        public readonly static string _0_tools = Path.Combine(ParentPath, @"0.tools\");
        public readonly static string _0_tools_jsduck;
        public readonly static string _1_src = Path.Combine(ParentPath, @"1.src\");
        public readonly static string[] _1_src_all;
        public readonly static string _2_docs = Path.Combine(ParentPath, @"2.docs\");
        public readonly static string[] _2_docs_all;
        public readonly static string _3_out = Path.Combine(ParentPath, @"3.out\");
        public readonly static string[] _3_out_all;

        static path()
        {
            if (!Directory.Exists(_0_tools))
                throw new DirectoryNotFoundException($@"{_0_tools} not found");
            _0_tools_jsduck = Directory.EnumerateFiles(_0_tools, "jsduck*.exe", SearchOption.TopDirectoryOnly).SingleOrDefault();
            if (_0_tools_jsduck == null)
                throw new FileNotFoundException($@"{_0_tools}jsduck.exe not found, please download then place in the folder");

            if (!Directory.Exists(_1_src))
                throw new DirectoryNotFoundException($@"{_1_src} not found, please download ExtJS sources to here");
            _1_src_all = Directory.EnumerateDirectories(_1_src, "*.*", SearchOption.TopDirectoryOnly).Where(p => Directory.EnumerateDirectories(p, "*.*", SearchOption.TopDirectoryOnly).Any() || Directory.EnumerateFiles(p, "*.*", SearchOption.TopDirectoryOnly).Any()).ToArray();
            ExtLibs = _1_src_all.Select(p => p.Substring(_1_src.Length)).ToArray();

            if (!Directory.Exists(_2_docs))
                Directory.CreateDirectory(_2_docs);
            _2_docs_all = ExtLibs.Select(p => _2_docs + p).ToArray();
            foreach (var _2_docs_item in _2_docs_all)
                if (!Directory.Exists(_2_docs_item))
                    Directory.CreateDirectory(_2_docs_item);

            if (!Directory.Exists(_3_out))
                Directory.CreateDirectory(_3_out);
            _3_out_all = ExtLibs.Select(p => _3_out + p + ".d.ts").ToArray();
        }



        public readonly static string ExtDocsPath = Path.Combine(ParentPath, @"jsduck\docs\");



        public readonly static string ExtDocsClassicPath = Path.Combine(ExtDocsPath, @"classic");
        public readonly static string ExtDocsClassicOutputPath = Path.Combine(ExtDocsClassicPath, @"output");
        public readonly static string ExtDocsClassicSourcePath = Path.Combine(ExtDocsClassicPath, @"source");

        public readonly static string ExtDocsModernPath = Path.Combine(ExtDocsPath, @"modern");
        public readonly static string ExtDocsModernOutputPath = Path.Combine(ExtDocsModernPath, @"output");
        public readonly static string ExtDocsModernSourcePath = Path.Combine(ExtDocsModernPath, @"source");

        public readonly static string ExtTsPath = Path.Combine(ParentPath, @"jsduck\ts\");
        public readonly static string ExtTsClassicPath = Path.Combine(ExtTsPath, @"classic.d.ts");
        public readonly static string ExtTsModernPath = Path.Combine(ExtTsPath, @"modern.d.ts");
    }
}
