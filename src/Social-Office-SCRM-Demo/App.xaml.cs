// Copyright (c) 2019 Javier Cañon 
// https://www.javiercanon.com 
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
// IN THE SOFTWARE.
using log4net;
using Microsoft.Win32;
using SODemo.Views.Browsers;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace SODemo
{


    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static ILog ilog;

        // PUBLIC VARS        

        // global vars
        private static Process _ApplicationProcessIISExpress;
        private static Process _ApplicationProcessSCRM;


        public static class Development
        {
            public static class Settings
            {
                public static bool Is_InDevMode;
            }

        }

        public static class SO
        {
            public static class User
            {
                public static string GUID;
                public static string Email;
                public static string Alias;

            }

        }


        public static class Configuration
        {
            public static class Webserver
            {

                public static string Webserver_Development;
                public static string Webserver_Production;
            }

            public static class Logger
            {

                public static bool Logger_LogDebugInfo;

            }

        }

        public App()
        {
            // Subscribe the Startup event 
            this.Startup += Application_Startup;
            Exit += Application_OnExit;

        }

        private void Application_OnExit(object sender, ExitEventArgs e)
        {
            StopIISExpress();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Create a new object, representing the German culture.  
            CultureInfo culture = CultureInfo.CreateSpecificCulture("es-MX");

            // The following line provides localization for the application's user interface.  
            Thread.CurrentThread.CurrentUICulture = culture;

            // The following line provides localization for data formats.  
            Thread.CurrentThread.CurrentCulture = culture;

            // Set this culture as the default culture for all threads in this application.  
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            SetAppSettings();
            InitiateLogging();


            #region IE 11
            try
            {
                SetIEVersion();

            }
            catch (Exception ex)
            {
                //throw;
                MessageBox.Show("Error, puede continuar pero es posible que se presente fallos, este aplicativo requiere permisos de administrador." + Environment.NewLine + ex.Message, "Error indicando version IE");
            }
            #endregion IE 11

            // show main window
            // NewWindow();

            // start IIS 
            StartIISLocal();


            LogInfo("App started.");
        }

        #region Comun

        static void InitiateLogging()
        {
            log4net.Config.XmlConfigurator.Configure();
            ilog = LogManager.GetLogger("loggerConsole");
            LogInfo("Starting logger...");
        }

        static void PrintHelp()
        {
            // System.Console.WriteLine( "Usage:" );
            // System.Console.WriteLine( "Subir Planos: -catplano=< Mercadeo = 1,Novedades = 2,Presupuestos = 3> -file=<ruta y nombre archivo texto csv> -email=<aviso@correo.com> -empresa=<nombre empresa creada en AV>" );
            // System.Console.WriteLine( "Mover y Procesar Planos Ventas: -moverplanos=1 -empresa=<nombre empresa creada en AV>" );
            // System.Console.WriteLine( "Cargar Pagina Web: -webrequest=http://localhost/mipagina.html -software=<nombre software>" );
        }

        public static void LogError(string msg)
        {
            // System.Console.WriteLine("[Browser Chrome] Error: " + msg);
            ilog.Error(msg);
        }

        public static void LogInfo(string msg)
        {
            // System.Console.WriteLine("[Browser Chrome] Info: " + msg);
            ilog.Info(msg);
        }

        public static void LogDebug(string msg)
        {
            // System.Console.WriteLine("[Browser Chrome] Debug: " + msg);
            if (Configuration.Logger.Logger_LogDebugInfo)
            {
                ilog.Debug(msg);
            }
        }

        #endregion Comun

        #region Metodos

        public static bool StartIISLocal()
        {
            bool ok = true;

            // release:
            string websitepath = AppDomain.CurrentDomain.BaseDirectory + @"Website\";

            // check process exit with error
            if (_ApplicationProcessIISExpress != null)
            {
                if (_ApplicationProcessIISExpress.HasExited)
                {
                    _ApplicationProcessIISExpress = null;
                }
            }

#if DEBUG
            //X:\PROYECTO_GESTASOCIAL_CRM\PROYECTOS\Softcanon.SocialOffice.CRM\Softcanon.SocialOffice.Web\
            websitepath = @"X:\PROYECTOS_SOCIAL_OFFICE\PROJECTS\Social-Office-SCRM-Demos\src\SO.Web";

            if (_ApplicationProcessIISExpress == null) ok = startIISExpress64bit(websitepath, 1975, true);
#else
            if (_ApplicationProcessIISExpress == null)
            {
                ok = startIISExpress64bit(websitepath, 1975, true);
            }
#endif
            if (!ok) {
                LogInfo("Cannot start IIS Express...");
            }


            return ok;
        }


        public static bool StartAppSCRM()
        {
            bool ok = true;

            if (_ApplicationProcessSCRM != null)
            {
                if (_ApplicationProcessSCRM.HasExited)
                {
                    _ApplicationProcessSCRM = null;
                }
            }

            if (_ApplicationProcessSCRM == null)
            {
                ProcessStartInfo info = new ProcessStartInfo()
                {
                    FileName = AppDomain.CurrentDomain.BaseDirectory + "SO.Desktop.exe",
                    //WindowStyle = ProcessWindowStyle.Maximized,
                    //Arguments = arguments,
                    //RedirectStandardOutput = true,
                    //UseShellExecute = false,
                    //CreateNoWindow = false
                };
                //bool started;
                _ApplicationProcessSCRM = new Process
                {
                    StartInfo = info
                };
                _ApplicationProcessSCRM.Exited += _GestasocialProcess_Exited;
                try
                {
                    ok = _ApplicationProcessSCRM.Start();
                }

                catch (Exception)
                {
                    //throw;
                    return false;
                }
            }


            return ok;
        }

        private static void _GestasocialProcess_Exited(object sender, EventArgs e)
        {
            _ApplicationProcessSCRM = null;
        }

        public static void StopApplication()
        {
            if (_ApplicationProcessSCRM != null)
            {
                if (!_ApplicationProcessSCRM.HasExited)
                {
                    _ApplicationProcessSCRM.Kill();
                }

                _ApplicationProcessSCRM.WaitForExit();
                _ApplicationProcessSCRM = null;
            }

        }


        public static void NewBrowserWindowIE(string URL)
        {

            if (!Uri.IsWellFormedUriString(URL, UriKind.Absolute))
            {
                return;
            }


            // https://stackoverflow.com/questions/1111369/how-do-i-create-and-show-wpf-windows-on-separate-threads
            // http://reedcopsey.com/2011/11/28/launching-a-wpf-window-in-a-separate-thread-part-1/

            // Create a thread
            Thread newWindowThread = new Thread(new ThreadStart(() =>
            {
                // Create our context, and install it:
                SynchronizationContext.SetSynchronizationContext(
                    new DispatcherSynchronizationContext(
                        Dispatcher.CurrentDispatcher));

                RibbonWindowBrowserIE tempWindow = new RibbonWindowBrowserIE(URL);

                // When the window closes, shut down the dispatcher
                tempWindow.Closed += (s, e) =>
                   Dispatcher.CurrentDispatcher.BeginInvokeShutdown(DispatcherPriority.Background);

                tempWindow.Show();
                // Start the Dispatcher Processing
                System.Windows.Threading.Dispatcher.Run();
            }));

            // Setup and start thread as before
            newWindowThread.SetApartmentState(ApartmentState.STA);
            newWindowThread.IsBackground = true;
            newWindowThread.Start();
        }




        static void SetAppSettings()
        {

#if DEBUG
            App.Development.Settings.Is_InDevMode = true;
#else
            App.Development.Settings.Is_InDevMode = false;
#endif

            Configuration.Webserver.Webserver_Development = ConfigurationManager.AppSettings["Webserver.Development"];
            Configuration.Webserver.Webserver_Production = ConfigurationManager.AppSettings["Webserver.Production"];


            //Configuration.Logger.Logger_LogDebugInfo = Convert.ToBoolean(ConfigurationManager.AppSettings["Logger.LogDebugInfo"]);

        }


        static void SetIEVersion()
        {
            // https://docs.microsoft.com/en-us/previous-versions/windows/internet-explorer/ie-developer/general-info/ee330730(v=vs.85)
            // https://dennymichael.net/2015/06/22/web-browser-control-specifying-the-ie-version/
            // https://blogs.msdn.microsoft.com/patricka/2015/01/12/controlling-webbrowser-control-compatibility/
            // fallback is set in header of webpage: <meta http-equiv="X-UA-Compatible" content="IE=edge">

            int RegVal;
            RegistryKey Regkey = null;

            RegVal = 11001;
            LogDebug("Set IE version to " + RegVal);

            // set the actual key
            Regkey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", true);
            Regkey.SetValue(System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe", RegVal, RegistryValueKind.DWord);
            Regkey.Close();
        }

        #endregion Metodos



        #region IIS

        static bool startIISExpress64bit(string websitepath, int port, bool bDebug)
        {
            /*
            
            Problem to startup:
            if websitepath have spaces?
            if use /path:"websitepath" dont working, but if have spaces?...
            */

            if (!Directory.Exists(websitepath))
            {
                throw new DirectoryNotFoundException("Path to Website is invalid: '" + websitepath + "'.");
            }

            string arguments = string.Format(
           CultureInfo.InvariantCulture, "/path:{0} /port:{1}", websitepath, port);

            /*
            ProcessStartInfo info = new ProcessStartInfo()
            {
                FileName = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\IIS Express\\iisexpress.exe",
                WindowStyle = bDebug ? ProcessWindowStyle.Normal : ProcessWindowStyle.Hidden,
                Arguments = arguments,
                RedirectStandardOutput = bDebug ? true : false,
                UseShellExecute = false,
                CreateNoWindow = bDebug ? true : false
            };
            */

            // Process.Start("c:\\program files\\iis express\\iisexpress.exe", "/path:" + websitepath + " /port:" + port);

            string exe = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\IIS Express\\iisexpress.exe";
            if (File.Exists(exe))
                LogInfo("IIS Express at " + exe);
            else
            {
                LogError("IIS Express not found at " + exe);
                return false;

            }

            ProcessStartInfo info = new ProcessStartInfo()
            {
                FileName = "\"" + exe + "\"",
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                ErrorDialog = true,
                LoadUserProfile = true,
                CreateNoWindow = true,
                UseShellExecute = false,
        };


            /*
            try
            {
                bool started = false;
                _ApplicationProcessIISExpress = new Process
                {
                    StartInfo = info
                };

                LogInfo("starting IIS Express:" + arguments);

                started = _ApplicationProcessIISExpress.Start();

                _ApplicationProcessIISExpress.WaitForExit(5000);
                return started;
            }

            catch (Exception err)
            {
                LogError("Cant start IIS Express. " + err.Message);
                MessageBox.Show("Error, cant start webserver");
                return false;
                //throw;
            }
            */

            var startThread = new Thread(() => StartIisExpress(info))
            {
                IsBackground = true
            };

            startThread.Start();

            return true; //TODO: get value from thread.
        }

        private static void StartIisExpress(ProcessStartInfo info)
        {
            // https://stackoverflow.com/questions/4291912/process-start-how-to-get-the-output/4291965
            try
            {
                //_ApplicationProcessIISExpress.EnableRaisingEvents = true;
                //_ApplicationProcessIISExpress.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(process_OutputDataReceived);
                //_ApplicationProcessIISExpress.ErrorDataReceived += new System.Diagnostics.DataReceivedEventHandler(process_ErrorDataReceived);

                _ApplicationProcessIISExpress = Process.Start(info);

                _ApplicationProcessIISExpress.BeginErrorReadLine();
                _ApplicationProcessIISExpress.BeginOutputReadLine();

                _ApplicationProcessIISExpress.WaitForExit();

            }
            catch (Exception err)
            {
                /*
                _ApplicationProcessIISExpress.Dispose();

                if (_ApplicationProcessIISExpress.HasExited == false)
                {
                    _ApplicationProcessIISExpress.Kill();
                }
                */
                LogError("Cant start IIS Express. " + err.Message);
                MessageBox.Show("Error, cant start webserver");

            }
        }


        static bool startIISExpress64bitConfigFile(bool bDebug)
        {
            /*
                         IIS Express Usage:
            ------------------
            iisexpress [/config:config-file] [/site:site-name] [/siteid:site-id] [/systray:boolean] 

            iisexpress /path:app-path [/port:port-number] [/clr:clr-version] [/systray:boolean] 

            /config:config-file 
            The full path to the applicationhost.config file. The default value is the IISExpress8\config\applicationhost.config file that is located in the user's Documents folder.

            /site:site-name 
            The name of the site to launch, as described in the applicationhost.config file. 

            /siteid:site-id 
            The ID of the site to launch, as described in the applicationhost.config file.

            /path:app-path 
            The full physical path of the application to run. You cannot combine this option with the /config and related options. 

            /port:port-number 
            The port to which the application will bind. The default value is 8080. You must also specify the /path option. 

            /clr:clr-version The .NET Framework version (e.g. v2.0) to use to run the application. The default value is v4.0. You must also specify the /path option. 

            /systray:boolean 
            Enables or disables the system tray application. The default value is true. 

            /trace:debug-trace-level 
            Valid values are info or i,warning or w,error or e. 

            Examples: 
            iisexpress /site:WebSite1 
            This command runs WebSite1 site from the user profile configuration file.

            iisexpress /config:c:\myconfig\applicationhost.config 
            This command runs the first site in the specified configuration file. 

            iisexpress /path:c:\myapp\ /port:80 
            This command runs the site from c:\myapp folder over port 80.
            */

            string arguments = string.Empty;


            // string websitepath = AppDomain.CurrentDomain.BaseDirectory + "web";
            string config = AppDomain.CurrentDomain.BaseDirectory + "applicationhost.config";

            if (string.IsNullOrEmpty(config) || !File.Exists(config))
            {
                throw new FileNotFoundException("Path to IIS Express configuration file is invalid: '" + config + "'.");
            }

            //todo: settings can change location or autodetect of IISExpress.
            //todo: can set port in settings

            arguments = "/config:\\" + config + "\"";


            ProcessStartInfo info = new ProcessStartInfo()
            {
                FileName = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\IIS Express\\iisexpress.exe",
                WindowStyle = bDebug ? ProcessWindowStyle.Normal : ProcessWindowStyle.Hidden,
                Arguments = arguments,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = bDebug ? false : true
            };

            //Process.Start("c:\\program files\\iis express\\iisexpress.exe", "/path:" + path + " /port:" + port);

            bool started = false;
            _ApplicationProcessIISExpress = new Process
            {
                StartInfo = info
            };
            _ApplicationProcessIISExpress.Exited += _IISExpressProcess_Exited;
            try
            {
                started = _ApplicationProcessIISExpress.Start();
                return started;
            }
            catch (Exception)
            {
                return false;
                //throw;
            }



        }

        private static void _IISExpressProcess_Exited(object sender, EventArgs e)
        {
            _ApplicationProcessIISExpress = null;
        }

        /// <summary>
        /// Stops the IIS Express instance that was formerly started via the <see cref="Start"/> method.
        /// </summary>
        public static void StopIISExpress()
        {
            if (_ApplicationProcessIISExpress != null)
            {
                if (!_ApplicationProcessIISExpress.HasExited)
                {
                    _ApplicationProcessIISExpress.Kill();
                }

                //_ApplicationProcessIISExpress.WaitForExit();
                //_ApplicationProcessIISExpress = null;
            }
        }


        #endregion IIS


    } // end app class



}
