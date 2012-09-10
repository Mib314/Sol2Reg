namespace Sol2Reg.LogicalComponent.ComponentBase
{
	using DataObject;
	using Interface.ComponentBase;

	public abstract class AnalogBasicComponent : BasicComponent, IAnalogBasicComponent
	{
		private const string PARAM_GAIN = "gain  ";
		private const string PARAM_OFFSET = "offset";

		protected AnalogBasicComponent(IParametersManager parametersManager)
			: base(parametersManager) {}

		/// <summary>
		/// Valeur multiplicative d'une source analogique.
		/// </summary>
		/// <value>Par default = 1.</value>
		public AnalogValue Gain
		{
			get { return (AnalogValue)this.InternalParametersManager.GetParameter(PARAM_GAIN).Value; }
			protected set { this.InternalParametersManager.SetParameter(PARAM_GAIN, value); }
		}

		/// <summary>
		/// Valeur additive d'une source analogique.
		/// </summary>
		/// <value>Par default = 0.</value>
		public AnalogValue Offset
		{
			get { return (AnalogValue)this.InternalParametersManager.GetParameter(PARAM_OFFSET).Value; }
			protected set { this.InternalParametersManager.SetParameter(PARAM_OFFSET, value); }
		}

		/// <summary>
		/// Initialize the AnalogAmplifier.
		/// </summary>
		/// <param name="code">The code.</param>
		/// <param name="gain">The gain.</param>
		/// <param name="offset">The offset.</param>
		public virtual void Initialize(string code, IValue gain, IValue offset)
		{
			// If gain or offset not define => add default value.
			if(gain == null) gain = new AnalogValue(1);
			if(offset == null) offset = new AnalogValue(0);

			// Add standart parameter for analog control.
			this.InitialParameters.Params.Add(PARAM_GAIN, new Parameter{ Key = PARAM_GAIN, Comment="Imupt gain for all analog imput of this componant. This value is static.\nTthis parameter is set to 1 by default.", Value = gain});
			this.InitialParameters.Params.Add(PARAM_OFFSET, new Parameter { Key = PARAM_OFFSET, Comment = "Imupt offset for all analog imput of this componant. This value is static.\nThis parameter is set to 1 by default.", Value = offset });
			base.Initialize(code);
			this.Gain = (AnalogValue)gain;
			this.Offset = (AnalogValue)offset;
		}
	}
}