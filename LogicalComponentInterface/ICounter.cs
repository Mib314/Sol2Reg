namespace Sol2Reg.LogicalComponent.Interface
{
	using DataObject;

	/// <summary>
	/// ICounter offre une interface pour les couposants offrant la capacité de compte le changement d'état.
	/// </summary>
	public interface ICounter
	{
		long Count { get; }

		/// <summary>Gets or sets the parameters.</summary>
		/// <value>The parameters.</value>
		IParameters InitialParameters { get; set; }

		/// <summary>
		/// Sets the recieve output info for input param.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="recieveOutputComponentKey">The recieve output component key.</param>
		/// <param name="recieveOutputKey">The recieve output key.</param>
		void SetRecieveOutputParameterInfoForInputParameter(string key, string recieveOutputComponentKey, string recieveOutputKey);
	}
}