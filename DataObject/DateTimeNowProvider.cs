namespace Sol2Reg.DataObject
{
	using System;

	/// <summary>
	/// Class to inject the value to DateTime.Now
	/// </summary>
	public class DateTimeNowProvider : IDateTimeNowProvider
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DateTimeNowProvider"/> class.
		/// </summary>
		/// <param name="now">The now.</param>
		public DateTimeNowProvider(DateTime now)
		{
			this.Now = now;
		}

		/// <summary>
		/// Gets the Date and time form now.
		/// </summary>
		public DateTime Now { get; private set; }
	}
}