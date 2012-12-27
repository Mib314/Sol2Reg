namespace Sol2Reg.BasicManager
{
	using System.ComponentModel.Composition;
	using Cache;

	[Export(typeof(LanguageManager))]
	public class LanguageManager
	{
		public EnumLanguage Language { get; set; }
	}
}