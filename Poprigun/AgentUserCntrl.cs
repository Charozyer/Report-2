using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Poprigun
{
    public partial class AgentUserCntrl : UserControl
    {
        public int ID;
        private Color colBColor;
        private Image pictureImg;
        private string typeAgent;
        private string phoneNum;
        private int priority;

        public delegate void PUCHandler(string message, int id);
        public event PUCHandler Notify;

        public AgentUserCntrl()
        {
            InitializeComponent();
        }

        public Image Picture
        {
            get { return pictureImg; }
            set
            {
                pictureImg = value;
                pictureBox1.Image = pictureImg;
            }
        }
        public string Phone
        {
            get { return phoneNum; }
            set
            {
                phoneNum = value;
                phoneNumLbl.Text = phoneNum;
            }
        }
        public int Prior
        {
            get { return priority; }
            set
            {
                priority = value;
                priorityLbl.Text = priority.ToString();
            }
        }

        public string Agent
        {
            get { return typeAgent; }
            set
            {
                typeAgent = value;
                TypeAgentLbl.Text = typeAgent;
            }
        }
        private void AgentUserCntrl_Load(object sender, EventArgs e)
        {

        }

        private void AgentUserCntrl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (Form1.lstSelectedAgent.IndexOf(ID) != -1)
                { // если данный ПЭУ ВЫБРАН
                    BackColor = colBColor;
                    // удаляем из выборанных
                    Form1.lstSelectedAgent.Remove(ID);
                }
                else
                { // если данный ПЭУ НЕ ВЫБРАН
                    BackColor = Color.LightGray;
                    // добавляем к выбранным
                    Form1.lstSelectedAgent.Add(ID);
                }
                // Notify.Invoke("xxxxxx");
                if (Notify != null) Notify("Правая кнопка", ID);
            }
            if (e.Button == MouseButtons.Left)
            {
                if (Notify != null) Notify("Левая кнопка", ID);
            }
        }

        private void AgentUserCntrl_Click(object sender, EventArgs e)
        {

        }
    }
}
