using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reversi2
{
    public class Reversi : Form
    {
        int[,] veldcordinatenXY;
        int breedtebord;
        int hoogtebord;
        int q;
        int r;
        int x;
        int y;
        int startX;
        int startY;
        int maxX;
        int maxY;
        int veldnummerX;
        int veldnummerY;
        int aantalZetten;
        int straal;
        int middelpuntX1;
        int middelpuntX2;
        int middelpuntY1;
        int middelpuntY2;
        int f;
        int z;
        int e;
        int g;
        Button nieuwspel;
        Button help;
        Label statusLabel;
        //Bij ons is kleur1 blauw
        int Kleur1;
        int Kleur2;
        int Xplus;
        int Xminus;
        int Yplus;
        int Yminus;
        int Xstandaard;
        int Ystandaard;
        int incrament;
        int Xpos;
        int Ypos;
        int Xneg;
        int Yneg;





        public Reversi()
        {
            breedtebord = 6;
            incrament = f++;
            hoogtebord = 6;
            straal = 35;
            startX = 50;
            startY = 170;
            maxX = (startX + breedtebord * 50);
            maxY = (startY + hoogtebord * 50);
            Kleur1 = 1;
            Kleur2 = 2;
            Xplus = (f + 1);
            Xminus = (f - 1);
            Yplus = (z + 1);
            Yminus = (z - 1);
            Xstandaard = (f);
            Ystandaard = (z);
            Xpos = 1;
            Ypos = 1;
            Xneg = 0;
            Yneg = 0;
            veldcordinatenXY = new int[breedtebord, hoogtebord];
            this.ClientSize = new Size((breedtebord * 50 + 100), (hoogtebord * 50 + 200));
            nieuwspel = new Button();
            help = new Button();
            help.Size = new Size(80, 25);
            help.Text = "Help";
            help.Location = new Point((breedtebord * 50 + 100) / 4, 20);
            nieuwspel.Size = new Size(80, 25);
            nieuwspel.Text = "Nieuw spel";
            nieuwspel.Location = new Point(((breedtebord * 50 + 100) / 4) * 2, 20);
            statusLabel = new Label();
            statusLabel.Text = "Blauw is aan zet";
            statusLabel.Location = new Point(startX, startY - 30);
            statusLabel.Font = new Font("Arial", 10);
            statusLabel.AutoSize = true;
            this.Controls.Add(statusLabel);
            this.Controls.Add(help);
            this.Controls.Add(nieuwspel);
            this.MiddelsteStenen();
            this.Text = "Reversi";
            this.Paint += this.TekenBord;
            this.MouseClick += this.ReversiStuk;
            this.help.Click += this.showPossibleMoves;

        }
        public void TekenBord(object obj, PaintEventArgs pea)
        {
            Graphics gr = pea.Graphics;
            Pen pen = new Pen(Color.Black, 2);
            for (q = 0; q < breedtebord; q++)
            {
                for (r = 0; r < hoogtebord; r++)
                {
                    gr.DrawRectangle(pen, (startX + 50 * q), (startY + 50 * r), 50, 50);
                    if (veldcordinatenXY[q, r] != 0)
                    {
                        if (veldcordinatenXY[q, r] == 1)
                            gr.FillEllipse(Brushes.Blue, (startX + 6 + 50 * q), (startY + 6 + 50 * r), straal, straal);
                        else if (veldcordinatenXY[q, r] == 2)
                            gr.FillEllipse(Brushes.Red, (startX + 6 + 50 * q), (startY + 6 + 50 * r), straal, straal);
                        else if (veldcordinatenXY[q, r] == 3)
                            gr.FillEllipse(Brushes.Black, (startX + 6 + 50 * q), (startY + 6 + 50 * r), straal, straal);
                    }
                }
            }

        }
        public void ReversiStuk(object obj, MouseEventArgs mea)
        {
            this.x = mea.X;
            this.y = mea.Y;
            if (x > startX && x < maxX && y > startY && y < maxY)
            {
                veldnummerX = (x - startX) / 50;
                veldnummerY = (y - startY) / 50;
                if (veldcordinatenXY[veldnummerX, veldnummerY] == 0)
                {
                    if (aantalZetten % 2 == 0)
                    {
                        veldcordinatenXY[veldnummerX, veldnummerY] = 1;
                        aantalZetten++;
                        statusLabel.Text = "Rood is aan zet";
                        Kleur1 = 2;
                        Kleur2 = 1;
                        this.Invalidate();
                    }
                    else
                    {
                        veldcordinatenXY[veldnummerX, veldnummerY] = 2;
                        aantalZetten++;
                        Kleur1 = 1;
                        Kleur2 = 2;
                        statusLabel.Text = "Blauw is aan zet";
                        this.Invalidate();
                    }
                }
            }
        }
        public void MiddelsteStenen()
        {
            middelpuntX1 = breedtebord / 2;
            middelpuntX2 = (breedtebord / 2) - 1;
            middelpuntY1 = hoogtebord / 2;
            middelpuntY2 = (hoogtebord / 2) - 1;
            veldcordinatenXY[middelpuntX1, middelpuntY1] = 1;
            veldcordinatenXY[middelpuntX1, middelpuntY2] = 2;
            veldcordinatenXY[middelpuntX2, middelpuntY1] = 2;
            veldcordinatenXY[middelpuntX2, middelpuntY2] = 1;
        }
        public void showPossibleMoves(object obj, EventArgs ea)
        {
            this.legaliteit(Kleur1, Kleur2);
            this.Invalidate();
        }
        public void legaliteit(int kleurS, int kleurE)
        {
            for (f = 0; f < breedtebord; f++)
            {
                for (z = 0; z < hoogtebord; z++)
                {
                    Console.WriteLine("hmmmm");
                   if (veldcordinatenXY[f,z] == kleurS)
                    {
                        status(f, z, kleurS, kleurE);
                    }
                }
            }
        }
        // elke richting kijken
        public void status(int x, int y, int kleurS, int kleurE)
        {
            statusEenRichting(x, y, kleurS, kleurE, 1 , 1);
            statusEenRichting(x, y, kleurS, kleurE, 1, 0);
            statusEenRichting(x, y, kleurS, kleurE, 1, -1);
            statusEenRichting(x, y, kleurS, kleurE, 0, 1 );
            statusEenRichting(x, y, kleurS, kleurE, 0 , -1);
            statusEenRichting(x, y, kleurS, kleurE, -1 , -1);
            statusEenRichting(x, y, kleurS, kleurE, -1 , 0);
            statusEenRichting(x, y, kleurS, kleurE, -1 , 1);
            Console.WriteLine("whatever");
        }
        // een enkele richting kijken
        public void statusEenRichting(int x, int y, int kleurS, int kleurE, int Xricht, int Yricht)
        {
            int xnewsteen = x + Xricht;
            int ynewsteen = y + Yricht;
            bool tussen = false;
            while (xnewsteen >= 0 && xnewsteen < breedtebord && ynewsteen >= 0 && ynewsteen < hoogtebord)
            {
                if (veldcordinatenXY[xnewsteen, ynewsteen] == kleurE)
                {
                    tussen = true;
                } else if (tussen && veldcordinatenXY[xnewsteen, ynewsteen] != kleurS)
                {
                    veldcordinatenXY[xnewsteen, ynewsteen] = 3;
                } else {
                    return;
                }
                xnewsteen = x + Xricht;
                ynewsteen = y + Yricht;
            }
        }
}
        static class Program
        {
            /// <summary>
            /// The main entry point for the application.
            /// </summary>
            [STAThread]
            static void Main()
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Reversi());
            }
        }
    }
