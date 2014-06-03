using E2FP.CSVLoader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2FP.Tpl
{
    public class Template
    {

        public Dictionary<String, String> Templatea { get; set; }


        #region Ctor

        public Template() { }

        public Template(string Filename)
        {
            this.Load(Filename: Filename);
        }

        #endregion


        /// <summary>
        /// Load a template from CSV
        /// </summary>
        /// <param name="Filename">File to load.</param>
        public void Load(string Filename)
        {
            CSV loader = new CSV();

            var TemplateTable = loader.ParseFile(Filename);

            Populate(TemplateTable);


        }

        public void Populate(List<List<string>> TemplateTable)
        {
            Templatea = new Dictionary<string, string>();

            for (int i = 0; i < TemplateTable[0].Count; i++)
            {
                Templatea.Add(TemplateTable[1][i].ToString(), TemplateTable[0][i].ToString());
            }
        }


    }
}
