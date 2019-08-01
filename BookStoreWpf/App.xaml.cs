using MvvmCross;
using MvvmCross.Core;
using MvvmCross.Platforms.Wpf.Core;
using MvvmCross.ViewModels;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace BookStoreWpf
{
    public class InternalApplication : MvvmCross.Platforms.Wpf.Views.MvxApplication
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

    public partial class App : Application//MvvmCross.Platforms.Wpf.Views.MvxApplication
    {
        private static Assembly ExecutingAssembly;
        private static string[] EmbeddedLibraries;
        private static InternalApplication _app;

        protected override void OnStartup(StartupEventArgs e)
        {
            ExecutingAssembly = Assembly.GetExecutingAssembly();
            EmbeddedLibraries = ExecutingAssembly.GetManifestResourceNames()
                                                 .Where(x => x.EndsWith(".dll"))
                                                 .ToArray();

            AppDomain.CurrentDomain.AssemblyResolve += LoadEmbeddedLibrary;
            _app = new InternalApplication();
            _app.Run(MainWindow);
            base.OnStartup(e);
        }

        //protected override void RegisterSetup()
        //{
        //    this.RegisterSetupType<MvxWpfSetup<Core.App>>();
        //}

        //protected override void RunAppStart(object hint = null)
        //{
        //    IMvxAppStart start = Mvx.IoCProvider.Resolve<IMvxAppStart>();
        //    start.StartAsync("here can go any object");
        //}

        private static Assembly LoadEmbeddedLibrary(object sender, ResolveEventArgs args)
        {
            // Get assembly name
            var assemblyName = new AssemblyName(args.Name).Name + ".dll";

            // Get resource name
            var resourceName = EmbeddedLibraries.FirstOrDefault(x => x.EndsWith(assemblyName));
            if (resourceName == null)
            {
                return null;
            }

            // Load assembly from resource
            using (var stream = ExecutingAssembly.GetManifestResourceStream(resourceName))
            {
                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                return Assembly.Load(bytes);
            }
        }
    }
}
