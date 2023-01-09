using System;
using System.Reflection;
using System.Timers;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WinFormsApp1
{
    public partial class DemoTransmit : Form
    {
        int index;
        int defaultRow = 8;

        private bool isInitSystem = false;
        private System.Windows.Forms.Timer systemTimer = null;

        public DemoTransmit()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            index = 0;
            if(BusMngr.Instance.Init() == false || DatabaseMngr.Instance.Init() == false)
            {
                dataGridView1.Rows[index].Cells["DLCColumn"].Value = "0";
            }
            
            
            
            this.dataGridView1.Rows.Add(defaultRow);
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            DatabaseMngr.Instance.SetInfo(this);
            BusMngr.Instance.SetInfo(this);

            systemTimer = new System.Windows.Forms.Timer();
            systemTimer.Tick += new EventHandler(OnTimedEventSystem);
            //systemTimer.Interval = Convert.ToInt32()
            systemTimer.Start();
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                e.PaintBackground(e.CellBounds, false);
                e.Paint(e.CellBounds, DataGridViewPaintParts.ContentForeground);
                if (dataGridView1.SortedColumn?.Index == e.ColumnIndex)
                {
                    var sortIcon = dataGridView1.SortOrder == SortOrder.Ascending ? "▲" : "▼";

                    //Just for example I rendered a character, you can draw an image.
                    TextRenderer.DrawText(e.Graphics, sortIcon,
                        e.CellStyle.Font, e.CellBounds, Color.Black,
                        TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
                }
            }
        }

        private void sendMessage1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(messageID1.Text) && !string.IsNullOrWhiteSpace(messageData1.Text))
            {
                /*int dlc = 0;
                string data = messageData1.Text;
                string id = messageID1.Text;

                if (data.Length % 2 == 0) 
                {
                    dlc = data.Length / 2;
                }
                else
                {
                    data += "0";
                    dlc = data.Length / 2;
                }
                
                if(dlc > 8)
                {
                    dlc = 8;
                    data = data.Substring(0, dlc*2);
                }

                if (index >= defaultRow) dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells["IDColumn"].Value = id;
                dataGridView1.Rows[index].Cells["DLCColumn"].Value = dlc;
                dataGridView1.Rows[index].Cells["DataColumn"].Value = data;
                dataGridView1.Rows[index].Cells["BusColumn"].Value = 1;
                dataGridView1.Rows[index++].Cells["IndexColumn"].Value = index;
                
                dataGridView1.Rows[index].Cells["TimeStampColumn"].Value = BusMngr.*/

                /*if (BusMngr.Instance.Init() == true)
                {

                    dataGridView1.Rows[index].Cells["IDColumn"].Value = "true";

                }
                else
                {
                    dataGridView1.Rows[index].Cells["DataColumn"].Value = "false";
                }*/

                if(BusMngr.Instance.TransmitData(messageID1.Text,messageData1.Text) == true) 
                {

                    dataGridView1.Rows[index].Cells["IDColumn"].Value = "true";

                }
                else
                {
                    dataGridView1.Rows[index].Cells["DataColumn"].Value = "false";
                }
            }
        }

        private void sendMessage2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(messageID2.Text) && !string.IsNullOrWhiteSpace(messageData2.Text))
            {
                int dlc = 0;
                string data = messageData2.Text;
                string id = messageID2.Text;

                if (data.Length % 2 == 0)
                {
                    dlc = data.Length / 2;
                }
                else
                {
                    data += "0";
                    dlc = data.Length / 2;
                }

                if (dlc > 8)
                {
                    dlc = 8;
                    data = data.Substring(0, dlc * 2);
                }

                if (index >= defaultRow) dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells["IDColumn"].Value = id;
                dataGridView1.Rows[index].Cells["DLCColumn"].Value = dlc;
                dataGridView1.Rows[index].Cells["DataColumn"].Value = data;
                dataGridView1.Rows[index].Cells["BusColumn"].Value = 1;
                dataGridView1.Rows[index++].Cells["IndexColumn"].Value = index;
                BusMngr.Instance.TransmitData(messageID2.Text, messageData2.Text);
            }
        }

        private void sendMessage3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(messageID3.Text) && !string.IsNullOrWhiteSpace(messageData3.Text))
            {

                int dlc = 0;
                string data = messageData3.Text;
                string id = messageID3.Text;

                if (data.Length % 2 == 0)
                {
                    dlc = data.Length / 2;
                }
                else
                {
                    data += "0";
                    dlc = data.Length / 2;
                }

                if (dlc > 8)
                {
                    dlc = 8;
                    data = data.Substring(0, dlc * 2);
                }

                if (index >= defaultRow) dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells["IDColumn"].Value = id;
                dataGridView1.Rows[index].Cells["DLCColumn"].Value = dlc;
                dataGridView1.Rows[index].Cells["DataColumn"].Value = data;
                dataGridView1.Rows[index].Cells["BusColumn"].Value = 1;
                dataGridView1.Rows[index++].Cells["IndexColumn"].Value = index;
                BusMngr.Instance.TransmitData(messageID3.Text, messageData3.Text);
            }
        }

        private void sendAllMessage_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(messageID1.Text) && !string.IsNullOrWhiteSpace(messageData1.Text))
            {
                int dlc = messageData1.Text.Length / 2;

                if (index >= defaultRow) dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells["IDColumn"].Value = messageID1.Text;
                dataGridView1.Rows[index].Cells["DLCColumn"].Value = dlc;
                dataGridView1.Rows[index].Cells["DataColumn"].Value = messageData1.Text;
                dataGridView1.Rows[index].Cells["BusColumn"].Value = 1;
                dataGridView1.Rows[index++].Cells["IndexColumn"].Value = index;
                BusMngr.Instance.TransmitData(messageID1.Text, messageData1.Text);/*
                dataGridView1.Rows[index].Cells["TimeStampColumn"].Value = BusMngr.*/
            }
            if (!string.IsNullOrWhiteSpace(messageID2.Text) && !string.IsNullOrWhiteSpace(messageData2.Text))
            {
                int dlc = messageData2.Text.Length / 2;

                if (index >= defaultRow) dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells["IDColumn"].Value = messageID2.Text;
                dataGridView1.Rows[index].Cells["DLCColumn"].Value = dlc;
                dataGridView1.Rows[index].Cells["DataColumn"].Value = messageData2.Text;
                dataGridView1.Rows[index].Cells["BusColumn"].Value = 1;
                dataGridView1.Rows[index++].Cells["IndexColumn"].Value = index;
                BusMngr.Instance.TransmitData(messageID2.Text, messageData2.Text);
            }
            if (!string.IsNullOrWhiteSpace(messageID3.Text) && !string.IsNullOrWhiteSpace(messageData3.Text))
            {
                int dlc = messageData3.Text.Length / 2;

                if (index >= defaultRow) dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells["IDColumn"].Value = messageID3.Text;
                dataGridView1.Rows[index].Cells["DLCColumn"].Value = dlc;
                dataGridView1.Rows[index].Cells["DataColumn"].Value = messageData3.Text;
                dataGridView1.Rows[index].Cells["BusColumn"].Value = 1;
                dataGridView1.Rows[index++].Cells["IndexColumn"].Value = index;
                BusMngr.Instance.TransmitData(messageID3.Text, messageData3.Text);
            }
        }

        private void InitializeSystem()
        {
            if(BusMngr.Instance.Init() == true)
            {
                if(DatabaseMngr.Instance.Init() == false)
                {
                    Console.WriteLine("error");
                }
            }
            
        }

        private delegate void dlgUpdateTrace(Message msg);
        public void UpdateTrace(Message msg)
        {
            if (dataGridView1.InvokeRequired)
            {
                dataGridView1.Invoke(new dlgUpdateTrace(UpdateTrace), msg);
            }
            else
            {
                try
                {
                    int index = msg.Index;
                    if (index >= defaultRow) dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells["IDColumn"].Value = msg.ID;
                    dataGridView1.Rows[index].Cells["DLCColumn"].Value = msg.DLC;
                    dataGridView1.Rows[index].Cells["DataColumn"].Value = msg.GetIDAsHex();
                    dataGridView1.Rows[index].Cells["BusColumn"].Value = 1;
                    dataGridView1.Rows[index++].Cells["IndexColumn"].Value = index;
                    dataGridView1.Rows[index].Cells["TimeStampColumn"].Value = msg.TimeStamp;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void OnTimedEventSystem(object sender, EventArgs e)
        {
            if(isInitSystem == true)
            {
                isInitSystem = false;

                InitializeSystem();

                systemTimer.Stop();
            }
        }
    }
}