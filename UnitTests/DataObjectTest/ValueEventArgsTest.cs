namespace Sol2Reg.Test.DataObject
{
	using System;
	using Sol2Reg.DataObject;
	using Sol2Reg.DataObject.Events;
	using Xunit;
	using FluentAssertions;

	public class ValueEventArgsTest
	{
		private ValueEventArgs testee;
		private readonly long cycle ;
		private readonly DateTime cycleTime;
		
		/// <summary>
		/// Initializes a new instance of the <see cref="ValueEventArgsTest"/> class.
		/// </summary>
		public ValueEventArgsTest()
		{
			this.cycle = 25;
			this.cycleTime = new DateTime(2012, 8, 13, 15, 59, 59);
		}

		/// <summary>
		/// Constructors the check initialisation analog value long version.
		/// </summary>
		[Fact]
		public void ConstructorCheckInitialisationAnalogValueLongVersion()
		{
			IValue expected = new AnalogValue(25.63M, this.cycle, this.cycleTime);

			this.testee = new ValueEventArgs(expected);

			((AnalogValue) this.testee.Value).Should().Be(expected, "The analog value is not correct");
			this.ValidationOfConstant();
		}
		
		/// <summary>
		/// Constructors the check initialisation digital value long version.
		/// </summary>
		[Fact]
		public void ConstructorCheckInitialisationDigitalValueLongVersion()
		{
			var expected = new DigitalValue(true);

			this.testee = new ValueEventArgs(expected);

			((DigitalValue)this.testee.Value).Should().Be(expected, "The digital value is not correct");
			this.ValidationOfConstant();
		}
 
		/// <summary>
		/// Constructors the check initialisation digital value no param.
		/// </summary>
		[Fact]
		public void ConstructorCheckInitialisationDigitalValueNoParam()
		{
			var expected = new DigitalValue();

			this.testee = new ValueEventArgs(expected);

			((DigitalValue)this.testee.Value).Value.Should().Be(false, "The digital value is not correct");
		}

		/// <summary>
		/// Validations the of constant.
		/// </summary>
		private void ValidationOfConstant()
		{
			this.testee.Value.Cycle.Should().Be(this.cycle, "The Cycle is not correct");
			this.testee.Value.CycleTime.Should().Be(this.cycleTime, "The CycleTime is not correct");
		}

	}
}