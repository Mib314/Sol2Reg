namespace Sol2Reg.BasicManager.Cache
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.ComponentModel.Composition;
	using System.Globalization;
	using System.Linq;
	using System.Reflection;

	/// <summary>
	/// CacheManager for MessageLanguageAttribute.
	/// </summary>
	[Export]
	public partial class CacheManager
	{
		/// <summary>
		/// Save readed element on this hashtable to read quickly for MessageLanguageAttribut (enum)
		/// </summary>
		private readonly Hashtable messageLanguageEnumValues = new Hashtable();

		public CacheManager()
		{
			this.messageLanguageEnumValues = new Hashtable();
		}

		/// <summary>
		/// Gets the message language enum.
		/// </summary>
		/// <param name="value">The value of the enum.</param>
		/// <param name="language">The language.</param>
		/// <returns>
		/// Translate text.
		/// </returns>
		public string GetMessageLanguageEnum(Enum value, EnumLanguage language = EnumLanguage.English)
		{
			this.SetToCache(value);
			var messageLanguageAttribute = this.GetCacheArrtibut(value);
			if (messageLanguageAttribute != null)
			{
				return messageLanguageAttribute.GetMessage(language);
			}

			return string.Empty;
		}

		/// <summary>
		/// Gets the message language enum.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <param name="valueName">The value name of the enum.</param>
		/// <param name="language">The language.</param>
		/// <returns>
		/// Translate text.
		/// </returns>
		public string GetMessageLanguageEnum<TEnum>(string valueName, EnumLanguage language = EnumLanguage.English) where TEnum : struct, IConvertible
		{
			this.CheckIfGenericTypeIsEnum<TEnum>();
			var typeEnum = typeof(TEnum);

			var @enum = (Enum)Enum.Parse(typeEnum, valueName);

			return this.GetMessageLanguageEnum(@enum, language);
		}

		/// <summary>
		/// Gets the message params count.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>Quantity of parameters.</returns>
		public int? GetMessageParamsCount(Enum value)
		{
			this.SetToCache(value);

			var messageLanguageAttribute = this.GetCacheArrtibut(value);
			if (messageLanguageAttribute != null)
			{
				return messageLanguageAttribute.ParamCount;
			}

			return null;
		}

		/// <summary>
		/// Gets the message params count.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <param name="valueName">The value name of the enum.</param>
		/// <returns>Quantity of parameters.</returns>
		public int? GetMessageParamsCount<TEnum>(string valueName) where TEnum : struct, IConvertible
		{
			this.CheckIfGenericTypeIsEnum<TEnum>();
			Type typeEnum = typeof(TEnum);
			var @enum = (Enum)Enum.Parse(typeEnum, valueName);

			return this.GetMessageParamsCount(@enum);
		}

		/// <summary>
		/// Gets all message.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <param name="language">The language.</param>
		/// <returns>Return a list of all message for this enum type.</returns>
		public IList<string> GetAllMessageList<TEnum>(EnumLanguage language = EnumLanguage.English) where TEnum : struct, IConvertible
		{
			this.CheckIfGenericTypeIsEnum<TEnum>();
			var typeEnum = typeof(TEnum);
			this.SetToCache(typeEnum);

			return (from DictionaryEntry element in (Hashtable) this.messageLanguageEnumValues[typeEnum]
			        where element.Value != null
			        select element.Value).OfType<MessageLanguageAttribute>().Select(elemValue => elemValue.GetMessage(language)).ToList();
		}


		/// <summary>
		/// Gets all message.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <param name="language">The language.</param>
		/// <returns>Return a list of all message for this enum type.</returns>
		public IDictionary<TEnum, string> GetAllMessageDictionary<TEnum>(EnumLanguage language = EnumLanguage.English) where TEnum : struct, IConvertible
		{
			this.CheckIfGenericTypeIsEnum<TEnum>();
			var typeEnum = typeof(TEnum);
			this.SetToCache(typeEnum);

			var messagees = new Dictionary<TEnum, string>();

			foreach (DictionaryEntry element in (Hashtable)this.messageLanguageEnumValues[typeEnum])
			{
				var elemValue = element.Value as MessageLanguageAttribute;
				var elemKey = (TEnum)Enum.Parse(typeEnum, element.Key.ToString());
				messagees.Add(elemKey, elemValue != null ? elemValue.GetMessage(language) : null);
			}

			return messagees;
		}


		private void SetToCache(Enum value)
		{
			Type type = value.GetType();
			this.SetToCache(type);
		}

		private void SetToCache(Type enumType)
		{
			if (this.messageLanguageEnumValues.ContainsKey(enumType))
			{
				return;
			}

			var subDictionary = new Hashtable();
			foreach (FieldInfo fi in enumType.GetFields())
			{
				//Check for our custom attribute
				var attr = this.GetAttribut(fi);
				if (attr != null)
				{
					subDictionary.Add(fi.Name, attr);
				}
			}

			this.messageLanguageEnumValues.Add(enumType, subDictionary);
		}

		/// <summary>
		/// Gets the cache arrtibut.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>MessageLanguageAttribute.</returns>
		private MessageLanguageAttribute GetCacheArrtibut(Enum value)
		{
			var subDictionary = (Hashtable)this.messageLanguageEnumValues[value.GetType()];

			var valueString = value.ToString(CultureInfo.InvariantCulture);
			if (subDictionary.ContainsKey(valueString))
			{
				return (MessageLanguageAttribute)subDictionary[valueString];
			}
			return null;
		}

		/// <summary>
		/// Gets the attribut.
		/// </summary>
		/// <param name="fieldInfo">The field info.</param>
		/// <returns>Attribut language message.</returns>
		private MessageLanguageAttribute GetAttribut(FieldInfo fieldInfo)
		{
			var attrs = fieldInfo.GetCustomAttributes(typeof(MessageLanguageAttribute), false) as MessageLanguageAttribute[];
			if (attrs != null)
			{
				return attrs.FirstOrDefault();
			}

			return null;
		}

		/// <summary>
		/// Checks if generic type is enum. If not throw an ArgumentException.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		private void CheckIfGenericTypeIsEnum<TEnum>()
		{
			if (!typeof(TEnum).IsEnum)
			{
				throw new ArgumentException("T must be an enumerated type");
			}
		}

	}
}