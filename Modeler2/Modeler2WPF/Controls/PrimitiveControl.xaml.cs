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
using MainLogic;
using Modeler2WPF.Classes;
using System.Diagnostics;

namespace Modeler2WPF.Controls
{
    /// <summary>
    /// Прорисовка
    /// </summary>
    public  partial class PrimitiveDraw : UserControl
    {
        #region Properties & Fields
       
        /// <summary>
        /// Прорисовка : связи
        /// </summary>
        public List<ArrowControl> Arrows { get; private set; }

        

        #endregion

        public PrimitiveDraw()
        {
            //связи (прорисованные)
            this.Arrows = new List<ArrowControl>();

            //инициализация
            InitializeComponent();
        }
        public virtual void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
        
    }
    /// <summary>
    /// Движение
    /// </summary>
    public class PrimitiveMove : PrimitiveDraw
    {
        #region For Move
        /// <summary>
        /// опорная точка
        /// </summary>
        public static Point offset;

        /// <summary>
        /// Я уверен что у каждого контрола есть панель, уверен в своем коде
        /// </summary>
        protected Grid ParentGrid;
        /// <summary>
        /// Может двигаться
        /// </summary>
        public bool IsMove;
        #endregion

        public PrimitiveMove()
        {
            base.Loaded += (o, e) => { Move(); };
            
            
        }

       
        public void Move()
        {
                
            var panel = this.Parent as Grid;
            if (panel != null) 
            {
                Debug.WriteLine("Primitive double click " + this.Name);
                this.ParentGrid = panel;
                this.MouseDoubleClick += (o, e) =>
                {//двойной клик

                        this.IsMove = true;
                        offset = Mouse.GetPosition(this);
                        ParentGrid.Children.Remove(this);
                        ParentGrid.Children.Add(this);
                        PrimitivePosition();
                        
                        Debug.WriteLine("Primitive start move " + this.Name);
                    
                };
                
                 
                
                
                this.MouseMove += (o, e) =>
                {//движение мыши
                    this.PrimitivePosition();   
                };
                
            }
            else { this.ParentGrid = null;  }
        }
        private void PrimitivePosition()
        {
            
            if (this.ParentGrid != null && this.IsMove &&
                   Mouse.GetPosition(this.ParentGrid).X - offset.X > 0 &&
                   Mouse.GetPosition(this.ParentGrid).Y - offset.Y > 0
                   && Mouse.GetPosition(this.ParentGrid).X + this.Width - offset.X < this.ParentGrid.Width &&
                   Mouse.GetPosition(this.ParentGrid).Y + (this.Height - offset.Y) < this.ParentGrid.Height)
            {
                
                this.Opacity = 0.5;
                Point mousePos = Mouse.GetPosition(this.ParentGrid);
                this.Margin = new Thickness(mousePos.X - offset.X, mousePos.Y - offset.Y, 0, 0);
                
                
                    foreach (ArrowControl arrow in this.Arrows)
                    {
                        if (arrow.OutgoingPrimitiveControl == this) { arrow.X1 = mousePos.X - offset.X + arrow.OutgoingPrimitivePoint.X; arrow.Y1 = mousePos.Y - offset.Y + arrow.OutgoingPrimitivePoint.Y; }
                        if (arrow.ComingPrimitiveControl == this) { arrow.X2 = mousePos.X - offset.X + arrow.ComingPrimitivePoint.X; arrow.Y2 = mousePos.Y - offset.Y + arrow.ComingPrimitivePoint.Y; }
                    }

                
                
            }
        }

        
        

    }
    /// <summary>
    /// Логика
    /// </summary>
    public class PrimitiveControl : PrimitiveMove, IPrimitiveControl
    {
        private IPrimitive iPrimitive;

        /// <summary>
        /// Логика : соответствующий примтив
        /// </summary>
        public IPrimitive LogicPrimitive { get; private set; }
        
        internal static PrimitiveControl CurrentPrimitive { get;  set; }

        
        public PrimitiveControl(PrimitiveTypes primitiveType)
        {
            this.LogicPrimitive = Primitive.Create(primitiveType);
            InitControl();
            
            
        }
        private void InitControl()
        {
            this.Name = this.textBlock_name.Text = this.LogicPrimitive.Name;
            Binding myNewBindDef = new Binding("Name");
            myNewBindDef.Mode = BindingMode.OneWay;
            myNewBindDef.Source = LogicPrimitive;
            myNewBindDef.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            textBlock_name.SetBinding(TextBlock.TextProperty, myNewBindDef);
            Clicks();
            
        }
        public void Clicks()
        {
            this.MouseLeftButtonDown += (o, e) =>
            {
                var v = LogicPrimitive as MainLogic.Primitives.Generator;
                if (v != null) { v.RefreshVariables(); Debug.WriteLine(v.ReplyPeriod); }

                PrimitiveControl.CurrentPrimitive = this;

            };
            this.MouseLeftButtonUp += (o, e) =>
            {
                if (ArrowControl.CurrentArrow != null)
                {
                    ArrowControl.CurrentArrow.ArrowSet(this);
                }
            };
        }
        public PrimitiveControl(IPrimitive iPrimitive)
        {
            // TODO: Complete member initialization
            this.LogicPrimitive = iPrimitive;
            InitControl();
        }
        public override string ToString()
        {
            return this.LogicPrimitive.ToString() ;
        }

        public void Remove()
        {
            
            this.LogicPrimitive.Remove();
            this.ParentGrid.Children.Remove(this);
        }
        public override void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var win = (Window.GetWindow(this) as Windows.ModelerWindow);

            switch(((MenuItem)sender).Tag as string)
            {
                case "Delete": win.UpdateObjectList(); this.Remove(); ; break;
                case "Edit":win.SetSelectObject(this.LogicPrimitive) ;break;
            }
            

            
            base.MenuItem_Click(sender, e);
        }
    }
    

    public interface IPrimitiveControl 
    {
        IPrimitive LogicPrimitive { get; }
        
       

    }
}
