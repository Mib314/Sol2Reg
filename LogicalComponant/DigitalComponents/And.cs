namespace Sol2Reg.LogicalComponent.DigitalComponents
{
	using System.Linq;
	using ComponentBase;
	using DataObject;
	using DataObject.Enum;
	using Interface.ComponentBase;
	using log4net;

	public class And : DigitalBasicComponent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="And"/> class.
		/// </summary>
		/// <param name="components">The components.</param>
		/// <param name="pParametersManager">The p parameters manager.</param>
		/// <param name="logger">The logger.</param>
		public And(Components components, IParametersManager pParametersManager, ILog logger)
			: base(components, pParametersManager, logger)
		{
		}

		public override void Calculate()
		{
			// return true if all input param has the calculate value with the same value. (all true or all false).
			var logAllTrue = this.ParametersManager.GetInputDynamicParameter().All(p => ((DigitalValue)p.Value).GetCalculateValue());

			this.InternalParametersManager.SetParameter(OUTPUT1, new DigitalValue(logAllTrue));
		}
	}
}