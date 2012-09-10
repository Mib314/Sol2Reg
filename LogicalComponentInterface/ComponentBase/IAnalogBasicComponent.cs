namespace Sol2Reg.LogicalComponent.Interface.ComponentBase
{
	using DataObject;

	public interface IAnalogBasicComponent : IBasicComponent
	{
		/// <summary>
		/// Initialize the AnalogAmplifier.
		/// </summary>
		/// <param name="code">The code.</param>
		/// <param name="gain">The gain.</param>
		/// <param name="offset">The offset.</param>
		void Initialize(string code, IValue gain, IValue offset);
		
		/// <summary>
		/// Valeur multiplicative d'une source analogique.
		/// </summary>
		/// <value>Par default = 1.</value>
		AnalogValue Gain { get; }

		/// <summary>
		/// Valeur additive d'une source analogique.
		/// </summary>
		/// <value>Par default = 0.</value>
		AnalogValue Offset { get; }
	}
}