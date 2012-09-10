namespace Sol2Reg.LogicalComponent.AnalogComponents
{
	using DataObject;
	using Interface.ComponentBase;

	public interface IAmplifier: IAnalogBasicComponent
	{
		void InitializeInput1(IParameter parameter);
	}
}