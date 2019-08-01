using MvvmCross;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace TestAppWpf.Core.ViewModels
{
  public class HomeViewModel : MvxViewModel
  {
    public string TestString { get; set; }

    public HomeViewModel()
    {
      IMvxAppStart start = Mvx.IoCProvider.Resolve<IMvxAppStart>();
      TestString = (start as AppStart).AppState as string;
    }

    public override async Task Initialize()
    {
      await base.Initialize();
    }
  }
}
