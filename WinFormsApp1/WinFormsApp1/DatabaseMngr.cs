using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using vxlapi_NET;

namespace WinFormsApp1
{
    class DatabaseMngr : BaseMngr
    {

        private Dictionary<string, Message> messages = null;
        private DatabaseMngr()
        {
            messages = new Dictionary<string,Message>();
        }

        private static DatabaseMngr instance = null;

        public static DatabaseMngr Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DatabaseMngr();
                }

                return instance;
            }
        }


        public override bool Init()
        {
            
            if (messages.Count > 0)
            {
                Console.WriteLine("Number of loaded message(s): " + messages.Count.ToString());
            }


            return true;
        }

        public override bool Release()
        {
            return true;
        }

        
        public Dictionary<string, Message> GetMessages()
        {
            return messages;
        }

        public Message GetMessage(string id)
        {
            if (messages.ContainsKey(id))
            {
                return messages[id];
            }
            else
            {
                return null;
            }
        }

        public void OnReceivedMessage(ulong timeStamp, uint id, ushort dlc, byte[] data)
        {
            string sID = id.ToString();

            lock(messages)
            {
                try
                {
                    if(messages.ContainsKey(sID) == false)
                    {
                        Message msg = new Message();
                        msg.Index = messages.Count;
                        msg.ID = sID;
                        
                        messages.Add(sID, msg);
                    }
                    messages[sID].TimeStamp = timeStamp.ToString();
                    messages[sID].SetData((int)dlc,data.ToList());

                    frm.UpdateTrace(messages[sID]);
                }
                catch(Exception ex) 
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}
