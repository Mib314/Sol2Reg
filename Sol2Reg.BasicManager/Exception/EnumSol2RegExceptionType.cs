namespace Sol2Reg.BasicManager.Exception
{
	/// <summary>
	/// Enum of all error on Sol2Reg
	/// </summary>
	public enum EnumSol2RegExceptionType
	{
		#region
		[MessageLanguage(
			"Erreur indefinie.",
			"Undefine error",
			null)]
		UndefineError = 0,

		#endregion

		#region Util
		[MessageLanguage(
			"La valuer {0} n'existe pas pour l'�num�ration {1}.",
			"The value {0} don't exist in enumeration {1}",
			null,
			2)]
		ElementNotInTheEnumeration = 9000,

		#endregion


		#region MaterialError
		[MessageLanguage(
			"Le canal num�ro {0} est invalide sur le mat�riel [{1}].\nType {2} -- {3}.",
			"This chanel id n�{0} is invalid by Material key [{1}].\nType {2} -- {3}.",
			null,
			4)]
		MaterialInvalidChanel = 10000,

		[MessageLanguage(
			"Num�ro de canal id {0} est hors de la port�e pour ce le mat�riel [{1}].\nType {2} -- {3}.",
			"Out of range chanel id {0} for material key [{1}].\nType {2} -- {3}.",
			null,
			4)]
		MaterialOutOfRangeChanel = 10001,

		#endregion

	}
}