namespace Sol2Reg.LogicalComponent.DigitalComponents
{
	using System.Linq;
	using ComponentBase;
	using DataObject;
	using DataObject.Enum;
	using Interface.ComponentBase;
	using log4net;

	public class Not : BasicComponent
	{
		public const string INPUT1 = "Input1";

		/// <summary>
		/// Initializes a new instance of the <see cref="Not"/> class.
		/// </summary>
		/// <param name="components">The components.</param>
		/// <param name="parametersManager">The parameters manager.</param>
		/// <param name="loger">The loger.</param>
		public Not(Components components, IParametersManager parametersManager, ILog loger)
			: base(components, parametersManager, loger)
		{
		}

		public new void Initialize(string code)
		{
			this.InitialParameters.Add(new Parameter().Initialize(INPUT1, new DigitalValue(), EnumParameterDirection.Input, "Input digital parameter."));
			this.InitialParameters.Add(new Parameter().Initialize(OUTPUT1, new DigitalValue(), EnumParameterDirection.Output, "Output parameter."));
			base.Initialize(code);
		}

		/// <summary>
		/// Executes the calculation.
		/// </summary>
		public override void Calculate()
		{
			var val = (DigitalValue)this.ParametersManager.GetParameter(INPUT1).Value;
			if (val.Value.HasValue)
			{
				val.Value = !val.Value;
			}
			this.InternalParametersManager.SetParameter(OUTPUT1, val);

			base.Calculate();
		}
	}
}