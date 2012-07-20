namespace Sol2Reg.SystemConfiguration.Interfaces
{
	/// <summary>
	/// A wrapper around a unity container to allow only the resolving of objects.
	/// </summary>
	public interface IResolver
	{
		/// <summary>
		/// Resolves the specified object with the given constructor parameters.
		/// </summary>
		/// <typeparam name="TObject">The type of the object.</typeparam>
		/// <param name="constructorParameters">The constructor parameters.</param>
		/// <returns>An instance of <typeparamref name="TObject"/>.</returns>
		TObject Resolve<TObject>(params object[] constructorParameters);
	}
}
