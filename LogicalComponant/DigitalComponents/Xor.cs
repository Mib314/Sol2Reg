namespace Sol2Reg.LogicalComponent.DigitalComponents
{
	using System.Linq;
	using ComponentBase;
	using DataObject;
	using DataObject.Enum;
	using Interface.ComponentBase;
	using log4net;

	public class Xor : DigitalBasicComponent
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="Xor"/> class.
		/// </summary>
		/// <param name="components">The components.</param>
		/// <param name="parametersManager">The parameters manager.</param>
		/// <param name="loger">The loger.</param>
		public Xor(Components components, IParametersManager parametersManager, ILog loger)
			: base(components, parametersManager, loger)
		{
		}

		/// <summary>
		/// Executes the calculation.
		/// </summary>
		public override void Calculate()
		{
			// False if all input is set to True or False.
			var logAllTrue = this.ParametersManager.GetInputDynamicParameter().All(p => ((DigitalValue)p.Value).GetCalculateValue());
			var logAllFalse = this.ParametersManager.GetInputDynamicParameter().All(p => !((DigitalValue)p.Value).GetCalculateValue());
			this.InternalParametersManager.SetParameter(OUTPUT1, new DigitalValue(!(logAllTrue || logAllFalse)));

			base.Calculate();
		}
	}
}