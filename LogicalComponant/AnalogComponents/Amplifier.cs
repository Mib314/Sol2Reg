namespace Sol2Reg.LogicalComponent.AnalogComponents
{
	using ComponentBase;
	using DataObject;
	using Interface.ComponentBase;

	public class Amplifier : AnalogBasicComponent, IAmplifier
	{
		public const string INPUT1 = "Input1";

		/// <summary>Initializes a new instance of the <see cref="Amplifier"/> class.</summary>
		/// <param name="parametersManager">The parameters manager.</param>
		public Amplifier(IParametersManager parametersManager)
			: base(parametersManager)
		{
		}

		/// <summary>
		/// Initializes the input1.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		public void InitializeInput1(IParameter parameter)
		{
			if (!this.InitialParameters.Params.ContainsKey(INPUT1))
			{
				this.InitialParameters.Params.Add(INPUT1, parameter);
			}
		}
		/// <summary>Initilises the specified code.</summary>
		/// <param name="code">The code.</param>
		/// <param name="gain">The gain.</param>
		/// <param name="offser">The offser.</param>
		/// <returns>This.</returns>
		public Amplifier Initilise(string code, IValue gain, IValue offser)
		{
			base.Initialize(code, gain, offser);
			return this;
		}

		/// <summary>Executes the calculation.</summary>
		public override void Calculate()
		{
			this.SetParameter(INPUT1, AnalogValue.AdjustValue(this.GetParameter(INPUT1), this.Gain, this.Offset));
			base.Calculate();
		}
	}
}