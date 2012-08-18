﻿namespace Sol2Reg.LogicalComponent.AnalogComponents
{
	using ComponentBase;
	using DataObject;
	using Interface;
	using Interface.AnalogComponants;
	using Interface.ComponentBase;

	public sealed class AnalogCompar : AnalogBasicComponent, IAnalogCompar
	{
		public const string INPUT2 = "input2";
		public const string PARAM_D_ON = "D_On  ";
		public const string PARAM_D_OFF = "D_Off ";
		public const string OUTPUT1 = "output1 ";

		protected AnalogCompar(IParametersManager parametersManager)
			: base(parametersManager)
		{
		}

		/// <summary>
		/// Gets or sets the delta value to set off.
		/// </summary>
		/// <value>
		/// The delta value to set off.
		/// </value>
		public AnalogValue DeltaValueToSetOff
		{
			get { return (AnalogValue)this.InternalParametersManager.GetParameter(PARAM_D_OFF).Value; }
			protected set { this.InternalParametersManager.SetParameter(PARAM_D_OFF, value); }
		}

		/// <summary>
		/// Gets or sets the delta value to set off.
		/// </summary>
		/// <value>
		/// The delta value to set off.
		/// </value>
		public AnalogValue DeltaValueToSetOn
		{
			get { return (AnalogValue)this.InternalParametersManager.GetParameter(PARAM_D_ON).Value; }
			protected set { this.InternalParametersManager.SetParameter(PARAM_D_ON, value); }
		}

		/// <summary>
		/// Gets or sets the output 1.
		/// </summary>
		/// <value>
		/// The output 1.
		/// </value>
		public AnalogValue Output1
		{
			get { return (AnalogValue)this.InternalParametersManager.GetParameter(OUTPUT1).Value; }
			protected set { this.InternalParametersManager.SetParameter(OUTPUT1, value); }
		}

		#region Overrides of BasicComponent

		/// <summary>
		/// Executes the calculation.
		/// </summary>
		public override void Calculate()
		{
			// Tous les input doivent avoir le cycle courrant pour pouvoir faire le calcule.
			var output = new DigitalValue(false);
			if (this.ParametersManager.IsAllInputParamUptodate())
			{
				return;
			}

			var realValue1 = AnalogValue.AdjustValue(this.GetParam(INPUT1), this.Gain, this.Offset);
			var realValue2 = AnalogValue.AdjustValue(this.GetParam(INPUT2), this.Gain, this.Offset);
			
			/* 
			 * Règle de calcul
			 *      Si seuil d'enclenchement (DeltaOn) ≥ seuil de déclenchement (DeltaOff), on a :
			 *          OutputState = 1, si (valeur réelle InputValue1 - valeur réelle InputValue2) > DeltaOn 
			 *          OutputState = 0, si (valeur réelle InputValue1 - valeur réelle InputValue2) ≤ DeltaOff.
			 *      Si seuil d'enclenchement (DeltaOn) < seuil de déclenchement (DeltaOff), on a : 
			 *          OutputState = 1, si On ≤ (valeur réelle InputValue1 - valeur réelle InputValue2) < DeltaOff.
			*/
			var deltaValue = realValue1 - realValue2;

			// Si seuil d'enclenchement (DeltaOn) ≥ seuil de déclenchement (DeltaOff),
			if (this.DeltaValueToSetOn >= this.DeltaValueToSetOff)
			{
				// OutputState = 1, si (valeur réelle InputValue1 - valeur réelle InputValue2) > DeltaOn
				if (deltaValue > this.DeltaValueToSetOn)
				{
					output.Value = true;
				}
				// OutputState = 0, si (valeur réelle InputValue1 - valeur réelle InputValue2) ≤ DeltaOff
				else if (deltaValue <= this.DeltaValueToSetOff)
				{
					output.Value = false;
				}
			}
			// Si seuil d'enclenchement (DeltaOn) < seuil de déclenchement (DeltaOff),
			else
			{
				// OutputState = 1, si On ≤ (valeur réelle InputValue1 - valeur réelle InputValue2) < DeltaOff.
				output.Value = (deltaValue >= this.DeltaValueToSetOn && deltaValue < this.DeltaValueToSetOff);
			}
			this.InternalParametersManager.OnEventOutputChange(output, OUTPUT1);
			this.StateChange();
		}

		/// <summary>
		/// Initialize the specified name.
		/// </summary>
		/// <param name="code">The code.</param>
		/// <param name="gain">The gain.</param>
		/// <param name="offset">The offset.</param>
		/// <param name="deltaValueToSetOn">The delta value to set on.</param>
		/// <param name="deltaValueToSetOff">The delta value to set off.</param>
		public void Initialize(string code, AnalogValue gain, AnalogValue offset, AnalogValue deltaValueToSetOn, AnalogValue deltaValueToSetOff)
		{
			Initialize(code, gain, offset);
			this.DeltaValueToSetOff = deltaValueToSetOff;
			this.DeltaValueToSetOn = deltaValueToSetOn;
			this.DeltaValueToSetOff = (AnalogValue)new AnalogValue().Initialize();
			this.DeltaValueToSetOn = (AnalogValue)new AnalogValue().Initialize();
			this.Output1 = (AnalogValue)new DigitalValue().Initialize();
		}
		#endregion
	}
}