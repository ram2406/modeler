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
using Modeler2WPF.Controls;
using Analizator;

namespace Modeler2WPF.Windows
{
    /// <summary>
    /// Interaction logic for FormulaWindow.xaml
    /// </summary>
    public partial class FormulaWindow : Window
    {
        int КоличествоПеременных; // не больше пяти
        Analizator.IParserFormula currentFormula;
        public FormulaWindow()
        {
            InitializeComponent();
            SetFormulas();
            Analizator.Log.MessageChanged += (o, e) =>
                {
                    textBlock5.Text = o.ToString();
                };
            listBox1.SelectionChanged += (o, e) =>
            {

                SetDataSource(GetSelected());
            };
        }

        private IParserFormula GetSelected()
        {
            return listBox1.SelectedItem as IParserFormula;
        }

        private void SetFormulas()
        {
            listBox1.ItemsSource = null;
            listBox1.ItemsSource = Analizator.Analizator.Instance.Formulas;
            listBox1.DisplayMemberPath = "Name";

            


        }
        private void SetBinding(string property,TextBox t , bool CanWrite = true)
        {
            Binding myNewBindDef = new Binding(property);
            if (CanWrite)
                myNewBindDef.Mode = BindingMode.TwoWay;
            else myNewBindDef.Mode = BindingMode.OneWay;
            myNewBindDef.Source = currentFormula;
            myNewBindDef.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            t.SetBinding(TextBox.TextProperty, myNewBindDef);

        }

        private void SetDataSource(IParserFormula formula)
        {
            if (formula != null)
            {
                currentFormula = formula;
                SetBinding("Name", textBox_name);
                SetBinding("Expression", textBox_expression);
                SetBinding("Variables", textBox_variables);
                SetBinding("Call", textBox_exec,false);
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
           

           // Analizator.Analizator.Instance.AddNewFormula(new Analizator.ParserFormula(textBox_name.Text,textBox_expression.Text,
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Analizator.Analizator.Instance.UpdateFormulas();
            textBlock_res.Text = Analizator.Analizator.Instance.Eval(textBox_test.Text).ToString();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {

            Analizator.Analizator.Instance.Formulas.Remove(GetSelected());
            SetFormulas();
            if(listBox1.Items.Count>0)
            listBox1.SelectedIndex = 0;
            SetDataSource(GetSelected());
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
             var r = new ParserFormula("NewFormula","x*2",new ParserVar("x",0));
             Analizator.Analizator.Instance.AddNewFormula(r);
             SetFormulas();
             SetDataSource(r );
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (!currentFormula.IsSys)
            {
                string path =
                (System.IO.Directory.GetCurrentDirectory())+@"\Formulas";
                if (!System.IO.Directory.Exists(path)) { System.IO.Directory.CreateDirectory(path); }
                System.IO.Stream TestFileStream = System.IO.File.Create(System.IO.Path.Combine( path , currentFormula.Name + ".mf"));
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter serializer = 
                    new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                serializer.Serialize(TestFileStream, currentFormula);
                TestFileStream.Dispose();
            }
        }

        

    }
}
