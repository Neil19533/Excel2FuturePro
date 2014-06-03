using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace E2FP.Tests
{
    [TestClass]
    public class CSV
    {
        

        [TestMethod]
        public void CSVParserOneLine()
        {
            E2FP.CSVLoader.CSV Csv = new CSVLoader.CSV();
            var res = Csv.Parse("a,b,c");

            List<List<string>> a = new List<List<string>>() { new List<string>(){ "a", "b", "c" } };

            Assert.IsTrue(res.Count == a.Count, "Csv parser returning wrong amount of rows");
            Assert.IsTrue(res[0].Count == a[0].Count, "Csv parser returning wrong number of cells");
            Assert.IsTrue(res[0][0] == "a", "Csv parser returning wrong cell data.");
            Assert.IsTrue(res[0][1] == "b", "Csv parser returning wrong cell data.");
            Assert.IsTrue(res[0][2] == "c", "Csv parser returning wrong cell data.");
        }

        [TestMethod]
        public void CSVParserOneLineWithNewLineInCell()
        {
            E2FP.CSVLoader.CSV Csv = new CSVLoader.CSV();
            var res = Csv.Parse("a,\"b"+Environment.NewLine+"\",c");

            List<List<string>> a = new List<List<string>>() { new List<string>() { "a", "\"b"+Environment.NewLine+"\"", "c" } };

            Assert.IsTrue(res.Count == a.Count, "Csv parser returning wrong amount of rows");
            Assert.IsTrue(res[0].Count == a[0].Count, "Csv parser returning wrong number of cells");
            Assert.IsTrue(res[0][0] == "a", "Csv parser returning wrong cell data.");
            Assert.IsTrue(res[0][1] == "b"+Environment.NewLine, "Csv parser returning wrong cell data.");
            Assert.IsTrue(res[0][2] == "c", "Csv parser returning wrong cell data.");
        }

        [TestMethod]
        public void CSVParsertwoLineWithNewLineInCell()
        {
            E2FP.CSVLoader.CSV Csv = new CSVLoader.CSV();
            var res = Csv.Parse("a,b,c" + Environment.NewLine + "d,e,f");

            List<List<string>> a = new List<List<string>>() { new List<string>() { "a", "b", "c" }, new List<string>() { "d", "e", "f" } };

            Assert.IsTrue(res.Count == a.Count, "Csv parser returning wrong amount of rows");
            Assert.IsTrue(res[0].Count == a[0].Count, "Csv parser returning wrong number of cells");
            Assert.IsTrue(res[0][0] == "a", "Csv parser returning wrong cell data.");
            Assert.IsTrue(res[0][1] == "b", "Csv parser returning wrong cell data.");
            Assert.IsTrue(res[0][2] == "c", "Csv parser returning wrong cell data.");
            Assert.IsTrue(res[1][0] == "d", "Csv parser returning wrong cell data.");
            Assert.IsTrue(res[1][1] == "e", "Csv parser returning wrong cell data.");
            Assert.IsTrue(res[1][2] == "f", "Csv parser returning wrong cell data.");
        }
    }
}
