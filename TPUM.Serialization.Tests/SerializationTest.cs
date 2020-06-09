using System;
using Xunit;

namespace TPUM.Serialization.Tests
{
    public class SerializationTest
    {

        class TestClass
        {
            public string StringValue { get; set; }
            public int IntValue { get; set; }
        }

        [Fact]
        public void SerializeTest()
        {
            TestClass test = new TestClass
            {
                IntValue = 111,
                StringValue = "222"
            };

            string serialized = Serializer.Serialize(test);
            Assert.Contains("StringValue", serialized);
            Assert.Contains("222", serialized);
        }

        [Fact]
        public void DeserializeTest()
        {
            TestClass test = new TestClass
            {
                IntValue = 111,
                StringValue = "222"
            };

            string serialized = Serializer.Serialize(test);
            TestClass deserialized = Serializer.Deserialize<TestClass>(serialized);
            Assert.Equal(111, deserialized.IntValue);
            Assert.Equal("222", deserialized.StringValue);
        }
    }
}
