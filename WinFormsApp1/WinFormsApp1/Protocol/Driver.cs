using System;
using System.Drawing;
using VXL;
using static VXL.Wrapper;

namespace WinFormsApp1
{
    public class Driver : DriverListener, WrapperListener
    {
        DriverListener listener = null;

        Wrapper wrapper = null;
        public Driver(DriverListener _listener = null)
        {
            listener = _listener;
            wrapper = new Wrapper(this);
        }
        public bool Init(string appName,string protocol,string hardwareType,uint appchannel)
        {
            return wrapper.Init(appName, protocol, hardwareType, appchannel);
        }
        public bool Release()
        {
            return wrapper.Release();
        }
        public bool StartCommunication(uint baudrate = 500000)
        {
            return wrapper.StartCommunication(baudrate);
        }
        public bool StopCommunication()
        {
            return wrapper.StopCommunication();
        }
        public bool Transmit(uint id,ushort dlc, byte[] data)
        {
            return wrapper.Transmit(id,dlc,data);
        }
        public void WrapperNotifyTransmitted(ulong timeStamp, uint id, ushort dlc, byte[] data)
        {
            if(listener != null)
            {
                listener.DriverNotifyTransmitted(timeStamp, id, dlc, data);
            }
        }

        public void WrapperNotifyReceived(ulong timeStamp, uint id, ushort dlc, byte[] data)
        {
                listener.DriverNotifyReceived(timeStamp, id, dlc, data);
            
        }

        public void DriverNotifyTransmitted(ulong timeStamp, uint id, ushort dlc, byte[] data)
        {

        }
        public void DriverNotifyReceived(ulong timeStamp, uint id, ushort dlc, byte[] data)
        {
            //Task.Run(() => BusMngr.Instance.OnReceivedMessage(timeStamp, id, dlc, data));
            //Task.Run(() => DatabaseMngr.Instance.OnReceivedMessage(timeStamp, id, dlc, data));
        }
    }
    public interface DriverListener
    {
        void DriverNotifyTransmitted(ulong timeStamp, uint id, ushort dlc, byte[] data);

        void DriverNotifyReceived(ulong timeStamp, uint id,ushort dlc, byte[] data);


    }
}
