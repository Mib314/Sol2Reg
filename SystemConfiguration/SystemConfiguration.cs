namespace Sol2Reg.SystemConfiguration
{
	using Interfaces;
	using LogicalComponent.ComponentBase;
	using LogicalComponent.Interface;
	using LogicalComponent.Interface.ComponentBase;
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
			this.unityContainer.RegisterType<IParametersManager, ParametersManager>();
			this.unityContainer.RegisterType<IInternalParametersManager, ParametersManager>();
			this.unityContainer.RegisterType<IBasicComponent, BasicComponent>();
			this.unityContainer.RegisterType<IAnalogBasicComponent, AnalogBasicComponent>();
			this.unityContainer.RegisterType<IDigitalBasicComponent, DigitalBasicComponent>();
			this.unityContainer.RegisterType<IBasicComponentForParameterManager, BasicComponent>();

			// As Singelton
			this.unityContainer.RegisterInstance(new Components());
			
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
