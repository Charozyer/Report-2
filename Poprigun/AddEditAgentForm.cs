using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Poprigun.ModelEF;
using System.Data.Entity;

namespace Poprigun
{

    public partial class AddEditAgentForm : Form
    {
        public Agent age { get; set; } = null;
        public AddEditAgentForm()
        {
            InitializeComponent();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (age == null)
            {
                age = (Agent)agentBindingSource.Current;
                Program.db.Agent.Add(age);
            }
            age.Logo = agentImage;
            try
            {
                Program.db.SaveChanges();
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddEditAgentForm_Load(object sender, EventArgs e)
        {
            agentBindingSource.DataSource = Program.db.Agent.ToList();
            if(age == null)
            {
                agentBindingSource.AddNew();
                // настраиваем картинку и надпись
                // запрещаем удаление еще не введенного агента
                agentImage = @"agents\picture.png";
                Text = "Добавление нового агента";
            }
            else
            {
                agentBindingSource.Add(age);
                if (age.Logo != "") return;
                else
                {
                    agentImage = @"agents\picture.png";
                }
                Text = "Изменение данных агента";
            }
        }
        string agentImage = "";
    }
}
