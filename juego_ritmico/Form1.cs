using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace juego_ritmico
{
    public partial class Form1 : Form
    {
        private Panel[] lanes = new Panel[4];
        private Panel hitbox;
        private List<Nota> notas = new List<Nota>();
        private Timer timer;
        private Timer timerGenerator;
        public Form1()
        {
            InitializeComponent();
            CrearCarriles(lanes);
            CrearCuadroDeAccion(lanes);

            timer = new Timer();
            timer.Interval = 20;
            timer.Tick += Timer_Tick;
            timer.Start();

            timerGenerator = new Timer();
            timerGenerator.Interval = 600;
            timerGenerator.Tick += GenerarNotaAleatoria;
            timerGenerator.Start();

            CrearNota(0);
            CrearNota(1);
            CrearNota(2);
            CrearNota(3);

            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int carril = -1;
            if (e.KeyCode == Keys.E) carril = 0;
            if (e.KeyCode == Keys.R) carril = 1;
            if (e.KeyCode == Keys.O) carril = 2;
            if (e.KeyCode == Keys.P) carril = 3;

            if (carril != -1)
            {
                foreach (var n in notas)
                {
                    if (n.Panel.Left >= lanes[carril].Left && n.Panel.Right <= lanes[carril].Right)
                    {
                        int toleranciaPx = (int)(n.Velocidad * 120 / timer.Interval);

                        if (n.Panel.Bottom >= hitbox.Top - toleranciaPx &&
                            n.Panel.Top <= hitbox.Bottom + toleranciaPx)
                        {
                            this.Controls.Remove(n.Panel);
                            notas.Remove(n);
                            break;
                        }


                    }
                }
            }
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

        private void CrearCuadroDeAccion(Panel[] lanes)
        {
            hitbox = new Panel();
            hitbox.Width = (lanes.Length * 80) + ((lanes.Length - 1) * 10);
            hitbox.Height = 20;
            hitbox.BackColor = Color.Cyan;
            hitbox.Left = lanes[0].Left;
            hitbox.Top = this.ClientSize.Height - hitbox.Height - 30;

            this.Controls.Add(hitbox);
            hitbox.BringToFront();
        }
        private void CrearNota(int carril)
        {
            Panel p = new Panel();
            p.Width = lanes[carril].Width - 10;
            p.Height = 20;
            p.BackColor = Color.Cyan;
            p.Left = lanes[carril].Left + 5;
            p.Top = -p.Height;

            this.Controls.Add((p));
            p.BringToFront();

            notas.Add(new Nota(p, 10));
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            foreach (var n in notas.ToList())
            {
                n.mover();
                if (n.Panel.Top > hitbox.Bottom)
                {
                    this.Controls.Remove(n.Panel);
                    notas.Remove(n);
                }
            }
        }



        private void GenerarNotaAleatoria(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int carril = rnd.Next(0, lanes.Length);
            CrearNota(carril);
        }

        class Nota
        {
            public Panel Panel { get; set; }
            public int Velocidad { get; set; }

            public Nota(Panel panel, int velocidad)
            {
                Panel = panel;
                Velocidad = velocidad;
            }
            public void mover()
            {
                Panel.Top += Velocidad;
            }
        }
    }
}
