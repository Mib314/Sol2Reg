namespace Sol2Reg.BasicManager.Exception
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.Composition;
	using System.Globalization;
	using Cache;
	using Sol2Reg.BasicManager;

	[Export(typeof(Sol2RegException))]
	public class Sol2RegException : Exception
	{
		[Import(typeof (CacheManager))]
		private readonly CacheManager cacheManager;

		private readonly string[] parameters = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="Sol2RegException"/> class.
		/// </summary>
		public Sol2RegException(EnumSol2RegExceptionType sol2RegExceptionType)
		{
			this.Language = EnumLanguage.Frensh;
			this.Sol2RegExceptionType = sol2RegExceptionType;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Sol2RegException"/> class.
		/// </summary>
		/// <param name="sol2RegExceptionType">The sol2 reg exception.</param>
		/// <param name="messageParameters1">The message parameters1.</param>
		public Sol2RegException(EnumSol2RegExceptionType sol2RegExceptionType, object messageParameters1)
			: this(sol2RegExceptionType)
		{
			this.parameters = new string[] { messageParameters1.ToString() };
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Sol2RegException"/> class.
		/// </summary>
		/// <param name="sol2RegExceptionType">The sol2 reg exception.</param>
		/// <param name="messageParameters1">The message parameters1.</param>
		/// <param name="messageParameters2">The message parameters2.</param>
		public Sol2RegException(EnumSol2RegExceptionType sol2RegExceptionType, object messageParameters1, object messageParameters2)
			: this(sol2RegExceptionType)
		{
			this.parameters = new string[] { messageParameters1.ToString(), messageParameters2.ToString() };
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Sol2RegException"/> class.
		/// </summary>
		/// <param name="sol2RegExceptionType">The sol2 reg exception.</param>
		/// <param name="messageParameters1">The message parameters1.</param>
		/// <param name="messageParameters2">The message parameters2.</param>
		/// <param name="messageParameters3">The message parameters3.</param>
		public Sol2RegException(EnumSol2RegExceptionType sol2RegExceptionType, object messageParameters1, object messageParameters2, object messageParameters3)
			: this(sol2RegExceptionType)
		{
			this.parameters = new string[] { messageParameters1.ToString(), messageParameters2.ToString(), messageParameters3.ToString() };
		}


		/// <summary>
		/// Initializes a new instance of the <see cref="Sol2RegException"/> class.
		/// </summary>
		/// <param name="sol2RegExceptionType">The sol2 reg exception.</param>
		/// <param name="messageParameters1">The message parameters1.</param>
		/// <param name="messageParameters2">The message parameters2.</param>
		/// <param name="messageParameters3">The message parameters3.</param>
		/// <param name="messageParameters4">The message parameters4.</param>
		public Sol2RegException(EnumSol2RegExceptionType sol2RegExceptionType, object messageParameters1, object messageParameters2, object messageParameters3, object messageParameters4)
			: this(sol2RegExceptionType)
		{
			this.parameters = new string[] { messageParameters1.ToString(), messageParameters2.ToString(), messageParameters3.ToString(), messageParameters4.ToString() };

		}

		/// <summary>
		/// Obtient un message décrivant l'exception en cours.
		/// </summary>
		/// <returns>Message d'erreur qui explique la raison de l'exception ou bien chaîne vide ("").</returns>
		public override string Message
		{
			get { return this.GetMessage(this.Language); }
		}

		/// <summary>
		/// Gets the sol2 reg exception.
		/// </summary>
		public EnumSol2RegExceptionType Sol2RegExceptionType { get; private set; }

		/// <summary>
		/// Gets or sets the language.
		/// </summary>
		/// <value>
		/// The language.
		/// </value>
		public EnumLanguage Language { get; private set; }

		/// <summary>
		/// Gets the message with the selected language.
		/// </summary>
		/// <param name="language">The language.</param>
		/// <returns>Message with the selected language.</returns>
		public string GetMessage(EnumLanguage language)
		{
			var result = cacheManager.GetMessageLanguageEnum(this.Sol2RegExceptionType, language);

			var paramsQuantity = cacheManager.GetMessageParamsCount(this.Sol2RegExceptionType);

			if (paramsQuantity == null || this.parameters.Length != paramsQuantity)
			{ 
				throw new IndexOutOfRangeException("The ParamsQuantity value for this exception don't match with the parameters list.");
			}

			for (int iParams = 0; iParams < paramsQuantity; iParams++)
			{

				result = result.Replace("{" + iParams.ToString(CultureInfo.InvariantCulture) + "}", this.parameters[iParams]);
			}

			return result;
		}
	}
}