namespace Поиск_хроматического_числа
{
    partial class fFindChromaticNumber
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.P_MainMenu = new System.Windows.Forms.GroupBox();
            this.B_Manual = new System.Windows.Forms.Button();
            this.B_Discribe = new System.Windows.Forms.Button();
            this.B_Download = new System.Windows.Forms.Button();
            this.P_Actions = new System.Windows.Forms.Panel();
            this.L_Actions = new System.Windows.Forms.Label();
            this.B_Calculate = new System.Windows.Forms.Button();
            this.P_Answer = new System.Windows.Forms.Panel();
            this.L_Result = new System.Windows.Forms.Label();
            this.L_Answer = new System.Windows.Forms.Label();
            this.L_InputResult = new System.Windows.Forms.Label();
            this.P_Input = new System.Windows.Forms.Panel();
            this.L_Input = new System.Windows.Forms.Label();
            this.B_Input = new System.Windows.Forms.Button();
            this.P_InputOutput = new System.Windows.Forms.Panel();
            this.TB_InputOutput = new System.Windows.Forms.TextBox();
            this.L_InputOutput = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.P_MainMenu.SuspendLayout();
            this.P_Actions.SuspendLayout();
            this.P_Answer.SuspendLayout();
            this.P_Input.SuspendLayout();
            this.P_InputOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // P_MainMenu
            // 
            this.P_MainMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.P_MainMenu.Controls.Add(this.B_Manual);
            this.P_MainMenu.Controls.Add(this.B_Discribe);
            this.P_MainMenu.Controls.Add(this.B_Download);
            this.P_MainMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.P_MainMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.P_MainMenu.Location = new System.Drawing.Point(0, 0);
            this.P_MainMenu.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.P_MainMenu.Name = "P_MainMenu";
            this.P_MainMenu.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.P_MainMenu.Size = new System.Drawing.Size(241, 331);
            this.P_MainMenu.TabIndex = 0;
            this.P_MainMenu.TabStop = false;
            this.P_MainMenu.Text = "Меню";
            // 
            // B_Manual
            // 
            this.B_Manual.BackColor = System.Drawing.Color.LightGray;
            this.B_Manual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.B_Manual.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.B_Manual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.B_Manual.Location = new System.Drawing.Point(9, 288);
            this.B_Manual.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.B_Manual.Name = "B_Manual";
            this.B_Manual.Size = new System.Drawing.Size(224, 35);
            this.B_Manual.TabIndex = 2;
            this.B_Manual.Text = "Справка";
            this.B_Manual.UseVisualStyleBackColor = false;
            this.B_Manual.Click += new System.EventHandler(this.B_Manual_Click);
            // 
            // B_Discribe
            // 
            this.B_Discribe.BackColor = System.Drawing.Color.LightGray;
            this.B_Discribe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.B_Discribe.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.B_Discribe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.B_Discribe.Location = new System.Drawing.Point(8, 74);
            this.B_Discribe.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.B_Discribe.Name = "B_Discribe";
            this.B_Discribe.Size = new System.Drawing.Size(224, 35);
            this.B_Discribe.TabIndex = 1;
            this.B_Discribe.Text = "Ввести граф";
            this.B_Discribe.UseVisualStyleBackColor = false;
            this.B_Discribe.Click += new System.EventHandler(this.B_Discribe_Click);
            // 
            // B_Download
            // 
            this.B_Download.BackColor = System.Drawing.Color.LightGray;
            this.B_Download.Cursor = System.Windows.Forms.Cursors.Hand;
            this.B_Download.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.B_Download.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.B_Download.Location = new System.Drawing.Point(8, 29);
            this.B_Download.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.B_Download.Name = "B_Download";
            this.B_Download.Size = new System.Drawing.Size(224, 35);
            this.B_Download.TabIndex = 0;
            this.B_Download.Text = "Загрузить граф из файла";
            this.B_Download.UseVisualStyleBackColor = false;
            this.B_Download.Click += new System.EventHandler(this.B_Download_Click);
            // 
            // P_Actions
            // 
            this.P_Actions.Controls.Add(this.L_Actions);
            this.P_Actions.Controls.Add(this.B_Calculate);
            this.P_Actions.Controls.Add(this.P_Answer);
            this.P_Actions.Controls.Add(this.L_InputResult);
            this.P_Actions.Controls.Add(this.P_Input);
            this.P_Actions.Location = new System.Drawing.Point(248, 0);
            this.P_Actions.Name = "P_Actions";
            this.P_Actions.Size = new System.Drawing.Size(296, 331);
            this.P_Actions.TabIndex = 1;
            this.P_Actions.Visible = false;
            // 
            // L_Actions
            // 
            this.L_Actions.AutoSize = true;
            this.L_Actions.Location = new System.Drawing.Point(0, 0);
            this.L_Actions.Name = "L_Actions";
            this.L_Actions.Size = new System.Drawing.Size(105, 25);
            this.L_Actions.TabIndex = 3;
            this.L_Actions.Text = "Действия";
            // 
            // B_Calculate
            // 
            this.B_Calculate.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.B_Calculate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.B_Calculate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.B_Calculate.Location = new System.Drawing.Point(42, 198);
            this.B_Calculate.Name = "B_Calculate";
            this.B_Calculate.Size = new System.Drawing.Size(207, 43);
            this.B_Calculate.TabIndex = 1;
            this.B_Calculate.Text = "Вычислить хром. число";
            this.B_Calculate.UseVisualStyleBackColor = false;
            this.B_Calculate.Visible = false;
            this.B_Calculate.Click += new System.EventHandler(this.B_Calculate_Click);
            // 
            // P_Answer
            // 
            this.P_Answer.Controls.Add(this.L_Result);
            this.P_Answer.Controls.Add(this.L_Answer);
            this.P_Answer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.P_Answer.Location = new System.Drawing.Point(0, 264);
            this.P_Answer.Name = "P_Answer";
            this.P_Answer.Size = new System.Drawing.Size(296, 67);
            this.P_Answer.TabIndex = 3;
            // 
            // L_Result
            // 
            this.L_Result.AutoSize = true;
            this.L_Result.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.L_Result.Location = new System.Drawing.Point(105, 10);
            this.L_Result.Name = "L_Result";
            this.L_Result.Size = new System.Drawing.Size(94, 27);
            this.L_Result.TabIndex = 3;
            this.L_Result.Text = "<ответ>";
            // 
            // L_Answer
            // 
            this.L_Answer.AutoSize = true;
            this.L_Answer.Location = new System.Drawing.Point(38, 10);
            this.L_Answer.Name = "L_Answer";
            this.L_Answer.Size = new System.Drawing.Size(78, 25);
            this.L_Answer.TabIndex = 2;
            this.L_Answer.Text = "Ответ:";
            // 
            // L_InputResult
            // 
            this.L_InputResult.AutoSize = true;
            this.L_InputResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.L_InputResult.Location = new System.Drawing.Point(38, 138);
            this.L_InputResult.Name = "L_InputResult";
            this.L_InputResult.Size = new System.Drawing.Size(97, 18);
            this.L_InputResult.TabIndex = 0;
            this.L_InputResult.Text = "Успешно/нет";
            this.L_InputResult.Visible = false;
            // 
            // P_Input
            // 
            this.P_Input.Controls.Add(this.L_Input);
            this.P_Input.Controls.Add(this.B_Input);
            this.P_Input.Location = new System.Drawing.Point(42, 29);
            this.P_Input.Name = "P_Input";
            this.P_Input.Size = new System.Drawing.Size(207, 106);
            this.P_Input.TabIndex = 4;
            this.P_Input.Visible = false;
            // 
            // L_Input
            // 
            this.L_Input.Dock = System.Windows.Forms.DockStyle.Top;
            this.L_Input.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.L_Input.Location = new System.Drawing.Point(0, 0);
            this.L_Input.Name = "L_Input";
            this.L_Input.Size = new System.Drawing.Size(207, 71);
            this.L_Input.TabIndex = 5;
            this.L_Input.Text = "Введите информацию о графе в окне справа в соответствии с требованиями справки";
            this.L_Input.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // B_Input
            // 
            this.B_Input.Enabled = false;
            this.B_Input.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.B_Input.Location = new System.Drawing.Point(0, 74);
            this.B_Input.Name = "B_Input";
            this.B_Input.Size = new System.Drawing.Size(207, 32);
            this.B_Input.TabIndex = 4;
            this.B_Input.Text = "Готово";
            this.B_Input.UseVisualStyleBackColor = true;
            this.B_Input.Click += new System.EventHandler(this.B_Input_Click);
            // 
            // P_InputOutput
            // 
            this.P_InputOutput.Controls.Add(this.TB_InputOutput);
            this.P_InputOutput.Controls.Add(this.L_InputOutput);
            this.P_InputOutput.Dock = System.Windows.Forms.DockStyle.Right;
            this.P_InputOutput.Location = new System.Drawing.Point(545, 0);
            this.P_InputOutput.Name = "P_InputOutput";
            this.P_InputOutput.Size = new System.Drawing.Size(292, 331);
            this.P_InputOutput.TabIndex = 2;
            this.P_InputOutput.Visible = false;
            // 
            // TB_InputOutput
            // 
            this.TB_InputOutput.BackColor = System.Drawing.SystemColors.Window;
            this.TB_InputOutput.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TB_InputOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TB_InputOutput.Location = new System.Drawing.Point(0, 29);
            this.TB_InputOutput.Multiline = true;
            this.TB_InputOutput.Name = "TB_InputOutput";
            this.TB_InputOutput.ReadOnly = true;
            this.TB_InputOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TB_InputOutput.Size = new System.Drawing.Size(292, 302);
            this.TB_InputOutput.TabIndex = 1;
            this.TB_InputOutput.WordWrap = false;
            this.TB_InputOutput.TextChanged += new System.EventHandler(this.TB_InputOutput_TextChanged);
            // 
            // L_InputOutput
            // 
            this.L_InputOutput.AutoSize = true;
            this.L_InputOutput.Location = new System.Drawing.Point(-4, 0);
            this.L_InputOutput.Name = "L_InputOutput";
            this.L_InputOutput.Size = new System.Drawing.Size(219, 25);
            this.L_InputOutput.TabIndex = 0;
            this.L_InputOutput.Text = "Информация о графе";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // fFindChromaticNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(837, 331);
            this.Controls.Add(this.P_InputOutput);
            this.Controls.Add(this.P_Actions);
            this.Controls.Add(this.P_MainMenu);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.HelpButton = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(855, 378);
            this.MinimumSize = new System.Drawing.Size(855, 378);
            this.Name = "fFindChromaticNumber";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Поиск хроматического числа графа";
            this.P_MainMenu.ResumeLayout(false);
            this.P_Actions.ResumeLayout(false);
            this.P_Actions.PerformLayout();
            this.P_Answer.ResumeLayout(false);
            this.P_Answer.PerformLayout();
            this.P_Input.ResumeLayout(false);
            this.P_InputOutput.ResumeLayout(false);
            this.P_InputOutput.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox P_MainMenu;
        private System.Windows.Forms.Button B_Discribe;
        private System.Windows.Forms.Button B_Download;
        private System.Windows.Forms.Button B_Manual;
        private System.Windows.Forms.Panel P_Actions;
        private System.Windows.Forms.Panel P_Answer;
        private System.Windows.Forms.Label L_InputResult;
        private System.Windows.Forms.Button B_Calculate;
        private System.Windows.Forms.Label L_Answer;
        private System.Windows.Forms.Label L_Result;
        private System.Windows.Forms.Label L_Actions;
        private System.Windows.Forms.Panel P_InputOutput;
        private System.Windows.Forms.TextBox TB_InputOutput;
        private System.Windows.Forms.Label L_InputOutput;
        private System.Windows.Forms.Panel P_Input;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button B_Input;
        private System.Windows.Forms.Label L_Input;
    }
}

