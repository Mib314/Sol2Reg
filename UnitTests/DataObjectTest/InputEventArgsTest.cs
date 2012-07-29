namespace Sol2Reg.Test.DataObject
{
	using System;
	using Sol2Reg.DataObject;
	using Sol2Reg.DataObject.Events;
	using Xunit;
	using FluentAssertions;

	public class InputEventArgsTest
	{
		private ValueEventArgs testee;
		private readonly string componentCode;
		private readonly string inputCode;
		private readonly long cycle ;
		private readonly DateTime cycleTime;
		/// <summary>
		/// Initializes a new instance of the <see cref="InputEventArgsTest"/> class.
		/// </summary>
		public InputEventArgsTest()
		{
			this.componentCode = "R2";
			this.inputCode = "D2";
			this.cycle = 25;
			this.cycleTime = new DateTime(2012, 8, 13, 15, 59, 59);
		}

		/// <summary>
		/// Constructors the check initialisation analog value long version.
		/// </summary>
		[Fact]
		public void ConstructorCheckInitialisationAnalogValue()
		{
			var expected = new AnalogValue(25.63M, this.cycle, this.cycleTime);

			this.testee = new ValueEventArgs(expected, this.componentCode, this.inputCode);

			((AnalogValue) this.testee.Value).Should().Be(expected, "The analog value is not correct");
			((AnalogValue)this.testee.Value).Cycle.Should().Be(this.cycle, "The analog cycle is not correct");
			((AnalogValue)this.testee.Value).CycleTime.Should().Be(this.cycleTime, "The analog cycletime is not correct");
			this.ValidationOfConstant();
		}

		/// <summary>
		/// Constructors the check initialisation digital value long version.
		/// </summary>
		[Fact]
		public void ConstructorCheckInitialisationDigitalValue()
		{
			var expected = new DigitalValue(true);

			this.testee = new ValueEventArgs(expected, this.componentCode, this.inputCode);

			((DigitalValue)this.testee.Value).Should().Be(expected, "The digital value is not correct");
			((DigitalValue)this.testee.Value).Cycle.Should().Be(this.cycle, "The digital cycle is not correct");
			((DigitalValue)this.testee.Value).CycleTime.Should().Be(this.cycleTime, "The digital cycletime is not correct");
			this.ValidationOfConstant();
		}

		/// <summary>
		/// Validations the of constant.
		/// </summary>
		private void ValidationOfConstant()
		{
			this.testee.ComponentCode.Should().Be(this.componentCode, "The compoent code is not correct");
			this.testee.InputCode.Should().Be(this.inputCode, "The input code is not correct");
		}
	}
}