namespace Sol2Reg.DataObject.Events
{
	using DataObject;

	/// <summary>
	/// Output event argument.
	/// </summary>
	public class ParameterEventArgs
	{

		/// <summary>
		/// The parameter.
		/// </summary>
		public readonly IParameter Parameter;

		/// <summary>
		/// Key of the component.
		/// </summary>
		public readonly string ComponentKey;


		/// <summary>
		/// Initializes a new instance of the <see cref="ParameterEventArgs"/> class.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		public ParameterEventArgs(IParameter parameter) 
			: this(string.Empty, parameter)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ParameterEventArgs"/> class.
		/// </summary>
		/// <param name="componentKey">The component key.</param>
		/// <param name="parameter">The parameter.</param>
		public ParameterEventArgs(string componentKey, IParameter parameter)
		{
			this.Parameter = parameter;
			this.ComponentKey = componentKey;
		}
	}
}