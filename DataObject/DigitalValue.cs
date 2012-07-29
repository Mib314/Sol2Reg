namespace Sol2Reg.DataObject
{
	using System;

	public class DigitalValue : IValue
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DigitalValue"/> class.
		/// </summary>
		public DigitalValue()
			: this(false)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DigitalValue"/> class.
		/// </summary>
		/// <param name="value">if set to <c>true</c> [value].</param>
		public DigitalValue(bool? value)
		{
			this.Value = value;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DigitalValue"/> class.
		/// </summary>
		/// <param name="value">if set to <c>true</c> [value].</param>
		public DigitalValue(bool value)
		{
			this.Value = value;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="DigitalValue"/> is value.
		/// </summary>
		/// <value>
		///   <c>true</c> if value; otherwise, <c>false</c>.
		/// </value>
		public bool? Value { get; set; }

		public IValue Initialize()
		{
			this.Value = false;
			return this;
		}

		#region Extension :
		#region Equals
		public static bool operator ==(DigitalValue value1, DigitalValue value2)
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

		public static bool operator !=(DigitalValue value1, DigitalValue value2)
		{
			return !(value1 == value2);
		}
		#endregion

		public override bool Equals(object obj)
		{
			// If parameter is null return false.
			if (obj == null)
			{
				return false;
			}

			// If parameter cannot be cast to Point return false.
			var p = obj as DigitalValue;
			if ((System.Object)p == null)
			{
				return false;
			}

			// Return true if the fields match:
			return (Value == p.Value);
		}

		public bool Equals(DigitalValue other)
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

		#region Private helper methode
		private static bool? CheckEqualBase(DigitalValue value1, DigitalValue value2)
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