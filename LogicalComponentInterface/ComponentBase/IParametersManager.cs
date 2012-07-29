namespace Sol2Reg.LogicalComponent.Interface.ComponentBase
{
	using System;
	using Sol2Reg.DataObject;
	using Sol2Reg.DataObject.Events;

	public interface IParametersManager
	{
		/// <summary>
		/// Occurs when [event output change].
		/// </summary>
		event ValueChangeHandler EventOutputChange;

		/// <summary>
		/// Occurs when [event input change].
		/// </summary>
		event ValueChangeHandler EventInputChange;

		/// <summary>
		/// Registers the link input.
		/// </summary>
		/// <param name="parameterName">Name of the parameter.</param>
		/// <param name="parametersManagerEventSender">The value manager event sender.</param>
		void RegisterLinkInput(string parameterName, IParametersManager parametersManagerEventSender);

		/// <summary>
		/// Gets the parameter.
		/// </summary>
		/// <param name="code">The code.</param>
		/// <returns>Return parameter value (IValue).</returns>
		IParameter GetParameter(string code);

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		/// <returns>This instance.</returns>
		IParametersManager Initialize();

		/// <summary>
		/// Determines whether [is all input param uptodate].
		/// </summary>
		/// <returns><c>true</c> if [is all input param uptodate]; otherwise, <c>false</c>.</returns>
		bool IsAllInputParamUptodate();
	}
}