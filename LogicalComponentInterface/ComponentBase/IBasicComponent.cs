namespace Sol2Reg.LogicalComponent.Interface
{
	using System;

	public interface IBasicComponent
	{

		/// <summary>
		/// Gets the component code.
		/// </summary>
		string Code { get; set; }

		/// <summary>
		/// Gets the input value manager.
		/// </summary>
		IValueManager ValueManager { get; }

		/// <summary>
		/// Executes the calculation.
		/// </summary>
		void Calculate();
	}
}