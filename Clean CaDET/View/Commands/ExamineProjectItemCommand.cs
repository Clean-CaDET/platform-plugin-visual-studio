using System;
using System.ComponentModel.Design;
using System.IO;
using System.Runtime.InteropServices;
using Clean_CaDET.Model;
using Clean_CaDET.Model.PlatformConnection.DTOs;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Task = System.Threading.Tasks.Task;

namespace Clean_CaDET.View.Commands
{
    internal sealed class ExamineProjectItemCommand
    {
        public const int CommandId = 256;
        public static readonly Guid CommandSet = new Guid("42057fd0-5cab-412d-a4f6-4330809ce9ee");
        private readonly AsyncPackage _package;

        private string _selectedFilePath;
        private readonly PlatformService _service = new PlatformService();
        private ExamineProjectItemCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            _package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new OleMenuCommand(this.Execute, menuCommandID);
            menuItem.BeforeQueryStatus += ShowMenuCommandIfCSFileSelected;

            commandService.AddCommand(menuItem);
        }

        private void ShowMenuCommandIfCSFileSelected(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            // get the menu that fired the event
            if (sender is OleMenuCommand menuCommand)
            {
                // start by assuming that the menu will not be shown
                menuCommand.Visible = false;
                menuCommand.Enabled = false;

                IVsHierarchy hierarchy;
                uint itemid;

                if (!IsSingleProjectItemSelection(out hierarchy, out itemid)) return;
                // Get the file path
                ((IVsProject)hierarchy).GetMkDocument(itemid, out _selectedFilePath);
                var transformFileInfo = new FileInfo(_selectedFilePath);

                bool isCSFile = transformFileInfo.Name.EndsWith(".cs");

                // if not leave the menu hidden
                if (!isCSFile) return;

                menuCommand.Visible = true;
                menuCommand.Enabled = true;
            }
        }

        private bool IsSingleProjectItemSelection(out IVsHierarchy hierarchy, out uint itemid)
        {
            hierarchy = null;
            itemid = VSConstants.VSITEMID_NIL;
            int hr;

            var monitorSelection = Package.GetGlobalService(typeof(SVsShellMonitorSelection)) as IVsMonitorSelection;
            var solution = Package.GetGlobalService(typeof(SVsSolution)) as IVsSolution;
            if (monitorSelection == null || solution == null) return false;

            IVsMultiItemSelect multiItemSelect;
            IntPtr hierarchyPtr = IntPtr.Zero;
            IntPtr selectionContainerPtr = IntPtr.Zero;

            try
            {
                hr = monitorSelection.GetCurrentSelection(out hierarchyPtr, out itemid, out multiItemSelect, out selectionContainerPtr);

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

        public static ExamineProjectItemCommand Instance
        {
            get;
            private set;
        }
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider => this._package;

        public static async Task InitializeAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new ExamineProjectItemCommand(package, commandService);
        }
        private async void Execute(object sender, EventArgs e)
        {
          /*  Uncomment this for loop if you want to test sending more than one request*/
/*            for (int i = 0; i < 100; i++)
            {*/
                ClassQualityAnalysisResponse codeQualityAnalysis = await _service.AnalyzeClassQualityAsync(_selectedFilePath);

                ToolWindowPane window = _package.FindToolWindow(typeof(TutoringWindow), 0, true);
                if (window?.Frame == null)
                {
                    throw new NotSupportedException("Cannot create tool window");
                }

                var tutoringWindow = window as TutoringWindow;
                tutoringWindow?.UpdateVMContent(codeQualityAnalysis.NewContent);

                IVsWindowFrame windowFrame = (IVsWindowFrame)window.Frame;
                ErrorHandler.ThrowOnFailure(windowFrame.Show());
/*            }*/
        }
    }
}
