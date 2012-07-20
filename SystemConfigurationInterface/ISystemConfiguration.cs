namespace Sol2Reg.SystemConfiguration.Interfaces
{
	public interface ISystemConfiguration
	{
		/// <summary>
		/// Creates the per call configuration.
		/// </summary>
		/// <returns>
		/// A new per-call configuration.
		/// </returns>
		IResolver CreatePerCallConfiguration();
	}
}
