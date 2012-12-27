namespace Sol2Reg.BasicManager
{
	/// <summary>
	/// Message text of frensh
	/// </summary>
	public class MessageLanguageAttribute : System.Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MessageLanguageAttribute"/> class.
		/// </summary>
		/// <param name="messageFr">The message fr.</param>
		/// <param name="messageEn">The message en.</param>
		/// <param name="messageDe">The message de.</param>
		/// <param name="paramCount">The param count.</param>
		public MessageLanguageAttribute(string messageFr, string messageEn, string messageDe, int paramCount = 0)
		{
			ParamCount = paramCount;
			this.MessageFr = messageFr;
			this.MessageEn = messageEn;
			this.MessageDe = messageDe;
		}

		/// <summary>
		/// Gets the param count.
		/// </summary>
		public int ParamCount { get; private set; }

		/// <summary>
		/// Gets the message fr.
		/// </summary>
		public string MessageFr { get; private set; }
		/// <summary>
		/// Gets the message en.
		/// </summary>
		public string MessageEn { get; private set; }

		/// <summary>
		/// Gets the message de.
		/// </summary>
		public string MessageDe { get; private set; }

		/// <summary>
		/// Gets the message.
		/// </summary>
		/// <param name="language">The language.</param>
		/// <returns></returns>
		public string GetMessage(EnumLanguage language)
		{
			switch (language)
			{
				case EnumLanguage.Frensh:
					return this.MessageFr;
				case EnumLanguage.English:
					return this.MessageEn;
				case EnumLanguage.German:
					return this.MessageDe;
				default:
					return null;
			}
		}
	}
}