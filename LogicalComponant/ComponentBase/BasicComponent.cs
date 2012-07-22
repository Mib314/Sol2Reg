namespace Sol2Reg.LogicalComponent.ComponentBase
{
	using System;
	using DataObject;
	using Interface;

	/// <summary>
	/// Basic component for analog and digital component.
	/// </summary>
	public abstract class BasicComponent : ICounter, IBasicComponent, IBasicComponentForValueManager
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BasicComponent"/> class.
		/// </summary>
		protected BasicComponent(IValueManager valueManager)
		{
			this.InternalValueManager = (IInternalValueManager)valueManager;
		}

		/// <summary>
		/// Sets the param.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="paramName">Name of the param.</param>
		protected void SetParam(IValue value, string paramName)
		{
			this.InternalValueManager.SetterParam(paramName, value);
		}

		/// <summary>
		/// Gets the param.
		/// </summary>
		/// <param name="paramName">Name of the param.</param>
		/// <returns></returns>
		protected IValue GetParam(string paramName)
		{
			return this.InternalValueManager.CurrentParams[paramName];
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
		public IValueManager ValueManager { get { return this.InternalValueManager; }}

		/// <summary>
		/// Gets the input value manager for internal ressource only.
		/// </summary>
		protected IInternalValueManager InternalValueManager { get; private set; }

		/// <summary>
		/// Executes the calculation.
		/// </summary>
		public abstract void Calculate();


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
		public bool SetCurrentCycle(long cycle, DateTime cycleTime)
		{
			if (this.InternalValueManager.Cycle < cycle && this.InternalValueManager.CycleTime < cycleTime)
			{
				this.InternalValueManager.Cycle = cycle;
				this.InternalValueManager.CycleTime = cycleTime;
				return true;
			}
			return false;
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