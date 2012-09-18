namespace Sol2Reg.Test.LogicalComponent.ComponentBase.ComponentBase
{
	using DataObject;
	using FluentAssertions;
	using Sol2Reg.LogicalComponent.ComponentBase;
	using Moq;
	using Sol2Reg.LogicalComponent.Interface.ComponentBase;
	using Xunit;
	using log4net;

	/// <summary>
	/// Test BasicComponent class
	/// </summary>
	public class BasicComponentTest
	{
		private const decimal PARAM_ANALOGUE1 = 15.36M;
		private const bool PARAM_DIGITAL1 = true;

		private readonly Components components;
		private readonly Mock<ILog> logger; 
		private readonly Mock<IInternalParametersManager> parametersManager;
		private readonly BasicComponantImplementation testee;

		/// <summary>
		/// Initializes a new instance of the <see cref="BasicComponentTest"/> class.
		/// </summary>
		public BasicComponentTest()
		{
			this.components = new Components();
			this.logger = new Mock<ILog>();
			this.parametersManager = new Mock<IInternalParametersManager>();
			this.testee = new BasicComponantImplementation(this.components, this.parametersManager.Object, this.logger.Object);
		}

		/// <summary>
		/// Sets the parameter when anamlog param then is in value manager.
		/// </summary>
		[Fact]
		public void SetParameterWhenAnamlogParamThenIsInParameterManager()
		{
			var newParam = this.SetAnalogValue(PARAM_ANALOGUE1);
			this.testee.SetParameter(BasicComponantImplementation.INPUT1, newParam);

			this.parametersManager.Verify(foo => foo.SetParameter(BasicComponantImplementation.INPUT1, newParam));
		}

		/// <summary>
		/// Sets the parameter when digital param then is in value manager.
		/// </summary>
		[Fact]
		public void SetParameterWhenDigitalParamThenIsInParameterManager()
		{
			var newParam = this.SetDigitalValue(PARAM_DIGITAL1);
			this.testee.SetParameter(BasicComponantImplementation.INPUT1, newParam);

			this.parametersManager.Verify(foo => foo.SetParameter(BasicComponantImplementation.INPUT1, newParam));
		}

		[Fact]
		public void GetParameterWhenCallThenReturnDataFromParameterManager()
		{
			string paramName = "ss";
			var value = 15.63M;
			var respValue = (DigitalValue)this.SetAnalogValue(value);

			// this.parametersManager.Setup(foo => foo.GetParameter(paramName)).Returns()

			var resp = (DigitalValue)this.testee.GetParameter(paramName);

			resp.Value.Should().Be(respValue.Value);
		}

		/// <summary>
		/// States the change when call then count plus 1.
		/// </summary>
		[Fact]
		public void StateChangeWhenCallThenCountPlus1()
		{
			var initialCount = this.testee.Count;
			this.testee.StateChange();

			this.testee.Count.Should().Be(initialCount + 1, "After the StateChange, count + 1.");
		}

		/// <summary>
		/// States the change when state change start 2 time then count plus 2.
		/// </summary>
		[Fact]
		public void StateChangeWhenStateChange2TimeThenCountPlus2()
		{
			var initialCount = this.testee.Count;
			this.testee.StateChange();
			this.testee.StateChange();

			this.testee.Count.Should().Be(initialCount + 2, "After the StateChange 2 time, count + 2.");
		}

		#region Private
		private IValue SetAnalogValue(decimal value)
		{
			return new AnalogValue(value);
		}

		private IValue SetDigitalValue(bool value)
		{
			return new DigitalValue(value);
		}

		#endregion
	}
}