using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Ozh.Utility.Reflection;
using Xunit;

namespace Ozh.Utility.Tests
{
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
    // ReSharper disable once ClassCanBeSealed.Global
    public class ExtensionsTests
    {
        [Fact]
        public void Should_Copy_Properties_As_Expected()
        {
            var source = new TestClass
            {
                Field = 2.0,
                IntProp = 15,
                LstProp = new[] {"a", "b"}
            };
            var destination = CreateDestination();
            var numberAffectedProperties = destination.CopyPublicProperties(source);
            Assert.Equal(2, numberAffectedProperties);
            Assert.Equal(3.0, destination.Field, 2);
            Assert.Equal(15, destination.IntProp);
            Assert.Equal(new List<string> {"a", "b"}, destination.LstProp);
        }

        [Fact]
        public void Should_Do_Nothing_When_Copying_From_Null()
        {
            var destination = CreateDestination();
            var numberAffectedProperties = destination.CopyPublicProperties(null);
            Assert.Equal(0, numberAffectedProperties);
            Assert.Equal(3.0, destination.Field, 2);
            Assert.Equal(30, destination.IntProp);
            Assert.Null(destination.LstProp);
        }


        [Fact]
        public void Should_Copy_Swallow_Objects_In_Props()
        {
            var source = new ComplexClass
            {
                Prop1 = new TestClass {Field = 1, IntProp = 2, LstProp = new string[] { }},
                PropList = new List<TestClass>
                    {new TestClass {Field = 4, IntProp = 5, LstProp = new List<string> {"a"}}}
            };

            var destination = new ComplexClass();
            var numberAffectedProperties = destination.CopyPublicProperties(source);
            Assert.Equal(2, numberAffectedProperties);
            Assert.Same(source.Prop1, destination.Prop1);
            Assert.Same(source.PropList.ToArray()[0], destination.PropList.ToArray()[0]);
        }

        private static TestClass CreateDestination()
        {
            var destination = new TestClass
            {
                Field = 3.0,
                IntProp = 30,
                LstProp = null
            };
            return destination;
        }

        private sealed class ComplexClass
        {
            public TestClass Prop1 { get; set; }

            public IEnumerable<TestClass> PropList { get; set; }
        }

        private sealed class TestClass
        {
            public double Field;
            public int IntProp { get; set; }

            public bool BoolProb { get; }

            private float FloatProp { get; set; } = 3;

            private float SecondFloatProp { get; set; } = 4;

            public IEnumerable LstProp { get; set; }

            public static short StaticProperty { get; set; } = 23;
        }
    }
}