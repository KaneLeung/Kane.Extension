using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kane.Extension.Test
{
    [TestClass]
    public class JsonHelper
    {
        [TestMethod]
        public void TestJsonHelper()
        {
            var jsonHelper = new Kane.Extension.JsonHelper(GlobalData.CustomOptionJsonString);
            var listIndex0 = jsonHelper.GetValue<Poco>("List:0");//即读取List元素里Index为0的元素
            Assert.AreEqual("Hello World", listIndex0.PocoValue);
            var longValue = jsonHelper.GetValue<long>("Long");
            Assert.AreEqual(99709394, longValue);
        }

        [TestMethod]
        public void TestJsonHelper2()
        {
            var level1 = new Level1()
            {
                Level1ID = 111,
                Level1Name = "Level1",
                Level1Child = new Level2()
                {
                    Level2ID = 222,
                    Level2Name = "Level2",
                    Level2Child = new Level3()
                    {
                        Level3ID = 333,
                        Level3Name = "Level3"
                    }
                },
                Level3List = new List<Level3>() { new Level3 { Level3ID = 3331, Level3Name = "Level31" }, new Level3 { Level3ID = 3332, Level3Name = "Level32" }, new Level3 { Level3ID = 3333, Level3Name = "Level33" } },
                Level2Array = new Level2[] { new Level2 { Level2ID = 2221, Level2Name = "Level21" }, new Level2 { Level2ID = 2222, Level2Name = "Level22" }, new Level2 { Level2ID = 2223, Level2Name = "Level23" } }
            };
            var json = "{\"Level1ID\":111,\"Level1Name\":\"Level1\",\"Level1Child\":{\"Level2ID\":222,\"Level2Name\":\"Level2\",\"Level2Child\":{\"Level3ID\":333,\"Level3Name\":\"Level3\"}},\"Level3List\":[{\"Level3ID\":3331,\"Level3Name\":\"Level31\"},{\"Level3ID\":3332,\"Level3Name\":\"Level32\"},{\"Level3ID\":3333,\"Level3Name\":\"Level33\"}],\"Level2Array\":[{\"Level2ID\":2221,\"Level2Name\":\"Level21\"},{\"Level2ID\":2222,\"Level2Name\":\"Level22\"},{\"Level2ID\":2223,\"Level2Name\":\"Level23\"}]}";
            var helper = new Extension.JsonHelper(json);
            Assert.AreEqual(level1.Level1ID, helper.GetValue<int>("Level1ID"));
            Assert.AreEqual(level1.Level1Name, helper.GetValue<string>("Level1Name"));
            Assert.AreEqual(level1.Level1Child.Level2ID, helper.GetValue<int>("Level1Child:Level2ID"));
            Assert.AreEqual(level1.Level1Child.Level2Name, helper.GetValue<string>("Level1Child:Level2Name"));
            Assert.AreEqual(level1.Level1Child.Level2Child.Level3ID, helper.GetValue<int>("Level1Child:Level2Child:Level3ID"));
            Assert.AreEqual(level1.Level1Child.Level2Child.Level3Name, helper.GetValue<string>("Level1Child:Level2Child:Level3Name"));
            Assert.AreEqual(level1.Level3List?.FirstOrDefault()?.Level3ID, helper.GetValue<Level3>("Level3List:0")?.Level3ID);
            Assert.AreEqual(level1.Level3List?.LastOrDefault()?.Level3ID, helper.GetValue<Level3>("Level3List:2")?.Level3ID);
            Assert.AreEqual(level1.Level2Array?.FirstOrDefault()?.Level2ID, helper.GetValue<Level2>("Level2Array:0")?.Level2ID);
            Assert.AreEqual(level1.Level2Array?.LastOrDefault()?.Level2ID, helper.GetValue<Level2>("Level2Array:2")?.Level2ID);
        }

        public class Level1
        {
            public int Level1ID { get; set; }
            public string Level1Name { get; set; }
            public Level2 Level1Child { get; set; }
            public List<Level3> Level3List { get; set; }
            public Level2[] Level2Array { get; set; }
        }

        public class Level2
        {
            public int Level2ID { get; set; }
            public string Level2Name { get; set; }
            public Level3 Level2Child { get; set; }
        }

        public class Level3
        {
            public int Level3ID { get; set; }
            public string Level3Name { get; set; }
        }
    }
}