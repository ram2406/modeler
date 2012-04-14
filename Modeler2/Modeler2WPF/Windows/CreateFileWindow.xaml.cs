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
using Microsoft.Win32;
using Modeler2WPF.Controls;
using System.IO;

namespace Modeler2WPF.Windows
{
    /// <summary>
    /// Interaction logic for CreateFileWindow.xaml
    /// </summary>
    public partial class CreateFileWindow : Window
    {
        private ModelerWindow modelerWindow;

        public string Path { get { return System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Areas");  } }
        public string NameArea { get; set; }
        public string UserName { get; set; }
        public string FileName { get { return System.IO.Path.Combine( this.Path , NameArea+".ma"); } }
        public AreaControl area { get; set; }


        public CreateFileWindow(ModelerWindow modelerWindow)
        {
            // TODO: Complete member initialization
            this.modelerWindow = modelerWindow;
            NameArea = "New";

            InitializeComponent();
            {
                Binding myNewBindDef = new Binding("NameArea");

                myNewBindDef.Mode = BindingMode.TwoWay;
                myNewBindDef.Source = this;
                myNewBindDef.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                textBox1.SetBinding(TextBox.TextProperty, myNewBindDef);
            }
            {
                Binding myNewBindDef = new Binding("UserName");

                myNewBindDef.Mode = BindingMode.TwoWay;
                myNewBindDef.Source = this;
                myNewBindDef.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                textBox2.SetBinding(TextBox.TextProperty, myNewBindDef);
            }
            {
                Binding myNewBindDef = new Binding("FileName");

                myNewBindDef.Mode = BindingMode.OneWay;
                myNewBindDef.Source = this;
                myNewBindDef.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                textBox3.SetBinding(TextBox.TextProperty, myNewBindDef);
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog op = new SaveFileDialog();
            op.FileName = FileName;
            if (!Directory.Exists(this.Path)) { Directory.CreateDirectory(this.Path); }
            op.Filter = "(*.ma)|*.ma";
            op.InitialDirectory = this.Path;
            op.ShowDialog();
            if (op.FileName != string.Empty)
            {
                textBox3.Text = op.FileName;
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            area = new AreaControl(NameArea,UserName,FileName);
            modelerWindow.SetArea(area);
            this.Close();
        }
    }
}
