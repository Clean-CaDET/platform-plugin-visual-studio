using Clean_CaDET.View.Commands;
using Clean_CaDET.View.TutoringPanel;
using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace Clean_CaDET
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid(PackageGuidString)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideToolWindow(typeof(TutoringWindow))]
    public sealed class Clean_CaDETPackage : AsyncPackage
    {
        public const string PackageGuidString = "549a40ba-7800-435f-8e0f-b0c5ec49fbf3";

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await TutoringWindowCommand.InitializeAsync(this);
            await SubmitChallengeCommand.InitializeAsync(this);
        }
    }
}
