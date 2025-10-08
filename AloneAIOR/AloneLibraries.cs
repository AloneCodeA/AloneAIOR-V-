using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Lifetime;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static AloneAIOR.AloneProcess;
using static AloneAIOR.ImageSystem;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using static System.TimeZoneInfo;

namespace AloneAIOR
{

    /*
     
        InputSystem.Click(994, 750);
        InputSystem.PostKey("Enter");

        AloneProcess.Sleep(30);
        InputSystem.SendKey(InputSystem.JumpKey);

        InputSystem.SendKey(InputSystem.Keys.Key_A, "KeyDown");
        AloneProcess.Sleep(30);
        InputSystem.SendKey(InputSystem.Keys.Key_A, "KeyUp");

        //Gamestate 1 :415, 751 #256D00
        MessageBox.Show(ImageSystem.GetPixelColor(415, 751).ToString(), "Color Information", MessageBoxButtons.OK);

        //MessageBox PixelSearch 415, 751 #256D00
        MessageBox.Show(ImageSystem.PixelSearch(415, 751, "0094B4").ToString(), "PixelSearch Information", MessageBoxButtons.OK);

        pictureBox1 = Properties.Resources.F10
        pictureBox1.Image = Properties.Resources.F10;
        var image = ImageSystem.ImageSearch(845, 612, 1009, 692, "F10", 20);
        if (image.x != -1 && image.y != -1)
            MessageBox.Show("found", "Information", MessageBoxButtons.OK);

        if (ImageSystem.ImageSearch(845, 612, 1009, 692, "F10", 20).x != -1)

		//GameFunction.CheckGameState(GameFunction.GameState.WaitingRoom, 1, 0, GameFunction.Mode.Fast);

		//(GameState PossibleState, int matchCount) = await CheckAllGameStates(1, 0);
        //if (highestMatchState != StateFunction.GameState.Unknown)

    */

    public class AloneProcess
    {
        private static readonly object ProcessLock = new object();
        public static AloneAIO _formInstance;

        public static void SetFormInstance(AloneAIO form)
        {
            _formInstance = form;
        }

        public static void UpdateTalesState(string newState)
        {
            if (_formInstance != null)
            {
                _formInstance.TalesState.Text = newState;
            }
        }

        private static Random _randomInstance;
        public static Process talesRunnerProcess;
        public static IntPtr talesRunnerHandle;       
        public static int NotStarted = 1;
        public static int FinishMap = 1;

        public static event EventHandler<ErrorEventArgs> OnError;

        public static Random RandomInstance
        {
            get
            {
                return _randomInstance ?? (_randomInstance = new Random());
            }
        }

        public static bool EnsureProcessInitialized()
        {
            lock (ProcessLock)
            {
                RECT TRwindowRect;
                try
                {
                    talesRunnerProcess = GetProcessByName("TalesRunner", "trgame");
                    talesRunnerHandle = talesRunnerProcess?.MainWindowHandle ?? IntPtr.Zero;

                    if (talesRunnerHandle != IntPtr.Zero)
                    {
                        if (GetWindowRect(talesRunnerHandle, out TRwindowRect))
                        {
                            _formInstance.Invoke((Action)(() =>
                            {
                                AloneProcess.SetWindowPos(_formInstance.Handle, IntPtr.Zero, TRwindowRect.Left + 120, TRwindowRect.Top + 1, 0, 0, InputSystem.WindowMessages.SWP_NOSIZE | InputSystem.WindowMessages.SWP_TOPMOST);
                            }));
                        }
                    }
                }
                catch (Exception ex)
                {
                    return false; // Indicate that the process initialization failed
                }
            }
            return true; // Indicate that the process initialization was successful
        }

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        public static Process GetProcessByName(params string[] processNames)
        {
            try
            {
                var process = Process.GetProcesses()
                    .FirstOrDefault(p => processNames.Any(name =>
                        p.MainWindowTitle.Equals(name, StringComparison.OrdinalIgnoreCase) ||
                        p.ProcessName.Equals(name, StringComparison.OrdinalIgnoreCase)));

                if (process == null)
                {
                    return null;
                }

                return process;
            }
            catch (Win32Exception ex)
            {
                return null;
            }

            return null;
        }

        private static readonly object RandomLock = new object();

        public static Task Sleep(int milliseconds) => Task.Delay(milliseconds);

        public static async Task SleepAsync(int milliseconds)
        {
            int totalsleep = int.Parse(ReadInI.Delay.SlowPC) + milliseconds;
            await Task.Delay(totalsleep);
        }

        public static async Task ShowMessageBoxAsync(string message)
        {

            var tcs = new TaskCompletionSource<bool>();

            // Capture the current synchronization context
            var syncContext = SynchronizationContext.Current;

            // Show the message box on the UI thread
            syncContext.Post(state =>
            {
                var form = new Form();
                MessageBox.Show(form, message);
                tcs.SetResult(true);
                form.Dispose();
            }, null);

            await AloneProcess.SleepAsync(3000);
            System.Windows.Forms.Application.Exit();
            System.Windows.Forms.Application.ExitThread();

            // Wait for the message box to close
            await tcs.Task;
        }

        public static async Task<int> SleepRandomAsync(int minMilliseconds, int maxMilliseconds)
        {
            int sleepDuration;

            lock (RandomLock)
            {
                if (_randomInstance == null)
                {
                    _randomInstance = new Random();
                }

                sleepDuration = maxMilliseconds == 0 ? minMilliseconds : _randomInstance.Next(minMilliseconds, maxMilliseconds);
            }

            int totalsleep = int.Parse(ReadInI.Delay.SlowPC) + sleepDuration;
            await Task.Delay(totalsleep);
            return totalsleep;
        }

