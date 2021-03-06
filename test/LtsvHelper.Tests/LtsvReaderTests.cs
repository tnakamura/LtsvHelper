﻿using LtsvHelper.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace LtsvHelper.Tests
{
    public class LtsvReaderTests
    {
        [Fact]
        public void ReadTest()
        {
            var ltsv = "name:kagawa\tage:26\nname:honda\tage:31\n";
            using (var stringReader = new StringReader(ltsv))
            using (var ltsvReader = new LtsvReader(stringReader))
            {
                Assert.True(ltsvReader.Read());
                Assert.True(ltsvReader.Read());
                Assert.False(ltsvReader.Read());
            }
        }

        [Fact]
        public async Task ReadAsyncTest()
        {
            var ltsv = "name:kagawa\tage:26\nname:honda\tage:31\n";
            using (var stringReader = new StringReader(ltsv))
            using (var ltsvReader = new LtsvReader(stringReader))
            {
                Assert.True(await ltsvReader.ReadAsync());
                Assert.True(await ltsvReader.ReadAsync());
                Assert.False(await ltsvReader.ReadAsync());
            }
        }

        [Fact]
        public void GetFieldTest()
        {
            var ltsv = "name:kagawa\tage:26\nname:honda\tage:31\n";
            using (var stringReader = new StringReader(ltsv))
            using (var ltsvReader = new LtsvReader(stringReader))
            {
                Assert.True(ltsvReader.Read());
                Assert.Equal("kagawa", ltsvReader.GetField("name"));
                Assert.Equal("26", ltsvReader.GetField("age"));
                Assert.True(ltsvReader.Read());
                Assert.Equal("31", ltsvReader.GetField("age"));
                Assert.Equal("honda", ltsvReader.GetField("name"));
                Assert.False(ltsvReader.Read());
            }
        }

        [Fact]
        public void GetFieldTTest()
        {
            var ltsv = "name:kagawa\tage:26\nname:honda\tage:31\n";
            using (var stringReader = new StringReader(ltsv))
            using (var ltsvReader = new LtsvReader(stringReader))
            {
                Assert.True(ltsvReader.Read());
                Assert.Equal("kagawa", ltsvReader.GetField<string>("name"));
                Assert.Equal(26, ltsvReader.GetField<int>("age"));
                Assert.True(ltsvReader.Read());
                Assert.Equal(31, ltsvReader.GetField<int>("age"));
                Assert.Equal("honda", ltsvReader.GetField<string>("name"));
                Assert.False(ltsvReader.Read());
            }
        }

        [Fact]
        public void GetFieldTimeTest()
        {
            var ltsv = "begin:12:00:00\tend:13:00:00\nbegin:14:00:00\tend:15:00:00\n";
            using (var stringReader = new StringReader(ltsv))
            using (var ltsvReader = new LtsvReader(stringReader))
            {
                Assert.True(ltsvReader.Read());
                Assert.Equal("12:00:00", ltsvReader.GetField("begin"));
                Assert.Equal("13:00:00", ltsvReader.GetField("end"));
                Assert.True(ltsvReader.Read());
                Assert.Equal("14:00:00", ltsvReader.GetField("begin"));
                Assert.Equal("15:00:00", ltsvReader.GetField("end"));
                Assert.False(ltsvReader.Read());
            }
        }

        [Fact]
        public void GetRecordTTest()
        {
            var ltsv = "Name:kagawa\tNumber:10\tPosition:MF\r\n";
            using (var stringReader = new StringReader(ltsv))
            using (var ltsvReader = new LtsvReader(stringReader))
            {
                Assert.True(ltsvReader.Read());

                var record = ltsvReader.GetRecord<Player>();
                Assert.Equal("kagawa", record.Name);
                Assert.Equal(10, record.Number);
                Assert.Equal("MF", record.Position);
            }
        }

        [Fact]
        public void GetRecordsTTest()
        {
            var ltsv = "Name:kagawa\tNumber:10\tPosition:MF\r\n"
                + "Number:4\tName:honda\tPosition:MF\r\n"
                + "Position:FW\tNumber:9\tName:okazaki\r\n";
            using (var stringReader = new StringReader(ltsv))
            using (var ltsvReader = new LtsvReader(stringReader))
            {
                var records = ltsvReader.GetRecords<Player>().ToList();
                Assert.Equal(3, records.Count);

                Assert.Equal("kagawa", records[0].Name);
                Assert.Equal(10, records[0].Number);
                Assert.Equal("MF", records[0].Position);

                Assert.Equal("honda", records[1].Name);
                Assert.Equal(4, records[1].Number);
                Assert.Equal("MF", records[1].Position);

                Assert.Equal("okazaki", records[2].Name);
                Assert.Equal(9, records[2].Number);
                Assert.Equal("FW", records[2].Position);
            }
        }

        [Fact]
        public void FluentClassMappingTest()
        {
            var configuration = new LtsvConfiguration();
            configuration.RegisterClassMap<PlayerMap>();

            var ltsv = "name:kagawa\tNumber:10\tPosition:MF\r\n";
            using (var stringReader = new StringReader(ltsv))
            using (var ltsvReader = new LtsvReader(stringReader, configuration))
            {
                Assert.True(ltsvReader.Read());

                var record = ltsvReader.GetRecord<Player>();
                Assert.Equal("kagawa", record.Name);
                Assert.Equal(10, record.Number);
                Assert.Null(record.Position);
            }
        }

        [Fact]
        public void UnregisterClassMapTest()
        {
            var configuration = new LtsvConfiguration();
            configuration.RegisterClassMap<PlayerMap>();
            configuration.UnregisterClassMap<PlayerMap>();

            var ltsv = "name:kagawa\tNumber:10\tPosition:MF\r\n";
            using (var stringReader = new StringReader(ltsv))
            using (var ltsvReader = new LtsvReader(stringReader, configuration))
            {
                Assert.True(ltsvReader.Read());

                var record = ltsvReader.GetRecord<Player>();
                Assert.Null(record.Name);
                Assert.Equal(10, record.Number);
                Assert.Equal("MF", record.Position);
            }
        }

        [Fact]
        public void GetField_throws_LtsvReaderException_when_has_not_been_read()
        {
            var ltsv = "Name:kagawa\tNumber:10\tPosition:MF\r\n";
            using (var stringReader = new StringReader(ltsv))
            using (var ltsvReader = new LtsvReader(stringReader))
            {
                Assert.Throws<LtsvReaderException>(() =>
                {
                    ltsvReader.GetField("Name");
                });
            }
        }

        [Fact]
        public void GetFieldT_throws_LtsvReaderException_when_has_not_been_read()
        {
            var ltsv = "Name:kagawa\tNumber:10\tPosition:MF\r\n";
            using (var stringReader = new StringReader(ltsv))
            using (var ltsvReader = new LtsvReader(stringReader))
            {
                Assert.Throws<LtsvReaderException>(() =>
                {
                    ltsvReader.GetField<int>("Number");
                });
            }
        }

        [Fact]
        public void GetRecord_throws_LtsvReaderException_when_has_not_been_read()
        {
            var ltsv = "Name:kagawa\tNumber:10\tPosition:MF\r\n";
            using (var stringReader = new StringReader(ltsv))
            using (var ltsvReader = new LtsvReader(stringReader))
            {
                Assert.Throws<LtsvReaderException>(() =>
                {
                    ltsvReader.GetRecord<Player>();
                });
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
                Map(p => p.Number);
                Map(p => p.Name).Label("name");
            }
        }
    }
}
