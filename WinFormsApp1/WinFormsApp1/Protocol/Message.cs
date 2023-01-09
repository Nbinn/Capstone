using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public class Message
    {
        List<byte> data = null;
        public Message() 
        {
            data = new List<byte>();
        }

        protected int index = 0;
        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        protected string id = string.Empty;
        
        public string ID
        {
            get { return id; }
            set { id = value; }
        }
        
        private string timeStamp = string.Empty;
        public string TimeStamp
        {
            get { return timeStamp; }
            set { timeStamp = value; }
        }

        protected string name = string.Empty;

        public string Name
        { 
            get { return name; } 
            set { name = value; } 
        }

        protected int dlc = 0;

        public int DLC
        { 
            get { return dlc; }
            set { dlc = value; } 
        }

        protected int cycleTime = 0;

        public int CycleTime
        { 
            get { return cycleTime; }
            set { cycleTime = value; }
        }

        public string GetIDAsHex()
        {
            return "0x" + Convert.ToInt64(id).ToString("X");
        }

        public bool SetData(int _dlc, List<byte> _data)
        {
            if(dlc != _dlc)
            {
                dlc = _dlc;

                if(data.Count < dlc) 
                {
                    while(data.Count < dlc) 
                    {
                        data.Add(0x00);
                    }
                }
                else
                {
                    while(data.Count > dlc)
                    {
                        data.RemoveAt(data.Count- 1);
                    }
                }


            }

            for (int i = 0;i < dlc; i++)
            {
                data[i] = _data[i];
            }
            return true;
        }

        public List<byte> GetData()
        {
            return data;
        }

        
        
    }
}
