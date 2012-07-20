namespace Sol2Reg.DataObject.Events
{
	/// <summary>
	/// External analog/digital handler.
	/// </summary>
	/// <param name="obj">The obj. source.</param>
	/// <param name="args">The args <see cref="Sol2Reg.DataObject.Events.ValueEventArgs"/>.</param>
	public delegate void ExternalSourceValueHandler(object obj, ValueEventArgs args);
}