namespace Sol2Reg.Test.DataObject
{
	using System;
	using System.Collections.Generic;
	using Sol2Reg.DataObject;
	using Xunit;
	using Xunit.Extensions;
	using FluentAssertions;

	/// <summary>
	/// Description résumée pour AnalogValueTest
	/// </summary>
	public class AnalogValueTest
	{
		private static readonly AnalogValue s_NullValue = null;

		/// <summary>
		/// Gets the test value for operator property.
		/// </summary>
		public static IEnumerable<AnalogValue[]> TestValueForOperatorProperty
		{
			get
			{
				yield return new[] { new AnalogValue(12.5M), new AnalogValue(12.5M), new AnalogValue(25M), new AnalogValue(0M), new AnalogValue(156.25M), new AnalogValue(1M) };
				yield return new[] { new AnalogValue(12.5M), new AnalogValue(26.5M), new AnalogValue(39M), new AnalogValue(-14M), new AnalogValue(331.25M), new AnalogValue(12.5M / 26.5M) };
				yield return new[] { new AnalogValue(12.5M), new AnalogValue(-25.69M), new AnalogValue(12.5M + -25.69M), new AnalogValue(12.5M - -25.69M), new AnalogValue(12.5M * -25.69M), new AnalogValue(12.5M / -25.69M) };
				yield return new[] { new AnalogValue(-59.36M), new AnalogValue(69.6655M), new AnalogValue(-59.36M + 69.6655M), new AnalogValue(-59.36M - 69.6655M), new AnalogValue(-59.36M * 69.6655M), new AnalogValue(-59.36M / 69.6655M) };
				yield return new[] { new AnalogValue(-986.32355M), new AnalogValue(-988.3655411M), new AnalogValue(-986.32355M + -988.3655411M), new AnalogValue(-986.32355M - -988.3655411M), new AnalogValue(-986.32355M * -988.3655411M), new AnalogValue(-986.32355M / -988.3655411M) };
				yield return new[] { new AnalogValue(855.66M), new AnalogValue(0M), new AnalogValue(855.66M), new AnalogValue(855.66M), new AnalogValue(0M), null };
				yield return new[] { new AnalogValue(-9988.66M), new AnalogValue(0M), new AnalogValue(-9988.66M), new AnalogValue(-9988.66M), new AnalogValue(0M), null };
				yield return new[] { new AnalogValue(0M), new AnalogValue(52.6665M), new AnalogValue(52.6665M), new AnalogValue(-52.6665M), new AnalogValue(0M), new AnalogValue(0) };
				yield return new[] { new AnalogValue(0M), new AnalogValue(-985.336M), new AnalogValue(-985.336M), new AnalogValue(985.336M), new AnalogValue(0), new AnalogValue(0) };
				yield return new[] { new AnalogValue(0M), new AnalogValue(0M), new AnalogValue(0M), new AnalogValue(0M), new AnalogValue(0M), null };
			}
		}

		/// <summary>
		/// Gets the test value for equal property.
		/// </summary>
		public static IEnumerable<object[]> TestValueForEqualProperty
		{
			get
			{
				yield return new object[] { new AnalogValue(12.5M), new AnalogValue(12.5M), true };
				yield return new object[] { new AnalogValue(12.5M), new AnalogValue(12.6M), false };
				yield return new object[] { new AnalogValue(-12.5M), new AnalogValue(12.5M), false };
				yield return new object[] { new AnalogValue(-12.5M), new AnalogValue(-12.5M), true };
				yield return new object[] { new AnalogValue(-12.5M), new AnalogValue(-12.5001M), false };
				yield return new object[] { new AnalogValue(12.5M), s_NullValue, false };
				yield return new object[] { new AnalogValue(-986.32355M), new AnalogValue(-988.3655411M), false };
			}
		}


		private const decimal DEC = 152.36548523M;

		private readonly AnalogValue testee;

		public AnalogValueTest()
		{
			this.testee = new AnalogValue();
		}

		[Fact]
		public void AnalogValueWhenSetValueEqualReturnValueThenOk()
		{
			this.testee.Value = DEC;
			this.testee.Value.Should().Be(DEC, "Value is not equal as {0}", DEC);
		}

		[Fact]
		public void AdjustValueThenGainNotZeroThenCalculate()
		{
			AnalogValue.AdjustValue(new AnalogValue(10), new AnalogValue(1.5M), new AnalogValue(8)).Value.Should().Be(
				(10M * 1.5M) + 8M);
		}

		#region Test Operator + - * /

		[Theory]
		[PropertyData("TestValueForOperatorProperty")]
		public void AnalogParamWhenOperatorAddValueThenResponseOk(AnalogValue param1, AnalogValue param2, AnalogValue responsePlus, AnalogValue responseMinus, AnalogValue reponseMultiply, AnalogValue responseDivid)
		{
			(param1 + param2).Should().Be(responsePlus, "{0} * {1} = {2}", param1, param2, responsePlus);
		}

