namespace Sol2Reg.Test.LogicalComponent.ComponentBase.ComponentBase
{
	using System;
	using DataObject;
	using FluentAssertions;
	using Sol2Reg.LogicalComponent.ComponentBase;
	using Moq;
	using Sol2Reg.LogicalComponent.Interface.ComponentBase;
	using Xunit;
	using log4net;

	public class ParametersManagerTest
	{
		private const string KEY1 = "input11";
		private const string KEY2 = "input22";
		private const long CYCLE = 29;

		private readonly Mock<IBasicComponentForParameterManager> basicComponent;
		private readonly Mock<IHelperHistoryParameters> helperHistoryParameters;
		private readonly Mock<ILog> logger;

		private readonly ParametersManager testee;

		private readonly AnalogValue value1 = new AnalogValue(15.3969M);
		private readonly AnalogValue value2 = new AnalogValue(41.9663M);
		private readonly DateTime cycleTime = new DateTime(2012, 8, 25, 14, 25, 54);
		/// <summary>
		/// Initializes a new instance of the <see cref="ParametersManagerTest"/> class.
		/// </summary>
		public ParametersManagerTest()
		{
			this.logger = new Mock<ILog>();
			this.helperHistoryParameters = new Mock<IHelperHistoryParameters>();
			this.basicComponent = new Mock<IBasicComponentForParameterManager>();

			this.testee = new ParametersManager(this.basicComponent.Object, this.helperHistoryParameters.Object, this.logger.Object);
		}

		[Fact]
		public void GetParameterWhenValueIsSetThenReturnValue()
		{
			this.Initialize();

			var rep = this.testee.GetParameter(KEY1);

			rep.Should().NotBeNull();
			rep.Value.Should().Be(value1);
		}

		[Fact]
		public void SetParameterWhenKeyNotExistThenThrowException()
		{
			
		}
		private void Initialize()
		{
			var initialParams = new Parameters(CYCLE, cycleTime)
			                    {
			                    	new Parameter().Initialize(KEY1, this.value1), 
									new Parameter().Initialize(KEY2, this.value2)
			                    };
			this.testee.Initialize(initialParams);
		}

		private void SetCurrentParam(string key, IValue value)
		{
			this.testee.SetParameter(key, value);
		}
	}
}
