using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace CmdGUI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Cmd Cmd { get; set; }
        private String CommandListFileName { get { return "CmdList.txt"; }}
        public MainWindow()
        {
            InitializeComponent();

            LoadCommandList();

            Cmd = new Cmd(ReadLineCallback);
        }

        private void LoadCommandList()
        {
            var configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CommandListFileName);

            if (!File.Exists(configPath)) return;

            var commands = File.ReadAllLines(configPath);

            foreach (var cmd in commands.Where(s => !CbCmd.Items.Contains(s)))
            {
                CbCmd.Items.Add(cmd);
            }
        }

        private void ReadLineCallback(String line)
        {
            Dispatcher.Invoke(new Action(() => TbResult.AppendText(line + Environment.NewLine)));
        }

        private void OnExcuteClick(object sender, RoutedEventArgs e)
        {
            TbResult.Clear();
            Cmd.RunCmdAsync(CbCmd.Text.Trim());
        }
    }
}
