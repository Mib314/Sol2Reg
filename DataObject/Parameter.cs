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
		public bool IsInverted { get{ this.value.i}}

		/// <summary>Initializes the specified key.</summary>
		/// <param name="key">The key.</param>
		/// <param name="recieveOutputComponentKey">The recieve output component key.</param>
		/// <param name="recieveOutputKey">The recieve output key.</param>
		/// <param name="type">The type.</param>
		/// <param name="direction">The direction.</param>
		/// <param name="isInverted">if set to <c>true</c> [is inverted].</param>
		/// <param name="comment">The comment.</param>
		/// <returns>This.</returns>
		public IParameter Initialize(string key, string recieveOutputComponentKey, string recieveOutputKey, EnumParameterType type = EnumParameterType.Analog, EnumParameterDirection direction = EnumParameterDirection.Input, bool isInverted = false, string comment = "")
		{
			this.RecieveOutputComponentKey = recieveOutputComponentKey;
			this.RecieveOutputKey = recieveOutputKey;
			return this;
		}

		/// <summary>Initializes the specified key.</summary>
		/// <param name="key">The key.</param>
		/// <param name="type">The type (Analog / Digital).</param>
		/// <param name="direction">The direction (Input / Output).</param>
		/// <param name="comment">The comment.</param>
		/// <returns>This.</returns>
		public IParameter Initialize(string key, EnumParameterType type = EnumParameterType.Analog, EnumParameterDirection direction = EnumParameterDirection.Input, string comment = "")
		{
			this.Key = key;
			this.Comment = comment;
			this.ParameterDirection = direction;
			this.ParameterType = type;
			this.IsUptoDate = false;
			return this;
		}

		public void SetRecieveInfoForInputParam(string recieveComponentKey, string recieveOutputKey)
		{
			this.RecieveOutputComponentKey = recieveComponentKey;
			this.RecieveOutputKey = RecieveOutputKey;
		}

		/// <summary>
		/// Sets the new cycle.
		/// </summary>
		public void SetNewCycle()
		{
			throw new System.NotImplementedException();
		}
	}
}