        public static int SleepRandom(int minMilliseconds, int maxMilliseconds)
        {
            int sleepDuration;

            lock (RandomLock)
            {
                if (_randomInstance == null)
                {
                    _randomInstance = new Random();
                }

                sleepDuration = maxMilliseconds == 0 ? minMilliseconds : _randomInstance.Next(minMilliseconds, maxMilliseconds);
            }

            int totalsleep = int.Parse(ReadInI.Delay.SlowPC) + sleepDuration;
            Task.Delay(totalsleep);
            return totalsleep;
        }

        //AloneProcess.SetForegroundWindowProcess();
        public static void SetForegroundWindowProcess()
        {
            if (EnsureProcessInitialized())
            {
                SetForegroundWindow(talesRunnerHandle);
            }
        }

        public static class ReadInI
        {

            public static class Window
            {
                public static string TRMove { get; set; }
                public static string TRCoordinateX { get; set; }
                public static string TRCoordinateY { get; set; }
            }

            public static class Setting
            {
                public static string TRFile { get; set; }
                public static string Account { get; set; }
                public static string Password { get; set; }
                public static string FullAuto { get; set; }
            }

            public static class Mercenary
            {
                public static string Mission1 { get; set; }
                public static string Mission2 { get; set; }
            }

            public static class AFKMode
            {
                public static string AFKmode { get; set; }
                public static string Map { get; set; }
            }

            public static class AFKOption
            {
                public static string AutoMercenary { get; set; }
                public static string AutoGuardianSpirit { get; set; }
                public static string AutoDailyMission { get; set; }
            }

            public static class DailyOption
            {
                public static string CheckDaily { get; set; }
                public static string AutoDailyStore { get; set; }
                public static string AutoDiscountStore { get; set; }
                public static string AutoNoobStore { get; set; }
                public static string AutoVIPStore { get; set; }
                public static string AutoBoxStore { get; set; }
                public static string AutoEventMission { get; set; }
                public static string AutoNetMission { get; set; }
                public static string AutoDailySignBoard { get; set; }
                public static string AutoDailyDoctorsbait { get; set; }
                public static string AutoContribute { get; set; }
                public static string LastContribute { get; set; }
            }

            public static class RoomSetting
            {
                public static string RoomMode { get; set; }
                public static string CreateRoom { get; set; }
                public static string OwnPassword { get; set; }
                public static string RoomPassword { get; set; }
            }

            public static class Delay
            {
                public static string SlowPC { get; set; }
                public static string JumpWait { get; set; }
                public static string JumpWaitRandom { get; set; }
                public static string Landing { get; set; }
            }

        }

        public static IniFile ini { get; } = new IniFile("Alone.ini");

        public static void InitializeSetting()
        {
            ReadInI.Setting.TRFile = ini.Read("TRFile", "Setting");
            ReadInI.Setting.Account = ini.Read("Account", "Setting");
            ReadInI.Setting.Password = ini.Read("Password", "Setting");
            ReadInI.Setting.FullAuto = ini.Read("FullAuto", "Setting");

            ReadInI.Window.TRMove = ini.Read("TRMove", "Window");
            ReadInI.Window.TRCoordinateX = ini.Read("TRCoordinateX", "Window");
            ReadInI.Window.TRCoordinateY = ini.Read("TRCoordinateY", "Window");

            ReadInI.Mercenary.Mission1 = ini.Read("Mission1", "Mercenary");
            ReadInI.Mercenary.Mission2 = ini.Read("Mission2", "Mercenary");

            ReadInI.AFKMode.AFKmode = ini.Read("AFKmode", "AFKmode");
            ReadInI.AFKMode.Map = ini.Read("Map", "AFKmode");

            ReadInI.AFKOption.AutoMercenary = ini.Read("AutoMercenary", "AFKOption");
            ReadInI.AFKOption.AutoGuardianSpirit = ini.Read("AutoGuardianSpirit", "AFKOption");
            ReadInI.AFKOption.AutoDailyMission = ini.Read("AutoDailyMission", "AFKOption");

            ReadInI.DailyOption.AutoDiscountStore = ini.Read("AutoDiscountStore", "DailyOption");
            ReadInI.DailyOption.AutoNoobStore = ini.Read("AutoNoobStore", "DailyOption");
            ReadInI.DailyOption.AutoVIPStore = ini.Read("AutoVIPStore", "DailyOption");
            ReadInI.DailyOption.AutoBoxStore = ini.Read("AutoBoxStore", "DailyOption");
            ReadInI.DailyOption.AutoContribute = ini.Read("AutoContribute", "DailyOption");


            if (ReadInI.DailyOption.AutoDiscountStore == "1" || ReadInI.DailyOption.AutoNoobStore == "1" || ReadInI.DailyOption.AutoVIPStore == "1" || ReadInI.DailyOption.AutoBoxStore == "1")
            {
                ReadInI.DailyOption.AutoDailyStore = "1";
            }

            //once a day

            ReadInI.DailyOption.AutoDailyDoctorsbait = ini.Read("AutoDailyDoctorsbait", "DailyOption");
            ReadInI.DailyOption.AutoDailySignBoard = ini.Read("AutoDailySignBoard", "DailyOption");
            ReadInI.DailyOption.AutoNetMission = ini.Read("AutoNetMission", "DailyOption");

            //multiple times a day 
            ReadInI.DailyOption.AutoEventMission = ini.Read("AutoEventMission", "DailyOption");


            if (ReadInI.DailyOption.AutoDailyStore == "1" || ReadInI.DailyOption.AutoDailyDoctorsbait == "1")
            {
                ReadInI.DailyOption.CheckDaily = "1";
            }


            ReadInI.RoomSetting.RoomMode = ini.Read("RoomMode", "RoomSetting");
            ReadInI.RoomSetting.CreateRoom = ini.Read("CreateRoom", "RoomSetting");
            ReadInI.RoomSetting.OwnPassword = ini.Read("OwnPassword", "RoomSetting");
            ReadInI.RoomSetting.RoomPassword = ini.Read("RoomPassword", "RoomSetting");

            ReadInI.Delay.SlowPC = ini.Read("SlowPC", "Delay");
            ReadInI.Delay.JumpWait = ini.Read("JumpWait", "Delay");
            ReadInI.Delay.JumpWaitRandom = ini.Read("JumpWaitRandom", "Delay");
            ReadInI.Delay.Landing = ini.Read("Landing", "Delay");
        }

