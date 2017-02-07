using LtsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
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
                ltsvWriter.WriteRecord(new Player()
                {
                    Number = 10,
                    Name = "Kagawa",
                    Position = "MF",
                });
            }

            Assert.Equal("Number:10\tName:Kagawa\tPosition:MF\r\n", sb.ToString());
        }

        [Fact]
        public void FluentClassMappingTest()
        {
            var configuration = new LtsvConfiguration();
            configuration.RegisterClassMap<PlayerMap>();

            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            using (var ltsvWriter = new LtsvWriter(stringWriter, configuration))
            {
                ltsvWriter.WriteRecord(new Player()
                {
                    Number = 10,
                    Name = "Kagawa",
                    Position = "MF",
                });
            }

            Assert.Equal("Name:Kagawa\tNo:10\r\n", sb.ToString());
        }

        [Fact]
        public void UnregisterClassMapTest()
        {
            var configuration = new LtsvConfiguration();
            configuration.RegisterClassMap<PlayerMap>();
            configuration.UnregisterClassMap<PlayerMap>();

            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            using (var ltsvWriter = new LtsvWriter(stringWriter, configuration))
            {
                ltsvWriter.WriteRecord(new Player()
                {
                    Number = 10,
                    Name = "Kagawa",
                    Position = "MF",
                });
            }

            Assert.Equal("Number:10\tName:Kagawa\tPosition:MF\r\n", sb.ToString());
        }

        [Fact]
        public void WriteRecordsTest()
        {
            var players = new List<Player>()
            {
                new Player()
                {
                    Number = 10,
                    Name = "Kagawa",
                    Position = "MF",
                },
                new Player()
                {
                    Number = 4,
                    Name = "Honda",
                    Position = "MF",
                },
            };

            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter(sb))
            using (var ltsvWriter = new LtsvWriter(stringWriter))
            {
                ltsvWriter.WriteRecords(players);
            }

            Assert.Equal(
                "Number:10\tName:Kagawa\tPosition:MF\r\nNumber:4\tName:Honda\tPosition:MF\r\n",
                sb.ToString());
        }

        [Fact]
        public void NextRecord_throws_LtsvWriterException_when_an_unexpected_error_occurred()
        {
            using (var textWriter = new DummyTextWriter())
            using (var ltsvWriter = new LtsvWriter(textWriter))
            {
                ltsvWriter.WriteField("foo", "bar");
                Assert.Throws<LtsvWriterException>(() =>
                {
                    ltsvWriter.NextRecord();
                });
            }
        }

        class DummyTextWriter : TextWriter
        {
            public override Encoding Encoding => Encoding.UTF8;

            public override void Write(char value)
            {
                throw new IOException("error");
            }
        }

        class Player
        {
            public int Number { get; set; }

            public string Name { get; set; }

            public string Position { get; set; }
        }

        class PlayerMap : LtsvClassMap<Player>
        {
            public PlayerMap()
                : base()
            {
                Map(p => p.Name);
                Map(p => p.Number).Label("No");
            }
        }
    }
}
