namespace Sol2Reg.LogicalComponent.ComponentBase
{
	using System.Linq;
	using DataObject;
	using DataObject.Enum;
	using Interface;
	using Interface.ComponentBase;
	using log4net;

	/// <summary>
	/// This abstact class add n input parameters methode (InitializeInputPorts) and add one output parameter with the initialize methode.
	/// </summary>
	/// <remarks>Input parameter is reconize with the position on position (start with 0).</remarks>
	public abstract class DigitalBasicComponent : BasicComponent, IDigitalBasicComponent
	{
		public const string PORT_PREFIX = "Input";

		/// <summary>
		/// Initializes a new instance of the <see cref="DigitalBasicComponent"/> class.
		/// </summary>
		/// <param name="components">The components.</param>
		/// <param name="parametersManager">The parameters manager.</param>
		/// <param name="loger">The loger.</param>
		protected DigitalBasicComponent(Components components, IParametersManager parametersManager, ILog loger)
			: base(components, parametersManager, loger) {}

		public void InitializeInputPorts(int totalInputPortNumber)
		{
			// Delete another input parameters.
			if (this.InitialParameters.Any(p => p.ParameterDirection == EnumParameterDirection.Input))
			{
				foreach (var inputParam in this.InitialParameters.Where(p => p.ParameterDirection == EnumParameterDirection.Input))
				{
					this.InitialParameters.Remove(inputParam);
				}
			}

			for (int i = 0; i < totalInputPortNumber - 1; i++)
			{
				this.InitialParameters.Add(new Parameter().Initialize(this.GetInputName(i), new DigitalValue(), EnumParameterDirection.Input, string.Format("Digital input number {0}", i)));
			}
		}

		public new void Initialize(string code)
		{
			this.InitialParameters.Add(new Parameter().Initialize(OUTPUT1, new DigitalValue(), EnumParameterDirection.Output, "AND component response."));
			base.Initialize(code);
		}

		/// <summary>
		/// Gets the name of the input.
		/// </summary>
		/// <param name="position">The position on the list of input parameters (Start with 0).</param>
		/// <returns></returns>
		private string GetInputName(int position)
		{
			return string.Format("{0}{1}", PORT_PREFIX, position);
		}
	}
}
