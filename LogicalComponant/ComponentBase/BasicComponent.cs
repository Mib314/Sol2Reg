namespace Sol2Reg.LogicalComponent.ComponentBase
{
	using System;
	using System.Linq;
	using System.Reflection;
	using DataObject;
	using DataObject.Enum;
	using Interface;
	using Interface.ComponentBase;
	using log4net;

	/// <summary>
	/// Basic component for analog and digital component.
	/// </summary>
	[System.Runtime.InteropServices.GuidAttribute("6031411B-4D52-4BAA-9B3F-388BB53ED80A")]
	public abstract class BasicComponent : ICounter, IBasicComponent, IBasicComponentForParameterManager
	{
		internal readonly ILog Logger;

		/// <summary>
		/// Output parameter 1.
		/// </summary>
		public const string OUTPUT1 = "Out1";

		/// <summary>
		/// singletons with all composant
		/// </summary>
		private readonly Components components;

		/// <summary>
		/// Initializes a new instance of the <see cref="BasicComponent"/> class.
		/// </summary>
		/// <param name="components">The components.</param>
		/// <param name="parametersManager">The parameters manager.</param>
		/// <param name="logger">The logger.</param>
		protected BasicComponent(Components components, IParametersManager parametersManager, ILog logger)
		{
			this.Logger = logger;
			this.components = components;
			this.components.Add(this);
			this.InternalParametersManager = (IInternalParametersManager)parametersManager;
		}

		/// <summary>Gets or sets the parameters.</summary>
		/// <value>The parameters.</value>
		public IParameters InitialParameters { get; set; }

		/// <summary>
		/// Gets the component code.
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// Gets or sets the count.
		/// </summary>
		/// <value>
		/// The count.
		/// </value>
		public long Count { get; private set; }

		/// <summary>
		/// Gets the input value manager.
		/// </summary>
		public IParametersManager ParametersManager { get { return this.InternalParametersManager; } }

		/// <summary>
		/// Gets the input value manager for internal ressource only.
		/// </summary>
		protected IInternalParametersManager InternalParametersManager { get; private set; }

		private string GetMessageHeader(string parameterKey)
		{
			if (string.IsNullOrWhiteSpace(parameterKey))
				return string.Format("Component {0} : ", this.Code);
			return string.Format("Component {0}, parameter {1} : ", this.Code, parameterKey);
		}

		/// <summary>
		/// Sets the param.
		/// </summary>
		/// <param name="key">Name of the param.</param>
		/// <param name="value">The value.</param>
		protected void SetParameter(string key, IValue value)
		{
			this.InternalParametersManager.SetParameter(key, value);
		}

		/// <summary>
		/// Gets the param.
		/// </summary>
		/// <param name="key">Name of the param.</param>
		/// <returns></returns>
		protected IValue GetParameter(string key)
		{
			return this.InternalParametersManager.GetParameter(key).Value;
		}

		public void RegisterLinkForInputParameter(string inputParameterKey, string recieveOutputComponentKey, string recieveOutputParameterKey)
		{
			if (!components.IsInitialTime)
			{
				throw new Exception(string.Format("{0}The initial time is finish. You can''t register new link beetwen component.", this.GetMessageHeader(inputParameterKey)));
			}

			var outputComponent = this.components.FirstOrDefault(c => c.Code == recieveOutputComponentKey);
			if (outputComponent == null)
			{
				throw new AmbiguousMatchException(string.Format("{0}The output component code {1} is not find.", this.GetMessageHeader(inputParameterKey), recieveOutputComponentKey));
			}

			outputComponent.SetRecieveOutputParameterInfoForInputParameter(inputParameterKey, recieveOutputComponentKey, recieveOutputParameterKey);

			outputComponent.InternalParametersManager.EventOutputChange += (obj, args) =>
																		   {
																			   if (this.CheckParameterType(inputParameterKey, args.Parameter.Value))
																			   {
																				   throw new Exception(string.Format("{0}The output event parameter key {1} type is not the same als the local input parameter.", this.GetMessageHeader(inputParameterKey), recieveOutputParameterKey));
																			   }
																			   this.InternalParametersManager.SetParameter(inputParameterKey, args.Parameter.Value);
																			   if (this.InternalParametersManager.IsAllInputParamUptodate())
																			   {
																				   this.Calculate();
																				   this.InternalParametersManager.SendOutputsEvent();
																			   }
																		   };
		}

		/// <summary>
		/// Executes the calculation.
		/// </summary>
		public abstract void Calculate();

		/// <summary>
		/// Sets the recieve output info for input param.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="recieveOutputComponentKey">The recieve output component key.</param>
		/// <param name="recieveOutputKey">The recieve output key.</param>
		public void SetRecieveOutputParameterInfoForInputParameter(string key, string recieveOutputComponentKey, string recieveOutputKey)
		{
			if (this.InitialParameters.ContainsKey(key))
			{
				this.InitialParameters.SetRecieveInfoForInputParam(key, recieveOutputComponentKey, recieveOutputKey);
			}
			else
			{
				this.Logger.Warn(string.Format("The key {0} don't exist on the parmeter list of {1}", key, this.Code));
			}
		}

		/// <summary>
		/// Initializes the specified code.
		/// </summary>
		/// <param name="code">The code.</param>
		protected virtual void Initialize(string code)
		{
			this.Code = code;
			this.Count = 0;
		}

		/// <summary>
		/// Sets the current cycle.
		/// </summary>
		/// <param name="cycle">The cycle.</param>
		/// <param name="cycleTime">The cycle time.</param>
		/// <remarks>The new cycle meed to be bigger than the current.</remarks>
		public bool SetCurrentCycle(long cycle, DateTime cycleTime)
		{
			return this.InternalParametersManager.SetNewCycle(cycle, cycleTime);
		}

		/// <summary>
		/// States the change.
		/// </summary>
		protected virtual void StateChange()
		{
			this.Count++;
		}

		/// <summary>
		/// Sends all output event.
		/// </summary>
		private void SendAllOutputEvent()
		{
			this.InternalParametersManager.SendOutputsEvent();
		}

		/// <summary>
		/// Checks the type of the parameter.
		/// </summary>
		/// <param name="parameterKey">The parameter key.</param>
		/// <param name="value">The value.</param>
		/// <returns>True if the same type.</returns>
		private bool CheckParameterType(string parameterKey, IValue value)
		{
			if (value is DigitalValue)
			{
				if (this.InternalParametersManager.GetParameter(parameterKey).ParameterType != EnumParameterType.Digital)
				{
					this.Logger.Error(string.Format("Component {0}, parameter {1} : The event arg value was a DigitalValue ant the attached input parameter a AnalogValue.", this.Code, parameterKey));
					return false;
				}
			}
			if (this.InternalParametersManager.GetParameter(parameterKey).ParameterType != EnumParameterType.Analog)
			{
				this.Logger.Error(string.Format("Component {0}, parameter {1} : The event arg value was a AnalogValue ant the attached input parameter a DigitalValue.", this.Code, parameterKey));
				return false;
			}
			return true;
		}
	}
}