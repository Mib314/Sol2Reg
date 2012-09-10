namespace Sol2Reg.DataObject.Events
{
	/// <summary>
	/// Output change handler for <see cref="Sol2Reg.DataObject.Events.ValueEventArgs"/>.
	/// </summary>
	/// <param name="sender">The sender.</param>
	/// <param name="args">The <see cref="Sol2Reg.DataObject.Events.ValueEventArgs"/> instance containing the event data.</param>
	public delegate void OutputChangeNotificationHandler(object sender, ValueEventArgs args);
}