namespace Sol2Reg.LogicalComponent.ComponentBase
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using DataObject;
	using DataObject.Enum;
	using DataObject.Events;
	using Interface.ComponentBase;
	using log4net;

	/// <summary>
	/// This class manage all input and ouput parameters of a component.
	/// Publisch input & ouptut event when a new info incoming or change.
	///		- Analog & Digital I/O.
	///		- Component input.
	///		- Component param.
	///		- History info (for a number of cycle or a time.
	///		- Initial information.
	/// </summary>
	public class ParametersManager : IInternalParametersManager
	{
		/// <summary>
		/// History of parameters.
		/// </summary>
		private readonly List<IParameters> historyParameters;

		/// <summary>
		/// Current parameters.
		/// </summary>
		private IParameters currentParameters;

		/// <summary>
		/// Last parameters.
		/// </summary>
		private IParameters lastParameters;

		/// <summary>
		/// Ref to the basic component <see cref="IBasicComponent"/>.
		/// </summary>
		private readonly IBasicComponentForParameterManager basicComponent;

		/// <summary>
		/// Helper history input value.
		/// </summary>
		private readonly IHelperHistoryParameters helperHistoryParameters;

		/// <summary>
		/// Log4Net.
		/// </summary>
		private readonly ILog loger;

		/// <summary>
		/// Initializes a new instance of the <see cref="ParametersManager"/> class.
		/// </summary>
		/// <param name="basicComponent">The basic component.</param>
		/// <param name="helperHistoryParameters">The helper history input value.</param>
		/// <param name="loger">The loger.</param>
		public ParametersManager(IBasicComponentForParameterManager basicComponent, IHelperHistoryParameters helperHistoryParameters, ILog loger)
		{
			this.basicComponent = basicComponent;
			this.helperHistoryParameters = helperHistoryParameters;
			this.loger = loger;
			this.historyParameters = historyParameters;
			this.historyParameters = new List<IParameters>();
		}

		#region Properties

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
		public long? HistoryCycleDuration { get; set; }

		/// <summary>
		/// Gets or sets the history frequency.
		/// - If history duration is a time => frequency is [sec].
		/// - If history duration is a cycle number => frequency is [number of cycle]
		/// </summary>
		/// <value>
		/// The history frequency.
		/// </value>
		public int HistoryFrequency { get; set; }

		/// <summary>
		/// Gets the current cycle number.
		/// </summary>
		public long Cycle { get { return this.currentParameters.Cycle; } }

		/// <summary>
		/// Gets or sets the cycle time.
		/// </summary>
		/// <value>
		/// The cycle time.
		/// </value>
		public DateTime CycleTime { get { return this.currentParameters.CycleTime; } }

		/// <summary>
		/// Histroy of Analog/Digital params.
		/// </summary>
		public List<IParameters> HistoryValues { get { return this.historyParameters; } }

		/// <summary>
		/// Occurs when [event output change].
		/// </summary>
		public event OutputChangeNotificationHandler EventOutputChange;
		#endregion

		/// <summary>Getters the param.</summary>
		/// <param name="code">The key.</param>
		/// <returns>Return parameter.</returns>
		public IParameter GetParameter(string code)
		{
			return this.currentParameters.GetParameter(code);
		}

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		public void Initialize(IParameters componentParmeters)
		{
			this.currentParameters = componentParmeters;
		}


		/// <summary>
		/// Determines whether [is all input param uptodate].
		/// </summary>
		/// <returns>
		///   <c>true</c> if [is all input param uptodate]; otherwise, <c>false</c>.
		/// </returns>
		public bool IsAllInputParamUptodate()
		{
			return this.currentParameters.IsAllInputParamUptodate();
		}

		#region Setter & Getter for Analog and Digital params.

		/// <summary>
		/// Setters the param (Analog and Digital).
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		public void SetParameter(string key, IValue value)
		{
			if (this.CheckParmaeterKey(key))
			{
				this.currentParameters.SetParameter(key, value);
			}
		}

		/// <summary>
		/// Setters the param (Analog and Digital).
		/// </summary>
		/// <param name="args">The <see cref="ParameterEventArgs"/> instance containing the event data.</param>
		public void SetParameter(ParameterEventArgs args)
		{
			if (args != null && args.Parameter != null && args.Parameter.Value != null)
			{
				this.SetParameter(args.Parameter.Key, args.Parameter.Value);
			}
		}

		/// <summary>
		/// Gets the out parameters.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<IParameter> GetOutputParameters()
		{
			return this.currentParameters.Where(p => p.ParameterDirection == EnumParameterDirection.Output);
		}

		public IList<IParameter> GetInputDynamicParameter()
		{
			return this.currentParameters.Where(p => p.ParameterDirection == EnumParameterDirection.Input && p.IsDynamic).ToList();
		}
		#endregion

		#region Manage events.

		/// <summary>
		/// Overides the current parameters with last parameters.
		/// </summary>
		public void OverideCurrentParametersWithLastParameters()
		{
			this.currentParameters = this.lastParameters;
			this.lastParameters = this.historyParameters[0];
			this.historyParameters.RemoveAt(0);
		}

		/// <summary>
		/// Set a new cyle.
		/// </summary>
		/// <param name="cycle">The cycle.</param>
		/// <param name="cycleTime">The cycle time.</param>
		/// <returns>
		/// True is the new cycle is set.
		/// </returns>
		public bool SetNewCycle(long cycle, DateTime cycleTime)
		{
			if (this.currentParameters.Cycle < cycle && this.currentParameters.CycleTime < cycleTime)
			{
				if (this.lastParameters != null)
				{
					// Add on the first position to the list.
					this.historyParameters.Insert(0, this.lastParameters);
				}
				this.lastParameters = this.currentParameters;
				this.CreateNewCurrentParameters(cycle, cycleTime);
				this.loger.Debug(string.Format("Component {0} : The set new cycle was called succesfuly", this.basicComponent.Code));
				return true;
			}

			this.loger.Warn(string.Format("Component {0} : The set new cycle was canceled why the current cycle and the cycletime was not ok", this.basicComponent.Code));
			return false;
		}

		/// <summary>
		/// Sends all output event.
		/// </summary>
		public void SendOutputsEvent()
		{
			foreach (var outputParameter in this.GetOutputParameters())
			{
				this.OnEventOutputParameterToSend(outputParameter);
			}
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

															 this.SetParameter(args);
															 if (this.helperHistoryParameters.CheckIfAllParamIsUpToDate(this.currentParameters))
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
			return this.currentParameters.Any(param => param.Key == key);
		}

		/// <summary>
		/// Creates the new current parameters.
		/// </summary>
		private void CreateNewCurrentParameters(long cycle, DateTime cycleTime)
		{
			this.currentParameters = new Parameters(cycle, cycleTime);

			foreach (var parameter in this.lastParameters)
			{
				var param = new Parameter
				{
					Key = parameter.Key,
					Comment = parameter.Comment,
					RecieveOutputKey = parameter.RecieveOutputKey,
					RecieveOutputComponentKey = parameter.RecieveOutputComponentKey,
					Value = parameter.Value
				};
				this.currentParameters.Add(param);
			}

		}

		/// <summary>
		/// Called when output parameter sended.
		/// </summary>
		/// <param name="outputParameter">The output parameter.</param>
		private void OnEventOutputParameterToSend(IParameter outputParameter)
		{
			this.SetParameter(outputParameter.Key, outputParameter.Value);
			var output = new ParameterEventArgs(this.basicComponent.Code, outputParameter);
			OutputChangeNotificationHandler notificationHandler = this.EventOutputChange;
			if (notificationHandler != null) notificationHandler(this, output);
		}
	}
}