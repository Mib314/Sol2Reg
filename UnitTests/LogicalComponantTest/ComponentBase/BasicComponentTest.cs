namespace Sol2Reg.Test.LogicalComponent.ComponentBase.ComponentBase
{
	using System.Collections.Generic;
	using DataObject;
	using FluentAssertions;
	using Sol2Reg.LogicalComponent.ComponentBase;
	using Sol2Reg.LogicalComponent.Interface;
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

		private readonly Components components;
		private readonly Mock<ILog> logger; 
		private readonly Mock<IInternalParametersManager> ParametersManager;
		private readonly BasicComponantImplementation testee;

		/// <summary>
		/// Initializes a new instance of the <see cref="BasicComponentTest"/> class.
		/// </summary>
		public BasicComponentTest()
		{
			this.components = new Components();
			this.logger = new Mock<ILog>();
			this.ParametersManager = new Mock<IInternalParametersManager>();
			this.testee = new BasicComponantImplementation(this.components, this.ParametersManager.Object, this.logger.Object);
		}

		/// <summary>
		/// Sets the param when anamlog param then is in value manager.
		/// </summary>
		[Fact]
		public void SetParamWhenAnamlogParamThenIsInValueManager()
		{
			var newParam = this.SetAnalogValue(PARAM_ANALOGUE1);
			this.testee.SetParam(newParam, BasicComponantImplementation.INPUT1);

			this.ParametersManager.Verify(foo => foo.SetParameter(BasicComponantImplementation.INPUT1, newParam));
		}

		/// <summary>
		/// Gets the param when read A param then read from value manager.
		/// </summary>
		[Fact]
		public void GetParamWhenReadAParamThenReadFromValueManager()
		{
			var newParam = this.SetAnalogValue(PARAM_ANALOGUE1);
//			this.ParametersManager.SetupGet(foo => foo.CurrentParams).Returns(new Dictionary<string, IValue> { { BasicComponantImplementation.INPUT1, newParam } });

			var readValue = this.testee.GetParam(BasicComponantImplementation.INPUT1);

			//this.ParametersManager.Verify(foo => foo.CurrentParams);
		}

		/// <summary>
		/// States the change when new object then count equal1.
		/// </summary>
		[Fact]
		public void StateChangeWhenNewObjectThenCountEqual1()
		{
			this.testee.StateChange();

			this.testee.Count.Should().Be(1, "After the first StateChange, count = 1.");
		}

		/// <summary>
		/// States the change when state change2 time then count equal1.
		/// </summary>
		[Fact]
		public void StateChangeWhenStateChange2TimeThenCountEqual1()
		{
			this.testee.StateChange();
			this.testee.StateChange();

			this.testee.Count.Should().Be(2, "After the seconde StateChange, count = 2.");
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