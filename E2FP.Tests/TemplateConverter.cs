using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E2FP.Tests
{
    [TestClass]
    public class TemplateConverter
    {
        [TestMethod]
        public void TestConvert()
        {
            E2FP.Tpl.TemplateConverter Converter = new Tpl.TemplateConverter();

            Converter.InputHeaders = new string[]
            {
                "Recipient",
                "Recipient Address line 1",
                "Recipient Address line 2",
                "Recipient Postcode",
                "Recipient Post Town",
                "Recipient Country",
                "Reference"
            };

            Converter.OutputHeaders = new string[]
            {
                "OrderId",
                "PostalServiceName",
                "Weight",
                "Date",
                "FullName",
                "Address1",
                "Address2",
                "Address3",
                "PostCode",
                "Country",
                "FPMID",
                "Format"
            };

            Converter.TemplateConvert = new Tpl.Template();

            Converter.TemplateConvert.Templatea = new Dictionary<string, string>()
            {
                {"OrderId","%Reference"},
                {"PostalServiceName",""},
                {"Weight","350g"},
                {"Date","$Date"},
                {"FullName","%Recipient"},
                {"Address1","%Recipient Address line 1"},
                {"Address2","$BCheck"},
                {"Address3","%CCheck"},
                {"PostCode","%Recipient Postcode"},
                {"Country","%Recipient Country"},
                {"FPMID","31"},
                {"Format","F or P"}
            };


            Converter.convert();
            
        }
    }
}
