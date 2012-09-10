namespace Sol2Reg.LogicalComponent.ComponentBase
{
	using System;
	using DataObject;
	using Interface;
	using Interface.ComponentBase;
	using log4net;

	/// <summary>
	/// Basic component for analog and digital component.
	/// </summary>
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

		/// <summary>
		/// Executes the calculation.
		/// </summary>
		public virtual void Calculate()
		{
			SendAllOutputEvent();
		}

		/// <summary>
		/// Sends all output event.
		/// </summary>
		private void SendAllOutputEvent()
		{
			this.InternalParametersManager.SendAllOutputEvent();
		}

		/// <summary>
		/// Sets the recieve output info for input param.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="recieveOutputComponentKey">The recieve output component key.</param>
		/// <param name="recieveOutputKey">The recieve output key.</param>
		public void SetRecieveOutputInfoForInputParam(string key, string recieveOutputComponentKey, string recieveOutputKey)
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
	}
}