using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsParser
{
    public partial class FormMain : Form
    {
        private ControllerFormMain controller;

        public FormMain()
        {
            InitializeComponent();

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            controller = new ControllerFormMain(this);
        }

        private void buttonGetToken_Click(object sender, EventArgs e)
        {
            controller.Autorize();
        }

        private void buttonGetBirthday_Click(object sender, EventArgs e)
        {
            controller.AddFriendsToDb();
        }

    }
}
