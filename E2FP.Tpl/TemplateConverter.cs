using E2FP.CSVLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace E2FP.Tpl
{
    public  class TemplateConverter
    {
        public CSV CsvLoader { get; set; }


        public List<List<string>> Data { get; set; }

        public string[] InputHeaders { get; set; }

        public string[] OutputHeaders { get; set; }

        public Template TemplateConvert { get; set; }

        public List<List<string>> Output { get; set; }


        public TemplateConverter()
        {
            CsvLoader = new CSV();


            //load input file.
            //CsvLoader.ParseFile(inputFilename);

            // validate input headers.
            validateHeaders();



            //Load templatecnverter
            //TemplateConvert.Load(convertTemplate);

            // convert
            //convert();
        }

        public void load()
        {
        }



        public void validateHeaders()
        {
           
        }

        public void convert()
        {
            List<List<string>> table = new List<List<string>>();

            List<string> header = new List<string>();
            header.AddRange(OutputHeaders);

            table.Add(header);

            foreach (List<string> item in Data)
            {
                if (item.Count == InputHeaders.Length)
                {
                    System.Collections.Generic.List<string> Row = new List<string>();

                    foreach (var itemv in TemplateConvert.Templatea)
                    {


                        if (itemv.Value.StartsWith("%"))
                        {
                            Row.Add(LookupInput(item, itemv.Value.Substring(1, itemv.Value.Length - 1)));
                        }
                        else if (itemv.Value.StartsWith("$"))
                        {
                            Row.Add(ExeCommand(itemv.Value.Substring(1, itemv.Value.Length - 1), item));
                        }
                        else
                        {
                            Row.Add(itemv.Value);
                        }

                    }

                    table.Add(Row);
                }
            }
            
            Output = table;


            //CsvLoader.SaveCSV(table, "");

        }

        private string LookupInput(List<string> row, string lookup)
        {
            for (int i = 0; i < InputHeaders.Length; i++)
            {
                if (InputHeaders[i].Equals(lookup))
                {
                    return row[i];
                }
            }
            return "";
        }

        private string ExeCommand(string command, List<string> data)
        {
            switch (command)
            {
                case "Date":
                    return DateTime.Now.Date.ToLongDateString();
                case "BCheck":
                    if (string.IsNullOrWhiteSpace(data[2]))
                    {
                        return data[4];
                    }
                    else
                    {
                        return data[2];
                    }
                case "CCheck":
                    if (string.IsNullOrWhiteSpace(data[2]))
                    {
                        return "";
                    }
                    else
                    {
                        return data[4];
                    }
                default:
                    return "";
            }

        }


    }
}
