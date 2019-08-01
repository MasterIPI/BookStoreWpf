using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BookStoreWpf.Core.ViewModels;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace BookStoreWpf.Core
{
    public class AppStart : IMvxAppStart
    {
        public object AppState { get; set; }
        public bool IsStarted => true;

        public void ResetStart()
        {
        }

        public void Start(object hint = null)
        {
            AppState = hint;
            Mvx.IoCProvider.Resolve<IMvxNavigationService>().Navigate<HomeViewModel>();
        }

        public Task StartAsync(object hint = null)
        {
            Action appStart = () =>
            {
                AppState = hint;
                Mvx.IoCProvider.Resolve<IMvxNavigationService>().Navigate<HomeViewModel>();
            };

            return Task.Run(appStart);
        }
    }
}
