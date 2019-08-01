using System;
using MvvmCross.ViewModels;

namespace BookStoreWpf.Core
{
    public class App : MvxApplication
    {
        public App()
        {
            var tmp = 123;
        }

        public override void Initialize()
        {
            RegisterAppStart(new AppStart());
        }
    }
}
