using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace SyringePump
{
    public partial class Form1 : Form
    {
        int volumen;                //Volumen de la solución en el trackbar
        int _tiempo;                //Selector de tiempo del radiobutton
        int ttiempo;                //Tiempo total
        double gotas = 0;           //Fórmula del goteo G(t) = V/3T
        int myoptiont;              //Obtener el valor del volúmen seleccionado

        string endTime;

        System.Timers.Timer t;
        int h, m, s;
        
        public Form1()
        {
            InitializeComponent();
            if(!serialPort1.IsOpen)
            {
                serialPort1.Open();
            }
            serialPort1.Write("d");
            t = new System.Timers.Timer();
            t.Interval = 1000;
            t.Elapsed += OnTimeEvent;
        }

        private void OnTimeEvent(object sender, ElapsedEventArgs e)
        {
            Invoke(new Action(() =>
            {
                s += 1;
                if (s == 60)
                {
                    s = 0;
                    m += 1;
                }
                if (m == 60)
                {
                    m = 0;
                    h += 1;
                }
                switch(_tiempo){
                    case 0:
                        if(h == 1){
                            t.Stop();
                            onButton();
                        }
                        break;
                    case 1:
                        if(h== 2){
                            t.Stop();
                            onButton();
                        }
                        break;
                    case 2:
                        if(h == 4){
                            t.Stop();
                            onButton();
                        }
                        break;
                    case 3:
                        if(h == 8){
                            t.Stop();
                            onButton();
                        }
                        break;
                    default:
                        break;
                }
                label6.Text = string.Format("{0}:{1}:{2}", h.ToString().PadLeft(2, '0'), m.ToString().PadLeft(2, '0'), s.ToString().PadLeft(2, '0'));
                TimeSpan duration = DateTime.Parse(endTime).Subtract(DateTime.Parse(label6.Text));
                double i = (duration.TotalMinutes * 100) / ttiempo;
                progressBar1.Maximum = 100;
                progressBar1.Value = (int)i;
                if(i > 99)
                {
                    serialPort1.Write("a");
                }
                else if((i <= 99) && (i > 98.5)){
                    serialPort1.Write("b");
                }
                else if(i <= 98.5)
                {
                    serialPort1.Write("c");
                }
                Console.WriteLine(i);
            }));
        }

        private void TrackBar2_Scroll(object sender, EventArgs e)
        {
            //Console.WriteLine(trackBar2.Value);
            myoptiont = trackBar2.Value;
            switch (myoptiont)
            {
                case 0:
                    volumen = 50;
                    break;
                case 1:
                    volumen = 100;
                    break;
                case 2:
                    volumen = 250;
                    break;
                case 3:
                    volumen = 500;
                    break;
                case 4:
                    volumen = 1000;
                    break;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            resetAll();
            progressBar1.Value = 100;
            if (trackBar2.Value == 0)
            {
                volumen = 50;
            }
            if (radioButton1.Checked == true)
            {
                gotas = volumen / (3 * 1);
                _tiempo = 0;
                ttiempo = 60;
                endTime = "01:00:00";
                offButton();
                t.Start();
                button1.Enabled = false;
            }
            else if (radioButton2.Checked == true)
            {
                gotas = volumen / (3 * 2);
                _tiempo = 1;
                ttiempo = 120;
                endTime = "02:00:00";
                offButton();
                t.Start();
            }
            else if (radioButton3.Checked == true)
            {
                gotas = volumen / (3 * 4);
                _tiempo = 2;
                ttiempo = 240;
                endTime = "04:00:00";
                offButton();
                t.Start();
            }
            else if (radioButton4.Checked == true)
            {
                gotas = volumen / (3 * 8);
                _tiempo = 3;
                ttiempo = 480;
                endTime = "08:00:00";
                offButton();
                t.Start();
            }
            else
            {
                progressBar1.Value = 0;
                MessageBox.Show("Favor de seleccionar un tiempo válido");
                return;
            }
            //Console.WriteLine(trackBar2.Value);
        }

        private void onButton()
        {
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
            radioButton3.Enabled = true;
            radioButton4.Enabled = true;
            radioButton5.Enabled = true;
            button1.Enabled = true;
        }

        private void resetAll()
        {
            t.Stop();
            label6.Text = "00:00:00";
            h = m = s = 0;
            ttiempo = 0;
            onButton();
            progressBar1.Value = 0;
            serialPort1.Write("d");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            resetAll();
        }

        private void offButton()
        {
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            radioButton3.Enabled = false;
            radioButton4.Enabled = false;
            radioButton5.Enabled = false;
            button1.Enabled = false;
        }

        private void TextProgressBar2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
