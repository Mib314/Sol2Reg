namespace Sol2Reg.DataObject
{
	using Enum;

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

		/// <summary>Gets or sets the key.</summary>
		/// <value>The key.</value>
		public string Key { get; set; }

		/// <summary>Gets or sets the recieve outpu component key.</summary>
		/// <value>The recieve outpu component key.</value>
		public string RecieveOutputComponentKey { get; set; }

		/// <summary> Gets or sets the recieve output key.</summary>
		/// <value>The recieve output key.</value>
		public string RecieveOutputKey { get; set; }

		/// <summary>Gets or sets the comment.</summary>
		/// <value>The comment.</value>
		public string Comment { get; set; }

		/// <summary>Gets a value indicating whether this instance is upto date.</summary>
		/// <value><c>true</c> if this instance is upto date; otherwise, <c>false</c>.</value>
		public bool IsUptoDate { get; private set; }

		/// <summary>Gets a value indicating whether this instance is dynamic.</summary>
		/// <value>	<c>true</c> if this instance is dynamic; otherwise, <c>false</c>.</value>
		public bool IsDynamic
		{
			get { return string.IsNullOrWhiteSpace(RecieveOutputComponentKey) && string.IsNullOrWhiteSpace(RecieveOutputKey); }
		}

		/// <summary>Gets a value indicating whether this instance is inverted.</summary>
		/// <value>	<c>true</c> if this instance is inverted; otherwise, <c>false</c>.</value>
		public bool IsInverted
		{
			get
			{
				if (this.ParameterType == EnumParameterType.Digital)
				{
					return ((DigitalValue)this.value).IsInverted;
				}
				return false;
			}
		}

		/// <summary>
		/// Initializes the specified key.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="initialValue">The initial value.</param>
		/// <param name="recieveOutputComponentKey">The recieve output component key.</param>
		/// <param name="recieveOutputKey">The recieve output key.</param>
		/// <param name="direction">The direction.</param>
		/// <param name="isInverted">if set to <c>true</c> [is inverted].</param>
		/// <param name="comment">The comment.</param>
		/// <returns>This.</returns>
		public IParameter Initialize(string key, IValue initialValue, string recieveOutputComponentKey, string recieveOutputKey, EnumParameterDirection direction = EnumParameterDirection.Input, bool isInverted = false, string comment = "")
		{
			this.Initialize(key, initialValue, direction, comment);
			this.RecieveOutputComponentKey = recieveOutputComponentKey;
			this.RecieveOutputKey = recieveOutputKey;
			return this;
		}

		/// <summary>
		/// Initializes the specified key.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="initialValue">The initial value.</param>
		/// <param name="direction">The direction (Input / Output).</param>
		/// <param name="comment">The comment.</param>
		/// <returns>This.</returns>
		public IParameter Initialize(string key, IValue initialValue, EnumParameterDirection direction = EnumParameterDirection.Input, string comment = "")
		{
			this.Key = key;
			this.Comment = comment;
			this.ParameterDirection = direction;
			this.ParameterType = initialValue.GetType() == typeof (DigitalValue) ? EnumParameterType.Digital : EnumParameterType.Analog;
			this.IsUptoDate = false;
			this.value = initialValue;
			return this;
		}

		/// <summary>
		/// Sets the recieve info for input param.
		/// </summary>
		/// <param name="recieveComponentKey">The recieve component key.</param>
		/// <param name="recieveOutputKey">The recieve output key.</param>
		public void SetRecieveInfoForInputParam(string recieveComponentKey, string recieveOutputKey)
		{
			this.RecieveOutputComponentKey = recieveComponentKey;
			this.RecieveOutputKey = RecieveOutputKey;
		}
	}
}
