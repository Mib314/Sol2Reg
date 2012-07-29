namespace Sol2Reg.LogicalComponent.Interface
{
	using ComponentBase;
	using DataObject;

	public interface IAnalogBasicComponent : IBasicComponent
	{
		/// <summary>
		/// Initialize the AnalogAmplifier.
		/// </summary>
		/// <param name="code">The code.</param>
		/// <param name="gain">The gain.</param>
		/// <param name="offset">The offset.</param>
		void Initialize(string code, AnalogValue gain, AnalogValue offset);

		/// <summary>
		/// Gets or sets the input 1.
		/// </summary>
		/// <value>
		/// The input1.
		/// </value>
		AnalogValue Input1 { get; }

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