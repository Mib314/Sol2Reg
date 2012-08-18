namespace Sol2Reg.LogicalComponent.Interface.ComponentBase
{
	using System;
	using System.Collections.Generic;
	using Sol2Reg.DataObject;
	using Sol2Reg.DataObject.Events;

	/// <summary>
	/// 
	/// </summary>
	public interface IHelperHistoryParameters {
		/// <summary>
		/// Adds the digital current values to history.
		/// </summary>
		/// <param name="currentValue">The current value.</param>
		/// <param name="historyTimeDuration">Duration of the history time.</param>
		/// <param name="historyCycleDuration">Duration of the history cycle.</param>
		/// <param name="historyFrequency">The history frequency.</param>
		/// <param name="cycle">The cycle.</param>
		/// <param name="historyValues">The history values.</param>
		/// <param name="currentTime">The current time.</param>
		void AddCurrentValuesToHistory(IDictionary<string, IValue> currentValue, DateTime historyTimeDuration, int? historyCycleDuration, int historyFrequency, long cycle, Dictionary<long, IDictionary<string, IValue>> historyValues, DateTime currentTime);

		/// <summary>
		/// Calculates if this value is to save.
		/// </summary>
		/// <param name="historyTimeDuration">Duration of the history time.</param>
		/// <param name="historyCycleDuration">Duration of the history cycle.</param>
		/// <param name="historyFrequency">The history frequency.</param>
		/// <param name="currentTime">The current time.</param>
		/// <param name="historyValues">The history values.</param>
		/// <returns>
		/// List of element to old.
		/// </returns>
		IEnumerable<long> CalculateIfThisValueIsToSave(DateTime historyTimeDuration, int? historyCycleDuration, int historyFrequency, DateTime currentTime, Dictionary<long, IDictionary<string, IValue>> historyValues);

		/// <summary>
		/// Saves the last param.
		/// </summary>
		/// <param name="paramName">Name of the param.</param>
		/// <param name="args">The <see cref="Sol2Reg.DataObject.Events.ValueEventArgs"/> instance containing the event data.</param>
		/// <param name="lastParams">The last params.</param>
		void SaveTheLastParam(string paramName, ValueEventArgs args, IParameters lastParams);

		/// <summary>
		/// Checks if all param is up to date (same cycle for DynamicValue).
		/// </summary>
		/// <param name="currentParams">The current params.</param>
		/// <returns></returns>
		bool CheckIfAllParamIsUpToDate(IParameters currentParams);
	}
}