namespace Sol2Reg.LogicalComponent.DigitalComponents
{
	using System.Linq;
	using ComponentBase;
	using DataObject;
	using Interface.ComponentBase;

	public class ComponentOr : BasicComponent
	{
		public const string OUTPUT1 = "Out1";

		/// <summary>
		/// Initializes a new instance of the <see cref="ComponentAnd"/> class.
		/// </summary>
		/// <param name="pParametersManager">The p parameters manager.</param>
		public ComponentOr(IParametersManager pParametersManager)
			: base(pParametersManager)
		{
		}

		public void InitializeInputPorts(IParameters parameters)
		{
			foreach (var parameter in parameters.Params)
			{
				if (!this.InitialParameters.Params.ContainsKey(parameter.Key))
				{
					this.InitialParameters.Params.Add(parameter.Key, parameter.Value);
				}
			}
		}

		public void InitializeOutput(IParameter parameter)
		{
			if (!this.InitialParameters.Params.ContainsKey(parameter.Key))
			{
				this.InitialParameters.Params.Add(parameter.Key, parameter);
			}
		}

		public override void Calculate()
		{
			IValue val = new DigitalValue(this.ParametersManager.GetInputDynamicParameter().Any(p => p.ParameterType == EnumParameterType.Digital && ((DigitalValue)p.Value).GetCalculateValue()));
			this.InternalParametersManager.SetParameter(OUTPUT1, val);

			base.Calculate();
		}
	}
}