        public class IniFile
        {
            string Path;
            string EXE = Assembly.GetExecutingAssembly().GetName().Name;

            [DllImport("kernel32", CharSet = CharSet.Unicode)]
            static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

            [DllImport("kernel32", CharSet = CharSet.Unicode)]
            static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

            public IniFile(string IniPath = null)
            {
                Path = new FileInfo(IniPath ?? EXE + ".ini").FullName;

                if (!File.Exists(Path))
                {
                    using (var client = new WebClient())
                    {
                        client.DownloadFile("https://raw.githubusercontent.com/AloneCodeA/AloneAIO/main/Alone.ini", Path);
                    }
                }
            }

            public string Read(string Key, string Section = null)
            {
                var RetVal = new StringBuilder(255);
                GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
                return RetVal.ToString();
            }

            public void Write(string Key, string Value, string Section = null)
            {
                WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
            }

            public void DeleteKey(string Key, string Section = null)
            {
                Write(Key, null, Section ?? EXE);
            }

            public void DeleteSection(string Section = null)
            {
                Write(null, null, Section ?? EXE);
            }

            public bool KeyExists(string Key, string Section = null)
            {
                return Read(Key, Section).Length > 0;
            }
        }
    }

    public class ImageSystem
    {
        public static event EventHandler<ErrorEventArgs> OnError;

