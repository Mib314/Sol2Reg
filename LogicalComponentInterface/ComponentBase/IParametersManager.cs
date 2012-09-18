namespace Sol2Reg.LogicalComponent.Interface.ComponentBase
{
	using System.Collections.Generic;
	using DataObject;
	using DataObject.Events;

	public interface IParametersManager
	{
		/// <summary>
		/// Occurs when [event output change].
		/// </summary>
		/// <remarks>Change value propagation to another component.</remarks>
		event OutputChangeNotificationHandler EventOutputChange;

		/// <summary>
		/// Registers the link input.
		/// </summary>
		/// <param name="componentKey">The component key.</param>
		/// <param name="parameterKey">The parameter key.</param>
		/// <param name="parametersManagerEventSender">The value manager event sender.</param>
		void RegisterComponentLink(string componentKey, string parameterKey, IParametersManager parametersManagerEventSender);

		/// <summary>Initializes this instance.</summary>
		/// <param name="componentParmeters">The component parmeters list.</param>
		/// <returns>This instance.</returns>
		void Initialize(IParameters componentParmeters);

		/// <summary>Determines whether [is all input param uptodate].</summary>
		/// <returns><c>true</c> if [is all input param uptodate]; otherwise, <c>false</c>.</returns>
		bool IsAllInputParamUptodate();

		/// <summary>
		/// Gets the parameter.
		/// </summary>
		/// <param name="code">The code.</param>
		/// <returns>Return parameter value (IValue).</returns>
		IParameter GetParameter(string code);

		/// <summary>
		/// Setters the param (Analog and Digital).
		/// </summary>
		/// <param name="key">The key.</param>
		/// <param name="value">The value.</param>
		void SetParameter(string key, IValue value);

		/// <summary>
		/// Setters the param (Analog and Digital).
		/// </summary>
		/// <param name="args">The <see cref="ParameterEventArgs"/> instance containing the event data.</param>
		void SetParameter(ParameterEventArgs args);

		/// <summary>
		/// Gets the out parameters.
		/// </summary>
		/// <returns>List of output parameters.</returns>
		IEnumerable<IParameter> GetOutputParameters();
		
		/// <summary>
		/// Gets the input dynamic parameter.
		/// </summary>
		/// <returns></returns>
		IList<IParameter> GetInputDynamicParameter();
	}
}