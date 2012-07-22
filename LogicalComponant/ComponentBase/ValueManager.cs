namespace Sol2Reg.LogicalComponent.ComponentBase
{
	using System;
	using System.Collections.Generic;
	using DataObject;
	using DataObject.Events;
	using Interface;

	/// <summary>
	/// This class manage all input and ouput of all component.
	/// Publisch input & ouptut event when a new info incoming or change.
	///		- Analog & Digital I/O.
	///		- Component input.
	///		- Component param.
	///		- History info (for a number of cycle or a time.
	///		- Initial information.
	/// </summary>
	public sealed class ValueManager : IInternalValueManager
	{
		/// <summary>
		/// Ref to the basic component <see cref="Sol2Reg.LogicalComponent.Interface.IBasicComponent"/>.
		/// </summary>
		private readonly IBasicComponentForValueManager basicComponent;

		/// <summary>
		/// Helper history input value.
		/// </summary>
		private readonly IHelperHistoryIOValue helperHistoryIoValue;

		/// <summary>
		/// Initializes a new instance of the <see cref="ValueManager"/> class.
		/// </summary>
		/// <param name="basicComponent">The basic component.</param>
		/// <param name="helperHistoryIoValue">The helper history input value.</param>
		public ValueManager(IBasicComponentForValueManager basicComponent, IHelperHistoryIOValue helperHistoryIoValue)
		{
			this.basicComponent = basicComponent;
			this.helperHistoryIoValue = helperHistoryIoValue;
		}

		#region Properties

		/// <summary>
		/// Occurs when [event output change].
		/// </summary>
		public event ValueChangeHandler EventOutputChange;

		/// <summary>
		/// Occurs when [event input change].
		/// </summary>
		public event ValueChangeHandler EventInputChange;

		/// <summary>
		/// Gets the component code.
		/// </summary>
		public string Code { get { return this.basicComponent.Code; } }

		/// <summary>
		/// Gets the current cycle number.
		/// </summary>
		public long Cycle { get; set; }

		/// <summary>
		/// Gets or sets the cycle time.
		/// </summary>
		/// <value>
		/// The cycle time.
		/// </value>
		public DateTime CycleTime { get; set; }

		/// <summary>
		/// Dictionary to register evry analog input and param for this component
		/// </summary>
		public Dictionary<string, IValue> CurrentParams { get; private set; }

		/// <summary>
		/// Gets the last params.
		/// </summary>
		public Dictionary<string, IValue> LastParams { get; private set; }

		/// <summary>
		/// Histroy of Analog/Digital params.
		/// </summary>
		public Dictionary<int, IDictionary<string, IValue>> HistoryValues { get; private set; }

		/// <summary>
		/// Gets or sets a value indicating whether [output state].
		/// </summary>
		/// <value>
		///   <c>true</c> if [output state]; otherwise, <c>false</c>.
		/// </value>
		public bool OutputState { get; set; }

		/// <summary>
		/// Gets or sets the duration of the history time.
		/// If this value is null no history per time.
		/// </summary>
		/// <value>
		/// The duration of the history time.
		/// </value>
		public DateTime? HistoryTimeDuration { get; set; }

		/// <summary>
		/// Gets or sets the duration of the history cycle.
		/// If this value is null no history per cycle.
		/// </summary>
		/// <value>
		/// The duration of the history cycle.
		/// </value>
		public int? HistoryCycleDuration { get; set; }

		/// <summary>
		/// Gets or sets the history frequency.
		/// - If history duration is a time => frequency is [sec].
		/// - If history duration is a cycle number => frequency is [number of cycle]
		/// </summary>
		/// <value>
		/// The history frequency.
		/// </value>
		public int HistoryFrequency { get; set; }

		#endregion

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		/// <returns>This instance.</returns>
		public IValueManager Initialize()
		{
			this.CurrentParams = new Dictionary<string, IValue>();

			this.Cycle = 0;
			this.CycleTime = DateTime.MinValue;

			// HistoryFrequency is by default disabled.
			this.HistoryValues = new Dictionary<int, IDictionary<string, IValue>>();
			this.HistoryCycleDuration = null;
			this.HistoryTimeDuration = null;
			this.HistoryFrequency = 0;
			return this;
		}

		public bool ValidCycle(IValue value)
		{
			return this.Cycle == value.Cycle && this.CycleTime == value.CycleTime;
		}

		#region Setter & Getter for Analog and Digital params.
		/// <summary>
		/// Setters the param (Analog and Digital).
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		public void SetterParam(string key, IValue value)
		{
			this.SetterParamWithoutEvent(key,value);
			this.OnEventInputChange(value, key);
		}

		/// <summary>
		/// Setters the param (Analog and Digital).
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="args">The <see cref="Sol2Reg.DataObject.Events.ValueEventArgs"/> instance containing the event data.</param>
		public void SetterParam(string key, ValueEventArgs args)
		{
			if (args != null && args.Value != null)
			{
				this.SetterParam(key, args.Value);
			}
		}

		/// <summary>
		/// Getters the param.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns>Return parameter value (IValue).</returns>
		public IValue GetterParam(string key)
		{
			if (this.CurrentParams.ContainsKey(key))
			{
				return this.CurrentParams[key];
			}

			return null;
		}
		#endregion

		#region Manage events.


		/// <summary>
		/// Called when [event output change].
		/// </summary>
		/// <param name="newOutputValue">The new output value.</param>
		/// <param name="outputName">Name of the output.</param>
		public void OnEventOutputChange(IValue newOutputValue, string outputName)
		{
			this.SetterParam(outputName, newOutputValue);
			var output = new ValueEventArgs(newOutputValue, this.basicComponent.Code, outputName);
			ValueChangeHandler handler = this.EventOutputChange;
			if (handler != null) handler(this, output);
		}

		/// <summary>
		/// Called when output value change to send .
		/// </summary>
		/// <param name="newInputValue">The new input value.</param>
		/// <param name="inputName">Name of the input.</param>
		public void OnEventInputChange(IValue newInputValue, string inputName)
		{
			this.SetterParamWithoutEvent(inputName, newInputValue);
			var args = new ValueEventArgs(newInputValue, this.Code, inputName);
			ValueChangeHandler handler = this.EventInputChange;
			if (handler != null) handler(this, args);
		}

		/// <summary>
		/// Registers the link input.
		/// </summary>
		/// <param name="paramName">Name of the param.</param>
		/// <param name="valueManagerEventSender">The value manager event sender.</param>
		public void RegisterLinkInput(string paramName, IValueManager valueManagerEventSender)
		{
			valueManagerEventSender.EventOutputChange += (o, args) =>
			{
				this.SetterParamWithoutEvent(paramName, args);
				this.helperHistoryIoValue.SaveTheLastParam(paramName, args, this.LastParams);
				if (this.helperHistoryIoValue.CheckIfAllParamIsUpToDate(this.CurrentParams))
				{
					this.basicComponent.Calculate();
				}
			};
		}
		#endregion

		private void SetterParamWithoutEvent(string key, IValue value)
		{
			if (!this.CurrentParams.ContainsKey(key))
			{
				this.CurrentParams.Add(key, value);
			}
			else
			{
				this.CurrentParams[key] = value;
			}
		}

		private void SetterParamWithoutEvent(string key, ValueEventArgs args)
		{
			if (args != null && args.Value != null)
			{
				this.SetterParamWithoutEvent(key, args.Value);
			}
		}

	}
}