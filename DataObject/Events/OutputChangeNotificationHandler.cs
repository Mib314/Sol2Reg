namespace Sol2Reg.DataObject.Events
{
	/// <summary>
	/// Output change handler for <see cref="ParameterEventArgs"/>.
	/// </summary>
	/// <param name="sender">The sender.</param>
	/// <param name="args">The <see cref="ParameterEventArgs"/> instance containing the event data.</param>
	public delegate void OutputChangeNotificationHandler(object sender, ParameterEventArgs args);
}