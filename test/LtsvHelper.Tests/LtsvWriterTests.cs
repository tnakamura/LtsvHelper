using System;
using System.Text;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LtsvHelper.Tests
{
    [TestClass]
    public class LtsvWriterTests
    {
        [TestMethod]
        public void WriteFieldTest()
        {
            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            using (var ltsvWriter = new LtsvWriter(stringWriter))
            {
                ltsvWriter.WriteField("name", "ichiro");
                ltsvWriter.WriteField("age", "42");
                ltsvWriter.NextRecord();
            }

            StringAssert.Contains(sb.ToString(), "name:ichiro\tage:42");
        }

        [TestMethod]
        public void WriteFieldTTest()
        {
            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            using (var ltsvWriter = new LtsvWriter(stringWriter))
            {
                ltsvWriter.WriteField<string>("name", "kagawa");
                ltsvWriter.WriteField("age", 26);
                ltsvWriter.NextRecord();
            }

            Assert.AreEqual(sb.ToString(), "name:kagawa\tage:26\r\n");
        }

        [TestMethod]
        public void NextRecordTest()
        {
            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            using (var ltsvWriter = new LtsvWriter(stringWriter))
            {
                ltsvWriter.WriteField("name", "kagawa");
                ltsvWriter.WriteField("age", 26);
                ltsvWriter.NextRecord();
                ltsvWriter.WriteField("name", "honda");
                ltsvWriter.WriteField("age", 31);
                ltsvWriter.NextRecord();
            }

            Assert.AreEqual(sb.ToString(), "name:kagawa\tage:26\r\nname:honda\tage:31\r\n");
        }

        [TestMethod]
        public void WriteRecordTest()
        {
            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            using (var ltsvWriter = new LtsvWriter(stringWriter))
            {
                ltsvWriter.WriteRecord(new
                {
                    Number = 10,
                    Name = "Kagawa",
                    Position = "MF",
                });
            }

            Assert.AreEqual(sb.ToString(), "Number:10\tName:Kagawa\tPosition:MF\r\n");
        }
    }
}
