namespace AplikacjaDoPrzegladaniaZdjec
{
    partial class ZdjeciaForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem plikToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem otworzFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zapiszJakoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem edycjaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filtryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pomocToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem instrukcjaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem prezentacjaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ulubioneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usunZdjecieToolStripMenuItem;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.Panel panelPodglad;
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.Panel panelFiltry;
        private System.Windows.Forms.FlowLayoutPanel panelButtons;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnGrayScale;
        private System.Windows.Forms.Button btnSepia;
        private System.Windows.Forms.Button btnInvert;
        private System.Windows.Forms.Button btnNoFilter;
        private System.Windows.Forms.Button btnAddToFavorites;
        private System.Windows.Forms.Button btnRemoveFromFavorites;
        private System.Windows.Forms.Button btnRotateLeft;
        private System.Windows.Forms.Button btnRotateRight;
        private System.Windows.Forms.Button btnFlipHorizontal;
        private System.Windows.Forms.Button btnRotate180;
        private System.Windows.Forms.DateTimePicker datePickerFrom;
        private System.Windows.Forms.DateTimePicker datePickerTo;
        private System.Windows.Forms.Button btnApplyDateFilter;

        /// <summary>
        /// Zwolnienie wszystkich używanych zasobów.
        /// </summary>
        /// <param name="disposing">true, jeśli zarządzane zasoby mają być zwolnione; w przeciwnym razie false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Metoda wymagana do obsługi Projektanta - nie modyfikować jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            Icon = new System.Drawing.Icon(Path.Combine(Application.StartupPath, "Icon", "app.ico"));
            components = new System.ComponentModel.Container();
            menuStrip = new MenuStrip();
            plikToolStripMenuItem = new ToolStripMenuItem();
            otworzFolderToolStripMenuItem = new ToolStripMenuItem();
            zapiszJakoToolStripMenuItem = new ToolStripMenuItem();
            usunZdjecieToolStripMenuItem = new ToolStripMenuItem();
            edycjaToolStripMenuItem = new ToolStripMenuItem();
            filtryToolStripMenuItem = new ToolStripMenuItem();
            pomocToolStripMenuItem = new ToolStripMenuItem();
            instrukcjaToolStripMenuItem = new ToolStripMenuItem();
            prezentacjaToolStripMenuItem = new ToolStripMenuItem();
            ulubioneToolStripMenuItem = new ToolStripMenuItem();
            listView = new ListView();
            imageList = new ImageList(components);
            panelPodglad = new Panel();
            pictureBox = new PictureBox();
            panelInfo = new Panel();
            searchTextBox = new TextBox();
            labelInfo = new Label();
            panelFiltry = new Panel();
            btnGrayScale = new Button();
            btnSepia = new Button();
            btnInvert = new Button();
            btnNoFilter = new Button();
            folderBrowserDialog = new FolderBrowserDialog();
            saveFileDialog = new SaveFileDialog();
            btnRefresh = new Button();
            btnRotateLeft = new Button();
            btnRotateRight = new Button();
            btnFlipHorizontal = new Button();
            btnAddToFavorites = new Button();
            btnRemoveFromFavorites = new Button();
            panelButtons = new FlowLayoutPanel();
            datePickerFrom = new System.Windows.Forms.DateTimePicker();
            datePickerTo = new System.Windows.Forms.DateTimePicker();
            btnApplyDateFilter = new System.Windows.Forms.Button();

            menuStrip.SuspendLayout();
            panelPodglad.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            panelInfo.SuspendLayout();
            panelFiltry.SuspendLayout();
            panelButtons.SuspendLayout();
            SuspendLayout();

            searchTextBox.KeyDown += new KeyEventHandler(searchTextBox_KeyDown);
            searchTextBox.Enter += new EventHandler(searchTextBox_Enter);
            searchTextBox.Leave += new EventHandler(searchTextBox_Leave);

