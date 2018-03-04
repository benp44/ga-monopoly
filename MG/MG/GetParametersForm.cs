using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MG
{
    public partial class GetParametersForm : Form
    {
        public int InitialPopulation
        {
            get
            {
                return int.Parse(textBoxPopulation.Text);
            }
        }

        public int WorkerCount
        {
            get
            {
                return int.Parse(textBoxWorkerCount.Text);
            }
        }

        public GetParametersForm()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (CheckValues())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private bool CheckValues()
        {
            int testPopulation;
            int testWorkerCount;

            if (int.TryParse(textBoxPopulation.Text, out testPopulation) == false)
            {
                textBoxPopulation.Select();
                return false;
            }

            if (int.TryParse(textBoxWorkerCount.Text, out testWorkerCount) == false)
            {
                textBoxPopulation.Select();
                return false;
            }

            if (testPopulation <= (testWorkerCount * 8))
            {
                MessageBox.Show("The population must exceed worker count x 8.");
                return false;
            }

            return true;
        }

        public double MutationRate { get; set; }
    }
}
