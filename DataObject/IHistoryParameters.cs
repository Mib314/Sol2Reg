namespace Sol2Reg.DataObject
{
	using System;
	using System.Collections.Generic;

	public interface IHistoryParameters {
		/// <summary>
		/// Histroy of Analog/Digital params.
		/// </summary>
		List<IParameters> HistoryValues { get; }

		/// <summary>
		/// Gets the current parameters.
		/// </summary>
		IParameters CurrentParameters { get; }

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
	}
}