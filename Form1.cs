using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace BreakpointResume
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DownloadProcessForm f = new DownloadProcessForm();
            f.ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UploadProgressForm f = new UploadProgressForm();
            f.ShowDialog(this);
        }
        static object obj = new object();
        WebClient client;
        private void button3_Click(object sender, EventArgs e)
        {
            j = 0;
            ServicePointManager.DefaultConnectionLimit = 500;

            for (int i = 0; i < int.Parse(textBox1.Text); i++)
            {
                //ThreadPool.QueueUserWorkItem((o) =>
                //{
                //实例化
                using (WebClient client = new WebClient())
                {
                    //地址
                    string path = "http://172.17.17.166:20009/lis/techsvr/request/uploadclientinfo";
                    //数据较大的参数
                    string datastr = "{ 'hospitalcode':'99998888','macaddress':'255.255.255.255'}";
                    //参数转流
                    byte[] bytearray = Encoding.UTF8.GetBytes(datastr);
                    //采取POST方式必须加的header，如果改为GET方式的话就去掉这句话即可
                    client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");//长度
                    client.Headers.Add("ContentLength", bytearray.Length.ToString());
                    client.Headers.Add("jwt", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWJzeXNjb2RlIjoiTElNUyIsInVzZXJuYW1lIjoiZ2gwMTIiLCJwd2QiOiI5NmU3OTIxODk2NWViNzJjOTJhNTQ5ZGQ1YTMzMDExMiIsIm9yZ2lkIjoiNDI1MDUxNzUxMDEiLCJpYXQiOjE1NDMzOTE2ODk5MDd9.zZbVYh9WJotWt9NEMfSxdOAr5igfJ8WpIn-py_e6Gu4");
                    client.Headers.Add("Authorization", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWJzeXNjb2RlIjoiTElNUyIsInVzZXJuYW1lIjoiZ2gwMTIiLCJwd2QiOiI5NmU3OTIxODk2NWViNzJjOTJhNTQ5ZGQ1YTMzMDExMiIsIm9yZ2lkIjoiNDI1MDUxNzUxMDEiLCJpYXQiOjE1NDMzOTE2ODk5MDd9.zZbVYh9WJotWt9NEMfSxdOAr5igfJ8WpIn-py_e6Gu4");

                    //上传，post方式，并接收返回数据（这是同步，需要等待接收返回值）
                    client.UploadDataAsync(new Uri(path), bytearray);
                    client.UploadDataCompleted += Client_UploadDataCompleted;

                };


                // });

            }
        }
        int j = 0;
        private void Client_UploadDataCompleted(object sender, UploadDataCompletedEventArgs e)
        {

            lock (obj)
            {
                j += 1;
                var d = Encoding.UTF8.GetString(e.Result);

                Console.WriteLine("请求次数:" + j.ToString() + d + e.Cancelled.ToString());
            }

        }
    }
}
