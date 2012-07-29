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
	public sealed class ParametersManager : IParametersManager
	{
		private readonly HistoryParameters historyParameters;

		/// <summary>
		/// Ref to the basic component <see cref="IBasicComponent"/>.
		/// </summary>
		private readonly IBasicComponentForParameterManager basicComponent;

		/// <summary>
		/// Helper history input value.
		/// </summary>
		private readonly IHelperHistoryIOValue helperHistoryIoValue;

		/// <summary>
		/// Initializes a new instance of the <see cref="ParametersManager"/> class.
		/// </summary>
		/// <param name="basicComponent">The basic component.</param>
		/// <param name="helperHistoryIoValue">The helper history input value.</param>
		/// <param name="historyParameters">The history parameters.</param>
		public ParametersManager(IBasicComponentForParameterManager basicComponent, IHelperHistoryIOValue helperHistoryIoValue, HistoryParameters historyParameters)
		{
			this.basicComponent = basicComponent;
			this.helperHistoryIoValue = helperHistoryIoValue;
			this.historyParameters = historyParameters;
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
		/// Gets or sets a value indicating whether [output state].
		/// </summary>
		/// <value>
		///   <c>true</c> if [output state]; otherwise, <c>false</c>.
		/// </value>
		public bool OutputState { get; set; }

		#endregion

		/// <summary>
		/// Registers the link input.
		/// </summary>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <param name="parametersManagerEventSender">The value manager event sender.</param>
		public void RegisterLinkInput(string parameterName, IParametersManager parametersManagerEventSender)
		{
			throw new NotImplementedException();
		}

		/// <summary>Getters the param.</summary>
		/// <param name="code">The code.</param>
		/// <returns>Return parameter.</returns>
		public IParameter GetParameter(string code)
		{
			return this.historyParameters.CurrentParameters.GetParameter(code);
		}

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		/// <returns>This instance.</returns>
		IParametersManager IParametersManager.Initialize()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		/// <returns>This instance.</returns>
		public IParametersManager Initialize()
		{
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
		/// Setters the param (Analog and Digital).
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		public void SetterParam(string key, IValue value)
		{
			this.SetterParamWithoutEvent(key, value);
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