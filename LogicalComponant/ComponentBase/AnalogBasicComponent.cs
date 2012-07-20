namespace Sol2Reg.LogicalComponent.ComponentBase
{
	using DataObject;
	using Interface;

	public abstract class AnalogBasicComponent : BasicComponent, IAnalogBasicComponent
	{
		public const string INPUT1 = "input1";
		public const string PARAM_GAIN = "gain  ";
		public const string PARAM_OFFSET = "offset";

		protected AnalogBasicComponent(IValueManager valueManager)
			: base(valueManager) {}


		/// <summary>
		/// Gets or sets the input 1.
		/// </summary>
		/// <value>
		/// The input1.
		/// </value>
		public AnalogValue Input1
		{
			get { return (AnalogValue)this.InternalValueManager.CurrentParams[PARAM_OFFSET]; }
			protected set { this.InternalValueManager.SetterParam(PARAM_OFFSET, value); }
		}
	
		/// <summary>
		/// Valeur multiplicative d'une source analogique.
		/// </summary>
		/// <value>Par default = 1.</value>
		public AnalogValue Gain
		{
			get { return (AnalogValue)this.InternalValueManager.CurrentParams[PARAM_GAIN]; }
			protected set { this.InternalValueManager.SetterParam(PARAM_GAIN, value); }
		}

		/// <summary>
		/// Valeur additive d'une source analogique.
		/// </summary>
		/// <value>Par default = 0.</value>
		public AnalogValue Offset
		{
			get { return (AnalogValue)this.InternalValueManager.CurrentParams[PARAM_OFFSET]; }
			protected set { this.InternalValueManager.SetterParam(PARAM_OFFSET, value); }
		}

		/// <summary>
		/// Initialize the AnalogAmplifier.
		/// </summary>
		/// <param name="code">The code.</param>
		/// <param name="input1">The input 1.</param>
		/// <param name="gain">The gain.</param>
		/// <param name="offset">The offset.</param>
		public virtual void Initialize(string code, AnalogValue input1, AnalogValue gain, AnalogValue offset)
		{
			base.Initialize(code);
			this.Input1 = input1;
			this.Gain = gain;
			this.Offset = offset;
		}
	}
}