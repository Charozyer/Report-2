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

namespace Poprigun
{
    public partial class Form1 : Form
    {
        static int nAgentList = 20; // Макс число агентов на странице
        static public List<int> lstSelectedAgent = new List<int>();
        public Form1()
        {
            InitializeComponent();
        }
        List<Agent> lstAgent = new List<Agent>(); // создание списка агентов
        List<AgentUserCntrl> lstControls = new List<AgentUserCntrl>();
        Button[] btnsList = new Button[5];
        private void Form1_Load(object sender, EventArgs e)
        {
            SortCombo.SelectedIndex = 0;
            List<string> lstTypes = Program.db.AgentType.Select(x => x.Title).ToList();
            lstTypes.Insert(0, "Все типы"); //установка сортировки
            FiltrCombo.DataSource = lstTypes;
            SortCombo.Text = "Сортировка";
            FiltrCombo.Text = "Фильтрация";

            for (int i = 0; i<nAgentList; i++)
            {
                lstControls.Add(new AgentUserCntrl());
                lstControls[i].Notify += Notify;
                flowLayoutPanel1.Controls.Add(lstControls[i]);
            }
            {
                btnsList[0] = button1;
                btnsList[1] = button2;
                btnsList[2] = button3;
                btnsList[3] = button4;
                btnsList[4] = button5;
                SetPageBtn(1);
            }
            DataWork();
        }
        string filtr = "Все типы"; // фильтр показа агентов
        string sort = "Наименование"; // сортировки агентов
        string search = "";  // поиск по наименованию
        int nAgentNumber = 0;  // начальный номер агента на странице
        int nPageFirst = 1;   // номер первой кнопки на странице
        int nPageCurrent = 1; // номер текущей страницы
        int nPageAll = 0;     // всего страниц с агентами
        int nActiveBtn = 1;      // Convert.ToInt32(button1.Text);   номер нажатой (текущей, активной) кнопки

        public void DataWork() //класс работы с данными
        {
            lstAgent = Program.db.Agent.ToList();
            if (filtr != "Все типы")
            {
                lstAgent = lstAgent.Where(a => (a.AgentType.Title == filtr)).ToList();
            }
            if (search != "")
            {
                search = search.ToUpper();
                lstAgent = lstAgent.Where(a => (a.Title.ToUpper().Contains(search))).ToList();
            }
            if (sort == "Без сортировки")
            {
                if (sort == "Название")
                {
                    if (!DownCheck.Checked) lstAgent = lstAgent.OrderBy(a => a.Title).ToList();
                    else lstAgent = lstAgent.OrderByDescending(a => a.Title).ToList();
                }
                if (sort == "Тип агента")
                {
                    if (!DownCheck.Checked) lstAgent = lstAgent.OrderBy(a => a.AgentTypeID).ToList();
                    else lstAgent = lstAgent.OrderByDescending(a => a.AgentTypeID).ToList();
                }
                if (sort == "Приоритет")
                {
                    if (!DownCheck.Checked) lstAgent = lstAgent.OrderBy(a => a.Priority).ToList();
                    else lstAgent = lstAgent.OrderByDescending(a => a.Priority).ToList();
                }
            }
                ShowCurrentPage();
                // что показываем на кнопках 
                nPageCurrent = 1;       // текущая страница
                nPageFirst = 1;  // первая кнопка = "1"
                // расчитываем общее кол-во страниц
                nPageAll = lstAgent.Count() / nAgentList;
                    if (nPageAll * nAgentList < lstAgent.Count())
                        nPageAll++;
                    // если страниц меньше, чем кнопок
                    if (nPageAll <= 5)
                    {
                        if (nPageAll < 5)
                    {
                    for (int i = nPageAll; i <= 5; i++)
                        btnsList[i - 1].Enabled = false;
                    }
                    RightBtn.Enabled = false;
                }
            }

