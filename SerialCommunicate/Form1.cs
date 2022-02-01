using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO.Ports;
namespace SerialCommunicate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = comboBox1.Text;
                serialPort1.BaudRate = Convert.ToInt32(comboBox2.Text,10);//十进制数据转换
                serialPort1.Open();
                button1.Enabled = false;//打开串口按钮不可用
                button2.Enabled = true;//关闭串口
            }
            catch
            {
                MessageBox.Show("端口错误,请检查串口", "错误");

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 1; i < 20; i++)
            {
                comboBox1.Items.Add("COM" + i.ToString());
            }
            comboBox1.Text = "COM1";//串口号多额默认值
            comboBox2.Text = "9600";//波特率默认值

            for (int m = 0; m < 256; m++)
            {
                comboBox6.Items.Add(m.ToString());
                comboBox7.Items.Add(m.ToString());
            }
            comboBox5.Text = "请选择";
            comboBox6.Text = "请选择";
            comboBox7.Text = "请选择";
            comboBox8.Text = "请选择";
            /*****************非常重要************************/

            serialPort1.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);//必须手动添加事件处理程序
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)//串口数据接收事件
        {
                byte data;
                data = (byte)serialPort1.ReadByte();//此处需要强制类型转换，将(int)类型数据转换为(byte类型数据，不必考虑是否会丢失数据
                string str = Convert.ToString(data, 16).ToUpper();//转换为大写十六进制字符串
                textBox1.AppendText(str);//空位补“0”   
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Close();//关闭串口
                button1.Enabled = true;//打开串口按钮可用
                button2.Enabled = false;//关闭串口按钮不可用
            }
            catch (Exception err)//一般情况下关闭串口不会出错，所以不需要加处理程序
            {

            }
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)//初始化
        {
            //string data_init = "00 06 00 02 00 00";//初始化命令            
            string data_init = "000600020000";//初始化命令
            byte[] buffer = new byte[1];
            for (int i = 0; i < 6; i++)
            {
                buffer[0] = Convert.ToByte(data_init.Substring(i * 2, 2), 16);//将字符串转化为byte型变量（byte相当于单片机中的unsigned char（0-255））
                serialPort1.Write(buffer, 0, 1);
            }
        }

        private void button6_Click(object sender, EventArgs e)//点名
        {
            string data_name = "000600010000";//点名命令
            byte[] buffer = new byte[1];
            for (int i = 0; i < 6; i++)
            {
                buffer[0] = Convert.ToByte(data_name.Substring(i * 2, 2), 16);//
                serialPort1.Write(buffer, 0, 1);
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            byte[] buffer = new byte[1];//数据一个字节就够用了
            if (serialPort1.IsOpen)//判断串口是否打开，如果打开执行下一步操作
            {
                if (textBox2.Text != "")
                {
                    for (int i = 0; i < (textBox2.Text.Length / 2); i++)
                    {
                        buffer[0] = Convert.ToByte(textBox2.Text.Substring(i * 2, 2), 16);
                        serialPort1.Write(buffer, 0, 1);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
