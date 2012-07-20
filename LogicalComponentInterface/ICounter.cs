namespace Sol2Reg.LogicalComponent.Interface
{
    /// <summary>
    /// ICounter offre une interface pour les couposants offrant la capacité de compte le changement d'état.
    /// </summary>
    public interface ICounter
    {
        long Count { get; }
    }
}