            private void ShowCurrentPage()
            {
                int nAgentMax = lstAgent.Count(); // максимальное коли-во агентов
                // расчитываем номер первого агента на странице
                nAgentNumber = (nPageCurrent - 1) * nAgentList;

                // счетчик номера агента на странице
                int i = nAgentNumber;

                // цикл по UserControls, которые будут показываться на форме 
                // задаем свойства показываемых ПЭУ
                foreach (AgentUserCntrl puc in lstControls)
                {// каждому UserControls передаем данные (с помощью свойств)
                 // проверяем, есть ли данные для очередного UserControls
                    if (i < nAgentMax)
                    {
                        // сохраняем ID агента
                        puc.ID = lstAgent[i].ID;

                        // проверяем - выбран этот ЭУ или нет?
                        if (lstSelectedAgent.IndexOf(puc.ID) != -1)
                        {// если данный ПЭУ ВЫБРАН, то фон меняем
                            puc.BackColor = Color.LightGray;
                        }
                        else
                        {// если не выбран, то начальный цвет
                            puc.BackColor = Color.White; // puc.BackColor1;
                        }
                        // задаем фото агента
                        if ((lstAgent[i].Logo != "") && (lstAgent[i].Logo != null))
                            // если фото у агента есть, добавляем его
                            puc.Picture = Image.FromFile(lstAgent[i].Logo);
                        else  // если фото нет, то добавляем картинку по умолчанию
                            puc.Picture = Image.FromFile(@"agents\picture.png");

                        // задаем строку "тип агента | название агента"
                        puc.Agent = lstAgent[i].AgentType.Title + " | "
                            + lstAgent[i].Title;

                        // задаем номер телефона
                        puc.Phone = lstAgent[i].Phone;

                        // задаем приоритет
                        puc.Prior = lstAgent[i].Priority;

                        // делаем данный ЭУ видимым 
                        puc.Visible = true;
                    }
                    else
                    {   // если данных для ПЭУ нет
                        // делаем этот ПЭУ невидимым
                        puc.Visible = false;
                        // если данных уже нет, то кнопка враво не активная
                        RightBtn.Enabled = false;
                    }
                    i++; // счетчик номера агентов увеличиваем
                }
                // если на последней странице показаны все агенты
                // то кнопку тоже отключаем 
                if (i == nAgentMax)
                {
                    RightBtn.Enabled = false;
                }
                ////////////////////////////////////////////
                //  выводим надпись внизу формы 
                // 
                int max = nPageCurrent * nAgentList;
                if (max > nAgentMax) max = nAgentMax;
                //  вывод сообщения о номерах показываемых агентов
                RangeLbl.Text = $"Агенты с {nAgentNumber + 1} по {max} (из всего {lstAgent.Count()})";
            }

            private void LeftBtn_Click(object sender, EventArgs e)
            {
                // вычисляем номер предыдущей страницы
                if (nActiveBtn > 1)
                {
                    nPageCurrent--;
                    nActiveBtn--;
                    SetPageBtn(nPageCurrent);
                }
                else if (nActiveBtn == 1 && nPageFirst != 1)
                {
                    ChangePageBtn(-1); // смещаем страницы влево на -1                
                    nPageCurrent = Convert.ToInt32(button1.Text); // или nActiveBtn
                    SetPageBtn(1);
                }
                // передаем данные для UserControl новой страницу с товарами
                ShowCurrentPage();

                if (nPageCurrent == 1 && nPageFirst == 1)
                    LeftBtn.Enabled = false;
                RightBtn.Enabled = false;
                if (nPageFirst + 4 < nPageAll)
                    RightBtn.Enabled = true;
            }
        private void RightBtn_Click(object sender, EventArgs e)
        {
            // вычисляем начальный номер товара на следующей странице
            if (nPageCurrent < nPageAll)
            {
                nPageCurrent++;
                if (nActiveBtn < 5)
                {
                    SetPageBtn(nPageCurrent);
                    nActiveBtn++;
                }
                else if (nActiveBtn == 5)
                {
                    ChangePageBtn(1);
                }
            }
            else
                RightBtn.Enabled = false;

            // передаем данные для UserControl новой страницу с товарами
            ShowCurrentPage();

            // делаем кнопку "налево" активной
            LeftBtn.Enabled = true;
        }

        private void SortCombo_SelectedIndexChanged(object sender, EventArgs e)
            {
                sort = SortCombo.Text;
                DataWork();
            }
            private void DownCheck_CheckedChanged(object sender, EventArgs e)
            {
                DataWork();
            }
            private void SearchTxt_TextChanged(object sender, EventArgs e)
            {
                search = SearchTxt.Text;
                DataWork();
            }

            private void FiltrCombo_SelectedIndexChanged(object sender, EventArgs e)
            {
                filtr = FiltrCombo.Text;
                DataWork();
            }

