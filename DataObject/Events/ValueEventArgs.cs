namespace Sol2Reg.DataObject.Events
{
	using DataObject;

	/// <summary>
	/// Input event argument.
	/// </summary>
	public class ValueEventArgs
	{

		/// <summary>
		/// The value.
		/// </summary>
		public readonly IValue Value;

		/// <summary>
		/// ValueCode of the component.
		/// </summary>
		public readonly string ComponentCode;

		/// <summary>
		/// ValueCode of the input.
		/// </summary>
		public readonly string InputCode;

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
		/// <param name="value">The analog value.</param>
		/// <param name="componentCode">The component code.</param>
		/// <param name="inputCode">The input code.</param>
		public ValueEventArgs(IValue value, string componentCode, string inputCode)
		{
			this.Value = value;
			this.ComponentCode = componentCode;
			this.InputCode = inputCode;
		}
	}
}