namespace Sol2Reg.LogicalComponent.Interface.AnalogComponants
{
	using ComponentBase;
	using DataObject;

	public interface IAnalogAmplifier : IAnalogBasicComponent
	{
		AnalogValue InputValue1 { get; }

		/// <summary>
		/// Gets the output value.
		/// </summary>
		AnalogValue OutputValue { get; }

		/// <summary>
		/// Registers the link input1.
		/// </summary>
		/// <param name="outputComponent">The output component.</param>
		void RegisterLinkInput1(IBasicComponent outputComponent);
	}
}