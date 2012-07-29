namespace Sol2Reg.LogicalComponent.Interface.ComponentBase
{
	using System;

	public interface IBasicComponentForParameterManager
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

		/// <summary>
		/// Sets the current cycle.
		/// </summary>
		/// <param name="cycle">The cycle.</param>
		/// <param name="cycleTime">The cycle time.</param>
		bool SetCurrentCycle(long cycle, DateTime cycleTime);
	}
}