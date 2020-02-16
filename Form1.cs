using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        static StreamReader file;
        static string linia;
        private TextBox textBox1;
        private TextBox textBox2;
        private Button buttonEnter;
        private Button buttonPaste;
        private NotifyIcon notifyIcon1;

        public Form1()
        {
            InitializeComponent();
            menuContext();
            this.BackColor = Color.FromArgb(47, 50, 55);
        }

        public void Sprawdzenie()
        {
            file = new StreamReader("odp.txt");

            while ((linia = file.ReadLine()) != null)
            {
                if (linia.Contains(textBox1.Text))
                {
                    textBox2.AppendText(linia);
                    linia = file.ReadLine();
                    textBox2.AppendText(Environment.NewLine + "        " + linia);
                    textBox2.AppendText(Environment.NewLine);
                }
            }
        }

        public void InitializeComponent()
        {
            textBox1 = new TextBox
            {
                AcceptsReturn = true,
                Size = new Size(100, 200),
                BackColor = Color.FromArgb(47, 50, 55),
                ForeColor = Color.FromArgb(210, 211, 214),
            };

            textBox2 = new TextBox
            {
                Name = "answer",
                Size = new Size(500, 80),
                Location = new Point(0, 22),
                BackColor = Color.FromArgb(47, 50, 55),
                ForeColor = Color.FromArgb(210, 211, 214),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                AcceptsReturn = true,
            };

            notifyIcon1 = new NotifyIcon
            {
                Text = "Aktualizacja sterownika NVIDIA",
            };

            buttonEnter = new Button
            {
                Text = "⏎",
                Size = new Size(30, 24),
                Location = new Point(100,0),
                BackColor = Color.FromArgb(47, 50, 55),
                ForeColor = Color.FromArgb(210, 211, 214),
            };

            buttonPaste = new Button
            {
                Text = "Paste",
                Size = new Size(43,24),
                Location = new Point(130,0),
                BackColor = Color.FromArgb(47, 50, 55),
                ForeColor = Color.FromArgb(210, 211, 214),
            };
            this.textBox1.KeyDown += new KeyEventHandler(this.textBox1_KeyPress);
            this.buttonEnter.Click += new EventHandler(this.buttonEnter_Click);

            this.buttonPaste.Click += new EventHandler(this.buttonPaste_Click);
            this.notifyIcon1.MouseClick += new MouseEventHandler(this.notifyIcon1_MouseClick);

            this.textBox1.KeyPress += new KeyPressEventHandler(this.textBox1_Keypress);
            this.textBox2.KeyPress += new KeyPressEventHandler(this.textBox1_Keypress);
            this.buttonEnter.KeyPress += new KeyPressEventHandler(this.textBox1_Keypress);
            this.buttonPaste.KeyPress += new KeyPressEventHandler(this.textBox1_Keypress);
            this.ClientSize = new Size(312, 100);
            this.Text = "";
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.buttonEnter);
            this.Controls.Add(this.buttonPaste);
            this.ResumeLayout(true);
            this.PerformLayout();
        }
        static void Main()
        {   
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private void textBox1_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text == "")
                {
                    textBox2.ResetText();
                    textBox1.Clear();
                }
                else
                {
                    textBox2.ResetText();
                    Sprawdzenie();
                }
                e.Handled = true;
                e.SuppressKeyPress = true;
            //    pytanie = textBox1.Text;
            //    textBox2.AppendText(pytanie);
                textBox1.Clear();
            }
        }
        private void buttonEnter_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                textBox2.ResetText();
                textBox1.Clear();
            }
            else
            {
                textBox2.ResetText();
                Sprawdzenie();
            }
            textBox1.Clear();
        }

        private void menuContext()
        {
            this.notifyIcon1.ContextMenuStrip = new ContextMenuStrip();
            this.notifyIcon1.ContextMenuStrip.Items.Add("Exit", null, this.MenuExit_Click);
        }

        public void textBox1_Keypress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '-' || e.KeyChar == '`')
                this.WindowState = FormWindowState.Minimized;
            if (this.WindowState == FormWindowState.Minimized)
            {
                notifyIcon1.Icon = SystemIcons.Question;
                this.ShowInTaskbar = false;
                notifyIcon1.Visible = true;
            }
        }

        private void buttonPaste_Click(object sender, EventArgs e)
        {
            textBox2.ResetText();
            textBox1.Paste();
            Sprawdzenie();
            textBox1.ResetText();
        } 


        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                notifyIcon1.ContextMenuStrip.Visible = true;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                if (this.WindowState == FormWindowState.Normal)
                {
                    this.ShowInTaskbar = true;
                    notifyIcon1.Visible = false;
                }
            }
        }

        private void MenuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
