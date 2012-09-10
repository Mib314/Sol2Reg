namespace Sol2Reg.LogicalComponent.Interface.ComponentBase
{
	using System;
	using System.Collections.Generic;
	using DataObject;

	public interface IInternalParametersManager : IParametersManager
	{
		/// <summary>
		/// Gets or sets the duration of the history time.
		/// If this value is null no history per time.
		/// </summary>
		/// <value>
		/// The duration of the history time.
		/// </value>
		DateTime? HistoryTimeDuration { get; set; }

		/// <summary>
		/// Gets or sets the duration of the history cycle.
		/// If this value is null no history per cycle.
		/// </summary>
		/// <value>
		/// The duration of the history cycle.
		/// </value>
		long? HistoryCycleDuration { get; set; }

		/// <summary>
		/// Gets or sets the history frequency.
		/// - If history duration is a time => frequency is [sec].
		/// - If history duration is a cycle number => frequency is [number of cycle]
		/// </summary>
		/// <value>
		/// The history frequency.
		/// </value>
		int HistoryFrequency { get; set; }

		/// <summary>
		/// Gets the current cycle number.
		/// </summary>
		long Cycle { get; }

		/// <summary>
		/// Gets the current cycle time.
		/// </summary>
		DateTime CycleTime { get; }

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
		bool SetNewCycle(long cycle, DateTime cycleTime);
	}
}