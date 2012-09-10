namespace Sol2Reg.LogicalComponent.Interface.ComponentBase
{
	using System;
	using System.Collections.Generic;
	using Sol2Reg.DataObject;
	using Sol2Reg.DataObject.Events;

	public interface IInternalParametersManager : IParametersManager
	{
		/// <summary>
		/// Gets the current cycle number.
		/// </summary>
		new long Cycle { get; }

		/// <summary>
		/// Gets the current cycle time.
		/// </summary>
		new DateTime CycleTime { get; }

		/// <summary>
		/// Histroy of Analog/Digital params.
		/// </summary>
		List<IParameters> HistoryValues { get; }

		/// <summary>
		/// Called when [event output change].
		/// </summary>
		/// <param name="newOutputValue">The new output value.</param>
		/// <param name="outputName">Name of the output.</param>
		void OnEventOutputChange(IValue newOutputValue, string outputName);

		/// <summary>
		/// Called when [event input change].
		/// </summary>
		/// <param name="newInputValue">The new input value.</param>
		/// <param name="inputName">Name of the input.</param>
		void OnEventInputChange(IValue newInputValue, string inputName);

		/// <summary>
		/// Overides the current parameters with last parameters.
		/// </summary>
		void OverideCurrentParametersWithLastParameters();

		/// <summary>
		/// Sends all output event.
		/// </summary>
		void SendAllOutputEvent();

		/// <summary>
		/// Sets the current cycle.
		/// </summary>
		/// <param name="cycle">The cycle.</param>
		/// <param name="cycleTime">The cycle time.</param>
		/// <returns>True is the new cycle is set.</returns>
		bool SetCurrentCycle(long cycle, DateTime cycleTime);
	}
}