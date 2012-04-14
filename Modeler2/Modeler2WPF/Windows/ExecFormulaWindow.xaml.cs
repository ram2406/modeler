using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Modeler2WPF.Windows
{
    /// <summary>
    /// Interaction logic for ExecFormulaWindow.xaml
    /// </summary>
    public partial class ExecFormulaWindow : Window
    {
        public ExecFormulaWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
           // Analizator.Analizator.Instance.
            textBox2.Text = Analizator.Analizator.Instance.Eval(textBox_exp.Text).ToString();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
