using Clean_CaDET.Model;
using Clean_CaDET.Model.PlatformConnection.DTOs.QualityAnalysis;
using Clean_CaDET.View.QualityAnalysisPanel;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Task = System.Threading.Tasks.Task;

namespace Clean_CaDET.View.Commands
{
    internal sealed class CodeAnalysisCommand
    {
        public const int CommandId = 257;
        public static readonly Guid CommandSet = new Guid("42057fd0-5cab-412d-a4f6-4330809ce9ee");
        private readonly AsyncPackage _package;

        private string _selectedFilePath;
        private readonly PlatformService _service;
        private CodeAnalysisCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            _package = package ?? throw new ArgumentNullException(nameof(package));
            var ccaDetPackage = _package as Clean_CaDETPackage;
            _service = new PlatformService(ccaDetPackage.ServerUrl);

            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new OleMenuCommand(Execute, menuCommandID);
            menuItem.BeforeQueryStatus += ShowMenuCommandWhenSuitable;
            commandService.AddCommand(menuItem);
        }

        //TODO: Refactor duplicate code (SubmitChallengeCommand)
        private void ShowMenuCommandWhenSuitable(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            // get the menu that fired the event
            if (!(sender is OleMenuCommand menuCommand)) return;

            // start by assuming that the menu will not be shown
            menuCommand.Visible = false;
            menuCommand.Enabled = false;

            IVsHierarchy hierarchy;
            uint itemid;

            if (!IsSingleProjectItemSelection(out hierarchy, out itemid)) return;
            // Get the file path
            ((IVsProject)hierarchy).GetMkDocument(itemid, out _selectedFilePath);
            var fileInfo = new FileInfo(_selectedFilePath);

            bool isCSFile = fileInfo.Name.EndsWith(".cs");
            if (isCSFile || IsFolderWithCsFiles(fileInfo.Directory))
            {
                menuCommand.Visible = true;
                menuCommand.Enabled = true;
            }
        }

        private static bool IsFolderWithCsFiles(DirectoryInfo directoryInfo)
        {
            if (directoryInfo == null) return false;

            var fileNames = directoryInfo.GetFiles().Select(f => f.Name);
            if (fileNames.Any(n => n.EndsWith(".cs"))) return true;

            return directoryInfo.GetDirectories().Any(IsFolderWithCsFiles);
        }

        private static bool IsSingleProjectItemSelection(out IVsHierarchy hierarchy, out uint itemid)
        {
            hierarchy = null;
            itemid = VSConstants.VSITEMID_NIL;

            var monitorSelection = Package.GetGlobalService(typeof(SVsShellMonitorSelection)) as IVsMonitorSelection;
            var solution = Package.GetGlobalService(typeof(SVsSolution)) as IVsSolution;
            if (monitorSelection == null || solution == null) return false;

            IntPtr hierarchyPtr = IntPtr.Zero;
            IntPtr selectionContainerPtr = IntPtr.Zero;

            try
            {
                IVsMultiItemSelect multiItemSelect;
                var hr = monitorSelection.GetCurrentSelection(out hierarchyPtr, out itemid, out multiItemSelect, out selectionContainerPtr);

                if (ErrorHandler.Failed(hr) || hierarchyPtr == IntPtr.Zero || itemid == VSConstants.VSITEMID_NIL)
                {
                    return false; // there is no selection
                }

                if (multiItemSelect != null) return false; // multiple items are selected

                // there is a hierarchy root node selected, thus it is not a single item inside a project
                if (itemid == VSConstants.VSITEMID_ROOT) return false;

                hierarchy = Marshal.GetObjectForIUnknown(hierarchyPtr) as IVsHierarchy;
                if (hierarchy == null) return false;

                Guid guidProjectID = Guid.Empty;

                if (ErrorHandler.Failed(solution.GetGuidOfProject(hierarchy, out guidProjectID)))
                {
                    return false; // hierarchy is not a project inside the Solution if it does not have a ProjectID Guid
                }
                // if we got this far then there is a single project item selected
                return true;
            }
            finally
            {
                if (selectionContainerPtr != IntPtr.Zero)
                {
                    Marshal.Release(selectionContainerPtr);
                }
                if (hierarchyPtr != IntPtr.Zero)
                {
                    Marshal.Release(hierarchyPtr);
                }
            }
        }

        public static CodeAnalysisCommand Instance
        {
            get;
            private set;
        }
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider => this._package;

        public static async Task InitializeAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync((typeof(IMenuCommandService))) as OleMenuCommandService;
            Instance = new CodeAnalysisCommand(package, commandService);
        }

        private async void Execute(object sender, EventArgs e)
        {
            CodeEvaluationDTO codeEvaluation = await _service.AnalyzeCodeAsync(_selectedFilePath);

            ToolWindowPane window = _package.FindToolWindow(typeof(CodeAnalysisWindow), 0, true);
            if (window?.Frame == null)
            {
                throw new NotSupportedException("Cannot create tool window");
            }

            var codeAnalysisWindow = window as CodeAnalysisWindow;
            codeAnalysisWindow?.UpdateVmContent(codeEvaluation);

            IVsWindowFrame windowFrame = (IVsWindowFrame)window.Frame;
            ErrorHandler.ThrowOnFailure(windowFrame.Show());
        }
    }
}
