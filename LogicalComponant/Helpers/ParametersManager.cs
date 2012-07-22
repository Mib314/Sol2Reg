namespace Sol2Reg.LogicalComponent.Helpers
{
	using System.Collections.Generic;
	using System.Linq;
	using DataObject;
	using Interface;

	public class ParametersManager
	{
		private readonly IValueManager valueManager;
		private readonly List<MappInputNameToOutput> inputOutputNames;

		/// <summary>
		/// Initializes a new instance of the <see cref="ParametersManager"/> class.
		/// </summary>
		/// <param name="valueManager">The value manager.</param>
		public ParametersManager(IValueManager valueManager)
		{
			this.valueManager = valueManager;
			this.inputOutputNames = new List<MappInputNameToOutput>();
		}

		public void Add(string inputName, string outputName, bool isInverted = false)
		{
			this.inputOutputNames.Add(new MappInputNameToOutput{InputParamName = inputName, OutputParamName = outputName, IsInverted = isInverted});
		}

		/// <summary>
		/// Checks if params is valid (Cycle and CycleTime).
		/// </summary>
		/// <returns>True if is Ok.</returns>
		public bool CheckIfParamsIsValid()
		{
			return this.inputOutputNames.Select(mappInputNameToOutput => this.valueManager.GetterParam(mappInputNameToOutput.InputParamName)).All(param => this.valueManager.ValidCycle(param));
		}

	}
}