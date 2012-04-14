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
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Modeler2WPF.Controls
{
    /// <summary>
    /// Interaction logic for AreaControl.xaml
    /// </summary>
    [Serializable()]
    public partial class AreaControl : UserControl
    {
        public MainLogic.Owner LogicOwner { get; set; }
        public string NameArea { get;private  set; }
        public string UserName { get;private set; }
        public string SavePath { get; private set; }

        

        public AreaControl(string Name, string Username, string SavePath)
        {
            this.LogicOwner = MainLogic.Owner.SetNewOwner(Name);
            this.UserName = Username;
            this.NameArea = Name;
            this.SavePath = SavePath;
            InitializeComponent();
            this.MouseLeftButtonUp += (o, e) =>
                {

                    if (PrimitiveControl.CurrentPrimitive != null)
                    {

                        PrimitiveControl.CurrentPrimitive.IsMove = false;
                        PrimitiveControl.CurrentPrimitive.Opacity = 1;
                        //if (ArrowControl.CurrentArrow != null) ArrowControl.CurrentArrow.ArrowSet(PrimitiveControl.CurrentPrimitive);
                        PrimitiveControl.CurrentPrimitive = null;
                    }

                    if (ArrowControl.CurrentArrow != null)
                    {
                        this.GridMain.Children.Remove(ArrowControl.CurrentArrow);
                        ArrowControl.CurrentArrow = null;
                    }
                     
                    
                    
                };
            
            this.MouseMove += (o, e) =>
                {
                    if (PrimitiveControl.CurrentPrimitive != null && !PrimitiveControl.CurrentPrimitive.IsMove && ArrowControl.CurrentArrow == null )
                    {

                        ArrowControl arrow = new ArrowControl(PrimitiveControl.CurrentPrimitive);
                        ArrowControl.CurrentArrow = arrow;
                        arrow.ArrowBegin();
                        
                        this.GridMain.Children.Add(arrow);
                    }
                    if (ArrowControl.CurrentArrow != null) 
                        ArrowControl.CurrentArrow.ArrowEndChange(this.GridMain);
                };
            
        }

        



        internal void AddObject(PrimitiveControl primitiveControl)
        {
            primitiveControl.VerticalAlignment = VerticalAlignment.Top;
            primitiveControl.HorizontalAlignment = HorizontalAlignment.Left;
            primitiveControl.Margin = new Thickness(10, 10, 0, 0);
            this.GridMain.Children.Add(primitiveControl);
        }
        internal void RemoveObject(PrimitiveControl primitiveControl)
        {
         
            //System.Diagnostics.Debug.WriteLine();
            //this.GridMain.Children.Remove(primitiveControl);
        }



        
    }
}
