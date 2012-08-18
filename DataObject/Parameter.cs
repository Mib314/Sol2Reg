namespace Sol2Reg.DataObject
{
	/// <summary>
	/// Parameter.
	/// </summary>
	public class Parameter : IParameter
	{
		private IValue value;
		/// <summary>Gets or sets the value.</summary>
		/// <value>The value.</value>
		public IValue Value
		{
			get { return value; }
			set
			{
				this.value = value;
				this.IsUptoDate = true;
			}
		}

		/// <summary>Gets the parameter direction.</summary>
		/// <value>The direction of the parameter.</value>
		public EnumParameterDirection ParameterDirection { get; private set; }

		/// <summary>Gets or sets a value indicating whether this parmaeter is of type analog or digital.</summary>
		/// <value>The type of the parameter.</value>
		public EnumParameterType ParameterType { get; private set; }

		/// <summary>Gets or sets the code.</summary>
		/// <value>The code.</value>
		public string Code { get; set; }

		/// <summary>
		/// Gets the recieve code.
		/// </summary>
		public string RecieveCode { get; set; }

		/// <summary>Gets or sets the comment.</summary>
		/// <value>The comment.</value>
		public string Comment { get; set; }

		/// <summary>Gets a value indicating whether this instance is upto date.</summary>
		/// <value><c>true</c> if this instance is upto date; otherwise, <c>false</c>.</value>
		public bool IsUptoDate { get; private set; }

		/// <summary>Initializes the specified code.</summary>
		/// <param name="code">The code.</param>
		/// <param name="recieveCode">The recieve code.</param>
		/// <param name="type">The type.</param>
		/// <param name="direction">The direction.</param>
		/// <param name="comment">The comment.</param>
		/// <returns>This.</returns>
		public IParameter Initialize(string code, string recieveCode, EnumParameterType type = EnumParameterType.Analog, EnumParameterDirection direction = EnumParameterDirection.Input, string comment = "")
		{
			this.Code = code;
			this.RecieveCode = RecieveCode;
			this.Comment = comment;
			this.ParameterDirection = direction;
			this.ParameterType = type;
			this.IsUptoDate = false;
			return this;
		}
	}
}
