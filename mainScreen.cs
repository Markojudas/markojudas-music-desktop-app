using System;
using System.Windows.Forms;

namespace markojudas_music
{
    public partial class MainScreen : Form
    {
        public MainScreen()
        {
            InitializeComponent();
        }

        private void addBand_Click(object sender, EventArgs e)
        {
            //open new form; disabling main form for a bit
            AddBand newBand = new AddBand();
            newBand.ShowDialog();
        }

        private void updateBand_Click(object sender, EventArgs e)
        {
            //open new form; disabling main form for a bit'
            NewAlbum newAlbum = new NewAlbum();
            newAlbum.ShowDialog();
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
