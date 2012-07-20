namespace Sol2Reg.DataObject
{
	using System;

	public interface IDateTimeNowProvider
	{
		DateTime Now { get; }
	}
}