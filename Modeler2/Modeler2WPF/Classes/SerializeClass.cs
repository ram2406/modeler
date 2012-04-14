using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Modeler2WPF.Controls;
using System.Diagnostics;

namespace Modeler2WPF.Classes
{
    [Serializable]
    class SerializeOwner
    {
        public string Name { get; private set; }
        public string UserName { get; private set; }
        public string Path { get; private set; }
        public MainLogic.Owner LogicOwner { get; private set; }
        public List<SerializePrimitive> sPrimitives { get;  set; }
        public List<SerializeArrows> sArrows { get;  set; }

        public static SerializeOwner GetSerializeOwner(AreaControl     area)
        {
            SerializeOwner so = new SerializeOwner();
            so.UserName = area.UserName;
            so.Name = area.NameArea;
            so.Path = area.SavePath;
            so.sPrimitives = new List<SerializePrimitive>();
            so.sArrows = new List<SerializeArrows>();
            so.LogicOwner = area.LogicOwner;
            foreach (var r in area.GridMain.Children)
            {
                if (r is PrimitiveControl) 
                { 
                    var p = r as PrimitiveControl;

                    so.sPrimitives.Add
                        (new SerializePrimitive
                            (new Point(p.Margin.Left,p.Margin.Top)
                            ,p.LogicPrimitive
                            )
                        );
 
                } else
                if (r is ArrowControl) 
                {
                    var a = r as ArrowControl;

                    so.sArrows.Add
                        (new SerializeArrows
                            (new Point(a.X1, a.Y1), new Point(a.X2, a.Y2), a.LogicLink,a.ComingPrimitivePoint,a.OutgoingPrimitivePoint)
                            );
                } 
                
            }

            return so;
        }
        public  AreaControl GetDeserializeOwner()
        {
            MainLogic.Owner.SetOwner(LogicOwner);
            AreaControl area = new AreaControl(Name, UserName, Path);
            area.LogicOwner = LogicOwner;
            

            foreach (var r in sPrimitives)
            {
                var p = new PrimitiveControl(r.LogicPrimitive);
                area.AddObject(p);
                p.Margin = new Thickness(r.Offset.X, r.Offset.Y, 0, 0);
                

            }

            foreach (var r in sArrows)
            {
                PrimitiveControl pc = null;
                foreach (var f in area.GridMain.Children)
                {
                    if (f is PrimitiveControl)
                    {
                        var p = f as PrimitiveControl;
                        if (p.LogicPrimitive == r.Link.OutgoingPrimitive)
                        {
                            pc = p;
                            
                            break;
                        }
                    }
                }
                var a = new ArrowControl(pc);
                pc.Arrows.Add(a);
                foreach (var f in area.GridMain.Children)
                {
                    if (f is PrimitiveControl)
                    {
                        var p = f as PrimitiveControl;
                        if (p.LogicPrimitive == r.Link.ComingPrimitive)
                        {
                            pc = p;
                            break;
                        }
                    }
                }
                pc.Arrows.Add(a);
                a.LoadArrow(  pc,r.Link,r.Start,r.End,r.cp,r.op);
                area.GridMain.Children.Add(a);
                
            }

            return area;
        }


    }
    [Serializable]
    class SerializePrimitive
    {
        public Point Offset { get; private set; }
        public MainLogic.IPrimitive LogicPrimitive { get; private set; }
        public SerializePrimitive(Point Offset,MainLogic.IPrimitive logicPrim)
        {
            this.Offset = Offset;
            this.LogicPrimitive = logicPrim;
        }
    }
    [Serializable]
    class SerializeArrows
    {
        public Point Start { get; private set; }
        public Point End { get; private set; }
        public Point cp { get; private set; }
        public Point op { get; private set; }
        public MainLogic.ILink Link { get; private set; }

        public SerializeArrows(Point start, Point end , MainLogic.ILink link,Point cp , Point op)
        {
            Start = start; End = end; Link = link; this.op = op; this.cp = cp;
        }
    }
}
