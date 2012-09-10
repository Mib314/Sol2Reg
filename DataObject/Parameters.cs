namespace Sol2Reg.DataObject
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Enum;

	/// <summary>
	/// La class Parameters contient la liste des paramètres d'un composant.
	/// - Paramètres input
	/// - Partamètres output
	/// Les valeurs du cycle (numéro et temps).
	/// Des fontions pour lire, modifier et ajouter des paramètres.
	/// Une fonction pour verifier si tous les paramètres sont uptodate.
	/// </summary>
	public class Parameters : List<IParameter>, IParameters
	{
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

		public Parameters(long cycle, DateTime cycleTime)
		{
			this.Cycle = cycle;
			this.CycleTime = cycleTime;
		}

		/// <summary>Adds the specified parameter.</summary>
		/// <param name="parameter">The parameter.</param>
		/// <returns>Ture if the parma is new and added, otherwise returnn false.</returns>
		public new void Add(IParameter parameter)
		{
			if (!this.ContainsKey(parameter.Key))
			{
				base.Add(parameter);
			}
		}

		public void SetParameter(string key, IValue value)
		{
			var param = this.FirstOrDefault(p => p.Key == key);
			if (param != null)
			{
				param.Value = value;
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
			if (this.ContainsKey(key))
			{
				return this.FirstOrDefault(p => p.Key == key);
			}

			return null;
		}

		/// <summary>Gets the parameter key with the recieve key.</summary>
		/// <param name="recieveOutpuComponentKey">The recieve outpu component key.</param>
		/// <param name="recieveOutputKey">The recieve output key.</param>
		/// <returns>Return the parameter key.</returns>
		public string GetParameterKey(string recieveOutpuComponentKey, string recieveOutputKey)
		{
			return this.Where(p => p.RecieveOutputComponentKey == recieveOutpuComponentKey && p.RecieveOutputKey == recieveOutputKey).Select(p => p.Key).FirstOrDefault();
		}

		/// <summary>
		/// Determines whether [is all input param uptodate].
		/// Relevant is Dynamic parameters.
		/// </summary>
		public bool IsAllInputParamUptodate()
		{
			return this.Where(p => p.ParameterDirection == EnumParameterDirection.Input && p.IsDynamic).All(p => !p.IsUptoDate);
		}

		/// <summary>Sets the recieve info for input param.</summary>
		/// <param name="key">The key.</param>
		/// <param name="recieveComponentKey">The recieve component key.</param>
		/// <param name="recieveOutputKey">The recieve output key.</param>
		public void SetRecieveInfoForInputParam(string key, string recieveComponentKey, string recieveOutputKey)
		{
			var param = this.FirstOrDefault(p => p.Key == key);
			if (param != null)
			{
				param.RecieveOutputKey = recieveOutputKey;
				param.RecieveOutputComponentKey = recieveComponentKey;
			}
		}

		/// <summary>
		/// Determines whether the specified key contains key.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns>
		///   <c>true</c> if the specified key contains key; otherwise, <c>false</c>.
		/// </returns>
		public bool ContainsKey(string key)
		{
			return this.Any(p => p.Key == key);
		}
	}
}