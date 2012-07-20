namespace Sol2Reg.LogicalComponent.Interface.AnalogComponants
{
	using DataObject;

	public interface IAnalogCompar : IAnalogBasicComponent
	{
		/// <summary>
		/// Initialize the specified name.
		/// </summary>
		/// <param name="code">The code.</param>
		/// <param name="gain">The gain.</param>
		/// <param name="offset">The offset.</param>
		/// <param name="deltaValueToSetOn">The delta value to set on.</param>
		/// <param name="deltaValueToSetOff">The delta value to set off.</param>
		void Initialize(string code, AnalogValue gain, AnalogValue offset, AnalogValue deltaValueToSetOn, AnalogValue deltaValueToSetOff);
	}
}