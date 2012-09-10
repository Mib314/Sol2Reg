namespace Sol2Reg.DataObject
{
	using Enum;

	/// <summary>
	/// Un paramètre est une entrée pour sortie d'un composant.
	/// Il est constiruée
	/// </summary>
	public interface IParameter
	{
		/// <summary>Gets or sets the value and set IsUptodate = true.1.</summary>
		/// <value>The value.</value>
		IValue Value { get; set; }

		/// <summary>
		/// Gets the parameter direction.
		/// </summary>
		/// <value>The direction of the parameter.</value>
		EnumParameterDirection ParameterDirection { get; }

		/// <summary>Gets or sets a value indicating whether this parmaeter is of type analog or digital.</summary>
		/// <value>The type of the parameter.</value>
		EnumParameterType ParameterType { get; }

		/// <summary>Gets or sets the key.</summary>
		/// <value>The key.</value>
		string Key { get; }

		/// <summary>Gets or sets the comment.</summary>
		/// <value>The comment.</value>
		string Comment { get; }

		/// <summary>Gets a value indicating whether this instance is upto date.</summary>
		/// <value><c>true</c> if this instance is upto date; otherwise, <c>false</c>.</value>
		bool IsUptoDate { get; }


		/// <summary>Gets or sets the recieve outpu component key.</summary>
		/// <value>The recieve outpu component key.</value>
		string RecieveOutputComponentKey { get; set; }

		/// <summary> Gets or sets the recieve output key.</summary>
		/// <value>The recieve output key.</value>
		string RecieveOutputKey { get; set; }

		/// <summary>Gets a value indicating whether this instance is dynamic.</summary>
		/// <value>	<c>true</c> if this instance is dynamic; otherwise, <c>false</c>.</value>
		bool IsDynamic { get; }

		/// <summary>Gets a value indicating whether this instance is inverted.</summary>
		/// <value>	<c>true</c> if this instance is inverted; otherwise, <c>false</c>.</value>
		bool IsInverted { get; }

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
		IParameter Initialize(string key, IValue initialValue, string recieveOutputComponentKey, string recieveOutputKey, EnumParameterDirection direction = EnumParameterDirection.Input, bool isInverted = false, string comment = "");

		/// <summary>
		/// Initializes the specified key.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="initialValue">The initial value.</param>
		/// <param name="direction">The direction (Input / Output).</param>
		/// <param name="comment">The comment.</param>
		/// <returns>This.</returns>
		IParameter Initialize(string key, IValue initialValue, EnumParameterDirection direction = EnumParameterDirection.Input, string comment = "");


		/// <summary>Sets the recieve info for input param.</summary>
		/// <param name="recieveComponentKey">The recieve component key.</param>
		/// <param name="recieveOutputKey">The recieve output key.</param>
		void SetRecieveInfoForInputParam(string recieveComponentKey, string recieveOutputKey);
	}
}