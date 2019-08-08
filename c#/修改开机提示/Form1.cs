using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace 修改开机提示
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            button1.Text = "Wait ……";
            DialogResult result;//设置局部变量 result为对话框结果
            result = MessageBox.Show("即将写入注册表，若有安全软件拦截请允许！是否继续？", "重要提示：", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                //确认以后正式开始写表          
                RegistryKey key = Registry.LocalMachine;
                RegistryKey openpath = key.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);//打开要新建值的项
                openpath.SetValue("LegalNoticeCaption", textBox1.Text);//新建名为 LegalNoticeCaption 的值，数据为 文本框1（标题） 内容
                openpath.SetValue("LegalNoticeText", textBox2.Text);
                

                //验证一下成功写入了没
                string info_caption = "";
                string info_text = "";
                info_caption = openpath.GetValue("LegalNoticeCaption").ToString();
                info_text = openpath.GetValue("LegalNoticeText").ToString();

                if (info_caption ==  textBox1 .Text && info_text == textBox2.Text )
                {
                    MessageBox.Show("修改成功", "结果验证：", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MessageBox.Show("修改失败，建议手动确认或修改", "结果验证：", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                openpath.Close();

            }
            else
            {
                MessageBox.Show("您选择了取消操作","提示：", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

            button1.Text = "< 一键执行 >";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "建议先阅读下方Tips……";
            textBox2.Text = "建议先阅读下方Tips……";
        }

    }
}
