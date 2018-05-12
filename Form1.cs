using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using ID3;
using ID3.ID3v2Frames.BinaryFrames;
using System.Runtime.InteropServices;
using System.IO;
using System.Security.Permissions;


namespace player
{
  

    public partial class Form1 : Form
    {
        bool IsLeft;
        Point MouseMove;

        

       

        public Form1()
        {
            InitializeComponent();
            ofbutton.AutoSize = false;
            label3.Font = new Font("微软雅黑", 15, FontStyle.Bold);
            label2.Font = new Font("微软雅黑", 15, FontStyle.Bold);
            label2.Text = " ";
            label3.Text = " ";
            BackGround.Text = "Change Background";
            timer1.Interval = 100;
            button1.Text = "AutoRewind";
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsStopped)
            {
                Playbutton.Text = "Play";
            }
            label3.BackColor = System.Drawing.Color.Transparent;
            label2.BackColor = System.Drawing.Color.Transparent;

            this.label1.Visible = false;
            while (Playbutton.Text == "pause")
            {
                timer1.Enabled = false;
            }
            this.listBox1.MouseDoubleClick += new MouseEventHandler(listBox1_MouseDoubleClick);
            this.button3.Text = "shuffle";

        }
        string filepath = null;
        OpenFileDialog of1;
        private void button1_Click(object sender, EventArgs e)
        {

            /*if(listBox1.SelectedItem.ToString() != null)
            {
                of1.FileName = listBox1.SelectedItem.ToString();
                if (of1.FileName != null)
                {
                    ID3.ID3Info info = new ID3.ID3Info(of1.FileName, true);
                    foreach (AttachedPictureFrame AP in info.ID3v2Info.AttachedPictureFrames.Items)
                    {

                        pictureBox1.Image = Image.FromStream(AP.Data); //添加到PicturBOX内

                    }
                    
                }
                goto aa;
            aa:
                int count = 0;
                filepath = of1.FileName;
                this.axWindowsMediaPlayer1.URL = of1.FileName;
                axWindowsMediaPlayer1.Ctlcontrols.play();
                timer1.Start();
                label2.Text = axWindowsMediaPlayer1.Ctlcontrols.currentItem.name;

                listBox1.Items.Add(of1.FileName);
                count++;
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
                Playbutton.Text = "pause";
                return;
            }*/
            
            of1 = new OpenFileDialog();
            of1.InitialDirectory = "C://";
            of1.Filter = "(音乐文件)*mp3|*mp3";
            
            
            if (of1.ShowDialog() == DialogResult.OK)
            {
               if (of1.FileName!=null)
                {
                    ID3.ID3Info info = new ID3.ID3Info(of1.FileName, true);
                    foreach (AttachedPictureFrame AP in info.ID3v2Info.AttachedPictureFrames.Items)
                    {
                        
                        pictureBox1.Image = Image.FromStream(AP.Data); //添加到PicturBOX内

                    }
                    goto aa;
                }
           aa: 
                int count = 0;
                filepath = of1.FileName;
                this.axWindowsMediaPlayer1.URL = of1.FileName;
                axWindowsMediaPlayer1.Ctlcontrols.play();
                timer1.Start();
                label2.Text = axWindowsMediaPlayer1.Ctlcontrols.currentItem.name;
                
                listBox1.Items.Add(of1.FileName);
                count++;
                listBox1.SelectedIndex = listBox1.Items.Count - 1;          
                /*new Thread(() =>
                {
                    while (true)
                    {
                        try { label3.BeginInvoke(new MethodInvoker(() => label3.Text = axWindowsMediaPlayer1.Ctlcontrols.currentPositionString)); }
                        catch { }
                        Thread.Sleep(1000);
                    }
                }) { IsBackground = true }.Start();*/
                Playbutton.Text = "pause";
                
               
            }
            
            
           if (1 == (int)axWindowsMediaPlayer1.playState)
            {
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            
            
            
            
            if (axWindowsMediaPlayer1.Ctlcontrols.get_isAvailable("pause"))
            {
                axWindowsMediaPlayer1.Ctlcontrols.pause();
                Playbutton.Text = "play"; 
            }

            else if (axWindowsMediaPlayer1.Ctlcontrols.get_isAvailable("play"))
            {
                axWindowsMediaPlayer1.Ctlcontrols.play();
                Playbutton.Text = "pause";
            }
   
        }

        private void BackGround_Click(object sender, EventArgs e)
        {
            OpenFileDialog of2 = new OpenFileDialog();
            of2.InitialDirectory = "C://";
            of2.Filter = "图像文件|*.jpg";
            if (of2.ShowDialog() == DialogResult.OK)
            {
                this.BackgroundImage = Image.FromFile(of2.FileName);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            bool judge;
            judge=axWindowsMediaPlayer1.settings.getMode("loop");
            if (!judge)
            {
                axWindowsMediaPlayer1.settings.setMode("loop",true);
                button1.Text="AutoRewind:ON";
                button3.Text = "shuffle:OFF";
            }
            else
            {
                axWindowsMediaPlayer1.settings.setMode("loop", false);
                button1.Text = "AutoRewind:OFF";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            timer1.Interval=1000;
            label3.Text = axWindowsMediaPlayer1.Ctlcontrols.currentPositionString;
            while (label3.Text == axWindowsMediaPlayer1.Ctlcontrols.currentItem.durationString)
            {
                timer1.Stop();
            }
        }

        int axWindowsMediaPlayer1_PlayStateChange()
        {
            int check;
            check = (int)axWindowsMediaPlayer1.playState;
            return check;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (listBox1.SelectedItem.ToString() != null)
            {
                this.axWindowsMediaPlayer1.URL = listBox1.SelectedItem.ToString();
                label2.Text = axWindowsMediaPlayer1.Ctlcontrols.currentItem.name;
                axWindowsMediaPlayer1.Ctlcontrols.play();
                timer1.Start();
            }
        }      
        private void addbutton_Click(object sender, EventArgs e)
        {
            OpenFileDialog of1;

            of1 = new OpenFileDialog();
            of1.InitialDirectory = "F://";
            of1.Filter = "(音乐文件)*mp3|*mp3";

            if (of1.ShowDialog() == DialogResult.OK)
            {

                if (listBox1.Items.Contains(of1.FileName))
                {
                    MessageBox.Show("it's been selected!");
                    return;
                }
                listBox1.Items.Add(of1.FileName);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*if (this.axWindowsMediaPlayer1.Ctlcontrols.currentItem.name != null)
            {
                this.axWindowsMediaPlayer1.Ctlcontrols.currentItem.name = listBox1.SelectedItem.ToString();
                this.axWindowsMediaPlayer1.URL = listBox1.SelectedItem.ToString();
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }*/
            /*axWindowsMediaPlayer1.Ctlcontrols.currentItem.name = listBox1.SelectedItem.ToString();
            this.axWindowsMediaPlayer1.URL = listBox1.SelectedItem.ToString();
            label2.Text = axWindowsMediaPlayer1.Ctlcontrols.currentItem.name;
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            /*if (this.axWindowsMediaPlayer1.Ctlcontrols.currentItem.name == null)
            {
                this.axWindowsMediaPlayer1.Ctlcontrols.currentItem.name = listBox1.SelectedItem.ToString();
                this.axWindowsMediaPlayer1.URL = listBox1.SelectedItem.ToString();
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }*/
            if (listBox1.SelectedItem == null) return;
            this.axWindowsMediaPlayer1.URL = listBox1.SelectedItem.ToString();
            label2.Text = axWindowsMediaPlayer1.Ctlcontrols.currentItem.name;
            timer1.Start();
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem!=null)
            {
                listBox1.Items.Remove(listBox1.SelectedItem); 
            }
            else
            {
                MessageBox.Show("please select an item!");
                return;
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            bool judging;
            judging = axWindowsMediaPlayer1.settings.getMode("shuffle");
            if (!judging)
            {
                axWindowsMediaPlayer1.settings.setMode("shuffle", true);
                button3.Text = "shuffle:ON";
                button1.Text = "AutoRewind:OFF";
            }
            else
            {
                axWindowsMediaPlayer1.settings.setMode("loop", false);
                button3.Text = "shuffle:OFF";
                button1.Text = "AutoRewind";
            }
            
        }       
    }
    
}


