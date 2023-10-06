using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework_04_10
{
    public partial class Form1 : Form
    {
        private string fullPath;
        public Form1()
        {


            InitializeComponent();

            ToolStripMenuItem file = new ToolStripMenuItem("File");

            ToolStripMenuItem exit = new ToolStripMenuItem("Exit");
            exit.Click += Exit_Click;

            file.DropDownItems.Add(exit);

            menuStrip1.Items.Add(file);

            DriveTreeInit();

            string[] drivesArray = Directory.GetLogicalDrives();

            foreach (string s in drivesArray)
                Console.Write("{0} ", s);


            DirectoryInfo[] diArray;
            string fullPath = "C:\\";

            DirectoryInfo di = new DirectoryInfo(fullPath);

            try
            {
                diArray = di.GetDirectories();
            }
            catch
            {
                MessageBox.Show("Error");
            }

            listView1.SmallImageList = imageList2;
            treeView1.AfterSelect += treeView1_OnAfterSelect;
            listView1.ItemActivate += listView1_OnItemActivate;
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void DriveTreeInit()
        {
            string[] drivesArray = Directory.GetLogicalDrives();

            treeView1.BeginUpdate();
            treeView1.Nodes.Clear();

            foreach (string s in drivesArray)
            {
                TreeNode drive = new TreeNode(s, 0, 0);
                treeView1.Nodes.Add(drive);

                GetDirs(drive);
            }

            treeView1.EndUpdate();
        }


        public void GetDirs(TreeNode node)
        {
            DirectoryInfo[] diArray;
            node.Nodes.Clear();
            string fullPath = node.FullPath;
            DirectoryInfo di = new DirectoryInfo(fullPath);

            try
            {
                diArray = di.GetDirectories();
            }
            catch
            {
                return;
            }

            foreach (DirectoryInfo dirinfo in diArray)
            {
                TreeNode dir = new TreeNode(dirinfo.Name, 1, 2);
                node.Nodes.Add(dir);
            }
        }


        private void treeView1_OnBeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            treeView1.BeginUpdate();

            foreach (TreeNode node in e.Node.Nodes)
            {
                GetDirs(node);
            }

            treeView1.EndUpdate();

        }

        private void treeView1_OnAfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selectedNode = e.Node;
            fullPath = selectedNode.FullPath;

            DirectoryInfo di = new DirectoryInfo(fullPath);
            FileInfo[] fiArray;
            DirectoryInfo[] diArray;

            try
            {
                fiArray = di.GetFiles();
                diArray = di.GetDirectories();
            }
            catch
            {
                return;
            }

            listView1.Items.Clear();

            foreach (DirectoryInfo dirInfo in diArray)
            {
                ListViewItem lvi = new ListViewItem(dirInfo.Name);
                lvi.SubItems.Add("0");
                lvi.SubItems.Add(dirInfo.LastWriteTime.ToString());
                lvi.ImageIndex = 0;

                listView1.Items.Add(lvi);
            }


            foreach (FileInfo fileInfo in fiArray)
            {
                ListViewItem lvi = new ListViewItem(fileInfo.Name);
                lvi.SubItems.Add(fileInfo.Length.ToString());
                lvi.SubItems.Add(fileInfo.LastWriteTime.ToString());

                string filenameExtension =
                  Path.GetExtension(fileInfo.Name).ToLower();

                switch (filenameExtension)
                {
                    case ".pdf":
                        {
                            lvi.ImageIndex = 1;
                            break;
                        }
                    case ".doc":
                        {
                            lvi.ImageIndex = 2;
                            break;
                        }
                    case ".xlsl":
                        {
                            lvi.ImageIndex = 3;
                            break;
                        }
                    case ".pptx":
                        {
                            lvi.ImageIndex = 4;
                            break;
                        }
                    case ".jpeg":
                        {
                            lvi.ImageIndex = 5;
                            break;
                        }
                    case ".png":
                        {
                            lvi.ImageIndex = 6;
                            break;
                        }
                    case ".gif":
                        {
                            lvi.ImageIndex = 7;
                            break;
                        }
                    case ".mp3":
                        {
                            lvi.ImageIndex = 8;
                            break;
                        }
                    case ".mp4":
                        {
                            lvi.ImageIndex = 9;
                            break;
                        }
                    case ".txt":
                        {
                            lvi.ImageIndex = 10;
                            break;
                        }
                    case ".zip":
                        {
                            lvi.ImageIndex = 11;
                            break;
                        }
                    case ".rar":
                        {
                            lvi.ImageIndex = 12;
                            break;
                        }
                    case ".exe":
                        {
                            lvi.ImageIndex = 13;
                            break;
                        }
                    default:
                        {
                            lvi.ImageIndex = 0;
                            break;
                        }
                }

                listView1.Items.Add(lvi);
            }
        }

        private void listView1_OnItemActivate(object sender,EventArgs e)
        {
            foreach (ListViewItem lvi in listView1.SelectedItems)
            {
                string ext = Path.GetExtension(lvi.Text).ToLower();
                if (ext == ".txt" || ext == ".htm" || ext == ".html")
                {
                    try
                    {
                        richTextBox1.LoadFile(Path.Combine(fullPath, lvi.Text),
                          RichTextBoxStreamType.PlainText);

                        statusStrip1.Text = lvi.Text;
                    }
                    catch
                    {
                        return;
                    }
                }
                else if (ext == ".rtf")
                {
                    try
                    {
                        richTextBox1.LoadFile(Path.Combine(fullPath, lvi.Text),
                          RichTextBoxStreamType.RichText);

                        statusStrip1.Text = lvi.Text;
                    }
                    catch
                    {
                        return;
                    }
                }
            }
        }

    }
}
