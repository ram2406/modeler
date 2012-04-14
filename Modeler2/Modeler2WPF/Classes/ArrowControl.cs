using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;
using MainLogic;
using Modeler2WPF.Controls;
using System.Windows.Controls;
using System.Windows.Input;

namespace Modeler2WPF.Classes
{
    public class ArrowControl :ArrowLine
    {
        public ILink LogicLink {get; private set;}
        public PrimitiveControl OutgoingPrimitiveControl { get; private set; }
        public PrimitiveControl ComingPrimitiveControl { get; private set; }
        internal static ArrowControl CurrentArrow { get;  set; }

        private static Point offset;

        public Point ComingPrimitivePoint, OutgoingPrimitivePoint;

        public ArrowControl(Controls.PrimitiveControl pc)
        {
            this.MouseLeftButtonDown += (o, e) =>
            {
                if (e.ClickCount == 2)
                {
                    this.ArrowDelete();
                }
            };
            this.OutgoingPrimitiveControl = pc;  
        }
        public void SetLink(PrimitiveControl pc)
        {
            
            if (this.LogicLink != null)
            {//если уже была связь 
                this.LogicLink.Delete();
                Log.SetMessage("Связь " + this.LogicLink.ToString() + " была удалена");
            }
           
                var link = MainLogic.Link.Create(OutgoingPrimitiveControl.LogicPrimitive, pc.LogicPrimitive);
                if (link.Validate())
                {
                    this.LogicLink = link;
                }
                else Log.SetMessage("Связь "+link.ToString()+" не была создана");
            
        }
        internal void LoadArrow(PrimitiveControl p, ILink link, Point s , Point e, Point cp , Point op)
        {
            ComingPrimitiveControl = p; LogicLink = link;
            this.X1 = s.X; this.Y1 = s.Y;
            this.X2 = e.X; this.Y2 = e.Y;
            this.Stroke = Brushes.OrangeRed;
            this.ComingPrimitivePoint = cp;
            this.OutgoingPrimitivePoint = op;
            this.StrokeThickness = 1;
        }


        internal void ArrowBegin()
        {
           // if (объектТекущий != null) { объектВыбранный = объектТекущий; объектТекущий = null; }


            CurrentArrow = this;

            //определить где мышь 
            

                offset.X = this.OutgoingPrimitiveControl.Margin.Left + this.OutgoingPrimitiveControl.Width; 
                offset.Y = this.OutgoingPrimitiveControl.Margin.Top + this.OutgoingPrimitiveControl.Height / 2;
                this.Stroke = Brushes.OrangeRed;
                //this.Fill = Brushes.CornflowerBlue;
                this.StrokeThickness = 1;
                this.X1 = offset.X;
                this.Y1 = offset.Y;


                

                
                Panel.SetZIndex(this, -1);
                
                
            

        }

        internal void ArrowEndChange(Grid currentGrid)
        {
            Point mousePos = Mouse.GetPosition(currentGrid);
            this.X2 = mousePos.X;
            this.Y2 = mousePos.Y;



        }



        internal void ArrowSet(PrimitiveControl primitiveControl)
        {
            this.ComingPrimitiveControl = primitiveControl;
            this.OutgoingPrimitiveControl.Arrows.Add(this);
            this.ComingPrimitiveControl.Arrows.Add(this);
            this.LogicLink = Link.Create(OutgoingPrimitiveControl.LogicPrimitive, ComingPrimitiveControl.LogicPrimitive);

            
            
            


            if (this.LogicLink != null)
            {
                Point center = new Point(ComingPrimitiveControl.Width / 2, ComingPrimitiveControl.Height / 2);

                Point point = new Point(this.X2 - ComingPrimitiveControl.Margin.Left, this.Y2 - ComingPrimitiveControl.Margin.Top);
                //33-25
                double x = point.X - center.X;
                double y = point.Y - center.Y;
                if ((x > 0 ? x : -x) > (y > 0 ? y : -y))
                { /*значит смещенее по x больше*/
                    this.Y2 = center.Y +  ComingPrimitiveControl.Margin.Top; ComingPrimitivePoint.Y = center.Y;

                    OutgoingPrimitivePoint.Y = center.Y; this.Y1 = OutgoingPrimitiveControl.Margin.Top + center.Y;
                    if (x < 0) 
                    { 
                        this.X2 = ComingPrimitiveControl.Margin.Left; 
                        ComingPrimitivePoint.X = 0;
                        OutgoingPrimitivePoint.X = OutgoingPrimitiveControl.Width;
                        this.X1 = OutgoingPrimitiveControl.Margin.Left + OutgoingPrimitivePoint.X;
                        /*значит левее*/ 
                    } 
                    else 
                    { 
                        this.X2 = ComingPrimitiveControl.Margin.Left + ComingPrimitiveControl.Width; 
                        ComingPrimitivePoint.X = ComingPrimitiveControl.Width ;
                        OutgoingPrimitivePoint.X = 0;
                        this.X1 = OutgoingPrimitiveControl.Margin.Left + OutgoingPrimitivePoint.X;
                        /*значит правее*/
                    }
                }
                else
                {
                    this.X2 = center.X + ComingPrimitiveControl.Margin.Left;   ComingPrimitivePoint.X = center.X;
                    OutgoingPrimitivePoint.X = center.X; OutgoingPrimitivePoint.Y = center.Y; this.X1 = OutgoingPrimitiveControl.Margin.Left + center.X;
                    if (y > 0) 
                    { 
                        this.Y2 = ComingPrimitiveControl.Margin.Top + ComingPrimitiveControl.Height; 
                        ComingPrimitivePoint.Y = ComingPrimitiveControl.Height;
                        OutgoingPrimitivePoint.Y = 0;
                        this.Y1 = OutgoingPrimitiveControl.Margin.Top + OutgoingPrimitivePoint.Y;
                        
                        /*значит выше*/ 
                    } 
                    else 
                    { 
                        this.Y2 = ComingPrimitiveControl.Margin.Top; 
                        ComingPrimitivePoint.Y = 0;
                        OutgoingPrimitivePoint.Y = OutgoingPrimitiveControl.Height ;
                        this.Y1 = OutgoingPrimitiveControl.Margin.Top + OutgoingPrimitivePoint.Y;
                        /*значит ниже*/ 
                    }
                }
                
                
                CurrentArrow = null;
            }
            else this.ArrowDelete();

        }
        
        internal void ArrowDelete()
        {
            this.ComingPrimitiveControl = null;
            (this.Parent as Grid).Children.Remove(this);
            if(this.LogicLink!= null) this.LogicLink.Delete();
        }
        public override string ToString()
        {
            return this.LogicLink.ToString();
        }

    }
}
