using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace NotePad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ChechEditorText();
            TextCopied();

        }
        string Fpath = ""; //File Path of text
        bool textChanged = false;
        bool isAllTextSelected = false;
        bool isCopiedText = false;
        private object openFileDialog1;

        private void OpenFile_Click(object sender, EventArgs e)
        {
            AllNote.Clear();
            Fpath = "";
        }

        private void NewFile_Click(object sender, EventArgs e)
        {
            Process.Start(Application.ExecutablePath);
        }

        private void OpenFileFromPc_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                AllNote.Text = File.ReadAllText(openFileDialog1.FileName);
                Fpath = openFileDialog1.FileName;
            }
        }
       
        private void SaveAs()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Fpath = saveFileDialog.FileName;
                File.WriteAllText(Fpath, AllNote.Text); // give path and text
            }
        }
        private void SsaveAss_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Fpath = saveFileDialog.FileName;
                File.WriteAllText(Fpath, AllNote.Text);
            }
        }

        private void Save_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Fpath))
            {
                SaveAs();
            }
            else
            {
                File.WriteAllText(Fpath, AllNote.Text);
            }
        }

        private void AllNote_TextChanged(object sender, EventArgs e)
        {
            textChanged = true;
            if (AllNote.Text == string.Empty)
            {
                textChanged = false;
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            if (textChanged)
            {
                DialogResult da = MessageBox.Show("Do you want to save your text ?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (da == DialogResult.Yes)
                    {
                    Save_Click_1(sender, e);
                }
                else if (da == DialogResult.Cancel)
                {
                    return;
                }
            } 
            Application.Exit();
        }

        private void Undo_Click(object sender, EventArgs e)
        {
            if (AllNote.CanUndo)
                AllNote.Undo();
        }

        private void Cut_Click(object sender, EventArgs e)
        {
            AllNote.Cut();
            isCopiedText = true;
        }
        private void Paste_Click(object sender, EventArgs e)
        {
            if (isCopiedText)
                AllNote.Paste();

        }
        private void Copy_Click(object sender, EventArgs e)
        {
            if (isCopiedText)
                AllNote.Paste();
        }

        private void SelectAll_Click(object sender, EventArgs e)
        {
            if (!isAllTextSelected)
            {
                AllNote.SelectAll(); // Select All
                isAllTextSelected = true;
            }
            else
            {
                AllNote.DeselectAll(); // DeSelect All
                isAllTextSelected = false;
            }

        }

        private void AllNote_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\u0001' && !string.IsNullOrEmpty(AllNote.Text))
            {
                isAllTextSelected = true;
            }

        }
        public void ChechEditorText()
        {
            if (!string.IsNullOrEmpty(AllNote.Text) && AllNote.SelectedText.Count() > 0)
            {
                Copy.Enabled = true;
                Cut.Enabled = true;
                Exit.Enabled = true;
                SelectAll.Enabled = true;
            }

            if (string.IsNullOrEmpty(AllNote.Text))
            {
                SelectAll.Enabled = false;
            }
            else
            {
                SelectAll.Enabled = true;
            }

            if (string.IsNullOrEmpty(AllNote.Text) || AllNote.SelectedText.Count() == 0)
            {
                Copy.Enabled = false;
                Cut.Enabled = false;
                Exit.Enabled = false;
            }
            //  selectAllToolStripMenuItem1.Enabled = false;
        }

   
     public void TextCopied()
        {
            if (Clipboard.ContainsText())
            {
                isCopiedText = true;
                Paste.Enabled = true;
                return;
            }
            Paste.Enabled = false;
        }

        private void AllNote_MouseClick(object sender, MouseEventArgs e)
        {
            ChechEditorText();
            TextCopied();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextCopied();
        }

        private bool _wordWrapEnabled = true;
        private void wordwrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _wordWrapEnabled = !_wordWrapEnabled;
            AllNote.WordWrap = _wordWrapEnabled;
            wordwrapToolStripMenuItem.Checked = _wordWrapEnabled;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AllNote.WordWrap = true;
            wordwrapToolStripMenuItem.Checked = true;
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog font = new FontDialog();
            font.Font = AllNote.Font;
            if(font.ShowDialog() == DialogResult.OK)
            {
                AllNote.Font = font.Font;
            }
        }
    }
}
