using System.Linq;
using System.Reflection;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Sample
{
    public partial class App : PrismApplication
	{
		public App(IPlatformInitializer initializer = null) : base(initializer) { }

		protected override void OnInitialized()
		{
			InitializeComponent();

			NavigationService.NavigateAsync("NavigationPage/MainPage");
		}

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
			containerRegistry.RegisterForNavigation<NavigationPage>();
			this.GetType().GetTypeInfo().Assembly.DefinedTypes
					  .Where(t => t.Namespace?.EndsWith(".Views", System.StringComparison.Ordinal) ?? false)
					  .ForEach(t => {
						  containerRegistry.RegisterForNavigation(t.AsType(), t.Name);
					  });
		}
	}
}

