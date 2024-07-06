using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace AplikacjaDoPrzegladaniaZdjec
{
    public partial class ZdjeciaForm : Form
    {
        // Ścieżka do aktualnie wybranego obrazu
        private string currentImagePath;
        // Oryginalny obraz przed edycją
        private Image originalImage;
        // Flaga określająca, czy pokaz slajdów jest aktywny
        private bool isSlideShowActive = false;
        // Timer do pokazu slajdów
        private System.Windows.Forms.Timer slideShowTimer;
        // Ścieżka do ostatnio otwartego folderu
        private string lastOpenedFolderPath;
        // Ścieżka do folderu ulubionych
        private string favoritesFolderPath;

        public ZdjeciaForm()
        {
            InitializeComponent();
            // Inicjalizacja ścieżki do folderu ulubionych
            favoritesFolderPath = Path.Combine(Application.StartupPath, "Favorites");
            if (!Directory.Exists(favoritesFolderPath))
            {
                Directory.CreateDirectory(favoritesFolderPath);
            }
            // Inicjalizacja timera do pokazu slajdów
            slideShowTimer = new System.Windows.Forms.Timer();
            slideShowTimer.Interval = 3000; // domyślnie 3 sekundy
            slideShowTimer.Tick += SlideShowTimer_Tick;

            // Dodanie obsługi zdarzenia zmiany rozmiaru
            this.Resize += new EventHandler(Form1_Resize);
        }

        // Aktualizacja pozycji elementów na formularzu w zależności od rozmiaru okna
        private void DynamicPosition()
        {
            int bottomPadding = 10;

            btnApplyDateFilter.Location = new Point(80, panelInfo.Height - btnApplyDateFilter.Height - btnRefresh.Height - bottomPadding);
            datePickerTo.Location = new Point(140, btnApplyDateFilter.Top - datePickerTo.Height - bottomPadding);
            datePickerFrom.Location = new Point(10, btnApplyDateFilter.Top - datePickerFrom.Height - bottomPadding);
        }

        // Obsługa zdarzenia zmiany rozmiaru formularza
        private void Form1_Resize(object sender, EventArgs e)
        {
            DynamicPosition();
        }

        // Obsługa kliknięcia menu "Otwórz folder"
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

                DynamicPosition();
            }
        }

        // Obsługa kliknięcia menu "Ulubione"
        private void ulubioneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadImages(favoritesFolderPath);
            btnAddToFavorites.Visible = false;
            btnRemoveFromFavorites.Visible = false;
        }

        // Zapis obrazu do pliku w wybranym formacie
        private void SaveImage(string path)
        {
            if (pictureBox.Image != null)
            {
                ImageFormat format;

                // Określenie formatu pliku na podstawie rozszerzenia
                switch (Path.GetExtension(path).ToLower())
                {
                    case ".bmp":
                        format = ImageFormat.Bmp;
                        break;
                    case ".png":
                        format = ImageFormat.Png;
                        break;
                    default:
                        format = ImageFormat.Jpeg;
                        break;
                }

                try
                {
                    using (Bitmap bmp = new Bitmap(pictureBox.Image))
                    {
                        bmp.Save(path, format);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Błąd podczas zapisywania pliku, upewnij się, że plik ma unikalną nazwę", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Obsługa kliknięcia menu "Zapisz jako"
        private void zapiszJakoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentImagePath != null)
            {
                saveFileDialog.FileName = Path.GetFileNameWithoutExtension(currentImagePath) + ".jpg";
                saveFileDialog.Filter = "JPEG Image|*.jpg|PNG Image|*.png|Bitmap Image|*.bmp";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    SaveImage(saveFileDialog.FileName);
                }
            }
        }

        // Obsługa kliknięcia menu "Prezentacja"
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

        // Obsługa kliknięcia menu "Filtry"
        private void filtryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelFiltry.Visible = !panelFiltry.Visible;
        }

        // Obsługa kliknięcia menu "Instrukcja"
        private void instrukcjaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Użyj strzałek do nawigacji po zdjęciach lub kliknij na zdjęcie z listy.", "Instrukcja", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Obsługa zdarzenia timera pokazu slajdów
        private void SlideShowTimer_Tick(object sender, EventArgs e)
        {
            if (listView.Items.Count > 0)
            {
                int nextIndex = (listView.SelectedItems.Count > 0 ? listView.SelectedItems[0].Index + 1 : 0) % listView.Items.Count;
                listView.Items[nextIndex].Selected = true;
            }
        }

        // Obsługa zmiany zaznaczenia w ListView
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

                // Czy zdjęcie jest w ulubionych
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

        // informacje o obrazie
        private void UpdateImageInfo(string imagePath)
        {
            FileInfo fileInfo = new FileInfo(imagePath);
            Image image = Image.FromFile(imagePath);
            labelInfo.Text = $"Nazwa pliku: {fileInfo.Name}\n" +
                             $"Rozmiar: {image.Width} x {image.Height}\n" +
                             $"Data utworzenia: {fileInfo.CreationTime}\n" +
                             $"Rozmiar pliku: {fileInfo.Length / 1024} KB";
        }

        // Zastosowanie filtra grayscale
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

        // Zastosowanie filtra sepia
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

        // Zastosowanie filtra invert
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

        // Obsługa kliknięcia przycisku odświeżania
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(lastOpenedFolderPath))
            {
                LoadImages(lastOpenedFolderPath);
            }
        }

        // Obsługa kliknięcia przycisku filtra grayscale
        private void btnGrayScale_Click(object sender, EventArgs e)
        {
            if (originalImage != null)
            {
                pictureBox.Image = ApplyGrayScaleFilter(new Bitmap(originalImage));
            }
        }

        // Obsługa kliknięcia przycisku filtra sepia
        private void btnSepia_Click(object sender, EventArgs e)
        {
            if (originalImage != null)
            {
                pictureBox.Image = ApplySepiaFilter(new Bitmap(originalImage));
            }
        }

        // Obsługa kliknięcia przycisku filtra invert
        private void btnInvert_Click(object sender, EventArgs e)
        {
            if (originalImage != null)
            {
                pictureBox.Image = ApplyInvertFilter(new Bitmap(originalImage));
            }
        }

        // Obsługa kliknięcia przycisku usunięcia wszystkich filtrów
        private void btnNoFilter_Click(object sender, EventArgs e)
        {
            if (originalImage != null)
            {
                pictureBox.Image = originalImage;
            }
        }

        // Załadowanie obrazów z podanego folderu
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

        // Obsługa wyszukiwania zdjęć
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
                        break; // wybierz pierwszy pasujący
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

        // Obsługa wejścia do pola wyszukiwania
        private void searchTextBox_Enter(object sender, EventArgs e)
        {
            if (searchTextBox.Text == "Wyszukaj zdjęcie..")
            {
                searchTextBox.Text = string.Empty;
                searchTextBox.ForeColor = Color.Black;
            }
        }

        // Obsługa opuszczenia pola wyszukiwania
        private void searchTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchTextBox.Text))
            {
                searchTextBox.ForeColor = Color.Gray;
                searchTextBox.Text = "Wyszukaj zdjęcie..";
            }
        }

        // Obsługa kliknięcia menu "Usuń zdjęcie"
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
                    listView.Items.Remove(listView.SelectedItems[0]); // Usunięcie elementu z ListView
                    pictureBox.Image = null;
                    labelInfo.Text = string.Empty;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Błąd podczas usuwania zdjęcia: {filePath}\n{ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Obsługa kliknięcia przycisku dodania do ulubionych
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

        // Obsługa kliknięcia przycisku usunięcia z ulubionych
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
                        if(lastOpenedFolderPath != null)
                        {
                            LoadImages(lastOpenedFolderPath);
                        }
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

        // Obsługa kliknięcia przycisku obrotu w lewo
        private void btnRotateLeft_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                pictureBox.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                pictureBox.Refresh();
            }
        }

        // Obsługa kliknięcia przycisku obrotu w prawo
        private void btnRotateRight_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                pictureBox.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                pictureBox.Refresh();
            }
        }

        // Obsługa kliknięcia przycisku lustrzanego odbicia
        private void btnFlipHorizontal_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                pictureBox.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                pictureBox.Refresh();
            }
        }

        // Obsługa kliknięcia przycisku obrotu o 180 stopni
        private void btnRotate180_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                pictureBox.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                pictureBox.Refresh();
            }
        }

        // Załadowanie obrazów z podanego folderu z uwzględnieniem filtrów daty
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

        // Obsługa kliknięcia przycisku zastosowania filtra daty
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
