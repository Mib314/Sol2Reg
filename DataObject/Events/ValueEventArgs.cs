namespace Sol2Reg.DataObject.Events
{
	using DataObject;

	/// <summary>
	/// Output event argument.
	/// </summary>
	public class ValueEventArgs
	{

		/// <summary>
		/// The value.
		/// </summary>
		public readonly IValue Value;

		/// <summary>
		/// Key of the component.
		/// </summary>
		public readonly string ComponentKey;

		/// <summary>
		/// Key of the output.
		/// </summary>
		public readonly string OutputKey;

		/// <summary>
		/// Initializes a new instance of the <see cref="Sol2Reg.DataObject.Events.ValueEventArgs"/> class.
		/// </summary>
		/// <param name="value">The analog value.</param>
		public ValueEventArgs(IValue value) 
			: this(value, string.Empty, string.Empty)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Sol2Reg.DataObject.Events.ValueEventArgs"/> class.
		/// </summary>
		/// <param name="value">The output value.</param>
		/// <param name="componentKey">The component key.</param>
		/// <param name="outputKey">The output key.</param>
		public ValueEventArgs(IValue value, string componentKey, string outputKey)
		{
			this.Value = value;
			this.ComponentKey = componentKey;
			this.OutputKey = outputKey;
		}
	}
}