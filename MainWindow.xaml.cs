using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BSK3_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RailFence_Encrypt_Click(object sender, RoutedEventArgs e)
        {
            string output = RailFence.Encipher(RailFenceText.Text, int.Parse(RailFenceKey.Text));
            OutputBlock.Text = $"RailFence encrypt:\n{output}";
        }

        private void RailFence_Decrypt_Click(object sender, RoutedEventArgs e)
        {
            string output = RailFence.Decipher(RailFenceText.Text, int.Parse(RailFenceKey.Text));
            OutputBlock.Text = $"RailFence decrypt:\n{output}";
        }

        private void MatrixA_Encrypt_Click(object sender, RoutedEventArgs e)
        {
            string key = MatrixAKey.Text;
            string[] split = key.Split('-');
            int[] keyArray = new int[split.Length];
            for(int i = 0; i < keyArray.Length; i++)
            {
                keyArray[i] = int.Parse(split[i]);
            }

            string output = MatrixA.Encipher(MatrixAText.Text, keyArray);
            OutputBlock.Text = $"MatrixA encrypt:\n{output}";
        }

        private void MatrixA_Decrypt_Click(object sender, RoutedEventArgs e)
        {
            string key = MatrixAKey.Text;
            string[] split = key.Split('-');
            int[] keyArray = new int[split.Length];
            for (int i = 0; i < keyArray.Length; i++)
            {
                keyArray[i] = int.Parse(split[i]);
            }

            string output = MatrixA.Decipher(MatrixAText.Text, keyArray);
            OutputBlock.Text = $"MatrixA Decrypt:\n{output}";
        }

        private void MatrixB_Encrypt_Click(object sender, RoutedEventArgs e)
        {
            string output = MatrixB.Encipher(MatrixBText.Text, MatrixBKey.Text);
            OutputBlock.Text = $"MatrixB encrypt:\n{output}";
        }

        private void MatrixB_Decrypt_Click(object sender, RoutedEventArgs e)
        {
            string output = MatrixB.Decipher(MatrixBText.Text, MatrixBKey.Text);
            OutputBlock.Text = $"MatrixB decrypt:\n{output}";
        }
    }
}
