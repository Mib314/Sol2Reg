﻿namespace Sol2Reg.SystemConfiguration
{
	using Interfaces;
	using LogicalComponent.ComponentBase;
	using LogicalComponent.Interface;
	using Microsoft.Practices.Unity;

	public class SystemConfiguration : ISystemConfiguration
	{
	
			private readonly UnityContainer unityContainer;

		private static readonly object s_SyncRoot = new object();
		private static volatile ISystemConfiguration _instance;

				#region Singelton

		private SystemConfiguration()
		{
			this.unityContainer = new UnityContainer();
		}

		/// <summary>
		/// 	Gets Instance.
		/// </summary>
		public static ISystemConfiguration Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (s_SyncRoot)
					{
						if (_instance == null)
						{
							_instance = new SystemConfiguration();
							((SystemConfiguration)_instance).Initialize();
						}
					}
				}

				return _instance;
			}
		}

		#endregion
		private void Initialize()
		{
			this.unityContainer.RegisterType<IValueManager, ValueManager>();
			this.unityContainer.RegisterType<IInternalValueManager, ValueManager>();
			this.unityContainer.RegisterType<IBasicComponent, BasicComponent>();
			this.unityContainer.RegisterType<IAnalogBasicComponent, AnalogBasicComponent>();
			this.unityContainer.RegisterType<IDigitalBasicComponent, DigitalBasicComponent>();
			this.unityContainer.RegisterType<IBasicComponentForValueManager, BasicComponent>();

		}

		#region Implementation of ISystemConfiguration

		/// <summary>
		/// Creates the per call configuration.
		/// </summary>
		/// <returns>
		/// A new per-call configuration.
		/// </returns>
		public IResolver CreatePerCallConfiguration()
		{
			throw new System.NotImplementedException();
		}

		#endregion
	}
}