namespace Sol2Reg.Test.LogicalComponent.ComponentBase.ComponentBase
{
	using System.Collections.Generic;
	using System;
	using Sol2Reg.DataObject;
	using Sol2Reg.DataObject.Events;
	using Sol2Reg.LogicalComponent.ComponentBase;
	using Sol2Reg.LogicalComponent.Interface;
	using Moq;
	using Sol2Reg.LogicalComponent.Interface.ComponentBase;
	using Xunit;
	using FluentAssertions;

	public class ValueManagerTest
	{
		private const long CYCLE = 25;
		private const string PARAM_NAME = "Input1";
		private readonly IInternalParametersManager testee;
		private readonly Mock<IBasicComponentForParameterManager> basicComponent;
		private readonly Mock<IInternalParametersManager> valueManager2;
		private readonly Mock<IHelperHistoryIOValue> helperHistoryInputValue;
		private readonly DateTime cycleTime;
		private readonly AnalogValue analogValue;
		private readonly DigitalValue digitalValue;
		private readonly AnalogValue newValue;


		public ValueManagerTest()
		{
			this.cycleTime = new DateTime(2012, 12, 13, 13, 56, 56);
			this.analogValue = new AnalogValue(123.256M, CYCLE, this.cycleTime, true);
			this.digitalValue = new DigitalValue(true);
			this.helperHistoryInputValue = new Mock<IHelperHistoryIOValue>();
			this.basicComponent = new Mock<IBasicComponentForParameterManager>();
			this.valueManager2 = new Mock<IInternalParametersManager>();
			this.newValue = new AnalogValue(this.analogValue.Value + 25, CYCLE + 1, this.cycleTime.AddSeconds(1));
			
			this.testee = new ParameterManager(this.basicComponent.Object, this.helperHistoryInputValue.Object);
			this.testee.Initialize();
		}

		[Fact]
		public void InitializeCheckCreationObject()
		{
			var rep = this.testee.Initialize();

			rep.Cycle.Should().Be(0, "Initial cycle is 0");
			rep.CycleTime.Should().Be(DateTime.MinValue, "CycleTime befor the first cycle is set to min date time");
			rep.Code.Should().Be(this.basicComponent.Object.Code, "Code is set from Basic Component");
			rep.HistoryCycleDuration.Should().Be(null, "This object is set with null");
			rep.HistoryFrequency.Should().Be(0, "This object is set with 0");
			rep.HistoryTimeDuration.Should().Be(null, "This object is set with null");
		}

		[Fact]
		public void SetterParamWhenAnalogParamThenOk()
		{
			this.testee.SetParameter(PARAM_NAME, this.analogValue);

			this.testee.CurrentParams[PARAM_NAME].Should().Be(this.analogValue, "The out value must same als the input value.");
		}

		[Fact]
		public void SetterParamWhenDigitalParamThenOk()
		{
			this.testee.SetParameter(PARAM_NAME, this.digitalValue);

			this.testee.CurrentParams[PARAM_NAME].Should().Be(this.digitalValue, "The out value must same als the input value.");
		}

		[Fact]
		public void SetterParamWhenEventAnalogParamThenOk()
		{
			var args = new ValueEventArgs(this.analogValue);
			this.testee.SetParameter(PARAM_NAME, args);

			this.testee.CurrentParams[PARAM_NAME].Should().Be(this.analogValue, "The out value must same als the input value.");
		}

		[Fact]
		public void SetterParamWhenEventDigitalParamThenOk()
		{
			var args = new ValueEventArgs(this.digitalValue);
			this.testee.SetParameter(PARAM_NAME, args);

			this.testee.CurrentParams[PARAM_NAME].Should().Be(this.digitalValue, "The out value must same als the input value.");
		}

		[Fact]
		public void RegisterLinkInputWhenEventIsRegistredThenOk()
		{
			this.valueManager2.Setup(foo => foo.SetParameter(PARAM_NAME, this.newValue));
			this.SetterInputParamAnalog();

			this.testee.RegisterLinkInput(PARAM_NAME, this.valueManager2.Object);

			// Send a test event
			this.valueManager2.Raise(raise => raise.EventOutputChange += null, null, new ValueEventArgs(this.newValue));

			// Check if the param has change.
			this.testee.CurrentParams[PARAM_NAME].Should().Be(this.newValue, "The input param is changed with the new value");

		}

		[Fact]
		public void OnEventOutputChangeWhenHandlerExistThenSendEvent()
		{
			this.valueManager2.Setup(foo => foo.SetParameter(PARAM_NAME, this.newValue));
			this.SetterInputParamAnalog();

			this.valueManager2.Setup(foo => foo.RegisterLinkInput(PARAM_NAME, this.testee));
			this.helperHistoryInputValue.Setup(foo => foo.CheckIfAllParamIsUpToDate(It.IsAny< Dictionary<string, IValue>>()));
			this.testee.OnEventInputChange(this.newValue, PARAM_NAME);

			// this.valueManager2.Verify(foo => foo.SetParameter(PARAM_NAME, newValue));
		}


		private void SetterInputParamAnalog()
		{
			this.testee.SetParameter(PARAM_NAME, this.analogValue);
		}
	}
}