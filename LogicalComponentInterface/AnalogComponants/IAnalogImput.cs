namespace Sol2Reg.LogicalComponent.Interface.AnalogComponants
{
	using DataObject;

	public interface IAnalogImput
	{
		decimal ExternalAnalogImput { get; set; }
		AnalogValue OutputValue { get; }
	}
}