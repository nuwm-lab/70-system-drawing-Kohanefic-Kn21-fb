namespace Lab_7
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        private bool drawGraph = false;
        private Button drawButton;

        public Form1()
        {
            InitializeComponent();

            drawButton = new Button();
            drawButton.Text = "Малювати графік";
            drawButton.Size = new Size(150, 40);
            drawButton.Location = new Point(10, 10);
            drawButton.Click += new EventHandler(DrawButton_Click);

            this.Controls.Add(drawButton);
            this.Resize += new EventHandler(Form1_Resize);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void DrawButton_Click(object sender, EventArgs e)
        {
            drawGraph = true;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (drawGraph)
            {
                DrawGraph(e.Graphics);
            }
        }

        private void DrawGraph(Graphics graph)
        {
            float width = this.ClientSize.Width;
            float height = this.ClientSize.Height;

            float xMin = 2.3f;
            float xMax = 7.8f;
            float deltaX = 0.9f;

            float xScale = width / (xMax - xMin);
            float yScale = height / 20f;

            Color color = Color.FromName("SlateBlue");
            Pen pen = new Pen(color, 2);

            graph.DrawLine(Pens.Black, 0, height / 2, width, height / 2);
            graph.DrawLine(Pens.Black, width / 2, 0, width / 2, height);

            Font axisFont = new Font("Arial", 12);
            Brush axisBrush = Brushes.Black;

            string xAxisLabel = "X";
            graph.DrawString(xAxisLabel, axisFont, axisBrush, width - 25, height / 2 - 30);

            string yAxisLabel = "Y";
            graph.DrawString(yAxisLabel, axisFont, axisBrush, width / 2 - 30, 5);

            for (float x = xMin; x <= xMax; x += deltaX)
            {
                float screenX = (x - xMin) * xScale;
                graph.DrawLine(Pens.Black, screenX, height / 2 - 5, screenX, height / 2 + 5);
                graph.DrawString(x.ToString("0.0"), axisFont, axisBrush, screenX - 10, height / 2 + 10);
            }

            for (int y = -10; y <= 10; y += 2)
            {
                float screenY = height / 2 - y * yScale;
                graph.DrawLine(Pens.Black, width / 2 - 5, screenY, width / 2 + 5, screenY);
                graph.DrawString(y.ToString(), axisFont, axisBrush, width / 2 + 10, screenY - 10);
            }

            float prevX = 0, prevY = 0;
            bool isFirstPoint = true;

            for (float x = xMin; x <= xMax; x += deltaX)
            {
                float y = (6 * x + 4) / (float)(Math.Sin(3 * x) - x);

                float screenX = (x - xMin) * xScale;
                float screenY = height / 2 - y * yScale;

                if (!isFirstPoint)
                {
                    graph.DrawLine(pen, prevX, prevY, screenX, screenY);
                }

                prevX = screenX;
                prevY = screenY;
                isFirstPoint = false;
            }
        }
    }
}

