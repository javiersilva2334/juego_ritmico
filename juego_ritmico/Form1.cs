using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace juego_ritmico
{
    public partial class Form1 : Form
    {
        private Panel[] lanes = new Panel[4];
        public Form1()
        {
            InitializeComponent();
            CrearCarriles(lanes);
        }
        private void CrearCarriles(Panel[] lanes)
        {
            int anchoCarril = 80;
            int altoCarril = this.ClientSize.Height - 50;
            int espacio = 10;

            for (int i = 0; i < 4; i++)
            {
                lanes[i] = new Panel();
                lanes[i].Width = anchoCarril;
                lanes[i].Height = altoCarril;
                lanes[i].BackColor = Color.Gray;
                lanes[i].Left = i * (anchoCarril + espacio) + 20;
                lanes[i].Top = 10;
                lanes[i].BorderStyle = BorderStyle.FixedSingle;

                this.Controls.Add(lanes[i]);
            }
        }
 
        
    }
}
