namespace Sol2Reg.LogicalComponent.AnalogComponents
{
	using ComponentBase;
	using DataObject;
	using Interface;
	using Interface.AnalogComponants;

	public sealed class AnalogCompar : AnalogBasicComponent, IAnalogCompar
	{
		public const string INPUT2 = "input2";
		public const string PARAM_D_ON = "D_On  ";
		public const string PARAM_D_OFF = "D_Off ";
		public const string OUTPUT1 = "output1 ";

		protected AnalogCompar(IValueManager valueManager)
			: base(valueManager) { }

		/// <summary>
		/// Gets or sets the delta value to set off.
		/// </summary>
		/// <value>
		/// The delta value to set off.
		/// </value>
		public AnalogValue DeltaValueToSetOff
		{
			get { return (AnalogValue)this.InternalValueManager.CurrentParams[PARAM_D_OFF]; }
			protected set { this.InternalValueManager.SetterParam(PARAM_D_OFF, value); }
		}

		/// <summary>
		/// Gets or sets the delta value to set off.
		/// </summary>
		/// <value>
		/// The delta value to set off.
		/// </value>
		public AnalogValue DeltaValueToSetOn
		{
			get { return (AnalogValue)this.InternalValueManager.CurrentParams[PARAM_D_ON]; }
			protected set { this.InternalValueManager.SetterParam(PARAM_D_ON, value); }
		}

		/// <summary>
		/// Gets or sets the output 1.
		/// </summary>
		/// <value>
		/// The output 1.
		/// </value>
		public AnalogValue Output1
		{
			get { return (AnalogValue)this.InternalValueManager.CurrentParams[OUTPUT1]; }
			protected set { this.InternalValueManager.SetterParam(OUTPUT1, value); }
		}

		#region Overrides of BasicComponent

		/// <summary>
		/// Executes the calculation.
		/// </summary>
		public override void Calculate()
		{
			// Tous les input doivent avoir le cycle courrant pour pouvoir faire le calcule.
			var input1 = this.GetParam(INPUT1);
			var input2 = this.GetParam(INPUT2);
			if ((input1 == null || input1.Cycle != this.ValueManager.Cycle) && (input2 == null || input2.Cycle != this.ValueManager.Cycle))
			{
				return;
			}

			var realValue1 = AnalogValue.AdjustValue(input1, this.Gain, this.Offset);
			var realValue2 = AnalogValue.AdjustValue(input1, this.Gain, this.Offset);
			
			/* jhhj
			 * Règle de calcul
			 *      Si seuil d'enclenchement (DeltaOn) ≥ seuil de déclenchement (DeltaOff), on a :
			 *          OutputState = 1, si (valeur réelle InputValue1 - valeur réelle InputValue2) > DeltaOn 
			 *          OutputState = 0, si (valeur réelle InputValue1 - valeur réelle InputValue2) ≤ DeltaOff.
			 *      Si seuil d'enclenchement (DeltaOn) < seuil de déclenchement (DeltaOff), on a : 
			 *          OutputState = 1, si On ≤ (valeur réelle InputValue1 - valeur réelle InputValue2) < DeltaOff.
			*/
			var deltaValue = realValue1 - realValue2;

			this.ValueManager. = this.OutputState;
			// Si seuil d'enclenchement (DeltaOn) ≥ seuil de déclenchement (DeltaOff),
			if (this.DeltaValueToSetOn >= this.DeltaValueToSetOff)
			{
				// OutputState = 1, si (valeur réelle InputValue1 - valeur réelle InputValue2) > DeltaOn
				if (deltaValue > this.DeltaOn)
				{
					this.OutputState.Value = true;
				}
				// OutputState = 0, si (valeur réelle InputValue1 - valeur réelle InputValue2) ≤ DeltaOff
				else if (deltaValue <= this.DeltaOff)
				{
					this.OutputState.Value = false;
				}
			}
			// Si seuil d'enclenchement (DeltaOn) < seuil de déclenchement (DeltaOff),
			else
			{
				// OutputState = 1, si On ≤ (valeur réelle InputValue1 - valeur réelle InputValue2) < DeltaOff.
				this.OutputState.Value = (deltaValue >= this.DeltaValueToSetOn && deltaValue < this.DeltaValueToSetOff);
			}

			this.InternalValueManager.OnEventOutputChange(this.OutputState);
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
			base.Initialize(code, gain, offset);
			this.DeltaValueToSetOff = deltaValueToSetOff;
			this.DeltaValueToSetOn = deltaValueToSetOn;
		}
		#endregion
	}
}