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

		/// <summary>
		/// Gets the current cycle number.
		/// </summary>
		long Cycle { get; }

		/// <summary>
		/// Gets or sets the cycle time.
		/// </summary>
		/// <value>
		/// The cycle time.
		/// </value>
		DateTime CycleTime { get; }

		/// <summary>Adds the specified parameter.</summary>
		/// <param name="parameter">The new parameter.</param>
		/// <returns>Ture if the parameter is new, otherwise returnn false.</returns>
		bool Add(IParameter parameter);

		/// <summary>
		/// Gets the parameter value.
		/// </summary>
		/// <param name="code">The code.</param>
		/// <returns>Return the parameter value.</returns>
		IValue GetParameterValue(string code);

		/// <summary>
		/// Gets the parameter.
		/// </summary>
		/// <param name="code">The code.</param>
		/// <returns>Return the parameter.</returns>
		IParameter GetParameter(string code);

		/// <summary>Determines whether [is all input param uptodate].</summary>
		/// <returns>  <c>true</c> if [is all input param uptodate]; otherwise, <c>false</c>.</returns>
		bool IsAllInputParamUptodate();

		/// <summary>
		/// Sets the parameter.
		/// </summary>
		/// <param name="code">The code.</param>
		/// <param name="value">The value.</param>
		void SetParameter(string code, IValue value);
	}
}