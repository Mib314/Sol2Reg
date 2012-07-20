namespace Sol2Reg.LogicalComponent.ComponentBase
{
	using System;
	using System.Collections.Generic;
	using DataObject;
	using DataObject.Events;
	using DateTime = System.DateTime;

	/// <summary>
	/// Helper class to calculate the history for ValueManager.
	/// </summary>
	public class HelperHistoryIoValue : IHelperHistoryIOValue
	{
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
		public void AddCurrentValuesToHistory(IDictionary<string, IValue> currentValue, DateTime historyTimeDuration, int? historyCycleDuration, int historyFrequency, long cycle, Dictionary<long, IDictionary<string, IValue>> historyValues, DateTime currentTime)
		{
			if (historyValues == null) return;

			var elementToDelete = CalculateIfThisValueIsToSave(historyTimeDuration, historyCycleDuration, historyFrequency, currentTime, historyValues);
			foreach (var key in elementToDelete)
			{
				historyValues.Remove(key);
			}

			historyValues.Add(cycle, currentValue);
		}

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
		public IEnumerable<long> CalculateIfThisValueIsToSave(DateTime historyTimeDuration, int? historyCycleDuration, int historyFrequency, DateTime currentTime, Dictionary<long, IDictionary<string, IValue>> historyValues)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Saves the last param.
		/// </summary>
		/// <param name="paramName">Name of the param.</param>
		/// <param name="args">The <see cref="Sol2Reg.DataObject.Events.ValueEventArgs"/> instance containing the event data.</param>
		/// <param name="lastParams">The last params.</param>
		public void SaveTheLastParam(string paramName, ValueEventArgs args, Dictionary<string, IValue> lastParams)
		{
			lastParams[paramName] = args.Value;
		}

		/// <summary>
		/// Checks if all param is up to date (same cycle for DynamicValue).
		/// </summary>
		/// <param name="currentParams">The current params.</param>
		/// <returns></returns>
		public bool CheckIfAllParamIsUpToDate(Dictionary<string, IValue> currentParams)
		{
			long cycle = 0;
			foreach (var currentParam in currentParams)
			{
				if(!currentParam.Value.DynamicValue)
				{
					continue;
				}

				if(cycle == 0)
				{
					cycle = currentParam.Value.Cycle;
				}

				if (cycle != currentParam.Value.Cycle)
				{
					return false;
				}
			}
			return true;
		}
	}
}