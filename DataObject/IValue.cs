namespace Sol2Reg.DataObject
{
	using System;

	/// <summary>Interface for Analog and digital value
	/// 
	/// </summary>
	public interface IValue {

		/// <summary>
		/// Gets or sets the cycle time.
		/// </summary>
		/// <value>
		/// The cycle time.
		/// </value>
		DateTime CycleTime { get; set; }

		/// <summary>
		/// Gets or sets the cycle.
		/// </summary>
		/// <value>
		/// The cycle.
		/// </value>
		long Cycle { get; set; }

		/// <summary>
		/// Gets a value indicating whether [dynamic value].
		/// </summary>
		/// <value>
		///   <c>true</c> if [dynamic value]; otherwise, <c>false</c>.
		/// </value>
		bool DynamicValue { get; }
	}
}