using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace AplikacjaDoPrzegladaniaZdjec
{
    public partial class Form1 : Form
    {
        private string currentImagePath;
        private Image originalImage;
        private bool isSlideShowActive = false;
        private System.Windows.Forms.Timer slideShowTimer;
        private string lastOpenedFolderPath;
        private string favoritesFolderPath;

        public Form1()
        {
            InitializeComponent();
            favoritesFolderPath = Path.Combine(Application.StartupPath, "Favorites");
            if (!Directory.Exists(favoritesFolderPath))
            {
                Directory.CreateDirectory(favoritesFolderPath);
            }
            slideShowTimer = new System.Windows.Forms.Timer();
            slideShowTimer.Interval = 3000; // Default 3 seconds
            slideShowTimer.Tick += SlideShowTimer_Tick;

            // Add Resize event handler
            this.Resize += new EventHandler(Form1_Resize);
        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            PositionDynamicControls();
        }

        private void otworzFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                lastOpenedFolderPath = folderBrowserDialog.SelectedPath;
                LoadImages(lastOpenedFolderPath);

                datePickerFrom.Visible = true;
                datePickerTo.Visible = true;
                btnApplyDateFilter.Visible = true;
                searchTextBox.Visible = true;

                PositionDynamicControls();
            }
        }

        private void PositionDynamicControls()
        {

            int bottomPadding = 5;

            btnApplyDateFilter.Location = new Point(80, panelInfo.Height - btnApplyDateFilter.Height - btnRefresh.Height - bottomPadding);
            datePickerTo.Location = new Point(120, btnApplyDateFilter.Top - datePickerTo.Height - bottomPadding);
            datePickerFrom.Location = new Point(10, btnApplyDateFilter.Top - datePickerFrom.Height - bottomPadding);
            searchTextBox.Location = new Point(10, datePickerFrom.Top - searchTextBox.Height - bottomPadding);
        }

        private void ulubioneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadImages(favoritesFolderPath);
            btnAddToFavorites.Visible = false;
            btnRemoveFromFavorites.Visible = false;
        }

        private void zapiszJakoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentImagePath != null)
            {
                saveFileDialog.FileName = Path.GetFileNameWithoutExtension(currentImagePath) + ".jpg";
                saveFileDialog.Filter = "JPEG Image|*.jpg|PNG Image|*.png|Bitmap Image|*.bmp";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    SaveImageAsJpg(saveFileDialog.FileName);
                }
            }
        }

        private void prezentacjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lastOpenedFolderPath))
            {
                MessageBox.Show("Nie wybrano żadnego folderu. Najpierw otwórz folder z obrazami.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var slideshowForm = new SlideshowForm();
            slideshowForm.LoadImages(lastOpenedFolderPath);
            slideshowForm.WindowState = FormWindowState.Maximized;
            slideshowForm.FormBorderStyle = FormBorderStyle.None;
            slideshowForm.StartPosition = FormStartPosition.CenterScreen;
            slideshowForm.Show();
            slideshowForm.StartSlideshow();
        }

        private void filtryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelFiltry.Visible = !panelFiltry.Visible;
        }

        private void instrukcjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Użyj strzałek do nawigacji po zdjęciach.", "Instrukcja", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SlideShowTimer_Tick(object sender, EventArgs e)
        {
            if (listView.Items.Count > 0)
            {
                int nextIndex = (listView.SelectedItems.Count > 0 ? listView.SelectedItems[0].Index + 1 : 0) % listView.Items.Count;
                listView.Items[nextIndex].Selected = true;
            }
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                currentImagePath = listView.SelectedItems[0].Tag.ToString();
                originalImage = Image.FromFile(currentImagePath);
                pictureBox.Image = originalImage;
                UpdateImageInfo(currentImagePath);
                btnRotateLeft.Visible = true;
                btnRotateRight.Visible = true;
                btnFlipHorizontal.Visible = true;
                btnRotate180.Visible = true;

                // Check if the image is a favorite
                string favoritePath = Path.Combine(favoritesFolderPath, Path.GetFileName(currentImagePath));
                if (File.Exists(favoritePath))
                {
                    btnAddToFavorites.Visible = false;
                    btnRemoveFromFavorites.Visible = true;

                }
                else
                {
                    btnAddToFavorites.Visible = true;
                    btnRemoveFromFavorites.Visible = false;
                }
            }
        }

        private void UpdateImageInfo(string imagePath)
        {
            FileInfo fileInfo = new FileInfo(imagePath);
            Image image = Image.FromFile(imagePath);
            labelInfo.Text = $"Nazwa pliku: {fileInfo.Name}\n" +
                             $"Rozmiar: {image.Width} x {image.Height}\n" +
                             $"Data utworzenia: {fileInfo.CreationTime}\n" +
                             $"Rozmiar pliku: {fileInfo.Length / 1024} KB";
        }

        private Bitmap ApplyGrayScaleFilter(Bitmap original)
        {
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);
            Graphics g = Graphics.FromImage(newBitmap);

            ColorMatrix colorMatrix = new ColorMatrix(new float[][] {
                new float[] {.3f, .3f, .3f, 0, 0},
                new float[] {.59f, .59f, .59f, 0, 0},
                new float[] {.11f, .11f, .11f, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {0, 0, 0, 0, 1}
            });

            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(colorMatrix);

            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
            g.Dispose();
            return newBitmap;
        }

        private Bitmap ApplySepiaFilter(Bitmap original)
        {
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);
            Graphics g = Graphics.FromImage(newBitmap);

            ColorMatrix colorMatrix = new ColorMatrix(new float[][] {
                new float[] {.393f, .349f, .272f, 0, 0},
                new float[] {.769f, .686f, .534f, 0, 0},
                new float[] {.189f, .168f, .131f, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {0, 0, 0, 0, 1}
            });

            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(colorMatrix);

            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
            g.Dispose();
            return newBitmap;
        }

        private Bitmap ApplyInvertFilter(Bitmap original)
        {
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    Color originalColor = original.GetPixel(x, y);
                    Color invertedColor = Color.FromArgb(255 - originalColor.R, 255 - originalColor.G, 255 - originalColor.B);
                    newBitmap.SetPixel(x, y, invertedColor);
                }
            }

            return newBitmap;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lastOpenedFolderPath))
            {
                LoadImages(lastOpenedFolderPath);
            }
        }

        private void btnGrayScale_Click(object sender, EventArgs e)
        {
            if (originalImage != null)
            {
                pictureBox.Image = ApplyGrayScaleFilter(new Bitmap(originalImage));
            }
        }

        private void btnSepia_Click(object sender, EventArgs e)
        {
            if (originalImage != null)
            {
                pictureBox.Image = ApplySepiaFilter(new Bitmap(originalImage));
            }
        }

        private void btnInvert_Click(object sender, EventArgs e)
        {
            if (originalImage != null)
            {
                pictureBox.Image = ApplyInvertFilter(new Bitmap(originalImage));
            }
        }

        private void btnNoFilter_Click(object sender, EventArgs e)
        {
            if (originalImage != null)
            {
                pictureBox.Image = originalImage;
            }
        }

        private void SaveImageAsJpg(string path)
        {
            if (pictureBox.Image != null)
            {
                using (Bitmap bmp = new Bitmap(pictureBox.Image))
                {
                    bmp.Save(path, ImageFormat.Jpeg);
                }
            }
        }

        private void LoadImages(string folderPath)
        {
            listView.Items.Clear();
            imageList.Images.Clear();
            var files = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly)
                .Where(f => f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                            f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                            f.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                            f.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase));

            foreach (var file in files)
            {
                try
                {
                    using (Image image = Image.FromFile(file))
                    {
                        imageList.Images.Add((Image)image.Clone());
                    }
                    ListViewItem item = new ListViewItem
                    {
                        ImageIndex = imageList.Images.Count - 1,
                        Text = Path.GetFileName(file),
                        Tag = file
                    };

                    // Czy zdjęcie jest w ulubionych
                    string favoritePath = Path.Combine(favoritesFolderPath, Path.GetFileName(file));
                    if (File.Exists(favoritePath))
                    {
                        item.Text += " ♥";
                    }

                    listView.Items.Add(item);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {file}\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void searchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Odznacz wszystkie zaznaczone elementy
                foreach (ListViewItem selectedItem in listView.SelectedItems)
                {
                    selectedItem.Selected = false;
                }

                string searchText = searchTextBox.Text.ToLower();
                bool found = false;
                foreach (ListViewItem item in listView.Items)
                {
                    if (item.Text.ToLower().Contains(searchText))
                    {
                        item.Selected = true;
                        item.EnsureVisible();
                        listView.Select();
                        found = true;
                        break; // wybierz pierwszy pasujacy
                    }
                }

                if (!found)
                {
                    MessageBox.Show("Brak wyników", "Wyszukiwanie", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Czyszczenie pola tekstowego po wyszukiwaniu
                searchTextBox.Text = string.Empty;
                searchTextBox.ForeColor = Color.Gray;


                e.SuppressKeyPress = true;
            }
        }

        private void searchTextBox_Enter(object sender, EventArgs e)
        {
            if (searchTextBox.Text == "Wyszukaj zdjęcie..")
            {
                searchTextBox.Text = string.Empty;
                searchTextBox.ForeColor = Color.Black;
            }
        }

        private void searchTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchTextBox.Text))
            {
                searchTextBox.ForeColor = Color.Gray;
                searchTextBox.Text = "Wyszukaj zdjęcie..";
            }
        }

        private void usunZdjecieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                string filePath = listView.SelectedItems[0].Tag.ToString();
                try
                {
                    // Upewnij się, że PictureBox nie używa już tego obrazu
                    if (pictureBox.Image != null)
                    {
                        pictureBox.Image.Dispose();
                        pictureBox.Image = null;
                    }

                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    File.Delete(filePath);
                    listView.Items.Remove(listView.SelectedItems[0]); // Remove the item from the listView
                    pictureBox.Image = null;
                    labelInfo.Text = string.Empty;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Błąd podczas usuwania zdjęcia: {filePath}\n{ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAddToFavorites_Click(object sender, EventArgs e)
        {
            if (currentImagePath != null)
            {
                string fileName = Path.GetFileName(currentImagePath);
                string destPath = Path.Combine(favoritesFolderPath, fileName);

                if (File.Exists(destPath))
                {
                    MessageBox.Show("Zdjęcie jest już w ulubionych.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    try
                    {
                        File.Copy(currentImagePath, destPath, true);
                        MessageBox.Show("Zdjęcie zostało dodane do ulubionych.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnAddToFavorites.Visible = false;
                        btnRemoveFromFavorites.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Błąd podczas dodawania zdjęcia do ulubionych: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnRemoveFromFavorites_Click(object sender, EventArgs e)
        {
            if (currentImagePath != null)
            {
                string fileName = Path.GetFileName(currentImagePath);
                string destPath = Path.Combine(favoritesFolderPath, fileName);

                if (File.Exists(destPath))
                {
                    try
                    {
                        // Upewnij się, że PictureBox nie używa już tego obrazu
                        if (pictureBox.Image != null)
                        {
                            pictureBox.Image.Dispose();
                            pictureBox.Image = null;
                        }

                        GC.Collect();
                        GC.WaitForPendingFinalizers();

                        File.Delete(destPath);
                        MessageBox.Show("Zdjęcie zostało usunięte z ulubionych.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadImages(lastOpenedFolderPath);
                        btnRemoveFromFavorites.Visible = false;
                        btnAddToFavorites.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Błąd podczas usuwania zdjęcia z ulubionych: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void btnRotateLeft_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                pictureBox.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                pictureBox.Refresh();
            }
        }

        private void btnRotateRight_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                pictureBox.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                pictureBox.Refresh();
            }
        }

        private void btnFlipHorizontal_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                pictureBox.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                pictureBox.Refresh();
            }
        }
        private void btnRotate180_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                pictureBox.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                pictureBox.Refresh();
            }
        }

        private void LoadImages2(string folderPath, DateTime? fromDate = null, DateTime? toDate = null)
        {
            listView.Items.Clear();
            imageList.Images.Clear();
            var files = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly)
                .Where(f => f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                            f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                            f.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                            f.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase));

            foreach (var file in files)
            {
                try
                {
                    FileInfo fileInfo = new FileInfo(file);
                    DateTime creationDate = fileInfo.CreationTime.Date;

                    if (fromDate.HasValue && creationDate < fromDate.Value)
                        continue;

                    if (toDate.HasValue && creationDate > toDate.Value)
                        continue;

                    using (Image image = Image.FromFile(file))
                    {
                        imageList.Images.Add((Image)image.Clone());
                    }
                    ListViewItem item = new ListViewItem
                    {
                        ImageIndex = imageList.Images.Count - 1,
                        Text = Path.GetFileName(file),
                        Tag = file
                    };

                    // Czy zdjęcie jest w ulubionych
                    string favoritePath = Path.Combine(favoritesFolderPath, Path.GetFileName(file));
                    if (File.Exists(favoritePath))
                    {
                        item.Text += " ♥";
                    }
                    listView.Items.Add(item);
                }

                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {file}\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnApplyDateFilter_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lastOpenedFolderPath))
            {
                DateTime fromDate = datePickerFrom.Value.Date;
                DateTime toDate = datePickerTo.Value.Date;

                LoadImages2(lastOpenedFolderPath, fromDate, toDate);
            }
        }



    }
}