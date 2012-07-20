namespace Sol2Reg.Test.DataObject
{
	using System.Collections.Generic;
	using FluentAssertions;
	using Sol2Reg.DataObject;
	using Xunit;
	using Xunit.Extensions;

	public class DigitalValueTest
	{
		private readonly DigitalValue testee;
		public static IEnumerable<object[]> TestValueForEqualProperty
		{
			get
			{
				yield return new object[] { new DigitalValue(true), new DigitalValue(true), true };
				yield return new object[] { new DigitalValue(true), new DigitalValue(false), false };
				yield return new object[] { new DigitalValue(false), new DigitalValue(false), true };
				yield return new object[] { new DigitalValue(false), new DigitalValue(true), false };
				yield return new object[] { new DigitalValue(false), (DigitalValue)null, false };
			}
		}

		public DigitalValueTest()
		{
			this.testee = new DigitalValue(false);
		}

		[Fact]
		public void DigitalValueWhenSetTrueThenTrue()
		{
			this.testee.Value = true;

			this.testee.Value.Should().BeTrue("Value set to true.");
		}

		[Theory]
		[PropertyData("TestValueForEqualProperty")]
		public void EqualTest(DigitalValue value1, DigitalValue value2, bool response)
		{
			value1.Equals(value2).Should().Be(response, "Value1 {0}, Value2 {1} => respone {2}", value1, value2, response);
		}

		[Theory]
		[PropertyData("TestValueForEqualProperty")]
		public void Equalitytest(DigitalValue value1, DigitalValue value2, bool response)
		{
			(value1 == value2).Should().Be(response, "Value1 {0}, Value2 {1} => respone {2}", value1, value2, response);
		}

		[Theory]
		[PropertyData("TestValueForEqualProperty")]
		public void NotEqualitytest(DigitalValue value1, DigitalValue value2, bool response)
		{
			(value1 != value2).Should().Be(!response, "Value1 {0}, Value2 {1} => respone {2}", value1, value2, !response);
		}
	}
}