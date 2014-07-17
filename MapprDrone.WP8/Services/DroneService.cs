using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ARDrone2Client.Common;

namespace MapprDrone.WP8.Services
{
    public class DroneService
    {
        private static DroneService _instance;
        private bool _connecting;
        private CancellationToken _ctoken;
        public static DroneService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DroneService();
                return _instance;
            }
            set { _instance = value; }
        }

        public bool IsDroneConnected { get; set; }

        public DroneClient Client { get; set; }

        public DroneService()
        {
            
        }

        public async Task<bool> Connect()
        {
            if (!_connecting)
            {
                _connecting = true;
                var t = new Task(async () =>
                {
                    if (await DroneClient.Instance.ConnectAsync())
                    {
                        IsDroneConnected = true;
                        if (DroneConnected != null)
                            DroneConnected();
                        
                        Debug.WriteLine("Connected");
                    }
                }, _ctoken, TaskCreationOptions.LongRunning);
                t.Start();
                _connecting = false;
                return IsDroneConnected;
            }
            return false;
        }

        public void Disconnect()
        {
            DroneClient.Instance.Close();
            IsDroneConnected = false;
            if (DroneDisconnected != null)
                DroneDisconnected();
            Debug.WriteLine("Disconnected");
        }

        public event Action DroneConnected;
        public event Action DroneDisconnected;
    }
}
