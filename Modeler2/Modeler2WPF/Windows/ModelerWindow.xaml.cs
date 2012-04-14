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
using MainLogic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Modeler2WPF.Controls;
using Microsoft.Win32;

namespace Modeler2WPF.Windows
{
    /// <summary>
    /// Interaction logic for ModelerWindow.xaml
    /// </summary>
    public partial class ModelerWindow : Window
    {
        

        public AreaControl CurrentArea { get; private set; }
        
        private string Dir;



        public ModelerWindow()
        {
            InitializeComponent();
            string Dir = @"\Areas";
            if (!Directory.Exists(Dir))
            {
                Directory.CreateDirectory(Dir);
            }
            
            this.Loaded += (o, e) =>
             {
                 EventHandler h = (o1, e1) => 
                 { 
                     textblock_error.Text = o1.ToString();
                     if (grid2.Visibility == System.Windows.Visibility.Hidden)
                     {
                         grid2.Visibility = System.Windows.Visibility.Visible;
                     }
                 };
                 
                 Classes.Log.MessageChanged += h;
                 MainLogic.Log.MessageChanged += h;
                 Analizator.Log.MessageChanged += h;


                 
                 
             };
            
            comboBox_objects.SelectionChanged += (o, e) =>
                {
                    if (comboBox_objects.SelectedItem != null )
                    {
                        editPrimitiveControl1.SetEditorProperty(comboBox_objects.SelectedItem);
                        
                    }
                };
            
           
        }
        public void SetArea(AreaControl area)
        {
            contentControl.Content = area;
            CurrentArea = area;
            textBlock_username.Text = area.UserName;
            textBlock_namearea.Text = area.NameArea;
            MainLogic.Owner.SetOwner(CurrentArea.LogicOwner);
            comboBox_objects.ItemsSource = CurrentArea.LogicOwner.Primitives.OrderBy(o=>o.Name);
            comboBox_objects.DisplayMemberPath = "Name";
            if (comboBox_objects.Items.Count == 0) { /* comboBox_objects. = "Нет объектов" ; */}
            else comboBox_objects.SelectedIndex = 0;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

       

        private void Формулы_Редактировать_click(object sender, RoutedEventArgs e)
        {
            new Windows.FormulaWindow().Show();
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var c = sender as Control;
            if (c != null)
            {
                PrimitiveTypes type = PrimitiveTypes.Prototype;
                switch (c.Tag.ToString())
                {
                    case "Drive": type = PrimitiveTypes.Drive; break;
                    case "Processor": type = PrimitiveTypes.Processor; break;
                    case "Terminal": type = PrimitiveTypes.Terminal; break;
                    case "Generator": type = PrimitiveTypes.Generator; break;
                    case "Blocker": type = PrimitiveTypes.Blocker; break;
                    default: Debug.WriteLine("не определен тип"); type = PrimitiveTypes.Prototype; break;
                }

                if (CurrentArea != null)
                {
                    CurrentArea.AddObject(new Controls.PrimitiveControl(type));
                    UpdateObjectList();
                }
            }
            
        }

        public void UpdateObjectList()
        {
            this.comboBox_objects.ItemsSource = null;
            this.comboBox_objects.ItemsSource = CurrentArea.LogicOwner.Primitives.OrderBy(o => o.Name);
        }
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            var c = sender as Control;
            if (c != null)
            {
                
                switch (c.Tag.ToString())
                {
                    case "Run": this.CurrentArea.LogicOwner.Run(); break;
                   // case "Stop": this.CurrentOwner.Stop() ; break;
                   // case "Step": this.CurrentOwner.Step( int.Parse( textBox_targetValue.Text) ); break;
                    default: Debug.WriteLine("нет варианта");  break;
                }

                
            }

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            new ExecFormulaWindow().Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Analizator.Analizator.Instance.UpdateFormulas();
            MainLogic.Owner.Instance.SetVariables(Analizator.Analizator.Instance.Parser);
            textBox_formula.Text = Analizator.Analizator.Instance.Eval(textBox_formula.Text ).ToString();
            Analizator.Analizator.Instance.Parser.GetVar().ToList().ForEach(o=>Debug.WriteLine("{0},{1}",o.Key, o.Value.Value));
        }

        private void grid2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            grid2.Visibility = System.Windows.Visibility.Hidden;
        }

        private void MenuItem_File(object sender, RoutedEventArgs e)
        {
            Control control = (Control)sender;
            switch (control.Tag as string)
            {
                case "Save": 
                    {
                        if (CurrentArea == null) return;
                        Stream TestFileStream = File.Create(CurrentArea.SavePath);
                        BinaryFormatter serializer = new BinaryFormatter();
                        var s = Classes.SerializeOwner.GetSerializeOwner(CurrentArea);
                        serializer.Serialize(TestFileStream, s);
                        
                        TestFileStream.Close();
                    }; break;
                case "Save as":
                    {
                        if (CurrentArea == null) return;
                        
                        SaveFileDialog dialog = new SaveFileDialog();
                        dialog.InitialDirectory = CurrentArea.SavePath;
                        dialog.Filter = "(*.ma)|*.ma";
                        dialog.FileName = CurrentArea.SavePath;
                        dialog.ShowDialog();
                        if(dialog.FileName != string.Empty)
                        {
                            Stream TestFileStream = File.Create(dialog.FileName);
                            BinaryFormatter serializer = new BinaryFormatter();
                            var s = Classes.SerializeOwner.GetSerializeOwner(CurrentArea);
                            serializer.Serialize(TestFileStream,s  );
                            
                            TestFileStream.Close();
                        }
                    }
                    break;
                case "Open":
                    {
                        OpenFileDialog dialog = new OpenFileDialog();
                        dialog.InitialDirectory = Directory.GetCurrentDirectory();
                        dialog.Filter = "(*.ma)|*.ma";
                        //dialog.FileName = CurrentArea.SavePath;
                        dialog.ShowDialog();
                        if (dialog.FileName != string.Empty)
                        {
                            Stream TestFileStream = File.OpenRead(dialog.FileName);
                            BinaryFormatter deserializer = new BinaryFormatter();
                            SetArea( ((Classes.SerializeOwner)deserializer.Deserialize(TestFileStream)).GetDeserializeOwner());
                            TestFileStream.Close();
                            TestFileStream.Dispose();
                        }
                    }
                    break;
                case "Create":
                    {
                        new Windows.CreateFileWindow(this).Show();
                        
                        

                    }

                    break;
            }
            
        }
        public void SetSelectObject(IPrimitive selectPrimitive)
        {
            comboBox_objects.SelectedItem = selectPrimitive;
        }

        
        
       
        
    }
    
}
