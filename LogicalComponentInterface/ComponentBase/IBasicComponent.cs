namespace Sol2Reg.LogicalComponent.Interface.ComponentBase
{
	public interface IBasicComponent
	{

		/// <summary>
		/// Gets the component code.
		/// </summary>
		string Code { get; set; }

		/// <summary>
		/// Gets the input value manager.
		/// </summary>
		IParametersManager ParametersManager { get; }

		/// <summary>
		/// Executes the calculation.
		/// </summary>
		void Calculate();

		void RegisterLinkForInputParameter(string inputParameterKey, string recieveOutputComponentKey, string recieveOutputParameterKey);
	}
}