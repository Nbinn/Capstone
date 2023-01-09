using Timer = System.Timers.Timer;

namespace WinFormsApp1
{
    public class TxTimer : Timer
    {
        private string id = string.Empty;

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        public TxTimer() : base() { }
    }
}
