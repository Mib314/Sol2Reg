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
		new long Cycle { get; set; }

		/// <summary>
		/// Gets or sets the cycle time.
		/// </summary>
		/// <value>
		/// The cycle time.
		/// </value>
		new DateTime CycleTime { get; set; }

		/// <summary>
		/// Histroy of Analog/Digital params.
		/// </summary>
		Dictionary<int, IParameters> HistoryValues { get; }

		/// <summary>
		/// Setters the param (Analog and Digital).
		/// </summary>
		/// <param name="code">The code.</param>
		/// <param name="value">The value.</param>
		void SetParameter(string code, IValue value);

		/// <summary>
		/// Setters the param (Analog and Digital).
		/// </summary>
		/// <param name="code">The code.</param>
		/// <param name="args">The <see cref="Sol2Reg.DataObject.Events.ValueEventArgs"/> instance containing the event data.</param>
		void SetParameter(string code, ValueEventArgs args);

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
		/// News the cycle.
		/// </summary>
		/// <param name="cycle">The cycle.</param>
		/// <param name="cycleTime">The cycle time.</param>
		void NewCycle(long cycle, DateTime cycleTime);
	}
}