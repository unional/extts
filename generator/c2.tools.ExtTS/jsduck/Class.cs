using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c2.tools.ExtTS.jsduck
{
    [Serializable]
    public sealed class Class
    {
        #region Properties

        public string tagname;
        public string name;
        public string[] alternateClassNames;
        public string extends;
        public bool? singleton;
        public Member[] members;
        public string[] mixins;
        public ClassEnum @enum;

        public ClassFile[] files;
        public Dictionary<string, HtmlDocument> docs;
        public string code_type;
        public string id;
        public bool component;
        public string short_doc;
        public string html;

        public Member[] OwnMembers
        {
            get { return this.ownMembers ?? (this.ownMembers = this.members.Where(m => this.name == m.owner && !m.meta.@private).ToArray()); }
        }
        private Member[] ownMembers;

        public string Href
        {
            get { return this.href ?? (this.href = name.Replace('.', '-')); }
        }
        private string href;

        public override string ToString()
        {
            return this.name;
        }

        #endregion

        #region Builder

        public void Initialize(Dictionary<string, Class> classMap, Dictionary<string, HtmlDocument> jsFileHtmlMap)
        {
            var docs = new Dictionary<string, HtmlDocument>(this.files.Distinct(ClassFile.Comparer.Default).ToDictionary(f => f.LocalFile, f => jsFileHtmlMap[f.LocalFile]));

            if (!String.IsNullOrEmpty(this.html))
            {
                var htmlDoc = new HtmlDocument();
                if (htmlDoc != null)
                    htmlDoc.LoadHtml(this.html);
                foreach (var member in this.OwnMembers)
                {
                    var aViewSource = htmlDoc.DocumentNode.SelectSingleNode($@"//div[@id = '{member.id}']").Descendants("a").Where(a => a.GetAttributeValue("class", null) == "view-source" && !String.IsNullOrEmpty(a.GetAttributeValue("href", null))).SingleOrDefault();
                    if (aViewSource != null)
                    {
                        var href = aViewSource.GetAttributeValue("href", null);
                        var pos = href.IndexOf('#');
                        href = pos > 0 ? href.Substring(0, pos) : href;
                        pos = href.LastIndexOf('/');
                        href = pos > 0 ? href.Substring(pos + 1) : href;
                        if (!docs.ContainsKey(href) && jsFileHtmlMap.ContainsKey(href))
                            docs.Add(href, jsFileHtmlMap[href]);
                    }
                }
            }
            this.docs = docs;
            foreach (var member in this.OwnMembers)
                member.Initialize(classMap, this);
        }

        #endregion
    }

    #region Class classes

    [Serializable]
    public sealed class ClassFile
    {
        public string filename;
        public string href;

        public string LocalFile
        {
            get { return this.localfile ?? (this.localfile = this.href.Substring(0, this.href.IndexOf('#'))); }
        }
        private string localfile;

        public override string ToString()
        {
            return this.LocalFile;
        }

        public class Comparer : IEqualityComparer<ClassFile>
        {
            public readonly static Comparer Default = new Comparer();

            private Comparer() { }

            public bool Equals(ClassFile x, ClassFile y)
            {
                return x.filename == y.filename && x.href == y.href;
            }

            public int GetHashCode(ClassFile obj)
            {
                return obj.filename.GetHashCode() ^ obj.href.GetHashCode();
            }
        }
    }

    [Serializable]
    public sealed class ClassEnum
    {
        public string type;
        public string @default;
        public bool doc_only;
    }

    #endregion
}
