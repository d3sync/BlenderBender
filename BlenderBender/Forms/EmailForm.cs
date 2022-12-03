using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlenderBender
{
    public partial class EmailForm : Form
    {
        public Form mf;
        public EmailForm(Form mf)
        {
            InitializeComponent();
            this.mf = mf;
        }
    }
}