            private void Notify(string message, int id)
            {
                if (message == "Правая кнопка")
                {

                }
                if (message == "Левая кнопка")
                {
                    AddEditAgentForm form = new AddEditAgentForm();
                    Agent age = lstAgent.Find(p => p.ID == id);
                    form.age = age;

                    DialogResult dr = form.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        DataWork();
                        ShowCurrentPage();
                    }
                }
            }
        private void SearchTxt_Enter(object sender, EventArgs e)
        {
            searchLabel.Text = "";
        }
        private void SearchTxt_Leave(object sender, EventArgs e)
        {
            searchLabel.Text = "Введите для поиска";
        }
        private void addButton_Click(object sender, EventArgs e)
        {
            AddEditAgentForm form = new AddEditAgentForm();
            form.age = null;
            DialogResult dr = form.ShowDialog();
            if (dr == DialogResult.OK)
            {
                DataWork();
                ShowCurrentPage();
            }
        }
        private void delButton_Click(object sender, EventArgs e)
        {
            Agent agent = (Agent)agentBindingSource.Current;
            DialogResult dr = MessageBox.Show("Удалить Агента - " + agent.Title,
                "Удаление данных об Агенте", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                Program.db.Agent.Remove(agent);
                Program.db.SaveChanges();
            }
        }
        private void editButton_Click(object sender, EventArgs e)
        {
            AddEditAgentForm form = new AddEditAgentForm();
            form.age = null;
            DialogResult dr = form.ShowDialog();
            if (dr == DialogResult.OK)
            {
                DataWork();
                ShowCurrentPage();
            }
        }
        private void buttonN_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            // определяем номер показываемой страницы
            nPageCurrent = Convert.ToInt32(btn.Text);

            // устанавливаем активную кнопку (выделяется цветом и рамкой)
            int n = btn.Name.Length; // длина имени кнопки
            string str = btn.Name.Substring(n - 1); // в конце номер кнопки
            nActiveBtn = Convert.ToInt32(str);
            // показываем все кнопки выделением активной
            ShowActiveBtn();
            // загружаем данные в другую страницу            
            ShowCurrentPage();
        }
        void SetPageBtn(int nPage)
        {
            for (int i = 0; i < 5; i++)
            {
                int ii = Convert.ToInt32(btnsList[i].Text);
                if (ii == nPage)
                    btnsList[i].BackColor = Color.LightBlue;
                else
                    btnsList[i].BackColor = Color.White;
            }
        }
        // показываем номер активной кнопки
        void ShowActiveBtn() // int nActiveBtn)
        {
            for (int i = 0; i < 5; i++)
            {
                int ii = Convert.ToInt32(btnsList[i].Text);
                if (ii == nActiveBtn)
                    btnsList[i].BackColor = Color.LightBlue;
                else
                    btnsList[i].BackColor = Color.White;
            }
        }
        void ChangePageBtn(int d)
        {
            if (nPageAll < 5)
            {
                for (int i = nPageAll; i < 5; i++)
                {
                    btnsList[i].Enabled = false;
                }
            }

            for (int i = 0; i < 5; i++)
            {
                int n = Convert.ToInt32(btnsList[i].Text) + d;
                if (n < 1) return;
                btnsList[i].Text = n.ToString();
            }
            nPageFirst = Convert.ToInt32(btnsList[0].Text);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void LeftBtn_Click_1(object sender, EventArgs e)
        {
            // вычисляем номер предыдущей страницы
            if (nActiveBtn > 1)
            {
                nPageCurrent--;
                nActiveBtn--;
                SetPageBtn(nPageCurrent);
            }
            else if (nActiveBtn == 1 && nPageFirst != 1)
            {
                ChangePageBtn(-1); // смещаем страницы влево на -1                
                nPageCurrent = Convert.ToInt32(button1.Text); // или nActiveBtn
                SetPageBtn(1);
            }
            // передаем данные для UserControl новой страницу с товарами
            ShowCurrentPage();

            if (nPageCurrent == 1 && nPageFirst == 1)
                LeftBtn.Enabled = false;
            RightBtn.Enabled = false;
            if (nPageFirst + 4 < nPageAll)
                RightBtn.Enabled = true;
        }

        private void RightBtn_Click_1(object sender, EventArgs e)
        {
            // вычисляем начальный номер товара на следующей странице
            if (nPageCurrent < nPageAll)
            {
                nPageCurrent++;
                if (nActiveBtn < 5)
                {
                    SetPageBtn(nPageCurrent);
                    nActiveBtn++;
                }
                else if (nActiveBtn == 5)
                {
                    ChangePageBtn(1);
                }
            }
            else
                RightBtn.Enabled = false;

            // передаем данные для UserControl новой страницу с товарами
            ShowCurrentPage();

            // делаем кнопку "налево" активной
            LeftBtn.Enabled = true;
        }
    }
}

