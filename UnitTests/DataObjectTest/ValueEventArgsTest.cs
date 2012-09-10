namespace Sol2Reg.Test.DataObject
{
	using Sol2Reg.DataObject;
	using Sol2Reg.DataObject.Events;
	using Xunit;
	using FluentAssertions;

	public class ValueEventArgsTest
	{
		private ValueEventArgs testee;

		/// <summary>
		/// Constructors the check initialisation analog value long version.
		/// </summary>
		[Fact]
		public void ConstructorCheckInitialisationAnalogValueLongVersion()
		{
			IValue expected = new AnalogValue(25.63M);

			this.testee = new ValueEventArgs(expected);

			this.testee.Value.Should().Be(expected, "The analog value is not correct");
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

			this.testee.Value.Should().Be(expected, "The digital value is not correct");
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
		}

	}
}