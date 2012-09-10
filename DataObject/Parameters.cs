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
		public long Cycle { get; set; }

		/// <summary>
		/// Gets or sets the cycle time.
		/// </summary>
		/// <value>
		/// The cycle time.
		/// </value>
		public DateTime CycleTime { get; set; }

		/// <summary>Adds the specified parameter.</summary>
		/// <param name="parameter">The parameter.</param>
		/// <returns>Ture if the parma is new and added, otherwise returnn false.</returns>
		public bool Add(IParameter parameter)
		{
			if (!this.Params.ContainsKey(parameter.Key))
			{
				this.Params.Add(parameter.Key, parameter);
				return true;
			}

			return false;
		}

		public void SetParameter(string key, IValue value)
		{
			if (this.Params.ContainsKey(key))
			{
				this.Params[key].Value = value;
			}
		}

		/// <summary>
		/// Gets the parameter value.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns>Value.</returns>
		public IValue GetParameterValue(string key)
		{
			var param = this.GetParameter(key);
			return param == null ? null : param.Value;
		}

		/// <summary>
		/// Gets the parameter.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns></returns>
		public IParameter GetParameter(string key)
		{
			if (this.Params.ContainsKey(key))
			{
				return this.Params[key];
			}

			return null;
		}

		/// <summary>Gets the parameter key with the recieve key.</summary>
		/// <param name="recieveOutpuComponentKey">The recieve outpu component key.</param>
		/// <param name="recieveOutputKey">The recieve output key.</param>
		/// <returns>Return the parameter key.</returns>
		public string GetParameterKey(string recieveOutpuComponentKey, string recieveOutputKey)
		{
			return this.Params.Where(param => param.Value.RecieveOutputComponentKey == recieveOutpuComponentKey && param.Value.RecieveOutputKey == recieveOutputKey).Select(param => param.Value.Key).FirstOrDefault();
		}

		/// <summary>
		/// Determines whether [is all input param uptodate].
		/// </summary>
		public bool IsAllInputParamUptodate()
		{
			return Params.Any(param => !param.Value.IsUptoDate);
		}

		/// <summary>Sets the recieve info for input param.</summary>
		/// <param name="key">The key.</param>
		/// <param name="recieveComponentKey">The recieve component key.</param>
		/// <param name="recieveOutputKey">The recieve output key.</param>
		public void SetRecieveInfoForInputParam(string key, string recieveComponentKey, string recieveOutputKey)
		{
			var param = this.Params.FirstOrDefault(p => p.Key == key).Value;
			if (param != null)
			{
				param.RecieveOutputKey = recieveOutputKey;
				param.RecieveOutputComponentKey = recieveComponentKey;
			}
		}

		/// <summary>Sets the new cycle.</summary>
		/// <param name="cycle">The cycle.</param>
		/// <param name="cycleTime">The cycle time.</param>
		public void SetNewCycle(long cycle, DateTime cycleTime)
		{
			this.Cycle = cycle;
			this.CycleTime = cycleTime;
			foreach (var parameter in Params)
			{
				parameter.Value.SetNewCycle();
			}
		}
	}
}