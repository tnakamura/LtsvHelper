using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LtsvHelper.Tests
{
    [TestClass]
    public class LtsvReaderTests
    {
        [TestMethod]
        public void ReadTest()
        {
            var ltsv = "name:kagawa\tage:26\nname:honda\tage:31\n";
            using (var stringReader = new StringReader(ltsv))
            using (var ltsvReader = new LtsvReader(stringReader))
            {
                Assert.IsTrue(ltsvReader.Read());
                Assert.IsTrue(ltsvReader.Read());
                Assert.IsFalse(ltsvReader.Read());
            }
        }

        [TestMethod]
        public void GetFieldTest()
        {
            var ltsv = "name:kagawa\tage:26\nname:honda\tage:31\n";
            using (var stringReader = new StringReader(ltsv))
            using (var ltsvReader = new LtsvReader(stringReader))
            {
                Assert.IsTrue(ltsvReader.Read());
                Assert.AreEqual("kagawa", ltsvReader.GetField("name"));
                Assert.AreEqual("26", ltsvReader.GetField("age"));
                Assert.IsTrue(ltsvReader.Read());
                Assert.AreEqual("31", ltsvReader.GetField("age"));
                Assert.AreEqual("honda", ltsvReader.GetField("name"));
                Assert.IsFalse(ltsvReader.Read());
            }
        }

        [TestMethod]
        public void GetFieldTTest()
        {
            var ltsv = "name:kagawa\tage:26\nname:honda\tage:31\n";
            using (var stringReader = new StringReader(ltsv))
            using (var ltsvReader = new LtsvReader(stringReader))
            {
                Assert.IsTrue(ltsvReader.Read());
                Assert.AreEqual("kagawa", ltsvReader.GetField<string>("name"));
                Assert.AreEqual(26, ltsvReader.GetField<int>("age"));
                Assert.IsTrue(ltsvReader.Read());
                Assert.AreEqual(31, ltsvReader.GetField<int>("age"));
                Assert.AreEqual("honda", ltsvReader.GetField<string>("name"));
                Assert.IsFalse(ltsvReader.Read());
            }
        }

        [TestMethod]
        public void GetRecordTTest()
        {
            var ltsv = "Name:kagawa\tNumber:10\tPosition:MF\r\n";
            using (var stringReader = new StringReader(ltsv))
            using (var ltsvReader = new LtsvReader(stringReader))
            {
                Assert.IsTrue(ltsvReader.Read());

                var record = ltsvReader.GetRecord<Player>();
                Assert.AreEqual("kagawa", record.Name);
                Assert.AreEqual(10, record.Number);
                Assert.AreEqual("MF", record.Position);
            }
        }

        [TestMethod]
        public void GetRecordsTTest()
        {
            var ltsv = "Name:kagawa\tNumber:10\tPosition:MF\r\n"
                + "Number:4\tName:honda\tPosition:MF\r\n"
                + "Position:FW\tNumber:9\tName:okazaki\r\n";
            using (var stringReader = new StringReader(ltsv))
            using (var ltsvReader = new LtsvReader(stringReader))
            {
                var records = ltsvReader.GetRecords<Player>().ToList();
                Assert.AreEqual(3, records.Count);

                Assert.AreEqual("kagawa", records[0].Name);
                Assert.AreEqual(10, records[0].Number);
                Assert.AreEqual("MF", records[0].Position);

                Assert.AreEqual("honda", records[1].Name);
                Assert.AreEqual(4, records[1].Number);
                Assert.AreEqual("MF", records[1].Position);

                Assert.AreEqual("okazaki", records[2].Name);
                Assert.AreEqual(9, records[2].Number);
                Assert.AreEqual("FW", records[2].Position);
            }
        }

        class Player
        {
            public int Number { get; set; }

            public string Name { get; set; }

            public string Position { get; set; }
        }
    }
}
