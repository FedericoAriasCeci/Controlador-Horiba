using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using JYCONFIGBROWSERCOMPONENTLib;
using JYMONOLib;
using JYSYSTEMLIBLib;

namespace HoribaBridge
{
    // One JSON object per line in stdin/stdout.  All JY calls happen on the
    // WinForms STA thread; the stdin thread never touches the COM object.
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BridgeForm());
        }
    }

    internal sealed class BridgeForm : Form
    {
        private readonly JavaScriptSerializer json = new JavaScriptSerializer();
        private readonly object stdoutLock = new object();
        private JYConfigBrowerInterface config;
        private MonochromatorClass mono;
        private string deviceId;
        private string deviceName;
        private string initialization = "not_connected";
        private string initializationError;

        public BridgeForm()
        {
            // A hidden form supplies the Win32 message queue that this SDK's
            // asynchronous COM events require.
            ShowInTaskbar = false;
            WindowState = FormWindowState.Minimized;
            Load += delegate { new Thread(ReadRequests) { IsBackground = true }.Start(); };
            Trace("Bridge started.");
        }

        private void ReadRequests()
        {
            string line;
            while ((line = Console.ReadLine()) != null)
            {
                Dictionary<string, object> request;
                try
                {
                    request = json.Deserialize<Dictionary<string, object>>(line);
                }
                catch (Exception ex)
                {
                    Write(Error("invalid_json", ex.Message));
                    continue;
                }

                try
                {
                    Invoke(new MethodInvoker(delegate { Write(ProcessRequest(request)); }));
                }
                catch (Exception ex)
                {
                    Write(Error("bridge_failure", ex.ToString()));
                }
            }
            BeginInvoke(new MethodInvoker(Close));
        }

        private Dictionary<string, object> ProcessRequest(Dictionary<string, object> request)
        {
            string command = RequireString(request, "command");
            if (command == "connect") return Connect(request);
            if (command == "status") return Status();
            if (command == "get_wavelength") return Ok("wavelength_nm", RequireMono().GetCurrentWavelength());
            if (command == "move_wavelength")
            {
                RequireMono().MovetoWavelength(RequireDouble(request, "wavelength_nm"));
                return Status();
            }
            if (command == "get_turret") return Ok("turret", RequireMono().GetCurrentTurret());
            if (command == "move_turret")
            {
                RequireMono().MovetoTurret(Convert.ToInt32(request["turret"], CultureInfo.InvariantCulture));
                return Status();
            }
            if (command == "get_grating")
            {
                double grating;
                object gratings;
                RequireMono().GetCurrentGrating(out grating, out gratings);
                return Ok("grating", grating);
            }
            if (command == "get_slit")
            {
                SlitLocation location = ParseSlit(RequireString(request, "location"));
                return Ok("width", RequireMono().GetCurrentSlitWidth(location));
            }
            if (command == "move_slit")
            {
                RequireMono().MovetoSlitWidth(ParseSlit(RequireString(request, "location")), RequireDouble(request, "width"));
                return Status();
            }
            if (command == "quit")
            {
                BeginInvoke(new MethodInvoker(Close));
                return Ok("closing", true);
            }
            return Error("unknown_command", command);
        }

        private Dictionary<string, object> Connect(Dictionary<string, object> request)
        {
            if (mono != null) return Status();
            try
            {
                Trace("Creating ConfigBrowser.");
                config = new JYConfigBrowerInterface();
                Trace("Calling ConfigBrowser.Load.");
                config.Load();
                Trace("Calling ConfigBrowser.GetFirstMono.");
                deviceId = config.GetFirstMono(out deviceName);
                string requestedId = request.ContainsKey("device_id") ? Convert.ToString(request["device_id"]) : null;
                while (!String.IsNullOrEmpty(deviceName) && !String.IsNullOrEmpty(requestedId) && deviceId != requestedId)
                    deviceId = config.GetNextMono(out deviceName);
                if (String.IsNullOrEmpty(deviceId) || String.IsNullOrEmpty(deviceName))
                    return Error("device_not_found", requestedId ?? "No monochromator is configured.");

                Trace("Creating MonochromatorClass for " + deviceId + ".");
                mono = new MonochromatorClass();
                Trace("MonochromatorClass created.");
                mono.Uniqueid = deviceId;
                mono._IJYDeviceReqdEvents_Event_Initialize += OnInitialize;
                Trace("Calling mono.Load.");
                mono.Load();
                Trace("Calling mono.OpenCommunications.");
                mono.OpenCommunications();
                initialization = "initializing";
                Trace("Calling mono.Initialize.");
                mono.Initialize(false, false, false);
                return Status();
            }
            catch (Exception ex)
            {
                Trace("Managed exception: " + ex);
                initialization = "failed";
                initializationError = ex.ToString();
                return Status();
            }
        }

        private void OnInitialize(int status, IJYEventInfo eventInfo)
        {
            Trace("Initialize event received: " + status + ".");
            initialization = status == 0 ? "ready" : "failed";
            if (status != 0) initializationError = "SDK Initialize event returned status " + status + ".";
        }

        private Dictionary<string, object> Status()
        {
            Dictionary<string, object> answer = Ok("state", initialization);
            answer["device_id"] = deviceId;
            answer["device_name"] = deviceName;
            answer["busy"] = mono != null && mono.IsBusy();
            if (initializationError != null) answer["error"] = initializationError;
            return answer;
        }

        private MonochromatorClass RequireMono()
        {
            if (mono == null || initialization != "ready") throw new InvalidOperationException("The monochromator is not ready; call connect and wait for state=ready.");
            return mono;
        }

        private static SlitLocation ParseSlit(string value)
        {
            return (SlitLocation)Enum.Parse(typeof(SlitLocation), value, true);
        }

        private static string RequireString(Dictionary<string, object> request, string name)
        {
            if (!request.ContainsKey(name)) throw new ArgumentException("Missing '" + name + "'.");
            return Convert.ToString(request[name], CultureInfo.InvariantCulture);
        }

        private static double RequireDouble(Dictionary<string, object> request, string name)
        {
            if (!request.ContainsKey(name)) throw new ArgumentException("Missing '" + name + "'.");
            return Convert.ToDouble(request[name], CultureInfo.InvariantCulture);
        }

        private static Dictionary<string, object> Ok(string name, object value)
        {
            Dictionary<string, object> answer = new Dictionary<string, object>();
            answer["ok"] = true;
            answer[name] = value;
            return answer;
        }

        private static Dictionary<string, object> Error(string code, string message)
        {
            Dictionary<string, object> answer = new Dictionary<string, object>();
            answer["ok"] = false;
            answer["code"] = code;
            answer["error"] = message;
            return answer;
        }

        private void Write(Dictionary<string, object> answer)
        {
            lock (stdoutLock) { Console.WriteLine(json.Serialize(answer)); Console.Out.Flush(); }
        }

        private static void Trace(string message)
        {
            try
            {
                System.IO.File.AppendAllText(
                    System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HoribaBridge.log"),
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff ") + message + Environment.NewLine);
            }
            catch { }
        }
    }
}
