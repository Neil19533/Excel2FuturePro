using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2FP.CSVLoader
{
    public class CSV
    {

        public List<List<string>> ParseFile(string FileName)
        {
            //open text file
            String rawtext = System.IO.File.OpenText(FileName).ReadToEnd();

            //parse char by char
            return Parse(rawtext);

        }

        public List<List<string>> Parse(string RawText)
        {
            List<List<string>> table = new List<List<string>>();

            List<string> line = new List<string>();
            string cell = "";

            bool Escaped = false;
            bool EscapeNext = false;

            for (int i = 0; i < RawText.Length; i++)
			{
			 
                char character = RawText[i];
            
                if (!Escaped)
                {
                    if (EscapeNext)
                    {
                        EscapeNext = false;
                    }
                    else if (isQuote(character))
                    {
                        Escaped = true;
                    }
                    else if (character.Equals(','))
                    {
                        line.Add(cell);
                        cell = "";
                    }
                    else if (Environment.NewLine.ToCharArray().Contains(character))
                    {
                        if (RawText[i - 1] == '\r')
                        {
                            line.Add(cell);
                            cell = "";
                            var dupLin = new List<string>();
                           dupLin.InsertRange(0,line.ToArray());
                            table.Add(dupLin);
                            line.Clear();
                        }

                    }
                    else if (character.Equals('\\'))
                    {
                        EscapeNext = true;
                    }
                    else
                    {
                        cell += character;
                    }
                }
                else
                {
                    if (isQuote(character))
                    {
                        Escaped = false;
                    }
                    else
                    {
                        cell += character;
                    }
                }


            }

            line.Add(cell);
            table.Add(line);

            return table;
        }

        private bool isQuote(char character)
        {
            return (character.Equals('"')); 
        }

        public void SaveFile(System.Data.DataTable ConvertedTemplate)
        {
            throw new NotImplementedException();
        }


        public void SaveCSV(List<List<string>> data, string filename)
        {
            StringBuilder CsvFile = new StringBuilder();
            data.RemoveAt(1);
            foreach (var row in data)
            {
                for (int i = 0; i < row.Count; i++)
                {
                    string cell = row[i];

                    CsvFile.Append("\"" + cell + "\"");
                    if (i<row.Count-1)
                    {
                        CsvFile.Append(",");
                        
                    }
                }

                CsvFile.AppendLine();
            }

            System.IO.File.WriteAllText(filename, CsvFile.ToString());

        }
    }
}
