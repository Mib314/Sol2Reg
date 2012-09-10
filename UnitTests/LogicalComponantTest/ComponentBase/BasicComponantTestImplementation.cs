namespace Sol2Reg.Test.LogicalComponent.ComponentBase.ComponentBase
{
	using DataObject;
	using Sol2Reg.LogicalComponent.ComponentBase;
	using Sol2Reg.LogicalComponent.Interface.ComponentBase;
	using log4net;

	public class BasicComponantImplementation : BasicComponent
	{
		public const string INPUT1 = "input1";

		/// <summary>
		/// Initializes a new instance of the <see cref="BasicComponantImplementation"/> class.
		/// </summary>
		/// <param name="components">The components.</param>
		/// <param name="parametersManager">The parameters manager.</param>
		/// <param name="loger">The loger.</param>
		public BasicComponantImplementation(Components components, IParametersManager parametersManager, ILog loger)
			: base(components, parametersManager, loger)
		{
		}
		
		/// <summary>
		/// Sets the param.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="paramName">Name of the param.</param>
		public new void SetParam(IValue value, string paramName)
		{
			base.SetParameter(paramName, value);
		}

		/// <summary>
		/// Gets the param.
		/// </summary>
		/// <param name="paramName">Name of the param.</param>
		/// <returns></returns>
		public new IValue GetParam(string paramName)
		{
			return base.GetParameter(paramName);
		}

		/// <summary>
		/// Gets the output value.
		/// </summary>
		public AnalogValue OutputValue { get; set; }

		/// <summary>
		/// Executes the calculation.
		/// </summary>
		public override void Calculate()
		{
			// Abstract methode => not implemented in BasicComponent
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Initializes the specified code.
		/// </summary>
		/// <param name="code">The code.</param>
		public new void Initialize(string code)
		{
			base.Initialize(code);
		}

		/// <summary>
		/// States the change.
		/// </summary>
		public new void StateChange()
		{
			base.StateChange();
		}
	}
}