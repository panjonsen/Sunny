using Sunny.Lib;
using System;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

namespace Sunny
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        
        }

        private delegate void dListViewAdd(string method, string url, string postData, int statusCode, int onlyId, string headers);

        private void ListViewAdd(string method, string url, string postData, int statusCode, int onlyId, string headers)
        {
            ListViewItem listViewItem = new ListViewItem();
            listViewItem.Tag = listView1.Items.Count;
            listViewItem.Text = Convert.ToString(listView1.Items.Count + 1);
            listViewItem.SubItems.Add(method);
            listViewItem.SubItems.Add(url);
            listViewItem.SubItems.Add(postData);
            listViewItem.SubItems.Add(statusCode.ToString());
            listViewItem.SubItems.Add(onlyId.ToString());
            listViewItem.SubItems.Add(headers.ToString());

            listView1.Items.Add(listViewItem);
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            if (btn_start.Text == "Stop")
            {
                btn_start.Text = "Start";

                SunnyLib.Stop();
                return;
            }
            //启动
            btn_start.Text = "Stop";

            bool result = SunnyLib.Start(Convert.ToInt32(textBox_prot.Text), CallBack_Http, null, null, true);
            if (!result)
            {
                MessageBox.Show(SunnyLib.Get_Error());
            }

            Console.WriteLine("启动成功");
        }

        /// <summary>
        /// HTTP/HTTPS 回调
        /// </summary>
        /// <param name="唯一ID"></param>
        /// <param name="MessageId"></param>
        /// <param name="消息类型">Const.Net_Http_</param>
        /// <param name="Method"></param>
        /// <param name="Url"></param>
        /// <param name="err"></param>
        /// <param name="pid">进程PID 若等于0 表示通过代理请求 无进程PID</param>
        public bool CallBack_Http(int OnlyId, int MessageId, int MsgType, string Method, string Url, string err, int pid)
        {
            Request sunny = SunnyLib.MessageIdToSunny(MessageId);

            if (MsgType == Const.Net_Http_Response)
            {
                dListViewAdd viewAdd = new dListViewAdd(ListViewAdd);
                this.Invoke(viewAdd, Method, Url, sunny.request.Get_Data_UTF8(), sunny.response.Get_StatusCode(), OnlyId, sunny.request.Get_Headers());
            }
            else if (MsgType == Const.Net_Http_Request)
            {
                //Console.WriteLine("发起请求：" + Url + " [POST数据]" + sunny.request.Get_Data_UTF8());
            }
            else if (MsgType == Const.Net_Http_Request_Fail)
            {
                //Console.WriteLine("请求错误：" + Url + " err=" + err);
            }

            //这里返回值是什么不重要，但得有
            return true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
         

          
                Directory.CreateDirectory("win32");
           
            
                Directory.CreateDirectory("win64");


            

          if (!File.Exists(@"win32/Sunny.dll") | !File.Exists(@"win64/Sunny.dll"))
            {
                MessageBox.Show("请在软件运行目录win32/win64下放置对应的版本的Sunny.dll 名字就叫这个");

            }

            SunnyLib.CreateCertificate();
            SunnyLib.Stop();

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            SunnyLib.Stop();
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (listView1.Items.Count != 0 && listView1.SelectedIndices != null && listView1.SelectedItems.Count > 0)
            {
                string headers = listView1.SelectedItems[0].SubItems[6].Text;

                string postData = listView1.SelectedItems[0].SubItems[3].Text;

                req_headers.Text = headers;
                req_body.Text = postData;
            }
        }
    }
}