using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Input;
using System.Drawing.Text;
using System.IO;

namespace _2048_Application
{
    public partial class Form1 : Form
    {
        protected IDictionary<int, string> InitColorIDictionary = new Dictionary<int, string>();
        protected void InitColorList()
        {
            InitColorIDictionary[0] = "#CCC0B3";
            InitColorIDictionary[2] = "#EEE4DA";
            InitColorIDictionary[4] = "#EDE0C8";
            InitColorIDictionary[8] = "#F2B179";
            InitColorIDictionary[16] = "#F59563";
            InitColorIDictionary[32] = "#F67C5F";
            InitColorIDictionary[64] = "#F65E3B";
            InitColorIDictionary[128] = "#EDCF72";
            InitColorIDictionary[256] = "#EDCC61";
            InitColorIDictionary[512] = "#EDC850";
            InitColorIDictionary[1024] = "#EDC53F";
            InitColorIDictionary[2048] = "#EDC22E";
            InitColorIDictionary[4096] = "#3E3933";
        }
        protected IDictionary<int, string> FontColorIDictionary = new Dictionary<int, string>();
        protected void FontColorList()
        {
            FontColorIDictionary[0] = "#776E65";
            FontColorIDictionary[2] = "#776E65";
            FontColorIDictionary[4] = "#776E65";
            FontColorIDictionary[8] = "#F9F6F2";
            FontColorIDictionary[16] = "#F9F6F2";
            FontColorIDictionary[32] = "#F9F6F2";
            FontColorIDictionary[64] = "#F9F6F2";
            FontColorIDictionary[128] = "#F9F6F2";
            FontColorIDictionary[256] = "#F9F6F2";
            FontColorIDictionary[512] = "#F9F6F2";
            FontColorIDictionary[1024] = "#F9F6F2";
            FontColorIDictionary[2048] = "#F9F6F2";
            FontColorIDictionary[4096] = "#F9F6F2";
        }
        protected Label[,] labels = new Label[4, 4];
        protected int[,] Board = new int[4, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
        protected int POINTS = 0;

        protected void GenerateBoard()
        {
            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile("ClearSans-Bold.ttf");
            int x_box = 30;
            int y_box = 180;
            for (int j = 0; j <= 3; j++)
            {
                for (int i = 0; i <= 3; i++)
                {
                    Label label = new Label();
                    label.AutoSize = false;
                    label.Size = new System.Drawing.Size(125, 125);
                    label.BackColor = System.Drawing.ColorTranslator.FromHtml("#CCC0B2");
                    label.Location = new System.Drawing.Point(x_box, y_box);
                    label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    label.Font = new System.Drawing.Font(pfc.Families[0], 35F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
                    label.ForeColor = System.Drawing.Color.White;
                    label.Text = "";
                    label.Show();
                    Controls.Add(label);
                    labels[j, i] = label;
                    x_box += 145;

                }
                x_box = 30;
                y_box += 145;
            }
        }
        protected void RefreshStatus()
        {
            for (int y = 0; y <= 3; y++)
            {
                for (int x = 0; x <= 3; x++)
                {
                    if(Board[y, x] == 0)
                    {
                        labels[y, x].Text = "";
                        labels[y, x].BackColor = System.Drawing.ColorTranslator.FromHtml(InitColorIDictionary[0]);
                    }
                    else
                    {
                        labels[y, x].Text = Board[y, x].ToString();
                        labels[y, x].BackColor = System.Drawing.ColorTranslator.FromHtml(InitColorIDictionary[Board[y, x]]);
                        if (Board[y, x] == 2 || Board[y, x] == 4)
                        {
                            labels[y, x].ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(110)))), ((int)(((byte)(101)))));
                        }
                        else
                        {
                            labels[y, x].ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(246)))), ((int)(((byte)(242)))));
                        }
                    }      
                }
            }
            label6.Text = POINTS.ToString();
        }
        private void ReturnArray()
        {
            for (int y = 0; y <= 3; y++)
            {
                for (int x = 0; x <= 3; x++)
                {
                    Console.Write("{0}, ", Board[y, x]);
                }
                Console.Write("\n\n");
            }
            Console.Write("\n\n");
        }
        private void FirstPoint()
        {
            Random n = new Random();
            var i = n.Next(0, 4);
            var j = n.Next(0, 4);
            Board[j, i] = 2;
        }
        private void ShuffleNumber()
        {
            bool ShuffleStatus = true;
            bool GeneralStatus = false;
            for(int i = 0; i <= 3; i++)
            {
                for(int j = 0; j <= 3; j++)
                {
                    if(Board[i, j] == 0)
                    {
                        GeneralStatus = true;
                    }
                }
            }
            if(GeneralStatus == true)
            {
                while (ShuffleStatus)
                {
                    Random t = new Random();
                    var y = t.Next(0, 4);
                    var x = t.Next(0, 4);
                    if (Board[y, x] == 0)
                    {
                        Board[y, x] = 2;
                        ShuffleStatus = false;
                    }
                }
            }
            else
            {
                if (POINTS > Int32.Parse(label7.Text))
                {
                    label7.Text = POINTS.ToString();
                }
                MessageBox.Show("Koniec gry", "Koniec gry", MessageBoxButtons.OK);
                RestartGame();
            }
        }
        private int[] BLOCK_MOVE = new int[4] { 0, 0, 0, 0 };
        protected void mLeft()
        {
            for (int y = 0; y <= 3; y++)
            {
                for(int x = 1; x <= 3; x++)
                {
                    if(Board[y, x] != 0)
                    {
                        for(int z = x - 1; z >= 0; z--)
                        {
                            if (Board[y, z] == 0)
                            {
                                Board[y, z] = Board[y, z + 1];
                                Board[y, z + 1] = 0;
                            }
                            else if (Board[y, z] != 0 && Board[y, z] != Board[y, z + 1])
                            {
                                BLOCK_MOVE[0]++;
                            }
                            else if (Board[y, z] == Board[y, z + 1])
                            {
                                Board[y, z] = Board[y, z + 1] * 2;
                                POINTS += Board[y, z + 1] * 2;
                                Board[y, z + 1] = 0;
                            }
                        }
                    }
                }
            }
        }  
        protected void mUp()
        {
            for (int y = 1; y <= 3; y++)
            {
                for (int x = 0; x <= 3; x++)
                {
                    if (Board[y, x] != 0)
                    {
                        for (int z = y - 1; z >= 0; z--)
                        {
                            if (Board[z, x] == 0)
                            {
                                Board[z, x] = Board[z + 1, x];
                                Board[z + 1, x] = 0;
                            }
                            else if (Board[z, x] != 0 && Board[z, x] != Board[z + 1, x])
                            {
                                BLOCK_MOVE[1]++;
                            }
                            else if (Board[z, x] == Board[z + 1, x])
                            {
                                Board[z, x] = Board[z + 1, x] * 2;
                                POINTS += Board[z + 1, x] * 2;
                                Board[z + 1, x] = 0;
                            }
                        }
                    }
                }
            }
        }
        protected void mRight()
        {
            for (int y = 0; y <= 3; y++)
            {
                for (int x = 2; x >= 0; x--)
                {
                    if (Board[y, x] != 0)
                    {
                        for (int z = x + 1; z <= 3; z++)
                        {
                            if (Board[y, z] == 0)
                            {
                                Board[y, z] = Board[y, z - 1];
                                Board[y, z - 1] = 0;
                            }
                            else if (Board[y, z] != 0 && Board[y, z] != Board[y, z - 1])
                            {
                                BLOCK_MOVE[2]++;
                            }
                            else if (Board[y, z] == Board[y, z - 1])
                            {
                                Board[y, z] = Board[y, z - 1] * 2;
                                POINTS += Board[y, z - 1] * 2;
                                Board[y, z - 1] = 0;
                            }
                        }
                    }
                }
            }
        }
        protected void mDown()
        {
            for (int y = 2; y >= 0; y--)
            {
                for (int x = 0; x <= 3; x++)
                {
                    if (Board[y, x] != 0)
                    {
                        for (int z = y + 1; z <= 3; z++)
                        {
                            if (Board[z, x] == 0)
                            {
                                Board[z, x] = Board[z - 1, x];
                                Board[z - 1, x] = 0;
                            }
                            else if (Board[z, x] != 0 && Board[z, x] != Board[z - 1, x])
                            {
                                BLOCK_MOVE[3]++;
                            }
                            else if (Board[z, x] == Board[z - 1, x])
                            {
                                Board[z, x] = Board[z - 1, x] * 2;
                                POINTS += Board[z - 1, x] * 2;
                                Board[z - 1, x] = 0;
                            }
                        }
                    }
                }
            }
        }
        private List<string> pressedList = new List<string>();
        public Form1()
        {
            InitializeComponent();
            InitColorList();
            GenerateBoard();
            FirstPoint();
            RefreshStatus();
            pressedList.Add(Keys.Space.ToString());
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {    
            string pressedKey = e.KeyData.ToString();
            switch (e.KeyCode)
            {
                case (Keys.Left):
                    pressedList.Add(pressedKey);
                    if(!pressedList[pressedList.Count() - 2].Equals(pressedKey.ToString()))
                    {
                        BLOCK_MOVE[0] = 0; BLOCK_MOVE[1] = 0; BLOCK_MOVE[2] = 0; BLOCK_MOVE[3] = 0;
                    }
                        mLeft();
                    if (BLOCK_MOVE[0] <= 24)
                    {
                            ShuffleNumber();
                    }
                        OpenConsoleToDebugMode();
                        ReturnArray();
                        RefreshStatus();
                        Console.Write("{0}, {1}, {2}, {3}, {4} |", BLOCK_MOVE[0], pressedList[pressedList.Count() - 2], pressedList[pressedList.Count() - 1], pressedKey, POINTS);

                    break;

                case (Keys.Up):
                    pressedList.Add(pressedKey);
                    if (!pressedList[pressedList.Count() - 2].Equals(pressedList[pressedList.Count() - 1].ToString()))
                    {
                        BLOCK_MOVE[0] = 0; BLOCK_MOVE[1] = 0; BLOCK_MOVE[2] = 0; BLOCK_MOVE[3] = 0;
                    }
                        mUp();
                    if (BLOCK_MOVE[1] <= 24)
                    {
                        ShuffleNumber();
                    }
                        ReturnArray();
                        RefreshStatus();
                        Console.Write("{0}, {1}, {2}, {3}, {4} |", BLOCK_MOVE[1], pressedList[pressedList.Count() - 2], pressedList[pressedList.Count() - 1], pressedKey, POINTS);
                    break;

                case (Keys.Right):
                    pressedList.Add(pressedKey);
                    if (!pressedList[pressedList.Count() - 2].Equals(pressedList[pressedList.Count() - 1].ToString()))
                    {
                        BLOCK_MOVE[0] = 0; BLOCK_MOVE[1] = 0; BLOCK_MOVE[2] = 0; BLOCK_MOVE[3] = 0;
                    }
                        mRight();
                    if (BLOCK_MOVE[2] <= 24)
                    {
                        ShuffleNumber();
                    }
                        ReturnArray();
                        RefreshStatus();
                        Console.Write("{0}, {1}, {2}, {3}, {4} |", BLOCK_MOVE[2], pressedList[pressedList.Count() - 2], pressedList[pressedList.Count() - 1], pressedKey, POINTS);
                     break;

                case (Keys.Down):
                    pressedList.Add(pressedKey);
                    if (!pressedList[pressedList.Count() - 2].Equals(pressedList[pressedList.Count() - 1].ToString()))
                    {
                        BLOCK_MOVE[0] = 0; BLOCK_MOVE[1] = 0; BLOCK_MOVE[2] = 0; BLOCK_MOVE[3] = 0;
                    }
                        mDown();
                        if (BLOCK_MOVE[3] <= 24)
                        {
                            ShuffleNumber();
                        }
                        ReturnArray();
                        RefreshStatus();
                        Console.Write("{0}, {1}, {2}, {3}, {4} |", BLOCK_MOVE[3], pressedList[pressedList.Count() - 2], pressedList[pressedList.Count() - 1], pressedKey, POINTS);
                    break;

                default:
                    break;

            }
        }
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, // x-coordinate of upper-left corner 
            int nTopRect, // y-coordinate of upper-left corner 
            int nRightRect, // x-coordinate of lower-right corner 
            int nBottomRect, // y-coordinate of lower-right corner 
            int nWidthEllipse, // height of ellipse 
            int nHeightEllipse // width of ellipse 
        );
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private void label2_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void OpenConsoleToDebugMode()
        {
            ProcessStartInfo psi = new ProcessStartInfo("cmd.exe")
            {
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };

            Process p = Process.Start(psi);

            StreamWriter sw = p.StandardInput;
            StreamReader sr = p.StandardOutput;
            foreach(var item in pressedList)
            {
                sw.WriteLine(item);
            }
            sr.Close();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            this.pictureBox1.Image = ((System.Drawing.Image)(Properties.Resources.iconhover1));
        }
        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox1.Image = ((System.Drawing.Image)(Properties.Resources.icon));
        }
        protected void RestartGame()
        {
            Board = new int[4, 4] { { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 }, { 0, 0, 0, 0 } };
            POINTS = 0;
            FirstPoint();
            RefreshStatus();
            pressedList.Add(Keys.Space.ToString());
        }
        protected void label1_Click(object sender, EventArgs e)
        {
            RestartGame();
        }
        private void label1_MouseLeave(object sender, EventArgs e)
        {
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(110)))), ((int)(((byte)(101)))));
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(228)))), ((int)(((byte)(218)))));
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
