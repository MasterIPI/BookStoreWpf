using System;
using MvvmCross.ViewModels;

namespace BookStoreWpf.Core
{
  public class App : MvxApplication
  {
    public override void Initialize()
    {
      RegisterAppStart(new AppStart());
    }
  }
}
