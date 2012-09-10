namespace Sol2Reg.LogicalComponent.ComponentBase
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using DataObject;
	using DataObject.Events;
	using Interface.ComponentBase;

	/// <summary>
	/// This class manage all input and ouput of all component.
	/// Publisch input & ouptut event when a new info incoming or change.
	///		- Analog & Digital I/O.
	///		- Component input.
	///		- Component param.
	///		- History info (for a number of cycle or a time.
	///		- Initial information.
	/// </summary>
	public sealed class ParametersManager : IInternalParametersManager
	{
		private readonly IHistoryParameters historyParameters;
		/// <summary>
		/// List of parameters define from the compenent.
		/// </summary>
		/// <remarks>Don't change this list.</remarks>
		private IParameters componentParmateres;

		/// <summary>
		/// Ref to the basic component <see cref="IBasicComponent"/>.
		/// </summary>
		private readonly IBasicComponentForParameterManager basicComponent;

		/// <summary>
		/// Helper history input value.
		/// </summary>
		private readonly IHelperHistoryParameters helperHistoryParameters;

		/// <summary>
		/// Initializes a new instance of the <see cref="ParametersManager"/> class.
		/// </summary>
		/// <param name="basicComponent">The basic component.</param>
		/// <param name="helperHistoryParameters">The helper history input value.</param>
		/// <param name="historyParameters">The history parameters.</param>
		public ParametersManager(IBasicComponentForParameterManager basicComponent, IHelperHistoryParameters helperHistoryParameters, IHistoryParameters historyParameters)
		{
			this.basicComponent = basicComponent;
			this.helperHistoryParameters = helperHistoryParameters;
			this.historyParameters = historyParameters;
		}

		#region Properties

		/// <summary>
		/// Occurs when [event output change].
		/// </summary>
		public event OutputChangeNotificationHandler EventOutputChange;

		/// <summary>
		/// Occurs when [event input change].
		/// </summary>
		public event OutputChangeNotificationHandler EventInputChange;

		#endregion
		/// <summary>Getters the param.</summary>
		/// <param name="code">The key.</param>
		/// <returns>Return parameter.</returns>
		public IParameter GetParameter(string code)
		{
			return this.historyParameters.CurrentParameters.GetParameter(code);
		}

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		/// <returns>This instance.</returns>
		public IParametersManager Initialize(IParameters componentParmeters)
		{
			this.componentParmateres = componentParmeters;
			return this;
		}


		/// <summary>
		/// Determines whether [is all input param uptodate].
		/// </summary>
		/// <returns>
		///   <c>true</c> if [is all input param uptodate]; otherwise, <c>false</c>.
		/// </returns>
		public bool IsAllInputParamUptodate()
		{
			return this.historyParameters.CurrentParameters.IsAllInputParamUptodate();
		}

		#region Setter & Getter for Analog and Digital params.

		/// <summary>
		/// Gets the current cycle number.
		/// </summary>
		public long Cycle
		{
			get { return this.historyParameters.CurrentParameters.Cycle; }
		}

		/// <summary>
		/// Gets or sets the cycle time.
		/// </summary>
		/// <value>
		/// The cycle time.
		/// </value>
		public DateTime CycleTime
		{
			get { return this.historyParameters.CurrentParameters.CycleTime; }
		}

		/// <summary>
		/// Histroy of Analog/Digital params.
		/// </summary>
		public List<IParameters> HistoryValues
		{
			get { return this.historyParameters.HistoryValues; }
		}

		/// <summary>
		/// Setters the param (Analog and Digital).
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		public void SetParameter(string key, IValue value)
		{
			if (this.CheckParmaeterKey(key))
			{
				this.historyParameters.CurrentParameters.SetParameter(key, value);
			}
		}

		/// <summary>
		/// Setters the param (Analog and Digital).
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="args">The <see cref="Sol2Reg.DataObject.Events.ValueEventArgs"/> instance containing the event data.</param>
		public void SetParameter(string key, ValueEventArgs args)
		{
			if (args != null && args.Value != null)
			{
				this.SetParameter(key, args.Value);
			}
		}

		/// <summary>
		/// Gets the out parameters.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<IParameter> GetOutputParameters()
		{
			return this.historyParameters.CurrentParameters.Params.Where(p => p.Value.ParameterDirection == EnumParameterDirection.Output).Select(p => p.Value);
		}

		public IList<IParameter> GetInputDynamicParameter()
		{
			return this.historyParameters.CurrentParameters.Params.Where(p => p.Value.ParameterDirection == EnumParameterDirection.Input && p.Value.IsDynamic).Select(p => p.Value).ToList();
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
			this.SetParameter(outputName, newOutputValue);
			var output = new ValueEventArgs(newOutputValue, this.basicComponent.Code, outputName);
			OutputChangeNotificationHandler notificationHandler = this.EventOutputChange;
			if (notificationHandler != null) notificationHandler(this, output);
		}

		/// <summary>
		/// Called when output value change to send .
		/// </summary>
		/// <param name="newInputValue">The new input value.</param>
		/// <param name="inputName">Name of the input.</param>
		public void OnEventInputChange(IValue newInputValue, string inputName)
		{
			this.SetParameter(inputName, newInputValue);
			var args = new ValueEventArgs(newInputValue, this.basicComponent.Code, inputName);
			OutputChangeNotificationHandler notificationHandler = this.EventInputChange;
			if (notificationHandler != null) notificationHandler(this, args);
		}

		/// <summary>
		/// Overides the current parameters with last parameters.
		/// </summary>
		public void OverideCurrentParametersWithLastParameters()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// News the cycle.
		/// </summary>
		/// <param name="cycle">The cycle.</param>
		/// <param name="cycleTime">The cycle time.</param>
		public bool SetCurrentCycle(long cycle, DateTime cycleTime)
		{
			if (this.historyParameters.CurrentParameters.Cycle < cycle && this.historyParameters.CurrentParameters.CycleTime < cycleTime)
			{
				this.historyParameters.CurrentParameters.Cycle = cycle;
				this.historyParameters.CurrentParameters.CycleTime = CycleTime;
				return true;
			}
			return false;
		}

		/// <summary>
		/// Sends all output event.
		/// </summary>
		public void SendAllOutputEvent()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Registers the link input.
		/// </summary>
		/// <param name="componentKey">The component key.</param>
		/// <param name="parameterKey">The parameter key.</param>
		/// <param name="valueManagerEventSender">The value manager event sender.</param>
		public void RegisterComponentLink(string componentKey, string parameterKey, IParametersManager valueManagerEventSender)
		{
			valueManagerEventSender.EventOutputChange += (o, args) =>
														 {
															 // Find the local parameter key with the recieve key (key of the output parameter)
															 // Set the new value on current parameter list.
															 // Check if all param is uptodate
															 //		Calculate

															 string code = this.historyParameters.CurrentParameters.GetParameterKey(componentKey, parameterKey);
															 this.SetParameter(code, args);
															 if (this.helperHistoryParameters.CheckIfAllParamIsUpToDate(this.historyParameters.CurrentParameters))
															 {
																 this.basicComponent.Calculate();
															 }
														 };
		}
		#endregion

		/// <summary>
		/// Checks the parmaeter key. If exist on this.componentParmateres.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns>True if exist.</returns>
		private bool CheckParmaeterKey(string key)
		{
			return this.componentParmateres.Params.Any(param => param.Key == key);
		}
	}
}