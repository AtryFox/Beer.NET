using System;
using System.Windows.Forms;

namespace DerAtrox.BeerNET.Examples {
    public partial class MainForm : Form {
        private IBeerEncoder encoder;

        public MainForm() {
            InitializeComponent();
        }

        private void btnGo_Click(object sender, EventArgs e) {
            txtOutput.Clear();

            if (optSBeer.Checked) {
                txtOutput.Text = encoder.Encode(txtInput.Text);
            } else if (optDBeer.Checked) {
                txtOutput.Text = encoder.Decode(txtInput.Text);
            } else {
                txtOutput.Text = encoder.Decode(encoder.Decode(txtInput.Text));
            }
        }

        private void EncoderSelection_CheckedChanged(object sender, EventArgs e) {
            if (optBeer.Checked) {
                encoder = new Beer();
            } else if (optBeerEx.Checked) {
                encoder = new BeerEx();
            }
        }
    }
}
