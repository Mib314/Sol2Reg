namespace Sol2Reg.LogicalComponent.AnalogComponents
{
	using ComponentBase;
	using DataObject;
	using Interface;
	using Interface.ComponentBase;

	public class Amplifier : AnalogBasicComponent 
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Amplifier"/> class.
		/// </summary>
		/// <param name="parametersManager"></param>
		public Amplifier(IParametersManager parametersManager)
			: base(parametersManager)
		{
		}

		/// <summary>
		/// Executes the calculation.
		/// </summary>
		public override void Calculate()
		{
			this.SetParam(AnalogValue.AdjustValue(this.GetParam(INPUT1), this.Gain, this.Offset), INPUT1);
		}

	}
}