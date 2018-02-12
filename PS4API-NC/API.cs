using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCAppInterface;
using PS4Lib;
using System.Windows.Forms;
namespace PS4API_NC
{
    public class API : IAPI
    {
        PS4API PS4;
        public API()
        {
            PS4 = new PS4API();
        }

        //Declarations of all our internal API variables
        string myName = "PS4 API";
        string myDescription = "netCheat API for PS4, you need PS4API server to be injected before you use thisYou can find it here https://github.com/BISOON/ps4-api-server";
        string myAuthor = "BISOON";
        string myVersion = "1.0";
        string myPlatform = "PS4";
        string myContactLink = "http://www.github.com/bisoon";

        //If you want an Icon, use resources to load an image
        //System.Drawing.Image myIcon = Properties.Resources.ico;
        System.Drawing.Image myIcon = Properties.Resources.ps4;

        /// <summary>
        /// Website link to contact info or download (leave "" if no link)
        /// </summary>
        public string ContactLink
        {
            get { return myContactLink; }
        }

        /// <summary>
        /// Name of the API (displayed on title bar of NetCheat)
        /// </summary>
        public string Name
        {
            get { return myName; }
        }

        /// <summary>
        /// Description of the API's purpose
        /// </summary>
        public string Description
        {
            get { return myDescription; }
        }

        /// <summary>
        /// Author(s) of the API
        /// </summary>
        public string Author
        {
            get { return myAuthor; }

        }

        /// <summary>
        /// Current version of the API
        /// </summary>
        public string Version
        {
            get { return myVersion; }
        }

        /// <summary>
        /// Name of platform (abbreviated, i.e. PC, PS3, XBOX, iOS)
        /// </summary>
        public string Platform
        {
            get { return myPlatform; }
        }

        /// <summary>
        /// Returns whether the platform is little endian by default
        /// </summary>
        public bool isPlatformLittleEndian
        {
            get { return false; }
        }

        /// <summary>
        /// Icon displayed along with the other data in the API tab, if null NetCheat icon is displayed
        /// </summary>
        public System.Drawing.Image Icon
        {
            get { return myIcon; }
        }
        /// <summary>
        /// Read bytes from memory of target process.
        /// Returns read bytes into bytes array.
        /// Returns false if failed.
        /// </summary>
        public bool GetBytes(ulong address, ref byte[] bytes)
        {
            /*I did this to ensure that the browser will not crash due the allocation , original  is 65000*/
            int sizeForOperation = 16384;
            int byteslength = bytes.Length;
            List<byte> tmpBuffer = new List<byte>();
            int loop = byteslength / sizeForOperation;
            byte[] operatinalBytes = new byte[sizeForOperation];
            for (int i = 0; i < loop; i++)
            {
                if (!PS4.GetMemory(address, ref operatinalBytes))
                    return false;
                tmpBuffer.AddRange(operatinalBytes);
            }
            bytes = tmpBuffer.ToArray();
            return true;
        }

        /// <summary>
        /// Write bytes to the memory of target process.
        /// </summary>
        public void SetBytes(ulong address, byte[] bytes)
        {
            PS4.SetMemory(address, bytes);
        }

        /// <summary>
        /// Shutdown game or platform
        /// </summary>
        public void Shutdown()
        {
            MessageBox.Show("Not supported yet!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Connects to target.
        /// If platform doesn't require connection, just return true.
        /// IMPORTANT:
        /// Since NetCheat connects and attaches a few times after the user does (Constant write thread, searching, ect)
        /// You must have it automatically use the settings that the user input, instead of asking again
        /// This can be reset on Disconnect()
        /// </summary>
        public bool Connect()
        {
            if (PS4.IsConnected)
                return true;
            string ip = string.Empty;
            using (FrmIP frmIp = new FrmIP())
            {
                frmIp.txtIp.Text = Properties.Settings.Default.ip;
                frmIp.btnConnect.Click += (o, e) => { frmIp.Close(); frmIp.DialogResult = DialogResult.OK; };

                frmIp.ShowDialog().ToString();
                Properties.Settings.Default.ip = frmIp.txtIp.Text;
                Properties.Settings.Default.Save();
                if (frmIp.DialogResult != DialogResult.OK)
                    return false;
                ip = frmIp.txtIp.Text;
            }
            return PS4.ConnectTarget(ip);

        }

        /// <summary>
        /// Disconnects from target.
        /// </summary>
        public void Disconnect()
        {
            PS4.DisconnectTarget();
        }

        /// <summary>
        /// Attaches to target process.
        /// This should automatically continue the process if it is stopped.
        /// IMPORTANT:
        /// Since NetCheat connects and attaches a few times after the user does (Constant write thread, searching, ect)
        /// You must have it automatically use the settings that the user input, instead of asking again
        /// This can be reset on Disconnect()
        /// </summary>
        public bool Attach()
        {
            return PS4.AttachProcess();
        }

        /// <summary>
        /// Pauses the attached process (return false if not available feature)
        /// </summary>
        public bool PauseProcess()
        {
            return false;
        }

        /// <summary>
        /// Continues the attached process (return false if not available feature)
        /// </summary>
        public bool ContinueProcess()
        {
            return PS4.Continue();
        }

        /// <summary>
        /// Tells NetCheat if the process is currently stopped (return false if not available feature)
        /// </summary>
        public bool isProcessStopped()
        {
            return false;
        }

        /// <summary>
        /// Called by user.
        /// Should display options for the API.
        /// Can be used for other things.
        /// </summary>
        public void Configure()
        {

        }

        /// <summary>
        /// Called on initialization
        /// </summary>
        public void Initialize()
        {

        }

        /// <summary>
        /// Called when disposed
        /// </summary>
        public void Dispose()
        {
            //Put any cleanup code in here for when the program is stopped
            PS4.DisconnectTarget();
        }
    }
}
