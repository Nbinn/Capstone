using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public abstract class BaseMngr
    {
        protected string lastError = string.Empty;

        protected DemoTransmit frm = null;
        public string LastError
        {
            get { return lastError; }
        }  

        protected bool isLoaded = false;

        public bool IsLoaded
        { 
            get { return isLoaded; }
        }

        public void SetInfo(DemoTransmit _frm)
        {
            frm = _frm;
        }
        public abstract bool Init();
        public abstract bool Release();
    }
}
