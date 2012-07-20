namespace Sol2Reg.Test.Utilities
{
	using System.Dynamic;
	using System.Linq;
	using System.Reflection;

	/// <summary>
	/// A 10 minute wrapper to access private members, havn't tested in detail.
	/// Use under your own risk - amazedsaint@gmail.com.
	/// </summary>
	public class AccessPrivateWrapper : DynamicObject
	{
		/// <summary>
		/// The object we are going to wrap
		/// </summary>
		private readonly object wrapped;

		/// <summary>
		/// Specify the flags for accessing members
		/// </summary>
		private const BindingFlags FLAGS = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;

		/// <summary>
		/// Initializes a new instance of the <see cref="AccessPrivateWrapper"/> class.
		/// </summary>
		/// <param name="object">The object parameter.</param>
		public AccessPrivateWrapper(object @object)
		{
			this.wrapped = @object;
		}

		/// <summary>
		/// Create an instance via the constructor matching the args.
		/// </summary>
		/// <param name="asm">The asm param.</param>
		/// <param name="type">Type of the param.</param>
		/// <param name="args">The args collection.</param>
		/// <returns>Instance object.</returns>
		public static dynamic FromType(Assembly asm, string type, params object[] args)
		{
			var allt = asm.GetTypes();
			var t = allt.First(item => item.Name == type);

			var types = from a in args
						select a.GetType();

			// Gets the constructor matching the specified set of args
			var ctor = t.GetConstructor(FLAGS, null, types.ToArray(), null);

			if (ctor != null)
			{
				var instance = ctor.Invoke(args);
				return new AccessPrivateWrapper(instance);
			}

			return null;
		}

		/// <summary>
		/// Try invoking a method.
		/// </summary>
		/// <param name="binder">The binder.</param>
		/// <param name="args">The args param of the call.</param>
		/// <param name="result">The result.</param>
		/// <returns>Call is ok.</returns>
		public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
		{
			var types = from a in args
						select a.GetType();

			var method = this.wrapped.GetType().GetMethod(binder.Name, FLAGS, null, types.ToArray(), null);

			if (method == null)
			{
				return base.TryInvokeMember(binder, args, out result);
			}

			result = method.Invoke(this.wrapped, args);
			return true;
		}

		/// <summary>
		/// Tries to get a property or field with the given name.
		/// </summary>
		/// <param name="binder">The binder.</param>
		/// <param name="result">The result.</param>
		/// <returns>True is ok.</returns>
		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			// Try getting a property of that name
			var prop = this.wrapped.GetType().GetProperty(binder.Name, FLAGS);

			if (prop == null)
			{
				// Try getting a field of that name
				var fld = this.wrapped.GetType().GetField(binder.Name, FLAGS);
				if (fld != null)
				{
					result = fld.GetValue(this.wrapped);
					return true;
				}

				return base.TryGetMember(binder, out result);
			}

			result = prop.GetValue(this.wrapped, null);
			return true;
		}

		/// <summary>
		/// Tries to set a property or field with the given name.
		/// </summary>
		/// <param name="binder">The binder.</param>
		/// <param name="value">The value.</param>
		/// <returns>True is ok.</returns>
		public override bool TrySetMember(SetMemberBinder binder, object value)
		{
			var prop = this.wrapped.GetType().GetProperty(binder.Name, FLAGS);
			if (prop == null)
			{
				var fld = this.wrapped.GetType().GetField(binder.Name, FLAGS);
				if (fld != null)
				{
					fld.SetValue(this.wrapped, value);
					return true;
				}

				return base.TrySetMember(binder, value);
			}

			prop.SetValue(this.wrapped, value, null);
			return true;
		}
	}
}