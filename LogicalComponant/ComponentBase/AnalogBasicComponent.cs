﻿namespace Sol2Reg.LogicalComponent.ComponentBase
{
	using DataObject;
	using Interface;
	using Interface.ComponentBase;

	public abstract class AnalogBasicComponent : BasicComponent, IAnalogBasicComponent
	{
		public const string INPUT1 = "input1";
		public const string PARAM_GAIN = "gain  ";
		public const string PARAM_OFFSET = "offset";

		protected AnalogBasicComponent(IParametersManager parametersManager)
			: base(parametersManager) {}

		/// <summary>
		/// Gets or sets the input 1.
		/// </summary>
		/// <value>
		/// The input1.
		/// </value>
		public AnalogValue Input1
		{
			get { return (AnalogValue)this.InternalParametersManager.CurrentParams[PARAM_OFFSET]; }
			protected set { this.InternalParametersManager.SetParameter(PARAM_OFFSET, value); }
		}
	
		/// <summary>
		/// Valeur multiplicative d'une source analogique.
		/// </summary>
		/// <value>Par default = 1.</value>
		public AnalogValue Gain
		{
			get { return (AnalogValue)this.InternalParametersManager.CurrentParams[PARAM_GAIN]; }
			protected set { this.InternalParametersManager.SetParameter(PARAM_GAIN, value); }
		}

		/// <summary>
		/// Valeur additive d'une source analogique.
		/// </summary>
		/// <value>Par default = 0.</value>
		public AnalogValue Offset
		{
			get { return (AnalogValue)this.InternalParametersManager.CurrentParams[PARAM_OFFSET]; }
			protected set { this.InternalParametersManager.SetParameter(PARAM_OFFSET, value); }
		}

		/// <summary>
		/// Initialize the AnalogAmplifier.
		/// </summary>
		/// <param name="code">The code.</param>
		/// <param name="gain">The gain.</param>
		/// <param name="offset">The offset.</param>
		public virtual void Initialize(string code, AnalogValue gain, AnalogValue offset)
		{
			base.Initialize(code);
			this.Gain = gain;
			this.Offset = offset;
			this.Input1 = (AnalogValue)new AnalogValue().Initialize();
		}
	}
}