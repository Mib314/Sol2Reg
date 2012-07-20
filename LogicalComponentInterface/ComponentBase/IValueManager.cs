namespace Sol2Reg.LogicalComponent.Interface
{
	using System;
	using DataObject;
	using DataObject.Events;

	public interface IValueManager
	{
		/// <summary>
		/// Occurs when [event output change].
		/// </summary>
		event ValueChangeHandler EventOutputChange;

		/// <summary>
		/// Occurs when [event input change].
		/// </summary>
		event ValueChangeHandler EventInputChange;

		/// <summary>
		/// Gets the component code.
		/// </summary>
		string Code { get; }

		/// <summary>
		/// Gets the current cycle number.
		/// </summary>
		long Cycle { get; }

		/// <summary>
		/// Gets or sets the cycle time.
		/// </summary>
		/// <value>
		/// The cycle time.
		/// </value>
		DateTime CycleTime { get; }

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
		int? HistoryCycleDuration { get; set; }

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
		/// Registers the link input.
		/// </summary>
		/// <param name="paramName">Name of the param.</param>
		/// <param name="valueManagerEventSender">The value manager event sender.</param>
		void RegisterLinkInput(string paramName, IValueManager valueManagerEventSender);

		/// <summary>
		/// Getters the param.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns>Return parameter value (IValue).</returns>
		IValue GetterParam(string key);

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		/// <returns>This instance.</returns>
		IValueManager Initialize();
	}
}