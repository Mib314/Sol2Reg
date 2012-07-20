namespace Sol2Reg.LogicalComponent.Interface
{
	using System;

	public interface IBasicComponentForValueManager
	{
		/// <summary>
		/// Gets the component code.
		/// </summary>
		string Code { get; set; }

		/// <summary>
		/// Gets the input value manager.
		/// </summary>
		IValueManager ValueManager { get; }

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