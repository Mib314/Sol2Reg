namespace Sol2Reg.LogicalComponent.ComponentBase
{
	using System;
	using DataObject;
	using Interface;
	using Interface.ComponentBase;

	/// <summary>
	/// Basic component for analog and digital component.
	/// </summary>
	public abstract class BasicComponent : ICounter, IBasicComponent, IBasicComponentForParameterManager
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BasicComponent"/> class.
		/// </summary>
		protected BasicComponent(IParametersManager parametersManager)
		{
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
		public void SetParameter(string key, IValue value)
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

		private void SendAllOutputEvent()
		{
			this.InternalParametersManager.SendAllOutputEvent();

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
				return this.InternalParametersManager.SetCurrentCycle(cycle, cycleTime);
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