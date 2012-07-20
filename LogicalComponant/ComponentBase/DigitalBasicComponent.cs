namespace Sol2Reg.LogicalComponent.ComponentBase
{
	using Interface;

	public abstract class DigitalBasicComponent : BasicComponent, IDigitalBasicComponent
	{
		protected DigitalBasicComponent(IValueManager valueManager)
			: base(valueManager) {}
	}
}
