using System;
using System.Drawing;
using System.Threading;
using Microsoft.Win32.SafeHandles;
using vxlapi_NET;

namespace VXL
{
    public class Wrapper
    {

        private WrapperListener listener = null;

        private Color COLOR_INFO = Color.White;
        private Color COLOR_ERROR = Color.Red;
        private Color COLOR_SUCCESS = Color.Green;
        private Color COLOR_DATA = Color.AliceBlue;
        private string appName = string.Empty;
        private string protocol = string.Empty;

        private XLDriver driver = null;
        private XLClass.xl_driver_config driverConfig = null;

        private XLDefine.XL_BusTypes busType = XLDefine.XL_BusTypes.XL_BUS_TYPE_NONE;
        private XLDefine.XL_BusCapabilities busCap = XLDefine.XL_BusCapabilities.XL_BUS_ACTIVE_CAP_CAN;
        private XLDefine.XL_InterfaceVersion interfaceVersion = XLDefine.XL_InterfaceVersion.XL_INTERFACE_VERSION;
        private XLDefine.XL_HardwareType hwType = XLDefine.XL_HardwareType.XL_HWTYPE_NONE;

        private byte hwIndex = 0;

        private int portHandle = -1;
        private ulong accessMask = 0;
        private ulong permissionMask = 0;
        private uint rxQueueSize = 1024;

        private enum THREADSTATUS { INIT = 0, RUNNING, STOPPED, EXIT };
        private THREADSTATUS threadStatus = THREADSTATUS.STOPPED;
        private Thread thread = null;
        private EventWaitHandle xlEventWaitHandle = null;

        public Wrapper(WrapperListener _listener = null)
        {
            listener = _listener;
        }