		[Theory]
		[PropertyData("TestValueForOperatorProperty")]
		public void AnalogParamWhenOperatorSoustractValueThenResponseOk(AnalogValue param1, AnalogValue param2, AnalogValue responsePlus, AnalogValue responseMinus, AnalogValue reponseMultiply, AnalogValue responseDivid)
		{
			(param1 - param2).Should().Be(responseMinus, "{0} * {1} = {2}", param1, param2, responseMinus);
		}

		[Theory]
		[PropertyData("TestValueForOperatorProperty")]
		public void AnalogParamWhenOperatorMultiplieThenResponseOk(AnalogValue param1, AnalogValue param2, AnalogValue responsePlus, AnalogValue responseMinus, AnalogValue reponseMultiply, AnalogValue responseDivid)
		{
			(param1 * param2).Should().Be(reponseMultiply, "{0} * {1} = {2}", param1, param2, reponseMultiply);
		}

		[Theory]
		[PropertyData("TestValueForOperatorProperty")]
		public void AnalogParamWhenOperatorDivideValueThenResponseOk(AnalogValue param1, AnalogValue param2, AnalogValue responsePlus, AnalogValue responseMinus, AnalogValue reponseMultiply, AnalogValue responseDivid)
		{
			if (param2.Value != 0)
			{
				(param1 / param2).Should().Be(responseDivid, "{0} * {1} = {2}", param1, param2, responseDivid);
			}
			else
			{
				try
				{
					AnalogValue rep1 = param1 / param2;
					const bool REP = false;
					REP.Should().BeTrue("Not Exception!");
				}
				catch (DivideByZeroException)
				{
					const bool REP = true;
					REP.Should().BeTrue("DivideByZeroException.");
				}
				catch (Exception)
				{
					const bool REP = false;
					REP.Should().BeTrue("Not DivideByZeroException!");
				}
			}
		}

		#endregion

		#region Operator == != >= =< < >

		[Theory]
		[PropertyData("TestValueForOperatorProperty")]
		public void AnalogParamWhenOperatorEqualThenResponseOk(AnalogValue param1, AnalogValue param2, AnalogValue responsePlus, AnalogValue responseMinus, AnalogValue reponseMultiply, AnalogValue responseDivid)
		{
			bool rep = param1.Value == param2.Value;
			(param1 == param2).Should().Be(rep, "{0} == {1} is {2}", param1, param2, rep);
		}

		[Theory]
		[PropertyData("TestValueForOperatorProperty")]
		public void AnalogParamWhenOperatorNotEqualThenResponseOk(AnalogValue param1, AnalogValue param2, AnalogValue responsePlus, AnalogValue responseMinus, AnalogValue reponseMultiply, AnalogValue responseDivid)
		{
			bool rep = param1.Value != param2.Value;
			(param1 != param2).Should().Be(rep, "{0} != {1} is {2}", param1, param2, rep);
		}

		[Theory]
		[PropertyData("TestValueForOperatorProperty")]
		public void AnalogParamWhenOperatorEqualOrBiggerThenResponseOk(AnalogValue param1, AnalogValue param2, AnalogValue responsePlus, AnalogValue responseMinus, AnalogValue reponseMultiply, AnalogValue responseDivid)
		{
			bool rep = param1.Value >= param2.Value;
			(param1 >= param2).Should().Be(rep, "{0} >= {1} is {2}", param1, param2, rep);
		}

		[Theory]
		[PropertyData("TestValueForOperatorProperty")]
		public void AnalogParamWhenOperatorBiggerThenResponseOk(AnalogValue param1, AnalogValue param2, AnalogValue responsePlus, AnalogValue responseMinus, AnalogValue reponseMultiply, AnalogValue responseDivid)
		{
			bool rep = param1.Value > param2.Value;
			(param1 > param2).Should().Be(rep, "{0} > {1} is {2}", param1, param2, rep);
		}

		[Theory]
		[PropertyData("TestValueForOperatorProperty")]
		public void AnalogParamWhenOperatorEqualOrLessThenResponseOk(AnalogValue param1, AnalogValue param2, AnalogValue responsePlus, AnalogValue responseMinus, AnalogValue reponseMultiply, AnalogValue responseDivid)
		{
			bool rep = param1.Value <= param2.Value;
			(param1 <= param2).Should().Be(rep, "{0} <= {1} is {2}", param1, param2, rep);
		}

		[Theory]
		[PropertyData("TestValueForOperatorProperty")]
		public void AnalogParamWhenOperatorLessThenResponseOk(AnalogValue param1, AnalogValue param2, AnalogValue responsePlus, AnalogValue responseMinus, AnalogValue reponseMultiply, AnalogValue responseDivid)
		{
			bool rep = param1.Value < param2.Value;
			(param1 < param2).Should().Be(rep, "{0} < {1} is {2}", param1, param2, rep);
		}

		#endregion

		#region Operator Equal

		[Theory]
		[PropertyData("TestValueForEqualProperty")]
		public void EqualThenValue1EqualValue2ThenTrue(AnalogValue param1, AnalogValue param2, bool response)
		{
			param1.Equals(param2).Should().Be(response, "param1.Equal(param2) => {0} == {1} is {2}", param1, param2, response);
		}



		#endregion
	}
}
