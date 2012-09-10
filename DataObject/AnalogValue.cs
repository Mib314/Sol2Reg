namespace Sol2Reg.DataObject
{
	using System.Globalization;
	using System;

	public class AnalogValue : IValue
	{
		/// <summary>
		/// Initialize a new instance of the <see cref="AnalogValue"/> class.
		/// </summary>
		public AnalogValue()
			: this(0M)
		{
		}

		/// <summary>
		/// Initialize a new instance of the <see cref="AnalogValue"/> class.
		/// </summary>
		/// <param name="value">The value.</param>
		public AnalogValue(decimal value)
		{
			this.Value = value;
		}

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		public decimal Value { get; set; }

		/// <summary>
		/// Adjust the <paramref name="value"/> with this formul : (<paramref name="value"/> * <paramref name="gain"/> ) + <paramref name="offset"/>
		/// </summary>
		/// <param name="value">Input analog Value.</param>
		/// <param name="gain">Gain. Multiply the <paramref name="value"/>.</param>
		/// <param name="offset">Offset is added of the result (<paramref name="value"/> * <paramref name="gain"/>).</param>
		/// <returns></returns>
		public static AnalogValue AdjustValue(IValue value, AnalogValue gain, AnalogValue offset)
		{
			var valueAnalog = value as AnalogValue;
			return AdjustValue(valueAnalog, gain, offset);
		}

		/// <summary>
		/// Adjust the <paramref name="value"/> with this formul : (<paramref name="value"/> * <paramref name="gain"/> ) + <paramref name="offset"/>
		/// </summary>
		/// <param name="value">Input analog Value.</param>
		/// <param name="gain">Gain. Multiply the <paramref name="value"/>.</param>
		/// <param name="offset">Offset is added of the result (<paramref name="value"/> * <paramref name="gain"/>).</param>
		/// <returns></returns>
		public static AnalogValue AdjustValue(AnalogValue value, AnalogValue gain, AnalogValue offset)
		{
			return (value * gain) + offset;
		}

		/// <summary>
		/// Deltas => value1 - value2.
		/// </summary>
		/// <param name="value1">The value1.</param>
		/// <param name="value2">The value2.</param>
		/// <returns></returns>
		public AnalogValue DeltaV1V2(AnalogValue value1, AnalogValue value2)
		{
			return value1 - value2;
		}
		#region Extension :
		#region Define operateur for Value.
		public static AnalogValue operator +(AnalogValue value1, AnalogValue value2)
		{
			return new AnalogValue { Value = value1.Value + value2.Value };
		}

		public static AnalogValue operator +(decimal value1, AnalogValue value2)
		{
			return new AnalogValue { Value = value1 + value2.Value };
		}

		public static AnalogValue operator +(AnalogValue value1, decimal value2)
		{
			return new AnalogValue { Value = value1.Value + value2 };
		}

		public static AnalogValue operator -(AnalogValue value1, AnalogValue value2)
		{
			return new AnalogValue { Value = value1.Value - value2.Value };
		}

		public static AnalogValue operator -(decimal value1, AnalogValue value2)
		{
			return new AnalogValue { Value = value1 - value2.Value };
		}

		public static AnalogValue operator -(AnalogValue value1, decimal value2)
		{
			return new AnalogValue { Value = value1.Value - value2 };
		}

		public static AnalogValue operator *(AnalogValue value1, AnalogValue value2)
		{
			return new AnalogValue { Value = value1.Value * value2.Value };
		}

		public static AnalogValue operator *(decimal value1, AnalogValue value2)
		{
			return new AnalogValue { Value = value1 * value2.Value };
		}

		public static AnalogValue operator *(AnalogValue value1, decimal value2)
		{
			return new AnalogValue { Value = value1.Value * value2 };
		}

		public static AnalogValue operator /(AnalogValue value1, AnalogValue value2)
		{
			if (value2.Value == 0)
			{
				throw new DivideByZeroException("Value2 is equal to 0.");
			}
			return new AnalogValue { Value = value1.Value / value2.Value };
		}

		public static AnalogValue operator /(decimal value1, AnalogValue value2)
		{
			if (value2.Value == 0)
			{
				throw new DivideByZeroException("Value2 is equal to 0.");
			}
			return new AnalogValue { Value = value1 / value2.Value };
		}

		public static AnalogValue operator /(AnalogValue value1, decimal value2)
		{
			if (value2 == 0)
			{
				throw new DivideByZeroException("Value2 is equal to 0.");
			}
			return new AnalogValue { Value = value1.Value / value2 };
		}

		public static bool operator ==(AnalogValue value1, AnalogValue value2)
		{
			bool? resp = CheckEqualBase(value1, value2);
			if (resp != null)
			{
				return resp.Value;
			}

			// Return true if the fields match:
			// ReSharper disable PossibleNullReferenceException
			return value1.Value == value2.Value;
			// ReSharper restore PossibleNullReferenceException
		}

		public static bool operator >=(AnalogValue value1, AnalogValue value2)
		{
			bool? resp = CheckEqualBase(value1, value2);
			if (resp != null)
			{
				return resp.Value;
			}

			// Return true if the fields match:
			return value1.Value >= value2.Value;
		}
		public static bool operator <=(AnalogValue value1, AnalogValue value2)
		{
			bool? resp = CheckEqualBase(value1, value2);
			if (resp != null)
			{
				return resp.Value;
			}

			// Return true if the fields match:
			return value1.Value <= value2.Value;
		}

		public static bool operator >(AnalogValue value1, AnalogValue value2)
		{
			bool? resp = CheckEqualBase(value1, value2);
			if (resp != null)
			{
				return !resp.Value;
			}

			// Return true if the fields match:
			return value1.Value > value2.Value;
		}
		public static bool operator <(AnalogValue value1, AnalogValue value2)
		{
			bool? resp = CheckEqualBase(value1, value2);
			if (resp != null)
			{
				return !resp.Value;
			}

			// Return true if the fields match:
			return value1.Value < value2.Value;
		}

		public static bool operator !=(AnalogValue value1, AnalogValue value2)
		{
			return !(value1 == value2);
		}
		#endregion

		#region Equals
		public override bool Equals(object obj)
		{
			// If parameter is null return false.
			if (obj == null)
			{
				return false;
			}

			// If parameter cannot be cast to Point return false.
			var p = obj as AnalogValue;
			if ((System.Object)p == null)
			{
				return false;
			}

			// Return true if the fields match:
			return (Value == p.Value);
		}

		public bool Equals(AnalogValue other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return other.Value == Value;
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}
		#endregion
		#endregion

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return this.Value.ToString(CultureInfo.InvariantCulture);
		}

		public IValue Initialize()
		{
			this.Value = 0M;
			return this;
		}
		#region Private helper methode
		private static bool? CheckEqualBase(AnalogValue value1, AnalogValue value2)
		{
			// If both are null, or both are same instance, return true.
			if (System.Object.ReferenceEquals(value1, value2))
			{
				return true;
			}

			// If one is null, but not both, return false.
			if (((object)value1 == null) || ((object)value2 == null))
			{
				return false;
			}
			return null;
		}
		#endregion
	}
}