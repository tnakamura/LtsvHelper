using System;
using System.Runtime.Serialization;
using System.Text;
using System.IO;
using Xunit;

namespace LtsvHelper.Tests
{
    public class LtsvWriterTests
    {
        [Fact]
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

            Assert.Equal("name:ichiro\tage:42\r\n", sb.ToString());
        }

        [Fact]
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

            Assert.Equal("name:kagawa\tage:26\r\n", sb.ToString());
        }

        [Fact]
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

            Assert.Equal("name:kagawa\tage:26\r\nname:honda\tage:31\r\n", sb.ToString());
        }

        [Fact]
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

            Assert.Equal("Number:10\tName:Kagawa\tPosition:MF\r\n", sb.ToString());
        }

        [Fact]
        public void WriteRecordDataMemberTest()
        {
            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            using (var ltsvWriter = new LtsvWriter(stringWriter))
            {
                ltsvWriter.WriteRecord(new Player()
                {
                    Number = 10,
                    Name = "Kagawa",
                    Position = "MF",
                });
            }

            Assert.Equal("number:10\tname:Kagawa\tposition:MF\r\n", sb.ToString());
        }

        class Player
        {
            [DataMember(Name = "number")]
            public int Number { get; set; }

            [DataMember(Name = "name")]
            public string Name { get; set; }

            [DataMember(Name = "position")]
            public string Position { get; set; }
        }
    }
}
