namespace Sol2Reg.LogicalComponent.Interface
{
	using System;
	using System.Collections.Generic;
	using DataObject;
	using DataObject.Events;

	public interface IInternalValueManager : IValueManager
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
		/// Dictionary to register evry analog input and param for this component
		/// </summary>
		Dictionary<string, IValue> CurrentParams { get; }

		/// <summary>
		/// Gets the last params.
		/// </summary>
		Dictionary<string, IValue> LastParams { get; }

		/// <summary>
		/// Histroy of Analog/Digital params.
		/// </summary>
		Dictionary<int, IDictionary<string, IValue>> HistoryValues { get; }

		/// <summary>
		/// Setters the param (Analog and Digital).
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		void SetterParam(string key, IValue value);

		/// <summary>
		/// Setters the param (Analog and Digital).
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="args">The <see cref="Sol2Reg.DataObject.Events.ValueEventArgs"/> instance containing the event data.</param>
		void SetterParam(string key, ValueEventArgs args);

		/// <summary>
		/// Called when [event output change].
		/// </summary>
		/// <param name="newOutputValue">The new output value.</param>
		void OnEventOutputChange(ValueEventArgs newOutputValue);

		/// <summary>
		/// Called when [event input change].
		/// </summary>
		/// <param name="newInputValue">The new input value.</param>
		/// <param name="inputName">Name of the input.</param>
		void OnEventInputChange(IValue newInputValue, string inputName);
	}
}