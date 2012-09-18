namespace Sol2Reg.LogicalComponent.AnalogComponents
{
	using ComponentBase;
	using DataObject;
	using DataObject.Enum;
	using Interface.ComponentBase;
	using log4net;

	public class Amplifier : AnalogBasicComponent, IAmplifier
	{
		public const string INPUT1 = "Input1";

		/// <summary>
		/// Initializes a new instance of the <see cref="Amplifier"/> class.
		/// </summary>
		/// <param name="components">The components.</param>
		/// <param name="parametersManager">The parameters manager.</param>
		/// <param name="loger">The loger.</param>
		public Amplifier(Components components, IParametersManager parametersManager, ILog loger)
			: base(components, parametersManager,loger)
		{
		}

		/// <summary>Initilises the specified code.</summary>
		/// <param name="code">The code.</param>
		/// <param name="gain">The gain.</param>
		/// <param name="offser">The offser.</param>
		/// <returns>This.</returns>
		public Amplifier Initilise(string code, IValue gain, IValue offser)
		{
			this.InitialParameters.Add(new Parameter
									   {
										   Key = INPUT1,
										   Comment = "This input parmaeter",
										   Value = new AnalogValue()
									   });
			base.Initialize(code, gain, offser);
			return this;
		}

		/// <summary>Executes the calculation.</summary>
		public override void Calculate()
		{
			this.InitialParameters.Add(new Parameter().Initialize(INPUT1, new AnalogValue(), EnumParameterDirection.Input, "Input value to amplifie."));
			this.InitialParameters.Add(new Parameter().Initialize(OUTPUT1, new AnalogValue(), EnumParameterDirection.Output, "output value amplified."));
		}
	}
}