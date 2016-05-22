using System;
using System.Windows.Forms;

namespace DerAtrox.BeerNET.Examples {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }

        private void btnGo_Click(object sender, EventArgs e) {
            txtOutput.Clear();

            if (optSBeer.Checked) {
                txtOutput.Text = Beer.SerializeBeer(txtInput.Text);
            } else if (optDBeer.Checked) {
                txtOutput.Text = Beer.DeserializeBeer(txtInput.Text);
            } else {
                txtOutput.Text = Beer.DeserializeBeer(Beer.SerializeBeer(txtInput.Text));
            }
        }
    }
}
