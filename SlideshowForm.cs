using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AplikacjaDoPrzegladaniaZdjec
{
    public partial class SlideshowForm : Form
    {
        private List<string> imagePaths;
        private int currentIndex;
        private System.Windows.Forms.Timer slideshowTimer;
        private PictureBox pictureBox;
        private Button closeButton;
        private Button stopButton;
        private Button speed1sButton;
        private Button speed5sButton;
        private Button speed10sButton;
        private FlowLayoutPanel buttonPanel;

        public SlideshowForm()
        {
            InitializeComponent();
            slideshowTimer = new System.Windows.Forms.Timer();
            slideshowTimer.Interval = 3000; // Default 3 seconds
            slideshowTimer.Tick += SlideshowTimer_Tick;

            pictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.FromArgb(30, 30, 30)
            };
            Controls.Add(pictureBox);

            closeButton = new Button
            {
                Text = "Zamknij",
                Dock = DockStyle.Top,
                Height = 30,
                Width = 100,
                BackColor = Color.FromArgb(128, 255, 255, 255), // Semi-transparent white
                ForeColor = Color.Black,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            closeButton.Click += CloseButton_Click;
            Controls.Add(closeButton);

            buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                FlowDirection = FlowDirection.LeftToRight,
                BackColor = Color.FromArgb(45, 45, 48)
            };

            stopButton = new Button
            {
                Text = "Zatrzymaj",
                Height = 30,
                Width = 100,
                BackColor = Color.FromArgb(128, 255, 255, 255), // Semi-transparent white
                ForeColor = Color.Black,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            stopButton.Click += StopButton_Click;
            buttonPanel.Controls.Add(stopButton);

            speed1sButton = new Button
            {
                Text = "1s",
                Height = 30,
                Width = 50,
                BackColor = Color.FromArgb(128, 255, 255, 255), // Semi-transparent white
                ForeColor = Color.Black,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            speed1sButton.Click += (sender, e) => SetSlideshowSpeed(1000);
            buttonPanel.Controls.Add(speed1sButton);

            speed5sButton = new Button
            {
                Text = "5s",
                Height = 30,
                Width = 50,
                BackColor = Color.FromArgb(128, 255, 255, 255), // Semi-transparent white
                ForeColor = Color.Black,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            speed5sButton.Click += (sender, e) => SetSlideshowSpeed(5000);
            buttonPanel.Controls.Add(speed5sButton);

            speed10sButton = new Button
            {
                Text = "10s",
                Height = 30,
                Width = 50,
                BackColor = Color.FromArgb(128, 255, 255, 255), // Semi-transparent white
                ForeColor = Color.Black,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            speed10sButton.Click += (sender, e) => SetSlideshowSpeed(10000);
            buttonPanel.Controls.Add(speed10sButton);

            Controls.Add(buttonPanel);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            StopSlideshow();
            this.Close();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            StopSlideshow();
        }

        private void SetSlideshowSpeed(int interval)
        {
            slideshowTimer.Interval = interval;
        }

        public void LoadImages(string folderPath)
        {
            imagePaths = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly)
                .Where(f => f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                            f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                            f.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                            f.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (imagePaths.Any())
            {
                currentIndex = 0;
                pictureBox.Image = Image.FromFile(imagePaths[currentIndex]);
            }
        }

        public void StartSlideshow()
        {
            slideshowTimer.Start();
        }

        public void StopSlideshow()
        {
            slideshowTimer.Stop();
        }

        private void SlideshowTimer_Tick(object sender, EventArgs e)
        {
            if (imagePaths == null || !imagePaths.Any()) return;

            currentIndex = (currentIndex + 1) % imagePaths.Count;
            pictureBox.Image = Image.FromFile(imagePaths[currentIndex]);
        }
    }
}
