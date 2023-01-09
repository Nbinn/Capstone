using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    class BusMngr : BaseMngr, DriverListener
    {
        

        private static BusMngr instance = null;
        public static BusMngr Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BusMngr();
                }

                return instance;
            }
        }
        private Dictionary<string, TxTimer> txTimers = null;
        private Driver driver = null;
        private BusMngr()
        {
            txTimers = new Dictionary<string, TxTimer>();
        }

        public enum BUS_STATUS { UNKNOWN = 0, INITIALIZED, STARTED, STOPPED, RELEASED };

        private BUS_STATUS busStatus = BUS_STATUS.UNKNOWN;
        public BUS_STATUS BusStatus
        {
            get { return busStatus; }
        }

        public override bool Init()
        {
            driver = new Driver(this);
            if (driver.Init("DemoTransmit", "CAN", "Virtual", 0) == false)
            {
                lastError = "Initialize driver failed";
                return false;
            }
            InitTimers();

            isLoaded = true;

            busStatus = BUS_STATUS.INITIALIZED;

            return true;
        }

        public override bool Release()
        {
            ReleaseTimers();

            if (driver != null)
            {
                if (driver.Release() == false)
                {
                    lastError = "Release driver failed";
                    return false;
                }
            }

            isLoaded = false;

            busStatus = BUS_STATUS.RELEASED;

            
            return true;
        }

        public bool StartCommunication()
        {
            if (isLoaded == false)
            {
                return false;
            }

            if (busStatus == BUS_STATUS.INITIALIZED || busStatus == BUS_STATUS.STOPPED)
            {
                
                if (driver.StartCommunication(500000) == false)
                {
                    return false;
                }

                StartTimers();

                busStatus = BUS_STATUS.STARTED;

            }


            return true;
        }

        public bool StopCommunication()
        {
            if (isLoaded == false)
            {

                return false;
            }

            if (busStatus == BUS_STATUS.STARTED)
            {

                StopTimers();

                if (driver.StopCommunication() == false)
                {
                    lastError = "Stop communication failed";
                    
                    return false;
                }

                busStatus = BUS_STATUS.STOPPED;

            }


            return true;
        }

        public bool TransmitData(string id, string hexData)
        {
            int dlc = hexData.Length / 2;

            List<byte> data = new List<byte>();
            for (int i = 0; i < dlc; i++)
            {
                data.Add(Convert.ToByte(hexData.Substring(i * 2, 2), 16));
            }

            Message msg = DatabaseMngr.Instance.GetMessage(id);
            if (msg != null)
            {

                return TransmitData(id);
            }
            else
            {
                return TransmitData(id, dlc, data);
            }
        }

        private bool TransmitData(string id)
        {
            
            Message msg = DatabaseMngr.Instance.GetMessage(id);
            if (msg == null)
            {
                lastError = "Message not found. ID: Ox" + Convert.ToInt64(id).ToString("X");
                
                return false;
            }

            return TransmitData(msg.ID, msg.DLC, msg.GetData());
        }

        private bool TransmitData(string id, int dlc, List<byte> data)
        {
            if (driver.Transmit(Convert.ToUInt32(id), (ushort)dlc, data.ToArray()) == false)
            {
                lastError = "Transmit data failed. ID: 0x" + Convert.ToInt64(id).ToString("X");
                return false;
            }

            return true;
        }


        private void InitTimers()
        {
            ReleaseTimers();

            Dictionary<string, Message> messages = DatabaseMngr.Instance.GetMessages();

            if (messages != null)
            {
                Message msg = null;

                foreach (KeyValuePair<string, Message> pair in messages)
                {
                    msg = pair.Value;

                    if ( msg.CycleTime > 0)
                    {
                        TxTimer timer = new TxTimer();
                        timer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimedEventTransmitData);
                        timer.Interval = msg.CycleTime;
                        timer.ID = msg.ID;

                        txTimers.Add(timer.ID, timer);
                    }
                }
            }
        }

        private void ReleaseTimers()
        {
            StopTimers();

            foreach (KeyValuePair<string, TxTimer> pair in txTimers)
            {
                pair.Value.Close();
            }

            txTimers.Clear();
        }

        private void StartTimers()
        {
            foreach (KeyValuePair<string, TxTimer> pair in txTimers)
            {
                pair.Value.Start();
            }
        }

        private void StopTimers()
        {
            foreach (KeyValuePair<string, TxTimer> pair in txTimers)
            {
                pair.Value.Stop();
            }
        }
        private void OnTimedEventTransmitData(object sender, System.Timers.ElapsedEventArgs e)
        {
            TransmitData(((TxTimer)sender).ID);
        }
        public void OnReceivedMessage(ulong timeStamp, uint id, ushort dlc, byte[] data)
        {
            Task.Run(() => driver.Transmit(id, dlc, data));
            Task.Run(() => DatabaseMngr.Instance.OnReceivedMessage(timeStamp, id, dlc, data));
            //Task.Run(() => DemoTransmit.Up)
        }


        public void DriverNotifyTransmitted(ulong timeStamp, uint id, ushort dlc, byte[] data) { }

        public void DriverNotifyReceived(ulong timeStamp, uint id, ushort dlc, byte[] data) { }
    }
}
