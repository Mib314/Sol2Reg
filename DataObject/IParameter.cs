namespace Sol2Reg.DataObject
{
	/// <summary>
	/// Un paramètre est une entrée pour sortie d'un composant.
	/// Il est constiruée
	/// </summary>
	public interface IParameter
	{
		/// <summary>Gets or sets the value.</summary>
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

		/// <summary>Gets or sets the code.</summary>
		/// <value>The code.</value>
		string Code { get; }

		/// <summary>Gets or sets the comment.</summary>
		/// <value>The comment.</value>
		string Comment { get; }

		/// <summary>Gets a value indicating whether this instance is upto date.</summary>
		/// <value><c>true</c> if this instance is upto date; otherwise, <c>false</c>.</value>
		bool IsUptoDate { get; }

		/// <summary>Initializes the specified code.</summary>
		/// <param name="code">The code.</param>
		/// <param name="type">The type.</param>
		/// <param name="direction">The direction.</param>
		/// <param name="comment">The comment.</param>
		/// <returns>This.</returns>
		IParameter Initialize(string code, EnumParameterType type = EnumParameterType.Analog, EnumParameterDirection direction = EnumParameterDirection.Input, string comment = "");
	}
}