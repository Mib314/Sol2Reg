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
	public interface IParameters
	{
		/// <summary>Gets or sets the params.</summary>
		/// <value>The params.</value>
		Dictionary<string, IParameter> Params { get; set; }

		/// <summary>Gets the current cycle number.</summary>
		/// <value>The cycle.</value>
		long Cycle { get; set; }

		/// <summary>Gets or sets the cycle time.</summary>
		/// <value>The cycle time.</value>
		DateTime CycleTime { get; set; }

		/// <summary>Adds the specified parameter.</summary>
		/// <param name="parameter">The new parameter.</param>
		/// <returns>Ture if the parameter is new, otherwise returnn false.</returns>
		bool Add(IParameter parameter);

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

		void SetRecieveInfoForInputParam(string key, string recieveComponentKey, string recieveOutputKey);
		void SetNewCycle(long cycle, DateTime cycleTime);
	}
}