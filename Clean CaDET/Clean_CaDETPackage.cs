using Clean_CaDET.View.Options;
using Clean_CaDET.View.QualityAnalysisPanel;
using Clean_CaDET.View.ChallengePanel;
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
    [ProvideToolWindow(typeof(ChallengeWindow))]
    [ProvideToolWindow(typeof(CodeAnalysisWindow))]
    [ProvideOptionPage(typeof(OptionPageGrid),
        "Clean CaDET", "Clean CaDET", 0, 0, true)]
    public sealed class Clean_CaDETPackage : AsyncPackage
    {
        public const string PackageGuidString = "549a40ba-7800-435f-8e0f-b0c5ec49fbf3";

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await StageChallengeCommand.InitializeAsync(this);
            await CodeAnalysisCommand.InitializeAsync(this);
        }

        public string ServerUrl
        {
            get
            {
                var page = (OptionPageGrid)GetDialogPage(typeof(OptionPageGrid));
                return page.ServerUrl;
            }
        }
    }
}
