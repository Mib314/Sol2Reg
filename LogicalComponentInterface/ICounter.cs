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
    }
}