        public bool Init(string _appname, string _protocol, string hardwareType, uint appChannel)
        {
            appName = _appname;
            protocol = _protocol;

            switch (protocol)
            {
                case "CAN":
                    busType = XLDefine.XL_BusTypes.XL_BUS_TYPE_CAN;
                    busCap = XLDefine.XL_BusCapabilities.XL_BUS_ACTIVE_CAP_CAN;
                    interfaceVersion = XLDefine.XL_InterfaceVersion.XL_INTERFACE_VERSION;
                    break;
                default:
                    return false;
            }

            switch (hardwareType)
            {
                case "Virtual":
                    hwType = XLDefine.XL_HardwareType.XL_HWTYPE_VIRTUAL;
                    break;
                case "VN1610":
                    hwType = XLDefine.XL_HardwareType.XL_HWTYPE_VN1610;
                    break;
                case "VN1630A":
                    hwType = XLDefine.XL_HardwareType.XL_HWTYPE_VN1630;
                    break;
                case "CANcaseXL":
                    hwType = XLDefine.XL_HardwareType.XL_HWTYPE_CANCASEXL;
                    break;
                default:
                    return false;
            }
            if (OpenDriver() == false)
            {
                return true;
            }
            if (GetDriverConfig() == false)
            {
                return false;
            }
            /*if (GetAppConfig(appChannel) == false)
            {
                return false;
            }
            if (OpenPort() == false)
            {
                return false;
            }
            if (InitThread() == false)
            {
                return false;
            }*/

            return true;
        }
        public bool Release()
        {
            if (driver == null)
            {
                return true;
            }
            if (StopCommunication() == false)
            {
                return false;
            }

            if (ReLeaseThread() == false)
            {
                return false;
            }

            if (ClosePort() == false)
            {
                return false;
            }

            if (CloseDriver() == false)
            {
                return false;
            }


            return true;
        }
        public bool StartCommunication(uint baudrate = 500000)
        {
            try
            {
                if (driver != null)
                {
                    XLDefine.XL_Status status = driver.XL_ActivateChannel(portHandle, accessMask, busType, XLDefine.XL_AC_Flags.XL_ACTIVATE_NONE);
                    if (status != XLDefine.XL_Status.XL_SUCCESS)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            threadStatus = THREADSTATUS.RUNNING;
            return true;
        }
        public bool StopCommunication()
        {
            threadStatus = THREADSTATUS.STOPPED;
            try
            {
                if (driver != null)
                {
                    XLDefine.XL_Status status = driver.XL_DeactivateChannel(portHandle, accessMask);
                    if (status != XLDefine.XL_Status.XL_SUCCESS)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            return true;
        }
        public bool Transmit(uint id, ushort dlc, byte[] data)
        {
            XLDefine.XL_Status status = XLDefine.XL_Status.XL_SUCCESS;
            XLClass.xl_event xlEvent = new XLClass.xl_event();

            if (driver == null)
            {
                driver = new XLDriver();
            }
            status = driver.XL_OpenDriver();
            if (status != XLDefine.XL_Status.XL_SUCCESS)
            {
                return false;
            }

            try
            {
                switch (protocol)
                {
                    case "CAN":
                        xlEvent.tagData.can_Msg.id = id;
                        xlEvent.tagData.can_Msg.dlc = dlc;
                        xlEvent.tagData.can_Msg.data = data;
                        xlEvent.tag = XLDefine.XL_EventTags.XL_TRANSMIT_MSG;

                        status = driver.XL_CanTransmit(portHandle, accessMask, xlEvent);
                        if (status != XLDefine.XL_Status.XL_SUCCESS)
                        {
                            return false;
                        }
                        break;
                    default:
                        return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return true;
        }
        private bool OpenDriver()
        {
            XLDefine.XL_Status status = XLDefine.XL_Status.XL_SUCCESS;
            try
            {
                if (driver == null)
                {
                    driver = new XLDriver();
                }
                status = driver.XL_OpenDriver();
                if (status != XLDefine.XL_Status.XL_SUCCESS)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;

            }
            return true;
        }
        private bool CloseDriver()
        {
            XLDefine.XL_Status status = XLDefine.XL_Status.XL_SUCCESS;
            try
            {
                if (driver != null)
                {
                    status = driver.XL_CloseDriver();
                    if (status != XLDefine.XL_Status.XL_SUCCESS)
                    {
                        return false;
                    }
                    driver = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            return true;
        }
        private bool GetDriverConfig()
        {
            XLDefine.XL_Status status = XLDefine.XL_Status.XL_SUCCESS;
            try
            {
                if (driverConfig == null)
                {
                    driverConfig = new XLClass.xl_driver_config();
                }

                status = driver.XL_GetDriverConfig(ref driverConfig);
                if (status != XLDefine.XL_Status.XL_SUCCESS)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            bool hwTypeFound = false;
            for (int i = 0; i < driverConfig.channelCount; i++)
            {
                if (hwType == driverConfig.channel[i].hwType)
                {
                    hwIndex = driverConfig.channel[i].hwIndex;
                    hwTypeFound = true;
                }

            }
            if (!hwTypeFound)
            {
                return false;
            }

            return true;

        }
        private bool GetAppConfig(uint appChannel)
        {
            XLDefine.XL_Status status = XLDefine.XL_Status.XL_SUCCESS;
            try
            {
                if (driver == null)
                {
                    Console.WriteLine("driver is null");
                }
                XLDefine.XL_HardwareType refHWType = XLDefine.XL_HardwareType.XL_HWTYPE_NONE;
                uint refHWIndex = 0;
                uint refHWChannel = 0;
                status = driver.XL_GetApplConfig(appName, appChannel, ref refHWType, ref refHWIndex, ref refHWChannel, busType);

                if (status != XLDefine.XL_Status.XL_SUCCESS || hwType != refHWType || hwIndex != refHWIndex || appChannel != refHWChannel)
                {
                    status = driver.XL_SetApplConfig(appName, appChannel, hwType, hwIndex, appChannel, busType);
                    if (status != XLDefine.XL_Status.XL_SUCCESS)
                    {
                        Console.WriteLine("Set app config failed. Status: " + status.ToString());
                    }
                    else
                    {
                        Console.WriteLine("Set app config successfully");
                    }

                    status = driver.XL_GetApplConfig(appName, appChannel, ref refHWType, ref refHWIndex, ref refHWChannel, busType);
                    if (status != XLDefine.XL_Status.XL_SUCCESS || hwType != refHWType || hwIndex != refHWIndex || appChannel != refHWChannel)
                    {
                        Console.WriteLine("Get app config failed. Status: " + status.ToString());
                    }
                    else
                    {
                        Console.WriteLine("Connecting to: " + "hwType " + refHWType.ToString() + ", hwIndex " + refHWIndex.ToString() + ", hwChannel " + refHWChannel.ToString());
                    }
                }
                else
                {
                    Console.WriteLine("Connecting to: " + "hwType " + refHWType.ToString() + ", hwIndex " + refHWIndex.ToString() + ", hwChannel " + refHWChannel.ToString());
                }

                int channelIndex = driver.XL_GetChannelIndex(hwType, (int)hwIndex, (int)appChannel);
                if (channelIndex < 0 || channelIndex >= driverConfig.channelCount)
                {
                    Console.WriteLine("Channel index is out of range: channelIndex = " + channelIndex.ToString());
                    return false;
                }

                if ((driverConfig.channel[channelIndex].channelBusCapabilities & busCap) == 0)
                {
                    Console.WriteLine("Bus capability is not active");
                    return false;
                }

                accessMask = driver.XL_GetChannelMask(hwType, (int)hwIndex, (int)appChannel);
                permissionMask = accessMask;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            return true;
        }
        private bool OpenPort()
        {
            XLDefine.XL_Status status = XLDefine.XL_Status.XL_SUCCESS;

            try
            {
                if (driver != null)
                {
                    status = driver.XL_OpenPort(ref portHandle, appName, accessMask, ref permissionMask, rxQueueSize, interfaceVersion, busType);
                    if (status != XLDefine.XL_Status.XL_SUCCESS)
                    {
                        return false;
                    }
                }
                switch (protocol)
                {
                    case "CAN":
                        status = driver.XL_CanRequestChipState(portHandle, accessMask);
                        if (status != XLDefine.XL_Status.XL_SUCCESS)
                        {
                            return false;
                        }
                        break;
                    default:
                        return false;
                }
                status = driver.XL_ResetClock(portHandle);
                if (status != XLDefine.XL_Status.XL_SUCCESS)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            return true;
        }
        private bool ClosePort()
        {
            XLDefine.XL_Status status = XLDefine.XL_Status.XL_SUCCESS;
            try
            {
                if (driver != null)
                {
                    status = driver.XL_ClosePort(portHandle);
                    if (status != XLDefine.XL_Status.XL_SUCCESS)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            return true;
        }
        private bool InitThread()
        {
            int temp = 1;
            XLDefine.XL_Status status = driver.XL_SetNotification(portHandle, ref temp, 1);
            if (status != XLDefine.XL_Status.XL_SUCCESS)
            {
                return false;
            }
            xlEventWaitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
            xlEventWaitHandle.SafeWaitHandle = new SafeWaitHandle(new IntPtr(temp), true);
            try
            {
                threadStatus = THREADSTATUS.INIT;
                thread = new Thread(new ThreadStart(RXThread));
                thread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return true;
        }
        private bool ReLeaseThread()
        {
            XLDefine.XL_Status status = XLDefine.XL_Status.XL_SUCCESS;
            if (xlEventWaitHandle != null && xlEventWaitHandle.SafeWaitHandle != null)
            {
                xlEventWaitHandle.SafeWaitHandle.SetHandleAsInvalid();
            }
            if (thread != null)
            {
                threadStatus = THREADSTATUS.EXIT;
                thread.Abort();
                thread.Join(3000);
                thread = null;
            }

            return true;
        }
        private void RXThread()
        {
            XLDefine.XL_Status xlStatus = XLDefine.XL_Status.XL_SUCCESS;
            XLClass.xl_event receivedEvent = new XLClass.xl_event();

            while (true)
            {
                while (threadStatus == THREADSTATUS.INIT)
                {
                    //UpdateInfo("RXThread initialized", COLOR_DATA);
                    Thread.Sleep(1000);
                }

                while (threadStatus == THREADSTATUS.STOPPED)
                {
                    //UpdateInfo("RXThread blocked", COLOR_DATA);
                    Thread.Sleep(1000);
                }

                if (threadStatus == THREADSTATUS.EXIT)
                {
                    Console.WriteLine("RXThread exit");
                    return;
                }

                if (xlEventWaitHandle.WaitOne(1000))
                {
                    xlStatus = XLDefine.XL_Status.XL_SUCCESS;

                    while (xlStatus != XLDefine.XL_Status.XL_ERR_QUEUE_IS_EMPTY)
                    {
                        xlStatus = driver.XL_Receive(portHandle, ref receivedEvent);

                        if (xlStatus == XLDefine.XL_Status.XL_SUCCESS)
                        {
                            if (receivedEvent.tag == XLDefine.XL_EventTags.XL_RECEIVE_MSG)
                            {
                                switch (protocol)
                                {
                                    case "CAN":
                                        if ((receivedEvent.tagData.can_Msg.flags & XLDefine.XL_MessageFlags.XL_CAN_MSG_FLAG_ERROR_FRAME) != 0)
                                        {
                                            Console.WriteLine("-- XL_CAN_MSG_FLAG_ERROR_FRAME --");
                                        }
                                        else if ((receivedEvent.tagData.can_Msg.flags & XLDefine.XL_MessageFlags.XL_CAN_MSG_FLAG_OVERRUN) != 0)
                                        {
                                            Console.WriteLine("-- XL_CAN_MSG_FLAG_OVERRUN --");
                                        }
                                        else if ((receivedEvent.tagData.can_Msg.flags & XLDefine.XL_MessageFlags.XL_CAN_MSG_FLAG_REMOTE_FRAME) != 0)
                                        {
                                            Console.WriteLine("-- XL_CAN_MSG_FLAG_REMOTE_FRAME --");
                                        }
                                        else if ((receivedEvent.tagData.can_Msg.flags & XLDefine.XL_MessageFlags.XL_CAN_MSG_FLAG_TX_COMPLETED) != 0)
                                        {
                                            if (listener != null)
                                            {
                                                listener.WrapperNotifyTransmitted(receivedEvent.timeStamp,
                                                                                        receivedEvent.tagData.can_Msg.id,
                                                                                        receivedEvent.tagData.can_Msg.dlc,
                                                                                        receivedEvent.tagData.can_Msg.data);
                                            }
                                            else
                                            {
                                                Console.WriteLine("IVXLWrapperListener is null");
                                            }
                                        }
                                        else
                                        {
                                            if (listener != null)
                                            {
                                                listener.WrapperNotifyReceived(receivedEvent.timeStamp,
                                                                                    receivedEvent.tagData.can_Msg.id,
                                                                                    receivedEvent.tagData.can_Msg.dlc,
                                                                                    receivedEvent.tagData.can_Msg.data);
                                            }
                                            else
                                            {
                                                Console.WriteLine("IVXLWrapperListener is null");
                                            }
                                        }

                                        break;

                                    default:

                                        Console.WriteLine("IVXLWrapperListener unsupport protocol: " + protocol);

                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }

        
    }
    public interface WrapperListener
    {
        void WrapperNotifyTransmitted(ulong timeStamp, uint id, ushort dlc, byte[] data);

        void WrapperNotifyReceived(ulong timeStamp, uint id, ushort dlc, byte[] data);

    }
}