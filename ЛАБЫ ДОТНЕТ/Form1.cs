using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab10NET
{
    public partial class Form1 : Form
    {
        

       
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        private Bitmap bmp_for_draw;
        private string full_name_of_image;
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Red), 10, 10, 100, 100);
        }

        Graphics g;
        private Font fnt = new Font("Arial", 10);






        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.BackColor = Color.White;
            // Connect the Paint event of the PictureBox to the event handler method.
            
        }

        private void mDAToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                string format = full_name_of_image.Substring(full_name_of_image.Length - 4, 4);
                SaveFileDialog savedialog = new SaveFileDialog();
                savedialog.OverwritePrompt = true; // если файл с указанным именем уже существует
                savedialog.CheckPathExists = true; //если пользователь вводит неверный путь или имя файла
                savedialog.ShowHelp = true;
                savedialog.Filter = "Image Files(*.BMP)|*.BMP|Image Files(*.JPG)|*.JPG|ImageFiles(*.GIF) | *.GIF | Image Files(*.PNG) | *.PNG | All files(*.*) | *.* ";
                if (savedialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Bitmap MM = new Bitmap(pictureBox1.Image);
                        MM.Save(savedialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    catch
                    {
                        MessageBox.Show("Impossible to save image", "FATAL ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void mdaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_dialog = new OpenFileDialog();
            open_dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)| *.BMP; *.JPG; *.GIF; *.PNG | All files(*.*) | *.* ";
            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    full_name_of_image = open_dialog.FileName;
                    bmp_for_draw = new Bitmap(open_dialog.FileName);
                    pictureBox1.Image = bmp_for_draw;
                    pictureBox1.Invalidate();
                }
                catch
                {
                    DialogResult rezult = MessageBox.Show("Impossible to open selected file",
                    "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        
        Random rnd = new Random();
        int x, y;
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            /*e.Graphics.DrawLine(
                new Pen(Color.Red, 2f),
                new Point(0, 0),
                new Point(pictureBox1.Size.Width, pictureBox1.Size.Height));*/

            e.Graphics.DrawEllipse(
                new Pen(Color.Red, 2f),
                x, y, rnd.Next(0, pictureBox1.Size.Width), rnd.Next(0, pictureBox1.Size.Width));
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            x = e.X; y = e.Y;
            

            
        }
        
        private void netToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void randomCircleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.Controls.Add(pictureBox1);
        }
    }
}
