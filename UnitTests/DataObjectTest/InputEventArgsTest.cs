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
		/// <summary>
		/// Initializes a new instance of the <see cref="InputEventArgsTest"/> class.
		/// </summary>
		public InputEventArgsTest()
		{
			this.componentCode = "R2";
			this.inputCode = "D2";
		}

		/// <summary>
		/// Constructors the check initialisation analog value long version.
		/// </summary>
		[Fact]
		public void ConstructorCheckInitialisationAnalogValue()
		{
			var expected = new AnalogValue( 25.63M);

			this.testee = new ValueEventArgs(expected, this.componentCode, this.inputCode);

			this.testee.Value.Should().Be(expected, "The analog value is not correct");
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

			this.testee.Value.Should().Be(expected, "The digital value is not correct");
			this.ValidationOfConstant();
		}

		/// <summary>
		/// Validations the of constant.
		/// </summary>
		private void ValidationOfConstant()
		{
			this.testee.OutputKey.Should().Be(this.inputCode, "The input code is not correct");
		}
	}
}