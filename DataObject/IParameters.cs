namespace Sol2Reg.DataObject
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// La class Parameters contient la liste des paramètres d'un composant.
	/// - Paramètres input
	/// - Partamètres output
	/// Les valeurs du cycle (numéro et temps).
	/// Des fontions pour lire, modifier et ajouter des paramètres.
	/// Une fonction pour verifier si tous les paramètres sont uptodate.
	/// </summary>
	public interface IParameters : IList<IParameter>
	{
		/// <summary>Gets the current cycle number.</summary>
		/// <value>The cycle.</value>
		long Cycle { get; }

		/// <summary>Gets or sets the cycle time.</summary>
		/// <value>The cycle time.</value>
		DateTime CycleTime { get; }

		/// <summary>
		/// Gets the parameter value.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns>Return the parameter value.</returns>
		IValue GetParameterValue(string key);

		/// <summary>
		/// Gets the parameter.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns>Return the parameter.</returns>
		IParameter GetParameter(string key);

		/// <summary>
		/// Gets the parameter key with the recieve key.
		/// </summary>
		/// <param name="recieveCode">The recieve key.</param>
		/// <param name="recieveOutputKey"> </param>
		/// <returns>Return the parameter key.</returns>
		string GetParameterKey(string recieveCode, string recieveOutputKey);

		/// <summary>Determines whether [is all input param uptodate].</summary>
		/// <returns>  <c>true</c> if [is all input param uptodate]; otherwise, <c>false</c>.</returns>
		bool IsAllInputParamUptodate();

		/// <summary>
		/// Sets the parameter.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		void SetParameter(string key, IValue value);

		/// <summary>
		/// Sets the recieve info for input param.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="recieveComponentKey">The recieve component key.</param>
		/// <param name="recieveOutputKey">The recieve output key.</param>
		void SetRecieveInfoForInputParam(string key, string recieveComponentKey, string recieveOutputKey);

		/// <summary>
		/// Determines whether the specified key contains key.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns>
		///   <c>true</c> if the specified key contains key; otherwise, <c>false</c>.
		/// </returns>
		bool ContainsKey(string key);
	}
}