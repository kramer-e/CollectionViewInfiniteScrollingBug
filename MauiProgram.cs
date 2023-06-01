#if IOS
using InfiniteScrollingMauiVersion.CustomControls;
using InfiniteScrollingMauiVersion.iOS.CustomRenderers;
#endif
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Compatibility.Hosting;

namespace InfiniteScrollingMauiVersion;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCompatibility()
            .ConfigureMauiHandlers(handlers =>
			{
#if IOS
            handlers.AddCompatibilityRenderer(typeof(FixedInfiniteCollectionView), typeof(CustomCollectionViewRenderer));
#endif
            })
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

