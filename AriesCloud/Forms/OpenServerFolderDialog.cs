using AriesCloud.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AriesCloud.Forms
{
    /// <summary>
    /// Диалог открытия сетевой папки.
    /// </summary>
    public partial class OpenServerFolderDialog : Form
    {
        /// <summary>
        /// Путь к папке.
        /// </summary>
        private string path;

        /// <summary>
        /// Путь к папке.
        /// </summary>
        public string Path
        {
            get => path;
        }

        /// <summary>
        /// Создание диалога открытия сетевой папки.
        /// </summary>
        public OpenServerFolderDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик загрузки формы.
        /// </summary>
        /// <param name="sender">Форма.</param>
        /// <param name="e">Данные события.</param>
        private void OpenServerFolderDialogOnLoad(object sender, EventArgs e)
        {
            try
            {
                List<string> directories = Core.GetDirecories();
                directories.RemoveAt(0);

                TreeNode mainTreeNode = new TreeNode("Корень")
                {
                    Name = "Корень"
                };

                foreach (string directoryPath in directories)
                {
                    List<string> pathNames = directoryPath.Split('/').ToList();
                    pathNames.RemoveAt(0);

                    TreeNode treeNode = mainTreeNode;

                    foreach (string directoryName in pathNames)
                    {
                        if (treeNode.Nodes.ContainsKey(directoryName))
                        {
                            treeNode = treeNode.Nodes[directoryName];
                        }
                        else
                        {
                            treeNode.Nodes.Add(directoryName, directoryName);
                        }
                    }
                }

                mainTreeView.BeginUpdate();

                foreach (TreeNode node in mainTreeNode.Nodes)
                {
                    mainTreeView.Nodes.Add(node);
                }

                mainTreeView.EndUpdate();
            }
            catch (Exception ex)
            {
                InfoViewer.ShowError(ex);
                Close();
            }
        }

        /// <summary>
        /// Обработчик кнопки "Отмена".
        /// </summary>
        /// <param name="sender">Кнопка "Отмена".</param>
        /// <param name="e">Данные события.</param>
        private void CancelButtonOnClick(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Обработчик кнопки "ОК".
        /// </summary>
        /// <param name="sender">Кнопка "ОК".</param>
        /// <param name="e">Данные события.</param>
        private void ApplyButtonOnClick(object sender, EventArgs e)
        {
            try
            {
                TreeNode node = mainTreeView.SelectedNode;
                path = $"/{node.FullPath}";
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception)
            {
                InfoViewer.ShowError("Выберите хотябы одну папку.");
            }
        }
    }
}
