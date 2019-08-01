using MvvmCross;
using MvvmCross.Core;
using MvvmCross.Platforms.Wpf.Core;
using MvvmCross.ViewModels;

namespace BookStoreWpf
{
  public partial class App : MvvmCross.Platforms.Wpf.Views.MvxApplication
  {
    protected override void RegisterSetup()
    {
      this.RegisterSetupType<MvxWpfSetup<Core.App>>();
    }

    protected override void RunAppStart(object hint = null)
    {
      IMvxAppStart start = Mvx.IoCProvider.Resolve<IMvxAppStart>();
      start.StartAsync("here can go any object");
    }
  }
}
