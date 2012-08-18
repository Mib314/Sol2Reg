namespace Sol2Reg.DataObject
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// La class Parameters contient la liste des paramètres d'un composant.
	/// - Paramètres input
	/// - Partamètres output
	/// Les valeurs du cycle (numéro et temps).
	/// Des fontions pour lire, modifier et ajouter des paramètres.
	/// Une fonction pour verifier si tous les paramètres sont uptodate.
	/// </summary>
	public class Parameters : IParameters
	{
		/// <summary>Gets or sets the params.</summary>
		/// <value>The params.</value>
		public Dictionary<string, IParameter> Params { get; set; }

		/// <summary>
		/// Gets the current cycle number.
		/// </summary>
		public long Cycle { get; private set; }

		/// <summary>
		/// Gets or sets the cycle time.
		/// </summary>
		/// <value>
		/// The cycle time.
		/// </value>
		public DateTime CycleTime { get; private set; }

		/// <summary>Adds the specified parameter.</summary>
		/// <param name="parameter">The parameter.</param>
		/// <returns>Ture if the parma is new and added, otherwise returnn false.</returns>
		public bool Add(IParameter parameter)
		{
			if (!this.Params.ContainsKey(parameter.Code))
			{
				this.Params.Add(parameter.Code, parameter);
				return true;
			}

			return false;
		}

		public void SetParameter(string code, IValue value)
		{
			if (this.Params.ContainsKey(code))
			{
				this.Params[code].Value = value;
			}
		}

		/// <summary>
		/// Gets the parameter value.
		/// </summary>
		/// <param name="code">The code.</param>
		/// <returns>Value.</returns>
		public IValue GetParameterValue(string code)
		{
			var param = this.GetParameter(code);
			return param == null ? null : param.Value;
		}

		/// <summary>
		/// Gets the parameter.
		/// </summary>
		/// <param name="code">The code.</param>
		/// <returns></returns>
		public IParameter GetParameter(string code)
		{
			if (this.Params.ContainsKey(code))
			{
				return this.Params[code];
			}

			return null;
		}

		/// <summary>
		/// Gets the parameter code with the recieve code.
		/// </summary>
		/// <param name="recieveCode">The recieve code.</param>
		/// <returns>Return the parameter code.</returns>
		public string GetParameterCode(string recieveCode)
		{
			return this.Params.Where(param => param.Value.RecieveCode == recieveCode).Select(param => param.Value.Code).FirstOrDefault();
		}

		/// <summary>
		/// Determines whether [is all input param uptodate].
		/// </summary>
		public bool IsAllInputParamUptodate()
		{
			return Params.Any(param => !param.Value.IsUptoDate);
		}
	}
}