            // MenuStrip
            menuStrip.BackColor = Color.FromArgb(30, 30, 30);
            menuStrip.Font = new Font("Segoe UI", 12F);
            menuStrip.ForeColor = Color.White;
            menuStrip.Items.AddRange(new ToolStripItem[] { plikToolStripMenuItem, edycjaToolStripMenuItem, pomocToolStripMenuItem, prezentacjaToolStripMenuItem, ulubioneToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Padding = new Padding(7, 2, 0, 2);
            menuStrip.Size = new Size(933, 29);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "menuStrip";
            // Plik Menu
            plikToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { otworzFolderToolStripMenuItem, zapiszJakoToolStripMenuItem, usunZdjecieToolStripMenuItem });
            plikToolStripMenuItem.ForeColor = Color.White;
            plikToolStripMenuItem.Name = "plikToolStripMenuItem";
            plikToolStripMenuItem.Size = new Size(47, 25);
            plikToolStripMenuItem.Text = "Plik";
            // Otwórz Folder
            otworzFolderToolStripMenuItem.Name = "otworzFolderToolStripMenuItem";
            otworzFolderToolStripMenuItem.Size = new Size(176, 26);
            otworzFolderToolStripMenuItem.Text = "Otwórz folder";
            otworzFolderToolStripMenuItem.Click += otworzFolderToolStripMenuItem_Click;
            // Zapisz Jako
            zapiszJakoToolStripMenuItem.Name = "zapiszJakoToolStripMenuItem";
            zapiszJakoToolStripMenuItem.Size = new Size(176, 26);
            zapiszJakoToolStripMenuItem.Text = "Zapisz jako";
            zapiszJakoToolStripMenuItem.Click += zapiszJakoToolStripMenuItem_Click;
            // Usuń Zdjęcie
            usunZdjecieToolStripMenuItem.Name = "usunZdjecieToolStripMenuItem";
            usunZdjecieToolStripMenuItem.Size = new Size(176, 26);
            usunZdjecieToolStripMenuItem.Text = "Usuń zdjęcie";
            usunZdjecieToolStripMenuItem.Click += usunZdjecieToolStripMenuItem_Click;
            // Edycja Menu
            edycjaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { filtryToolStripMenuItem });
            edycjaToolStripMenuItem.ForeColor = Color.White;
            edycjaToolStripMenuItem.Name = "edycjaToolStripMenuItem";
            edycjaToolStripMenuItem.Size = new Size(66, 25);
            edycjaToolStripMenuItem.Text = "Edycja";
            // Filtry
            filtryToolStripMenuItem.Name = "filtryToolStripMenuItem";
            filtryToolStripMenuItem.Size = new Size(115, 26);
            filtryToolStripMenuItem.Text = "Filtry";
            filtryToolStripMenuItem.Click += filtryToolStripMenuItem_Click;
            // Pomoc Menu
            pomocToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { instrukcjaToolStripMenuItem });
            pomocToolStripMenuItem.ForeColor = Color.White;
            pomocToolStripMenuItem.Name = "pomocToolStripMenuItem";
            pomocToolStripMenuItem.Size = new Size(69, 25);
            pomocToolStripMenuItem.Text = "Pomoc";
            // Instrukcja
            instrukcjaToolStripMenuItem.Name = "instrukcjaToolStripMenuItem";
            instrukcjaToolStripMenuItem.Size = new Size(147, 26);
            instrukcjaToolStripMenuItem.Text = "Instrukcja";
            instrukcjaToolStripMenuItem.Click += instrukcjaToolStripMenuItem_Click;
            // Prezentacja Menu
            prezentacjaToolStripMenuItem.ForeColor = Color.White;
            prezentacjaToolStripMenuItem.Name = "prezentacjaToolStripMenuItem";
            prezentacjaToolStripMenuItem.Size = new Size(101, 25);
            prezentacjaToolStripMenuItem.Text = "Prezentacja";
            prezentacjaToolStripMenuItem.Click += prezentacjaToolStripMenuItem_Click;
            // Ulubione Menu
            ulubioneToolStripMenuItem.ForeColor = Color.White;
            ulubioneToolStripMenuItem.Name = "ulubioneToolStripMenuItem";
            ulubioneToolStripMenuItem.Size = new Size(85, 25);
            ulubioneToolStripMenuItem.Text = "Ulubione";
            ulubioneToolStripMenuItem.Click += ulubioneToolStripMenuItem_Click;
            // ListView
            listView.BackColor = Color.FromArgb(45, 45, 48);
            listView.Dock = DockStyle.Bottom;
            listView.ForeColor = Color.White;
            listView.LargeImageList = imageList;
            listView.Location = new Point(0, 403);
            listView.Margin = new Padding(4, 3, 4, 3);
            listView.Name = "listView";
            listView.Size = new Size(933, 142);
            listView.TabIndex = 2;
            listView.UseCompatibleStateImageBehavior = false;
            listView.SelectedIndexChanged += listView_SelectedIndexChanged;
            // ImageList
            imageList.ColorDepth = ColorDepth.Depth32Bit;
            imageList.ImageSize = new Size(100, 100);
            imageList.TransparentColor = Color.Transparent;
            // Button Add to Favorites
            btnAddToFavorites = new Button();
            btnAddToFavorites.Dock = DockStyle.Top;
            btnAddToFavorites.Height = 30;
            btnAddToFavorites.Text = "Dodaj do ulubionych";
            btnAddToFavorites.BackColor = Color.FromArgb(45, 45, 48);
            btnAddToFavorites.ForeColor = Color.White;
            btnAddToFavorites.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnAddToFavorites.Click += btnAddToFavorites_Click;
            btnAddToFavorites.Visible = false;
            // Button Remove from Favorites
            btnRemoveFromFavorites = new Button();
            btnRemoveFromFavorites.Dock = DockStyle.Top;
            btnRemoveFromFavorites.Height = 30;
            btnRemoveFromFavorites.Text = "Usuń z ulubionych";
            btnRemoveFromFavorites.BackColor = Color.FromArgb(45, 45, 48);
            btnRemoveFromFavorites.ForeColor = Color.White;
            btnRemoveFromFavorites.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnRemoveFromFavorites.Click += btnRemoveFromFavorites_Click;
            btnRemoveFromFavorites.Visible = false;
            // Wspólne ustawienia dla przycisków
            int buttonHeight = 40;
            int buttonWidth = 150;
            Color buttonBackColor = Color.FromArgb(45, 45, 48);
            Color buttonForeColor = Color.White;
            Font buttonFont = new Font("Segoe UI", 10, FontStyle.Bold);
            ContentAlignment textAlign = ContentAlignment.MiddleCenter;
            Padding buttonPadding = new Padding(0);
            // Button Rotate Left
            btnRotateLeft = new Button();
            btnRotateLeft.Height = buttonHeight;
            btnRotateLeft.Width = buttonWidth;
            btnRotateLeft.Text = "Obróć w lewo";
            btnRotateLeft.BackColor = buttonBackColor;
            btnRotateLeft.ForeColor = buttonForeColor;
            btnRotateLeft.Font = buttonFont;
            btnRotateLeft.TextAlign = textAlign;
            btnRotateLeft.Padding = buttonPadding;
            btnRotateLeft.Click += btnRotateLeft_Click;
            btnRotateLeft.Visible = false;
            btnRotateLeft.Margin = new Padding(5);
            // Button Rotate Right
            btnRotateRight = new Button();
            btnRotateRight.Height = buttonHeight;
            btnRotateRight.Width = buttonWidth;
            btnRotateRight.Text = "Obróć w prawo";
            btnRotateRight.BackColor = buttonBackColor;
            btnRotateRight.ForeColor = buttonForeColor;
            btnRotateRight.Font = buttonFont;
            btnRotateRight.TextAlign = textAlign;
            btnRotateRight.Padding = buttonPadding;
            btnRotateRight.Click += btnRotateRight_Click;
            btnRotateRight.Visible = false;
            btnRotateRight.Margin = new Padding(5);
            // Button Flip Horizontal
            btnFlipHorizontal = new Button();
            btnFlipHorizontal.Height = buttonHeight;
            btnFlipHorizontal.Width = buttonWidth;
            btnFlipHorizontal.Text = "Lustrzane odbicie";
            btnFlipHorizontal.BackColor = buttonBackColor;
            btnFlipHorizontal.ForeColor = buttonForeColor;
            btnFlipHorizontal.Font = buttonFont;
            btnFlipHorizontal.TextAlign = textAlign;
            btnFlipHorizontal.Padding = buttonPadding;
            btnFlipHorizontal.Click += btnFlipHorizontal_Click;
            btnFlipHorizontal.Visible = false;
            btnFlipHorizontal.Margin = new Padding(5);
            // Button Rotate 180
            btnRotate180 = new Button();
            btnRotate180.Height = buttonHeight;
            btnRotate180.Width = buttonWidth;
            btnRotate180.Text = "Obróć o 180 stopni";
            btnRotate180.BackColor = buttonBackColor;
            btnRotate180.ForeColor = buttonForeColor;
            btnRotate180.Font = buttonFont;
            btnRotate180.TextAlign = textAlign;
            btnRotate180.Padding = buttonPadding;
            btnRotate180.Click += btnRotate180_Click;
            btnRotate180.Visible = false;
            btnRotate180.Margin = new Padding(5);
            // Panel Podgląd
            panelPodglad.BackColor = Color.FromArgb(45, 45, 48);
            panelPodglad.Controls.Add(pictureBox);
            panelPodglad.Controls.Add(panelButtons);
            panelPodglad.Controls.Add(btnAddToFavorites);
            panelPodglad.Controls.Add(btnRemoveFromFavorites);
            panelPodglad.Dock = DockStyle.Fill;
            panelPodglad.Location = new Point(233, 29);
            panelPodglad.Margin = new Padding(4, 3, 4, 3);
            panelPodglad.Name = "panelPodglad";
            panelPodglad.Size = new Size(467, 351);
            panelPodglad.TabIndex = 3;
            // Panel Buttons
            panelButtons.BackColor = Color.FromArgb(45, 45, 48);
            panelButtons.Dock = DockStyle.Bottom;
            panelButtons.Height = buttonHeight + 10;
            panelButtons.Controls.Add(btnRotateLeft);
            panelButtons.Controls.Add(btnRotateRight);
            panelButtons.Controls.Add(btnFlipHorizontal);
            panelButtons.Controls.Add(btnRotate180);
            panelButtons.FlowDirection = FlowDirection.LeftToRight;
            panelButtons.Padding = new Padding(5, 5, 5, 5);
            panelButtons.Margin = new Padding(0, 0, 0, 5);
            // PictureBox
            pictureBox.BackColor = Color.FromArgb(30, 30, 30);
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Location = new Point(0, 0);
            pictureBox.Margin = new Padding(4, 3, 4, 3);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(467, 291);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.TabIndex = 0;
            pictureBox.TabStop = false;
            // Panel Info
            panelInfo.BackColor = Color.FromArgb(45, 45, 48);
            panelInfo.Controls.Add(datePickerFrom);
            panelInfo.Controls.Add(datePickerTo);
            panelInfo.Controls.Add(btnApplyDateFilter);
            panelInfo.Controls.Add(searchTextBox);
            panelInfo.Controls.Add(labelInfo);
            panelInfo.Dock = DockStyle.Left;
            panelInfo.Location = new Point(0, 29);
            panelInfo.Margin = new Padding(4, 3, 4, 3);
            panelInfo.Name = "panelInfo";
            panelInfo.Size = new Size(233, 351);
            panelInfo.TabIndex = 4;
            // Date Picker From
            datePickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            datePickerFrom.Size = new System.Drawing.Size(100, 23);
            datePickerFrom.TabIndex = 7;
            datePickerFrom.Visible = false;
            btnApplyDateFilter.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            datePickerFrom.CustomFormat = "dd.MM.yyyy";
            datePickerFrom.Dock = DockStyle.None;
            // Date Picker To
            datePickerTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            datePickerTo.Size = new System.Drawing.Size(100, 23);
            datePickerTo.TabIndex = 8;
            datePickerTo.Visible = false;

            datePickerTo.CustomFormat = "dd.MM.yyyy";
            datePickerTo.Dock = DockStyle.None;
            // Button Apply Date Filter
            btnApplyDateFilter.Size = new System.Drawing.Size(75, 23);
            btnApplyDateFilter.TabIndex = 9;
            btnApplyDateFilter.Text = "Filtruj";
            btnApplyDateFilter.UseVisualStyleBackColor = true;
            btnApplyDateFilter.Visible = false;

            btnApplyDateFilter.Click += new System.EventHandler(this.btnApplyDateFilter_Click);
            // Search TextBox
            searchTextBox.Font = new Font("Segoe UI", 12F);
            searchTextBox.ForeColor = Color.Gray;
            searchTextBox.Margin = new Padding(4, 3, 4, 3);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.Size = new Size(213, 26);
            searchTextBox.TabIndex = 0;
            searchTextBox.Text = "Wyszukaj zdjęcie..";
            searchTextBox.Visible = false;
            searchTextBox.Dock = DockStyle.Bottom;
            // Label Info
            labelInfo.Dock = DockStyle.Fill;
            labelInfo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelInfo.ForeColor = Color.White;
            labelInfo.Location = new Point(0, 26);
            labelInfo.Margin = new Padding(4, 3, 4, 3);
            labelInfo.Name = "labelInfo";
            labelInfo.Size = new Size(233, 325);
            labelInfo.TabIndex = 0;
            // Panel Filtry
            panelFiltry.BackColor = Color.FromArgb(45, 45, 48);
            panelFiltry.Controls.Add(btnGrayScale);
            panelFiltry.Controls.Add(btnSepia);
            panelFiltry.Controls.Add(btnInvert);
            panelFiltry.Controls.Add(btnNoFilter);
            panelFiltry.Dock = DockStyle.Right;
            panelFiltry.Location = new Point(700, 29);
            panelFiltry.Margin = new Padding(4, 3, 4, 3);
            panelFiltry.Name = "panelFiltry";
            panelFiltry.Size = new Size(233, 351);
            panelFiltry.TabIndex = 6;
            panelFiltry.Visible = false;
            // Button GrayScale
            btnGrayScale.Dock = DockStyle.Top;
            btnGrayScale.Location = new Point(0, 69);
            btnGrayScale.Name = "btnGrayScale";
            btnGrayScale.Size = new Size(233, 23);
            btnGrayScale.TabIndex = 0;
            btnGrayScale.Text = "GrayScale";
            btnGrayScale.UseVisualStyleBackColor = true;
            btnGrayScale.Click += btnGrayScale_Click;
            // Button Sepia
            btnSepia.Dock = DockStyle.Top;
            btnSepia.Location = new Point(0, 46);
            btnSepia.Name = "btnSepia";
            btnSepia.Size = new Size(233, 23);
            btnSepia.TabIndex = 1;
            btnSepia.Text = "Sepia";
            btnSepia.UseVisualStyleBackColor = true;
            btnSepia.Click += btnSepia_Click;
            // Button Invert
            btnInvert.Dock = DockStyle.Top;
            btnInvert.Location = new Point(0, 23);
            btnInvert.Name = "btnInvert";
            btnInvert.Size = new Size(233, 23);
            btnInvert.TabIndex = 2;
            btnInvert.Text = "Invert";
            btnInvert.UseVisualStyleBackColor = true;
            btnInvert.Click += btnInvert_Click;
            // Button No Filter
            btnNoFilter.Dock = DockStyle.Top;
            btnNoFilter.Location = new Point(0, 0);
            btnNoFilter.Name = "btnNoFilter";
            btnNoFilter.Size = new Size(233, 23);
            btnNoFilter.TabIndex = 3;
            btnNoFilter.Text = "No Filter";
            btnNoFilter.UseVisualStyleBackColor = true;
            btnNoFilter.Click += btnNoFilter_Click;
            // Button Refresh
            btnRefresh.Dock = DockStyle.Bottom;
            btnRefresh.Location = new Point(0, 380);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(933, 23);
            btnRefresh.TabIndex = 5;
            btnRefresh.Text = "Odśwież";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // Form1
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 545);
            Controls.Add(panelPodglad);
            Controls.Add(panelFiltry);
            Controls.Add(panelInfo);
            Controls.Add(btnRefresh);
            Controls.Add(listView);
            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "Aplikacja do przeglądania zdjęć";
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            panelPodglad.ResumeLayout(false);
            panelPodglad.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            panelInfo.ResumeLayout(false);
            panelInfo.PerformLayout();
            panelFiltry.ResumeLayout(false);
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
