using System;
using MvvmCross.ViewModels;

namespace TestAppWpf.Core
{
  public class App : MvxApplication
  {
    public override void Initialize()
    {
      RegisterAppStart(new AppStart());
    }
  }
}
