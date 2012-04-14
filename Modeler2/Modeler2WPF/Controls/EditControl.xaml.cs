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
using System.Windows.Navigation;
using System.Windows.Shapes;

using Modeler2WPF.Classes;
using System.Diagnostics;

namespace Modeler2WPF.Controls
{
    
    /// <summary>
    /// Interaction logic for EditPrimitiveControl.xaml
    /// </summary>
    public partial class EditorControl : UserControl
    {
         

        public EditorControl()
        {
            
            InitializeComponent();
            
        }

        public void SetEditorProperty(object obj)
        {
            GridMain.Children.Clear();
            if (obj != null)
            {
                #region Action
                var properties = obj.GetType().GetProperties();
                int i = 2; //текущая строка
                foreach (var property in properties)
                {
                    var arr = property.GetCustomAttributes(typeof(MainLogic.EditorAttribute), false);
                    if (arr.Count() > 0)
                    {
                        #region 
                        MainLogic.VariableAttribute ar2 = null;
                        var arr2 = property.GetCustomAttributes(typeof(MainLogic.VariableAttribute),false);
                        if(arr2.Count()>0) ar2 = arr2[0] as MainLogic.VariableAttribute;
                        var ar = arr[0] as MainLogic.EditorAttribute;
                        this.GridMain.RowDefinitions.Add(new RowDefinition() 
                                    { Height=new GridLength() /* Height = new GridLength(25),MinHeight = 25,MaxHeight=30*/ }
                                    );
                        Line p = new Line() 
                        { 
                            HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch, 
                            VerticalAlignment = System.Windows.VerticalAlignment.Bottom,
                            Stroke = Brushes.Azure,
                            X1 = 0, X2 = 1000,
                            Y1 = 0, Y2 = 0,
                            StrokeThickness = 0.5F
                        };
                        this.GridMain.Children.Add(p);
                        Grid.SetColumnSpan(p, 3);
                        Grid.SetRow(p, i);

                        {
                            
                            var ui = new TextBlock() { Text = ar.ToString(), TextWrapping=TextWrapping.Wrap};
                            //if(arr.Where(o=>o is EditorAttribute).Count()>0)
                            this.GridMain.Children.Add(ui);
                            Grid.SetRow(ui, i);

                        }
                        {
                            Binding myNewBindDef = new Binding(property.Name);
                            
                            if (ar.CanWrite && property.CanWrite)
                                myNewBindDef.Mode = BindingMode.TwoWay;
                            else myNewBindDef.Mode = BindingMode.OneWay;
                            myNewBindDef.Source = obj;
                            myNewBindDef.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                            UIElement ui;
                            if (ar.CanWrite &&( property.PropertyType == typeof(string) || property.PropertyType == typeof(int)))
                            {
                                ui = new TextBox() { Text = (property.GetValue(obj, null) ?? "(отсутствует)").ToString(), TextWrapping = TextWrapping.Wrap };                               
                                (ui as TextBox).SetBinding(TextBox.TextProperty, myNewBindDef);
                            }
                            else 
                            {
                                ui = new TextBlock() { Text = (property.GetValue(obj, null) ?? "(отсутствует)").ToString(), TextWrapping = TextWrapping.Wrap };
                                (ui as TextBlock).SetBinding(TextBlock.TextProperty, myNewBindDef);
                            }
                            this.GridMain.Children.Add(ui);
                            Grid.SetColumn(ui, 2);
                            Grid.SetRow(ui, i);
                            
                        }
                        if(ar2!= null)
                        {
                        	var ui = new TextBlock() { Text = ar2.ToString(), TextWrapping=TextWrapping.Wrap};
                            //if(arr.Where(o=>o is EditorAttribute).Count()>0)
                            this.GridMain.Children.Add(ui);
                            Grid.SetRow(ui, i);
                            Grid.SetColumn(ui,1);

                        }
                        Debug.WriteLine(property.GetValue(obj, null));
                        Debug.WriteLine(property.Name);
                        i++;
                        #endregion
                    }
                       
                    this.GridMain.RowDefinitions.Add(new RowDefinition() { Height = new GridLength() });
                }
                #endregion
            }
        }


    }

}
