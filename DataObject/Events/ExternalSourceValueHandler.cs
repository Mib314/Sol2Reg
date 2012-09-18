namespace Sol2Reg.DataObject.Events
{
	/// <summary>
	/// External analog/digital handler.
	/// </summary>
	/// <param name="obj">The obj. source.</param>
	/// <param name="args">The args <see cref="ParameterEventArgs"/>.</param>
	public delegate void ExternalSourceValueHandler(object obj, ParameterEventArgs args);
}