namespace Sol2Reg.LogicalComponent.ComponentBase
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using DataObject;
	using DataObject.Enum;
	using DataObject.Events;
	using Interface.ComponentBase;

	/// <summary>
	/// Helper class to calculate the history for parameterManager.
	/// </summary>
	public class HelperHistoryParameters : IHelperHistoryParameters
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
		/// <param name="args">The <see cref="ParameterEventArgs"/> instance containing the event data.</param>
		/// <param name="lastParams">The last params.</param>
		public void SaveTheLastParam(ParameterEventArgs args, IParameters lastParams)
		{
			lastParams.SetParameter(args.Parameter.Key, args.Parameter.Value);
		}

		/// <summary>
		/// Checks if all param is up to date (same cycle for DynamicValue).
		/// </summary>
		/// <param name="currentParams">The current params.</param>
		/// <returns></returns>
		public bool CheckIfAllParamIsUpToDate(IEnumerable<IParameter> currentParams)
		{
			// Check if all input dynamic (input param with a recieve code) param is up to date
			return currentParams.Where(param => (param.ParameterDirection == EnumParameterDirection.Input) || (!string.IsNullOrWhiteSpace(param.RecieveOutputKey))).All(currentParam => currentParam.IsUptoDate);
		}
	}
}