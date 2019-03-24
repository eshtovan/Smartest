using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms; 
using UserControl = System.Windows.Controls.UserControl;

namespace Smartest.Views
{
    /// <summary>
    /// Interaction logic for UnityWindow.xaml
    /// </summary>
    public partial class UnityWindow : UserControl
    {
        [DllImport("user32.dll", EntryPoint = "GetWindowThreadProcessId", SetLastError = true,
            CharSet = CharSet.Unicode, ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)]
        private static extern long GetWindowThreadProcessId(long hWnd, long lpdwProcessId);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongA", SetLastError = true)]
        private static extern long GetWindowLong(IntPtr hwnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongA", SetLastError = true)]
        public static extern int SetWindowLongA([System.Runtime.InteropServices.InAttribute()]
            System.IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetWindowPos(IntPtr hwnd, long hWndInsertAfter, long x, long y, long cx, long cy,
            long wFlags);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

        private const int SWP_NOOWNERZORDER = 0x200;
        private const int SWP_NOREDRAW = 0x8;
        private const int SWP_NOZORDER = 0x4;
        private const int SWP_SHOWWINDOW = 0x0040;
        private const int WS_EX_MDICHILD = 0x40;
        private const int SWP_FRAMECHANGED = 0x20;
        private const int SWP_NOACTIVATE = 0x10;
        private const int SWP_ASYNCWINDOWPOS = 0x4000;
        private const int SWP_NOMOVE = 0x2;
        private const int SWP_NOSIZE = 0x1;
        private const int GWL_STYLE = (-16);
        private const int WS_VISIBLE = 0x10000000;
        private const int WS_CHILD = 0x40000000;


        //[System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        //public struct HWND__
        //{

        //    /// int
        //    public int unused;
        //}



        System.Diagnostics.Process _cmdProcess;

        readonly System.Windows.Forms.Panel _panel = new System.Windows.Forms.Panel();


        ~UnityWindow()
        {
            this.Dispose();
        }


        public UnityWindow()
        {
            InitializeComponent();
            this.Loaded += OnVisibleChanged;
        }


        public void Dispose()
        {
            if (_cmdProcess != null)
            {
                _cmdProcess.Close();
                _cmdProcess.Dispose();
            }
            // this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Create control when visibility changes
        /// </summary>
        /// <param name="e">Not used</param>
        protected void OnVisibleChanged(object s, RoutedEventArgs e)
        {


            /*
           //                  https://carldesouza.com/console-app-wpf-winapi-uii/
            //https://stackoverflow.com/questions/6837330/make-process-window-visible-invisble-in-net

            */

            var processStartInfo = new System.Diagnostics.ProcessStartInfo(@"C:\OLD_PC\ConvoyUnity-master\Builds\Convoy.exe")
                {
                   // WindowStyle = ProcessWindowStyle.Hidden
                };
            

            _cmdProcess = Process.Start(processStartInfo);
             
            if (_cmdProcess != null)
            {
                _cmdProcess.WaitForInputIdle();

                Thread.Sleep(500);
                //_panel.AutoSize = true;
                _panel.Dock = DockStyle.Fill;
                // Use Win32API to set the command process to the panel
                SetParent(_cmdProcess.MainWindowHandle, _panel.Handle);
                WindowsFormsHost.Child = _panel;

                ShowWindow(_cmdProcess.MainWindowHandle, 5);
                //            // Move the window to overlay it on this window
                //            MoveWindow(_appWin, 0, 0, (int)this.ActualWidth, (int)this.ActualHeight, true);

            }
        }
    }
}
