using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using MetroFramework;
using MetroFramework.Forms;

namespace RTE
{
    public partial class Form1 : MaterialForm
    {
        bool FileOpen = false;
        string FilePath = "";
        bool CanChangeFont = false;
        public Form1()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue400, Primary.Blue800, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            comboBoxFontSize.SelectedIndex = 3;
            CapslockOnOrOff();
            label2.Text = DateTime.Now.ToShortDateString() + ", " + DateTime.Now.ToLongTimeString();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (richTextBox.SelectionLength > 0)
            {
                richTextBox.Cut();
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (richTextBox.SelectionLength > 0)
            {
                richTextBox.Copy();
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            richTextBox.Paste();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (richTextBox.SelectionLength > 0)
            {
                richTextBox.SelectedText = "";
            }
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            richTextBox.SelectAll();
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            richTextBox.Undo();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox.Text != "")
            {
                DialogResult dialog = MetroMessageBox.Show(this, "Вы уверены?\nВсе ваши данные будут утеряны.", "Новый документ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (DialogResult.Yes == dialog)
                {
                    richTextBox.Text = "";
                    this.Text = "Новый документ";
                    FileOpen = false;
                    FilePath = "";
                }
            }
        }

        private void новыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "TXT Files(*.txt)|*.txt|RTF Files(*.rtf)|*.rtf";
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                try
                {
                    if (openFileDialog1.FileName.EndsWith("txt"))
                    {
                        richTextBox.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                    }
                    else
                    {
                        richTextBox.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.RichText);
                    }
                    this.Text = Path.GetFileName(openFileDialog1.FileName);
                    FileOpen = true;
                    FilePath = openFileDialog1.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Возникла ошибка: {ex.ToString()}");
                }
            }
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void SaveFile()
        {
            saveFileDialog1.Filter = "TXT Files(*.txt)|*.txt|RTF Files(*.rtf)|*.rtf";
            saveFileDialog1.FileName = this.Text;
            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                try
                {
                    if (saveFileDialog1.FileName.EndsWith("txt"))
                    {
                        richTextBox.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.UnicodePlainText);
                    }
                    else
                    {
                        richTextBox.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.RichText);
                    }
                    FileOpen = true;
                    FilePath = saveFileDialog1.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Возникла ошибка: {ex.ToString()}");
                }
            }
        }

        private void toolStripButton6_Click_1(object sender, EventArgs e)
        {
            if (FileOpen)
            {
                if (FilePath.EndsWith("txt"))
                {
                    richTextBox.SaveFile(FilePath, RichTextBoxStreamType.PlainText);
                }
                else
                {
                    richTextBox.SaveFile(FilePath, RichTextBoxStreamType.RichText);
                }
                FileOpen = true;
                FilePath = openFileDialog1.FileName;
            }
            else
            {
                SaveFile();
            }
        }

        private void toolStripButtonBold_Click(object sender, EventArgs e)
        {
            PressFontStyleButton(toolStripButtonBold);
            if (toolStripButtonBold.Checked == true)
            {
                richTextBox.SelectionFont = new Font(richTextBox.Font.FontFamily, richTextBox.SelectionFont.Size, richTextBox.SelectionFont.Style | FontStyle.Bold);
            }
            else
            {
                if (toolStripButtonItalic.Checked && toolStripButtonUnderline.Checked)
                {
                    richTextBox.SelectionFont = new Font(richTextBox.Font.FontFamily, richTextBox.SelectionFont.Size, FontStyle.Underline | FontStyle.Italic);
                }
                else if (toolStripButtonItalic.Checked)
                {
                    richTextBox.SelectionFont = new Font(richTextBox.Font.FontFamily, richTextBox.SelectionFont.Size, FontStyle.Italic);
                }
                else if (toolStripButtonUnderline.Checked)
                {
                    richTextBox.SelectionFont = new Font(richTextBox.Font.FontFamily, richTextBox.SelectionFont.Size, FontStyle.Underline);
                }
                else
                {
                    richTextBox.SelectionFont = new Font(richTextBox.Font.FontFamily, richTextBox.SelectionFont.Size, FontStyle.Regular);
                }
            }
        }
        private void PressFontStyleButton(ToolStripButton button)
        {
            if (button.Checked == false)
            {
                button.Checked = true;
            }
            else
            {
                button.Checked = false;
            }
        }

        private void toolStripButtonItalic_Click(object sender, EventArgs e)
        {
            PressFontStyleButton(toolStripButtonItalic);
            if (toolStripButtonItalic.Checked == true)
            {
                richTextBox.SelectionFont = new Font(richTextBox.Font.FontFamily, richTextBox.SelectionFont.Size, richTextBox.SelectionFont.Style | FontStyle.Italic);
            }
            else
            {
                if (toolStripButtonBold.Checked && toolStripButtonUnderline.Checked)
                {
                    richTextBox.SelectionFont = new Font(richTextBox.Font.FontFamily, richTextBox.SelectionFont.Size, FontStyle.Underline | FontStyle.Bold);
                }
                else if (toolStripButtonBold.Checked)
                {
                    richTextBox.SelectionFont = new Font(richTextBox.Font.FontFamily, richTextBox.SelectionFont.Size, FontStyle.Bold);
                }
                else if (toolStripButtonUnderline.Checked)
                {
                    richTextBox.SelectionFont = new Font(richTextBox.Font.FontFamily, richTextBox.SelectionFont.Size, FontStyle.Underline);
                }
                else
                {
                    richTextBox.SelectionFont = new Font(richTextBox.Font.FontFamily, richTextBox.SelectionFont.Size, FontStyle.Regular);
                }
            }
        }

        private void toolStripButtonUnderline_Click(object sender, EventArgs e)
        {
            PressFontStyleButton(toolStripButtonUnderline);
            if (toolStripButtonUnderline.Checked == true)
            {
                richTextBox.SelectionFont = new Font(richTextBox.Font.FontFamily, richTextBox.SelectionFont.Size, richTextBox.SelectionFont.Style | FontStyle.Underline);
            }
            else
            {
                if (toolStripButtonBold.Checked && toolStripButtonItalic.Checked)
                {
                    richTextBox.SelectionFont = new Font(richTextBox.Font.FontFamily, richTextBox.SelectionFont.Size, FontStyle.Bold | FontStyle.Italic);
                }
                else if (toolStripButtonItalic.Checked)
                {
                    richTextBox.SelectionFont = new Font(richTextBox.Font.FontFamily, richTextBox.SelectionFont.Size, FontStyle.Italic);
                }
                else if (toolStripButtonUnderline.Checked)
                {
                    richTextBox.SelectionFont = new Font(richTextBox.Font.FontFamily, richTextBox.SelectionFont.Size, FontStyle.Bold);
                }
                else
                {
                    richTextBox.SelectionFont = new Font(richTextBox.Font.FontFamily, richTextBox.SelectionFont.Size, FontStyle.Regular);
                }
            }
        }

        private void comboBoxFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox.SelectionFont = new Font(richTextBox.Font.FontFamily, Convert.ToInt32(comboBoxFontSize.Items[comboBoxFontSize.SelectedIndex]), richTextBox.SelectionFont.Style);
        }

        private void comboBoxFontSize_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void comboBoxFontSize_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
            else
            {
                CanChangeFont = true;

            }
        }

        private void comboBoxFontSize_TextUpdate(object sender, EventArgs e)
        {
            if (comboBoxFontSize.Text != "")
            {
                if (comboBoxFontSize.Text == "0" || int.Parse(comboBoxFontSize.Text) > 100)
                {
                    MetroMessageBox.Show(this, "Число должно находиться в диапазоне от 1 до 100", "Rich Text Editor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CanChangeFont = false;
                }
                else if (CanChangeFont && comboBoxFontSize.Text != "")
                {
                    richTextBox.SelectionFont = new Font(richTextBox.Font.FontFamily, int.Parse(comboBoxFontSize.Text), richTextBox.SelectionFont.Style);
                    CanChangeFont = false;
                }
            }

        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            CapslockOnOrOff();
        }
        private void CapslockOnOrOff()
        {
            if (IsKeyLocked(Keys.CapsLock))
            {
                toolStripStatusCapslock.ForeColor = Color.Red;
                toolStripStatusCapslock.Text = "CAPSLOCK ON";
            }
            else
            {
                toolStripStatusCapslock.ForeColor = Color.White;
                toolStripStatusCapslock.Text = "CAPSLOCK OFF";
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToShortDateString() + ", " + DateTime.Now.ToLongTimeString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            About_Info about = new About_Info();
            about.Show();
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            richTextBox.Redo();
        }

        private void richTextBox_SelectionChanged(object sender, EventArgs e)
        {
            if (richTextBox.SelectionFont.Bold && richTextBox.SelectionFont.Italic && richTextBox.SelectionFont.Underline)
            {
                toolStripButtonItalic.Checked = true;
                toolStripButtonBold.Checked = true;
                toolStripButtonUnderline.Checked = true;
            }
            else if (richTextBox.SelectionFont.Bold && richTextBox.SelectionFont.Italic)
            {
                toolStripButtonBold.Checked = true;
                toolStripButtonItalic.Checked = true;
                toolStripButtonUnderline.Checked = false;
            }
            else if (richTextBox.SelectionFont.Bold && richTextBox.SelectionFont.Underline)
            {
                toolStripButtonBold.Checked = true;
                toolStripButtonItalic.Checked = false;
                toolStripButtonUnderline.Checked = true;
            }
            else if (richTextBox.SelectionFont.Italic && richTextBox.SelectionFont.Underline)
            {
                toolStripButtonBold.Checked = false;
                toolStripButtonItalic.Checked = true;
                toolStripButtonUnderline.Checked = true;
            }
            else if (richTextBox.SelectionFont.Italic)
            {
                toolStripButtonBold.Checked = false;
                toolStripButtonUnderline.Checked = false;
                toolStripButtonItalic.Checked = true;
            }
            else if (richTextBox.SelectionFont.Bold)
            {
                toolStripButtonBold.Checked = true;
                toolStripButtonItalic.Checked = false;
                toolStripButtonUnderline.Checked = false;
            }
            else if (richTextBox.SelectionFont.Underline)
            {
                toolStripButtonItalic.Checked = false;
                toolStripButtonBold.Checked = false;
                toolStripButtonUnderline.Checked = true;
            }
            else
            {
                toolStripButtonItalic.Checked = false;
                toolStripButtonBold.Checked = false;
                toolStripButtonUnderline.Checked = false;
            }
        }
    }
}
