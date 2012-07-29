namespace Sol2Reg.LogicalComponent.ComponentBase
{
	using Interface;
	using Interface.ComponentBase;

	public abstract class DigitalBasicComponent : BasicComponent, IDigitalBasicComponent
	{
		protected DigitalBasicComponent(IParametersManager parametersManager)
			: base(parametersManager) {}
	}
}
