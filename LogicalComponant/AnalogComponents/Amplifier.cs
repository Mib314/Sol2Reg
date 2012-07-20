namespace Sol2Reg.LogicalComponent.AnalogComponents
{
	using ComponentBase;
	using DataObject;
	using Interface;

	public class Amplifier : AnalogBasicComponent 
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Amplifier"/> class.
		/// </summary>
		/// <param name="valueManager"></param>
		public Amplifier(IValueManager valueManager)
			: base(valueManager)
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