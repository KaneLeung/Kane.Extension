using Kane.Extension;

namespace ExtensionTest
{
    [TestClass]
    public class DateTimeExtensionTest
    {
        [TestMethod]
        public void MinTest()
        {
            DateTime min = DateTime.MinValue;
            DateTime mid = new(2024, 08, 30);
            DateTime max = DateTime.MaxValue;
            DateTime? nullableMin = null;
            DateTime? nullableMin2 = DateTime.MinValue;
            DateTime? nullableMid = null;
            DateTime? nullableMid2 = new(2024, 08, 30);
            DateTime? nullableMax = null;
            DateTime? nullableMax2 = DateTime.MaxValue;
            //两个不可空比较
            Assert.AreEqual(DateTimeExtension.Min(min, max), min);
            Assert.AreEqual(DateTimeExtension.Min(max, min), min);
            //三个不可空比较
            Assert.AreEqual(DateTimeExtension.Min(min, mid, max), min);
            Assert.AreEqual(DateTimeExtension.Min(min, max, mid), min);
            Assert.AreEqual(DateTimeExtension.Min(mid, min, max), min);
            Assert.AreEqual(DateTimeExtension.Min(mid, max, min), min);
            Assert.AreEqual(DateTimeExtension.Min(max, mid, min), min);
            Assert.AreEqual(DateTimeExtension.Min(max, min, mid), min);
            //两个可空比较
            Assert.IsNull(DateTimeExtension.Min(nullableMin, nullableMax));
            Assert.AreEqual(DateTimeExtension.Min(nullableMin2, nullableMax), nullableMin2);
            Assert.IsNull(DateTimeExtension.Min(nullableMax, nullableMin));
            Assert.AreEqual(DateTimeExtension.Min(nullableMax, nullableMin2), nullableMin2);
            //两个可空比较(有值)
            Assert.AreEqual(DateTimeExtension.Min(nullableMin2, nullableMax2), nullableMin2);
            Assert.AreEqual(DateTimeExtension.Min(nullableMax2, nullableMin2), nullableMin2);
            //三个可空比较(无值)
            Assert.IsNull(DateTimeExtension.Min(nullableMin, nullableMid, nullableMax));
            Assert.IsNull(DateTimeExtension.Min(nullableMin, nullableMax, nullableMid));
            Assert.IsNull(DateTimeExtension.Min(nullableMid, nullableMin, nullableMax));
            Assert.IsNull(DateTimeExtension.Min(nullableMid, nullableMax, nullableMin));
            Assert.IsNull(DateTimeExtension.Min(nullableMax, nullableMin, nullableMid));
            Assert.IsNull(DateTimeExtension.Min(nullableMax, nullableMid, nullableMin));
            //三个可空比较(有值)
            Assert.AreEqual(DateTimeExtension.Min(nullableMin2, nullableMid2, nullableMax2), nullableMin2);
            Assert.AreEqual(DateTimeExtension.Min(nullableMin2, nullableMax2, nullableMid2), nullableMin2);
            Assert.AreEqual(DateTimeExtension.Min(nullableMid2, nullableMin2, nullableMax2), nullableMin2);
            Assert.AreEqual(DateTimeExtension.Min(nullableMid2, nullableMax2, nullableMin2), nullableMin2);
            Assert.AreEqual(DateTimeExtension.Min(nullableMax2, nullableMin2, nullableMid2), nullableMin2);
            Assert.AreEqual(DateTimeExtension.Min(nullableMax2, nullableMid2, nullableMin2), nullableMin2);
        }

        [TestMethod]
        public void TestDateTimeEnd()
        {
            var now = new DateTime(2025, 5, 20, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            Assert.AreEqual(new DateTime(2025, 5, 19, 23, 59, 59), now.LastDayEnd());
            Assert.AreEqual(new DateTime(2025, 5, 20, 23, 59, 59), now.DayEnd());
            Assert.AreEqual(new DateTime(2025, 5, 21, 23, 59, 59), now.NextDayEnd());

            Assert.AreEqual(new DateTime(2025, 4, 30, 23, 59, 59), now.LastMonthEnd());
            Assert.AreEqual(new DateTime(2025, 5, 31, 23, 59, 59), now.MonthEnd());
            Assert.AreEqual(new DateTime(2025, 6, 30, 23, 59, 59), now.NextMonthEnd());
        }
    }
}
