namespace Sol2Reg.LogicalComponent.DigitalComponents
{
	using System.Linq;
	using ComponentBase;
	using DataObject;
	using Interface.ComponentBase;
	using log4net;

	public class Or : DigitalBasicComponent
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="Or"/> class.
		/// </summary>
		/// <param name="components">The components.</param>
		/// <param name="parametersManager">The parameters manager.</param>
		/// <param name="loger">The loger.</param>
		public Or(Components components, IParametersManager parametersManager, ILog loger)
			: base(components,parametersManager, loger)
		{
		}

		/// <summary>
		/// Executes the calculation.
		/// </summary>
		public override void Calculate()
		{
		// If all input is false => then return false.
			var logAllFalse = this.ParametersManager.GetInputDynamicParameter().All(p => !((DigitalValue)p.Value).GetCalculateValue());

			this.InternalParametersManager.SetParameter(OUTPUT1, new DigitalValue(!logAllFalse));

			base.Calculate();
		}
	}
}