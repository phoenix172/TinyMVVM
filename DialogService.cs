﻿using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace TinyMVVM
{
    public class DialogService : IDialogService
    {
        public void ShowError(string error)
        {
            MessageBox.Show(error);
        }

        public bool ShowFolderBrowser(string currentPath, out string selectedPath)
        {
            selectedPath = null;
            using (FolderBrowserDialog browserDialog = new FolderBrowserDialog())
            {
                browserDialog.SelectedPath = currentPath;
                DialogResult dialogResult = browserDialog.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    selectedPath = browserDialog.SelectedPath;
                    return true;
                }
            }
            return false;
        }
    }
}