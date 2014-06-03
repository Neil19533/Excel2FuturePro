using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E2FP.CSVLoader;
using System.Data;
using System.Net;
using System.IO;
namespace Excel2FuturePro
{
    class Program
    {


        static void Main(string[] args)
        {

            var converter = new E2FP.Tpl.TemplateConverter();
            var csvLoader = new E2FP.CSVLoader.CSV();


            string InFolder = @"C:\Template\In\";
            string OutFolder = @"C:\Template\Out\";
            string templates = @"C:\Template\Templates\";


            var InputTemplateHeaders = csvLoader.ParseFile(templates + "input.csv");
            var OutputTemplateHeaders = csvLoader.ParseFile(templates + "output.csv");
            var ConverterTemplate = csvLoader.ParseFile(templates + "converter.csv");


            converter.InputHeaders = InputTemplateHeaders[0].ToArray();
            converter.OutputHeaders = OutputTemplateHeaders[0].ToArray();

            E2FP.Tpl.Template template = new E2FP.Tpl.Template();

            template.Populate(ConverterTemplate);

            converter.TemplateConvert = template;


            foreach (var InputFile in System.IO.Directory.GetFiles(InFolder))
            {
                
                var inputfile = csvLoader.ParseFile(InputFile);

                converter.Data = inputfile;

                converter.convert();

                csvLoader.SaveCSV(converter.Output, OutFolder + System.IO.Path.GetFileNameWithoutExtension(InputFile)+DateTime.Now.TimeOfDay.ToString("mmssfff")+".csv");

            }


            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(@"ftp://innox.co.uk:21");
            request.Method = WebRequestMethods.Ftp.UploadFile;

            request.Credentials = new NetworkCredential("neil", "123456");

            foreach (var item in System.IO.Directory.GetFiles(OutFolder))
            {
                StreamReader sourceStream = new StreamReader(item);
                byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                sourceStream.Close();

                request.ContentLength = fileContents.Length;

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);
                requestStream.Close();

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                response.Close();
            }


        }
    }
}