        [DllImport("user32.dll")]
        static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int GetClientRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("gdi32.dll")]
        static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth, int nHeight);

        [DllImport("gdi32.dll")]
        static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport("gdi32.dll")]
        static extern bool DeleteObject(IntPtr hObject);

        [DllImport("gdi32.dll")]
        static extern int DeleteDC(IntPtr hDC);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("gdi32.dll")]
        static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, CopyPixelOperation dwRop);

        public static async Task<Bitmap> GetScreenshotOfTalesRunner()
        {
            return await Task.Run(() =>
            {
                try
                {
                    EnsureProcessInitialized();

                    Bitmap screenshot = null;
                    Rectangle bounds = Rectangle.Empty;
                    int borderSizeX;
                    int borderSizeY;

                    try
                    {
                        GetWindowRect(talesRunnerHandle, out RECT windowRect);
                        bounds = new Rectangle(windowRect.Left, windowRect.Top, windowRect.Right - windowRect.Left, windowRect.Bottom - windowRect.Top);

                        GetClientRect(talesRunnerHandle, out RECT clientRect);

                        borderSizeX = (bounds.Width - (clientRect.Right - clientRect.Left)) / 2;
                        borderSizeY = bounds.Height - (clientRect.Bottom - clientRect.Top) - borderSizeX;

                        bounds.X += borderSizeX;
                        bounds.Y += borderSizeY;
                        bounds.Width -= borderSizeX * 2;
                        bounds.Height -= borderSizeY;

                        if (borderSizeY == 31)
                        {
                            bounds.Height -= 8;
                        }
                        else if (borderSizeY == 1)
                        {
                            bounds.Height -= 1;
                        }

                        screenshot = new Bitmap(bounds.Width, bounds.Height);
                    }
                    catch (ArgumentException ex)
                    {
                        return null;
                    }

                    using (Graphics graphics = Graphics.FromImage(screenshot))
                    {
                        IntPtr hdcSrc = GetWindowDC(talesRunnerHandle);
                        IntPtr hdcDest = graphics.GetHdc();
                        IntPtr hBitmap = CreateCompatibleBitmap(hdcSrc, bounds.Width, bounds.Height);
                        IntPtr hOld = SelectObject(hdcSrc, hBitmap);

                        BitBlt(hdcDest, 0, 0, bounds.Width, bounds.Height, hdcSrc, borderSizeX, borderSizeY, CopyPixelOperation.SourceCopy);

                        SelectObject(hdcSrc, hOld);
                        DeleteObject(hBitmap);
                        DeleteDC(hdcSrc);
                        graphics.ReleaseHdc(hdcDest);
                    }

                    return screenshot;
                }
                catch (Exception ex)
                {
                    return null;
                }
            });
        }

        // MessageBox.Show((await ImageSystem.GetPixelColor(595, 666)).ToString(), "PixelSearch Information", MessageBoxButtons.OK);
        public static async Task<string> GetPixelColor(int x, int y)
        {
            Bitmap screenshot = await GetScreenshotOfTalesRunner();
            if (screenshot == null)
            {
                return "#000000";
            }

            try
            {
                if (x >= 0 && x < screenshot.Width && y >= 0 && y < screenshot.Height)
                {
                    Color color = screenshot.GetPixel(x, y);
                    screenshot.Dispose();
                    return ColorToHex(color);
                }
                else
                {
                    return "#000000";
                }
            }
            catch (Exception ex)
            {
                return "#000000";
            }
        }

        public static async Task<string> GetPixelColorByScreenshot(Bitmap screenshot, int x, int y)
        {
            try
            {
                if (x >= 0 && x < screenshot.Width && y >= 0 && y < screenshot.Height)
                {
                    Color color = screenshot.GetPixel(x, y);
                    return ColorToHex(color);
                }
                else
                {
                    return "#000000";
                }
            }
            catch (Exception ex)
            {
                return "#000000";
            }
        }

        //MessageBox.Show(await ImageSystem.PixelSearch(900, 380, "F8FF00", 10) ? "Color found" : "Color not found", "PixelSearch Information", MessageBoxButtons.OK);
        public static async Task<bool> PixelSearch(int x, int y, string colorHex, int variation = 0)
        {

            if (!colorHex.StartsWith("#"))
            {
                colorHex = "#" + colorHex;
            }

            var searchColor = ColorTranslator.FromHtml(colorHex);
            var currentColor = ColorTranslator.FromHtml(await GetPixelColor(x, y));

            return ColorsMatch(searchColor, currentColor, variation);
        }

        public static async Task<bool> PixelSearchByScreenshot(Bitmap screenshot, int x, int y, string colorHex, int variation = 0)
        {

            if (!colorHex.StartsWith("#"))
            {
                colorHex = "#" + colorHex;
            }

            var searchColor = ColorTranslator.FromHtml(colorHex);
            var currentColor = ColorTranslator.FromHtml(await GetPixelColorByScreenshot(screenshot, x, y));

            return ColorsMatch(searchColor, currentColor, variation);
        }

        //MessageBox.Show(await ImageSystem.PixelSearchArea(415, 751, 415, 751, "0094B4").ToString(), "PixelSearchArea Information", MessageBoxButtons.OK);
        public static async Task<(int x, int y)> PixelSearchArea(Bitmap screenshot, int x1, int y1, int x2, int y2, string colorHex, int variation = 0)
        {
            if (!colorHex.StartsWith("#"))
            {
                colorHex = "#" + colorHex;
            }

            var searchColor = ColorTranslator.FromHtml(colorHex);

            int startX = x1 <= x2 ? Math.Min(x1, x2) : Math.Max(x1, x2);
            int endX = x1 <= x2 ? Math.Max(x1, x2) : Math.Min(x1, x2);
            int stepX = x1 <= x2 ? 1 : -1;

            int startY = y1 <= y2 ? Math.Min(y1, y2) : Math.Max(y1, y2);
            int endY = y1 <= y2 ? Math.Max(y1, y2) : Math.Min(y1, y2);
            int stepY = y1 <= y2 ? 1 : -1;

            for (int x = startX; stepX > 0 ? x <= endX : x >= endX; x += stepX)
            {
                for (int y = startY; stepY > 0 ? y <= endY : y >= endY; y += stepY)
                {
                    var currentColor = ColorTranslator.FromHtml(await GetPixelColorByScreenshot(screenshot, x, y));

                    if (ColorsMatch(searchColor, currentColor, variation))
                    {
                        return (x, y); // Match found, return the coordinates
                    }
                }
            }

            return (-1, -1); // No match found, return invalid coordinates
        }

        /*
            var image = ImageSystem.ImageSearch(845, 612, 1009, 692, "F10", 20);
            if (image.x != -1 && image.y != -1)
            MessageBox.Show("found", "Information", MessageBoxButtons.OK);
        */

        public static async Task<(int x, int y)> ImageSearch(int x1, int y1, int x2, int y2, string imageName, int variation = 0)
        {
            EnsureProcessInitialized();

            var screenshot = await GetScreenshotOfTalesRunner();
            if (screenshot == null)
            {
                return (-1, -1);
            }

            Bitmap image = (Bitmap)Properties.Resources.ResourceManager.GetObject(imageName);

            if (image == null)
            {
                return (-1, -1);
            }

            var searchRect = new Rectangle(x1, y1, x2 - x1, y2 - y1);

            int imageWidth = image.Width;
            int imageHeight = image.Height;
            int screenWidth = screenshot.Width;
            int screenHeight = screenshot.Height;

            for (int screenX = searchRect.X; screenX < searchRect.Right - imageWidth; screenX++)
            {
                for (int screenY = searchRect.Y; screenY < searchRect.Bottom - imageHeight; screenY++)
                {
                    bool found = true;

                    for (int imageX = 0; imageX < imageWidth; imageX++)
                    {
                        for (int imageY = 0; imageY < imageHeight; imageY++)
                        {
                            int pixelX = screenX + imageX;
                            int pixelY = screenY + imageY;

                            if (pixelX >= screenWidth || pixelY >= screenHeight)
                            {
                                found = false;
                                break;
                            }

                            Color imagePixel = image.GetPixel(imageX, imageY);
                            Color screenPixel = screenshot.GetPixel(pixelX, pixelY);

                            if (!ColorsMatch(imagePixel, screenPixel, variation))
                            {
                                found = false;
                                break;
                            }
                        }

                        if (!found)
                            break;
                    }

                    if (found)
                    {
                        return (screenX, screenY);
                    }
                }
            }
            return (-1, -1);
        }

        public static async Task<(int x, int y)> ImageSearchByScreenshot(Bitmap screenshot, int x1, int y1, int x2, int y2, string imageName, int variation = 0)
        {
            EnsureProcessInitialized();

            if (screenshot == null)
            {
                return (-1, -1);
            }

            Bitmap image = (Bitmap)Properties.Resources.ResourceManager.GetObject(imageName);

            if (image == null)
            {
                return (-1, -1);
            }

            var searchRect = new Rectangle(x1, y1, x2 - x1, y2 - y1);

            int imageWidth = image.Width;
            int imageHeight = image.Height;
            int screenWidth = screenshot.Width;
            int screenHeight = screenshot.Height;

            for (int screenX = searchRect.X; screenX < searchRect.Right - imageWidth; screenX++)
            {
                for (int screenY = searchRect.Y; screenY < searchRect.Bottom - imageHeight; screenY++)
                {
                    bool found = true;

                    for (int imageX = 0; imageX < imageWidth; imageX++)
                    {
                        for (int imageY = 0; imageY < imageHeight; imageY++)
                        {
                            int pixelX = screenX + imageX;
                            int pixelY = screenY + imageY;

                            if (pixelX >= screenWidth || pixelY >= screenHeight)
                            {
                                found = false;
                                break;
                            }

                            Color imagePixel = image.GetPixel(imageX, imageY);
                            Color screenPixel = screenshot.GetPixel(pixelX, pixelY);

                            if (!ColorsMatch(imagePixel, screenPixel, variation))
                            {
                                found = false;
                                break;
                            }
                        }

                        if (!found)
                            break;
                    }

                    if (found)
                    {
                        return (screenX, screenY);
                    }
                }
            }
            return (-1, -1);
        }

        private static bool ColorsMatch(Color color1, Color color2, int variation)
        {
            return Math.Abs(color1.R - color2.R) <= variation &&
                   Math.Abs(color1.G - color2.G) <= variation &&
                   Math.Abs(color1.B - color2.B) <= variation;
        }

        //MessageBox.Show(ImageSystem.ColorToHex(Color.FromArgb(255, 255, 255)), "ColorToHex Information", MessageBoxButtons.OK);
        private static string ColorToHex(Color color)
        {
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        }

    }

    public class InputSystem
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool SetCursorPos(int x, int y);
        public struct POINT
        {
            public int X;
            public int Y;
        }

        public static event EventHandler<ErrorEventArgs> OnError;

        public static class WindowMessages
        {
            public const uint SWP_NOSIZE = 0x0001;
            public const uint SWP_TOPMOST = 0x0008;
            public const int WM_MOUSEMOVE = 0x0200;
            public const int WM_LBUTTONDOWN = 0x0201;
            public const int WM_LBUTTONUP = 0x0202;
            public const int WM_RBUTTONDOWN = 0x0204;
            public const int WM_RBUTTONUP = 0x0205;
            public const int GWL_STYLE = -16;
            public const int WS_BORDER = 0x00800000;
            public const int WS_CAPTION = 0x00C00000;
        }

        [Flags]
        public enum InputType
        {
            Mouse = 0,
            Keyboard = 1,
            Hardware = 2
        }

        public static class KeyEvent
        {
            public const uint KEYUP = 0x0002;
            public const uint SCANCODE = 0x0008;
        }

        public class SendInputFailedException : Exception
        {
            public SendInputFailedException(string message) : base(message) { }
        }

        [DllImport("user32.dll")]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        private static IntPtr originalForegroundWindow;
        private static POINT originalCursorPosition;

        public static void Move(int x, int y)
        {
            if (EnsureProcessInitialized())
            {
                // Get the window bounds and client bounds
                ImageSystem.GetWindowRect(talesRunnerHandle, out RECT windowRect);
                ImageSystem.GetClientRect(talesRunnerHandle, out RECT clientRect);

                var bounds = new Rectangle(windowRect.Left, windowRect.Top, windowRect.Right - windowRect.Left, windowRect.Bottom - windowRect.Top);

                // Calculate border sizes
                int borderSizeX = (bounds.Width - (clientRect.Right - clientRect.Left)) / 2;
                int borderSizeY = bounds.Height - (clientRect.Bottom - clientRect.Top) - borderSizeX;

                bounds.X += borderSizeX;
                bounds.Y += borderSizeY;
                bounds.Width -= borderSizeX * 2;
                bounds.Height -= borderSizeY;

                if (borderSizeY == 31)
                {
                    bounds.Height -= 8;
                }
                else if (borderSizeY == 1)
                {
                    bounds.Height -= 1;
                }

                // Calculate the absolute coordinates
                int absoluteX = bounds.Left + x;
                int absoluteY = bounds.Top + y;

                // Move the cursor
                SetCursorPos(absoluteX, absoluteY);
            }
        }

        //InputSystem.PostKey(roomPassword); // Default mode (false)
        //InputSystem.PostKey(roomPassword, 2); // Default mode with count 2
        //InputSystem.PostKey(roomPassword, 3, "string"); // String mode with count 3
        public static bool PostKey(string key, int count = 1, string inputMode = "")
        {
            EnsureProcessInitialized();

            // Validate inputs
            if (string.IsNullOrEmpty(key))
            {
                return false;
            }

            if (string.Equals(inputMode, "string", StringComparison.OrdinalIgnoreCase))
            {
                return PostStringKey(key);
            }
            else
            {
                return PostEnumKey(key, count);
            }
        }

        private static bool PostStringKey(string key)
        {
            const uint WM_KEYDOWN = 0x0100;
            const uint WM_KEYUP = 0x0101;
            const uint WM_CHAR = 0x0102;

            foreach (char c in key)
            {
                bool isUpperCase = char.IsUpper(c);
                string keyToSend = char.ToUpper(c).ToString();
                IntPtr wParam = (IntPtr)keyToSend[0];

                if (isUpperCase)
                {
                    // Simulate CapsLock press
                    if (!PostMessage(talesRunnerHandle, WM_KEYDOWN, (IntPtr)Keys.CapsLock, IntPtr.Zero) ||
                        !PostMessage(talesRunnerHandle, WM_KEYUP, (IntPtr)Keys.CapsLock, IntPtr.Zero))
                    {
                        return false;
                    }
                }

                // Send the character
                uint message = isUpperCase ? WM_CHAR : WM_KEYDOWN;
                if (!PostMessage(talesRunnerHandle, message, wParam, IntPtr.Zero))
                {
                    return false;
                }

                if (isUpperCase)
                {
                    // Simulate CapsLock release
                    if (!PostMessage(talesRunnerHandle, WM_KEYDOWN, (IntPtr)Keys.CapsLock, IntPtr.Zero) ||
                        !PostMessage(talesRunnerHandle, WM_KEYUP, (IntPtr)Keys.CapsLock, IntPtr.Zero))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool PostEnumKey(string key, int count)
        {
            const uint WM_KEYDOWN = 0x0100;

            if (Enum.TryParse<Keys>(key, out Keys keyCode))
            {
                for (int i = 0; i < count; i++)
                {
                    if (!PostMessage(talesRunnerHandle, WM_KEYDOWN, (IntPtr)keyCode, IntPtr.Zero))
                    {
                        return false;
                    }
                    AloneProcess.SleepRandom(150, 300);
                }
                return true;
            }
            else
            {
                return false;
            }
        }


        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, Input[] pInputs, int cbSize);

        [DllImport("user32.dll")]
        private static extern IntPtr GetMessageExtraInfo();

        public static ushort DashJumpKey { get; private set; }
        public static ushort DashKey { get; private set; }
        public static ushort ItemKey { get; private set; }
        public static ushort JumpKey { get; private set; }
        public static ushort LandingKey { get; private set; } = JumpKey;
        public static ushort ShuKey { get; private set; }
        public static ushort UpKey { get; private set; }
        public static ushort DownKey { get; private set; }
        public static ushort LeftKey { get; private set; }
        public static ushort RightKey { get; private set; }
        public static ushort Number0 { get; private set; } = 0x0B;
        public static ushort Number1 { get; private set; } = 0x02;
        public static ushort Number2 { get; private set; } = 0x03;
        public static ushort Number3 { get; private set; } = 0x04;
        public static ushort Number4 { get; private set; } = 0x05;
        public static ushort Number5 { get; private set; } = 0x06;
        public static ushort Number6 { get; private set; } = 0x07;
        public static ushort Number7 { get; private set; } = 0x08;
        public static ushort Number8 { get; private set; } = 0x09;
        public static ushort Number9 { get; private set; } = 0x0A;
        public static ushort F9 { get; private set; } = 0x43;
        public static ushort F10 { get; private set; } = 0x44;
        public static ushort F11 { get; private set; } = 0x57;
        public static ushort ShiftKey { get; private set; } = 0x2A;

        public static void SetTalesRunnerKey()
        {
            string talesOptionFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TalesRunner", "Option.txt");
            string[] lines = System.IO.File.ReadAllLines(talesOptionFilePath);

            if (lines.Length >= 36) // 確保陣列長度足夠長
            {
                DashJumpKey = TalesRunnerKey(GetKeyValue(lines[4]));
                DashKey = TalesRunnerKey(GetKeyValue(lines[5]));
                ItemKey = TalesRunnerKey(GetKeyValue(lines[10]));
                JumpKey = TalesRunnerKey(GetKeyValue(lines[11]));
                LandingKey = JumpKey;
                ShuKey = TalesRunnerKey(GetKeyValue(lines[28]));
                UpKey = TalesRunnerKey(GetKeyValue(lines[35]));
                DownKey = TalesRunnerKey(GetKeyValue(lines[6]));
                LeftKey = TalesRunnerKey(GetKeyValue(lines[13]));
                RightKey = TalesRunnerKey(GetKeyValue(lines[26]));
            }
            else
            {
                MessageBox.Show("Option.txt 檔案格式不正確，腳本可能運作不正確 請重開跑online 後再開 腳本");
            }

            if (lines.Length >= 26 && lines[25] != "Resolution=1024X768")
            {
                AloneProcess.ShowMessageBoxAsync("請將遊戲解析度設定為 1024x768");
            }
        }

        private static ushort TalesRunnerKey(string key)
        {
            if (int.TryParse(key, out int keyInt))
            {
                switch (keyInt)
                {
                    case 2: return 0x02;
                    case 3: return 0x03;
                    case 4: return 0x04;
                    case 5: return 0x05;
                    case 6: return 0x06;
                    case 7: return 0x07;
                    case 8: return 0x08;
                    case 9: return 0x09;
                    case 10: return 0x0A;
                    case 11: return 0x0B;
                    case 14: return 0x0E;
                    case 15: return 0x0F;
                    case 16: return 0x10;
                    case 17: return 0x11;
                    case 18: return 0x12;
                    case 19: return 0x13;
                    case 20: return 0x14;
                    case 21: return 0x15;
                    case 22: return 0x16;
                    case 23: return 0x17;
                    case 24: return 0x18;
                    case 25: return 0x19;
                    case 29: return 0x1D;
                    case 30: return 0x1E;
                    case 31: return 0x1F;
                    case 32: return 0x20;
                    case 33: return 0x21;
                    case 34: return 0x22;
                    case 35: return 0x23;
                    case 36: return 0x24;
                    case 37: return 0x25;
                    case 38: return 0x26;
                    case 42: return 0x2A;
                    case 44: return 0x2C;
                    case 45: return 0x2D;
                    case 46: return 0x2E;
                    case 47: return 0x2F;
                    case 48: return 0x30;
                    case 49: return 0x31;
                    case 50: return 0x32;
                    case 56: return 0x38;
                    case 57: return 0x39;
                    case 199: return 0xC7;
                    case 200: return 0xC8;
                    case 201: return 0xC9;
                    case 203: return 0xCB;
                    case 205: return 0xCD;
                    case 208: return 0xD0;
                    case 209: return 0xD1;
                    case 210: return 0xD2;
                    default: return 0x00;
                }
            }
            return 0x00;
        }

        private static string GetKeyValue(string line)
        {
            string[] parts = line.Split('=');
            if (parts.Length > 1)
            {
                return parts[1];
            }
            return string.Empty;
        }

        //InputSystem.SendKey(InputSystem.DashJumpKey);
        public static async Task<bool> SendKey(ushort key, string keyState, InputType inputType = InputType.Keyboard)
        {
            bool isKeyUp = keyState.Equals("KeyUp", StringComparison.OrdinalIgnoreCase);
            return await SendKey(key, isKeyUp, inputType);
        }

        public static async Task<bool> SendKey(ushort key, InputType inputType = InputType.Keyboard)
        {
            bool success = await SendKey(key, false, inputType);
            success &= await SendKey(key, true, inputType);
            return success;
        }

        private static async Task<bool> SendKey(ushort key, bool keyUp, InputType inputType = InputType.Keyboard)
        {
            AloneProcess.SetForegroundWindowProcess();

            if (key == InputSystem.LandingKey)
            {
                int jumpWaitRandomDelay = await AloneProcess.SleepRandomAsync(1, int.Parse(ReadInI.Delay.JumpWaitRandom));
                await AloneProcess.SleepAsync(int.Parse(ReadInI.Delay.JumpWait) + jumpWaitRandomDelay);
            }

            uint flagtosend = keyUp ? KeyEvent.KEYUP | KeyEvent.SCANCODE : KeyEvent.SCANCODE;

            var input = new Input
            {
                type = (int)inputType,
                u = new InputUnion
                {
                    ki = new KeyboardInput
                    {
                        wVk = 0,
                        wScan = key,
                        dwFlags = flagtosend,
                        dwExtraInfo = GetMessageExtraInfo()
                    }
                }
            };

            var inputs = new Input[] { input };

            if (SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Input))) == 0)
            {
                return false;
            }
            return true;
        }

        private struct Input
        {
            public int type;
            public InputUnion u;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct InputUnion
        {
            [FieldOffset(0)] public readonly MouseInput mi;
            [FieldOffset(0)] public KeyboardInput ki;
            [FieldOffset(0)] public readonly HardwareInput hi;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MouseInput
        {
            public readonly int dx;
            public readonly int dy;
            public readonly uint mouseData;
            public readonly uint dwFlags;
            public readonly uint time;
            public readonly IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct KeyboardInput
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public readonly uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct HardwareInput
        {
            public readonly uint uMsg;
            public readonly ushort wParamL;
            public readonly ushort wParamH;
        }
    }

    public class MapFunction
    {
        public static async Task Burdles()
        {
            AloneProcess.UpdateTalesState("Running..Burdles");
            await RunFunction.ReleaseKey();
            await InputSystem.SendKey(InputSystem.UpKey, "KeyDown");
            await InputSystem.SendKey(InputSystem.LeftKey, "KeyDown");

            if (true)    // Join WaitingRoom Success
            {
                await InputSystem.SendKey(InputSystem.UpKey, "KeyDown");

                int landingDelay = int.Parse(ReadInI.Delay.Landing);

                for (int i = 0; i < landingDelay; i++)
                {
                    await InputSystem.SendKey(InputSystem.LandingKey);

                    if (i >= landingDelay)
                    {
                        await InputSystem.SendKey(InputSystem.LeftKey, "KeyUp");
                        await InputSystem.SendKey(InputSystem.DashKey, "KeyDown");
                        await AloneProcess.SleepAsync(30);
                        await InputSystem.SendKey(InputSystem.DashKey, "KeyUp");
                    }
                }

                while (true)
                {
                    await InputSystem.SendKey(InputSystem.UpKey, "KeyDown");

                    await RunFunction.DetectAngry();
                    await InputSystem.SendKey(InputSystem.LeftKey, "KeyDown");
                    await InputSystem.SendKey(InputSystem.DashKey, "KeyDown");
                    await AloneProcess.SleepAsync(45);
                    await InputSystem.SendKey(InputSystem.JumpKey, "KeyDown");
                    await AloneProcess.SleepAsync(30);
                    await InputSystem.SendKey(InputSystem.JumpKey, "KeyUp");
                    await AloneProcess.SleepAsync(45);
                    await InputSystem.SendKey(InputSystem.JumpKey, "KeyDown");
                    await AloneProcess.SleepAsync(30);
                    await InputSystem.SendKey(InputSystem.JumpKey, "KeyUp");
                    await InputSystem.SendKey(InputSystem.DashKey, "KeyUp");
                    await AloneProcess.SleepAsync(45);
                    await InputSystem.SendKey(InputSystem.LeftKey, "KeyUp");
                    await AloneProcess.SleepAsync(680);
                    await RunFunction.DashJump(1, 20, 680);
                }
            }
            InputSystem.PostKey("Escape");
        }

        public static async Task Map3()
        {
            AloneProcess.UpdateTalesState("Running..Map3");
            await RunFunction.ReleaseKey();
            int loopCounter = 0;

            while (true)
            {
                await InputSystem.SendKey(InputSystem.UpKey, "KeyDown");

                await RunFunction.DetectShock();
                await InputSystem.SendKey(InputSystem.ItemKey, "KeyDown");
                await AloneProcess.SleepAsync(30);
                await InputSystem.SendKey(InputSystem.ItemKey, "KeyUp");
                loopCounter++;

                if (loopCounter % 8 == 0)
                {
                    await RunFunction.LongJump(1, 60, 100);
                    await InputSystem.SendKey(InputSystem.ItemKey, "KeyDown");
                    await AloneProcess.SleepAsync(30);
                    await InputSystem.SendKey(InputSystem.ItemKey, "KeyUp");
                    loopCounter = 0;
                }
            }
            
            InputSystem.PostKey("Escape");
        }
    }

    public class RunFunction
    {

        public static async Task<int> CheckRunPointRaw()
        {
            Bitmap screenshot = await ImageSystem.GetScreenshotOfTalesRunner();
            string[] colors = { "5AAD00", "BE9400", "0085BA"};
            foreach (string color in colors)
            {
          (int X, int Y) result = await ImageSystem.PixelSearchArea(screenshot, 230, 675, 783, 675, color, 5);
                if (result.X != -1)
                {
                    int talesState = (int)(result.X);
                    string talesStateMeassage = $"RunPoint.." + talesState.ToString();
                    AloneProcess.UpdateTalesState(talesStateMeassage);
                    return result.X; // Return the found value directly
                }
            }
            return 0; // Return 0 if no match is found
        }

        public static async Task<int> CheckRunPoint()
        {
            Bitmap screenshot = await ImageSystem.GetScreenshotOfTalesRunner();
            string[] colors = { "5AAD00", "BE9400", "0085BA" };
            foreach (string color in colors)
            {
                (int X, int Y) result = await ImageSystem.PixelSearchArea(screenshot, 230, 675, 783, 675, color, 5);
                if (result.X != -1)
                {
                    int talesState = (int)(((result.X - 240) / 543f) * 100f);
                    string talesStateMeassage = $"Running...地圖完成度" + talesState.ToString() + "%";
                    AloneProcess.UpdateTalesState(talesStateMeassage);
                    return result.X; // Return the found value directly
                }
            }
            return 0; // Return 0 if no match is found
        }

        public static async Task<int> CheckAlreadyRunPoint()
        {
            Bitmap screenshot = await ImageSystem.GetScreenshotOfTalesRunner();
            (int X, int Y) result = await ImageSystem.PixelSearchArea(screenshot, 800, 722, 200, 722, "1455DA", 30);
            if (result.X != -1)
            {
                return result.X; // Return the found value directly
            }
            return 0; // Return 0 if no match is found
        }

        public static async Task ReleaseKey()
        {
            await InputSystem.SendKey(InputSystem.UpKey, "KeyUp");
            await InputSystem.SendKey(InputSystem.DownKey, "KeyUp");
            await InputSystem.SendKey(InputSystem.LeftKey, "KeyUp");
            await InputSystem.SendKey(InputSystem.RightKey, "KeyUp");
            await InputSystem.SendKey(InputSystem.JumpKey, "KeyUp");
            await InputSystem.SendKey(InputSystem.DashKey, "KeyUp");
            await InputSystem.SendKey(InputSystem.ItemKey, "KeyUp");
        }

        public static async Task DashJump(int jumpTime, int sleepTime1, int sleepTime2)
        {
            while (true)
            {
                await InputSystem.SendKey(InputSystem.DashKey, "KeyDown");
                await AloneProcess.SleepAsync(sleepTime1);
                await InputSystem.SendKey(InputSystem.JumpKey, "KeyDown");
                await AloneProcess.SleepAsync(60);
                await InputSystem.SendKey(InputSystem.JumpKey, "KeyUp");
                await AloneProcess.SleepAsync(sleepTime1);
                await InputSystem.SendKey(InputSystem.JumpKey, "KeyDown");
                await AloneProcess.SleepAsync(60);
                await InputSystem.SendKey(InputSystem.JumpKey, "KeyUp");
                await InputSystem.SendKey(InputSystem.DashKey, "KeyUp");
                await AloneProcess.SleepAsync(sleepTime2);
                jumpTime--;

                if (jumpTime <= 0)
                {
                    break;
                }
            }
        }

        public static async Task LongJump(int jumpTime, int sleepTime1, int sleepTime2)
        {
            while (true)
            {
                await InputSystem.SendKey(InputSystem.JumpKey, "KeyDown");
                await AloneProcess.SleepAsync(60);
                await InputSystem.SendKey(InputSystem.JumpKey, "KeyUp");
                await AloneProcess.SleepAsync(sleepTime1);
                await InputSystem.SendKey(InputSystem.JumpKey, "KeyDown");
                await AloneProcess.SleepAsync(60);
                await InputSystem.SendKey(InputSystem.JumpKey, "KeyUp");
                await AloneProcess.SleepAsync(sleepTime2);
                jumpTime--;

                if (jumpTime <= 0)
                {
                    break;
                }
            }
        }

        public static async Task RNGUturn(ushort Beforedirection)
        {
            Random random = AloneProcess.RandomInstance;
            int turningRNG = random.Next(1, 3);

            ushort firstKey = turningRNG == 1 ? InputSystem.RightKey : InputSystem.LeftKey;
            ushort secondKey = turningRNG == 1 ? InputSystem.LeftKey : InputSystem.RightKey;

            ushort Uturndirection = Beforedirection == InputSystem.UpKey ? InputSystem.DownKey : InputSystem.UpKey;

            for (int i = 0; i < 2; i++)
            {
                await InputSystem.SendKey(i == 0 ? firstKey : secondKey, "KeyDown");
                await InputSystem.SendKey(i == 0 ? Beforedirection : Uturndirection, "KeyUp");
                await InputSystem.SendKey(i == 0 ? Uturndirection : Beforedirection, "KeyDown");
                await InputSystem.SendKey(i == 0 ? firstKey : secondKey, "KeyUp");
                if (i == 0)
                {
                    await AloneProcess.SleepAsync(200);
                }
            }
        }

        public static async Task DetectShock()
        {
            // Client: 905, 478 (Shock)
            // Color: 2E2E2E (Red=2E Green=2E Blue=2E)
            if (await ImageSystem.PixelSearch(905, 478, "2E2E2E", 15))
            {
                for (int i = 0; i < 50; i++)
                {
                    await InputSystem.SendKey(InputSystem.RightKey, "KeyDown");
                    await AloneProcess.SleepAsync(31);
                    await InputSystem.SendKey(InputSystem.RightKey, "KeyUp");
                    await AloneProcess.SleepAsync(31);
                    await InputSystem.SendKey(InputSystem.LeftKey, "KeyDown");
                    await AloneProcess.SleepAsync(31);
                    await InputSystem.SendKey(InputSystem.LeftKey, "KeyUp");
                    await AloneProcess.SleepRandomAsync(31, 50);
                    if (!await ImageSystem.PixelSearch(905, 478, "2E2E2E", 15))
                    {
                        break;
                    }
                }
            }
        }

        public static async Task DetectAngry()
        {
            // Client: 897, 627 (Angrry+)
            // Color: FCFCFC (Red=FC Green=FC Blue=FC)
            if (await ImageSystem.PixelSearch(897, 627, "FCFCFC", 30))
            {
                await InputSystem.SendKey(InputSystem.ItemKey, "KeyDown");
                await InputSystem.SendKey(InputSystem.DashKey, "KeyDown");
                await AloneProcess.SleepRandomAsync(100, 250);
                await InputSystem.SendKey(InputSystem.ItemKey, "KeyUp");
                await InputSystem.SendKey(InputSystem.DashKey, "KeyUp");
            }
        }


    }
}

