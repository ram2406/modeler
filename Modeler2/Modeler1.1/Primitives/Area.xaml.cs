using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Modeler1._1.Primitives.arrow;
using System.Windows.Shapes;
using System.Collections.Generic;


namespace Modeler1._1.Primitives
{
    /// <summary>
    /// Interaction logic for Area.xaml
    /// </summary>
    public partial class Area : UserControl
    {
        #region Properties
        //----------------------------------
        private БазовыйОбъект объектТекущий;
        private БазовыйОбъект объектВыбранный;
        public List<БазовыйОбъект> Объекты { get; set; }
        private Связь текущаяСвязь;
        
        private Point offset;
        private ГлавноеОкно win;
        private bool IsRun;
        private int[] mas { get; set; }
        //----------------------------------
        #endregion
        #region Main
       

        public Area()
        {
            InitializeComponent();
            mas = new int[5];
            Объекты = new List<БазовыйОбъект>();
        }
        private void AreaGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ObjectMove(e.Source);
            ObjectSelected();
            ArrowEnd();
            ArrowSelect(e.Source );
            

        }
        private void Win_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
            ObjectEnd();

            Linked(e.Source);
            ArrowEnd();
            ArrowMove(false);
        }
        private void win_MouseMove(object sender, MouseEventArgs e)
        {
            ObjectPosition();
            
            ArrowMove(true);
        }

        private void AreaGrid_Loaded(object sender, RoutedEventArgs e)
        {
            win = Window.GetWindow(this) as ГлавноеОкно;
            offset.Offset(0, 0);
        }

        private void AreaGrid_KeyDown(object sender, KeyEventArgs e) 
        {
           
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e) { }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            ObjectEnd();
        }
        #endregion
        #region Object
        public void ObjectAdd(БазовыйОбъект newObject)
        {

            newObject.VerticalAlignment = VerticalAlignment.Top;
            newObject.HorizontalAlignment = HorizontalAlignment.Left;
            newObject.Margin = new Thickness(10, 10, 0, 0);
             
            
            string s=null;
            switch (newObject.Тип)
            {
                case PrimitivesTypes.Генератор:s=(++mas[0]).ToString() ;break ;
                case PrimitivesTypes.Накопитель: s = (++mas[1]).ToString(); break;
                case PrimitivesTypes.Процессор: s = (++mas[2]).ToString(); break;
                case PrimitivesTypes.Блокиратор: s = (++mas[3]).ToString(); break;
                case PrimitivesTypes.Терминал: s = (++mas[4]).ToString(); break;
            }
            
            newObject.Наименование = newObject.Тип.ToString() + s;
            newObject.SetParameters();
            this.Объекты.Add(newObject);
            AreaGrid.Children.Add(newObject);

        }
       
        public void ObjectDelete()
        {
            if (объектВыбранный != null)
            {

                foreach (Связь arrow in объектВыбранный.Связи)
                {
                    объектВыбранный.DeleteLinks(arrow);
                   
                    AreaGrid.Children.Remove(arrow);

                }
                AreaGrid.Children.Remove(объектВыбранный);
            }
        }
        private void ObjectPosition()
        {
            if (объектТекущий != null)
            {
                if (this.объектТекущий != null &&
                    Mouse.GetPosition(this).X - offset.X > 0 &&
                    Mouse.GetPosition(this).Y - offset.Y > 0
                    && Mouse.GetPosition(this).X + объектТекущий.Width - offset.X < this.Width &&
                    Mouse.GetPosition(this).Y + (объектТекущий.Height - offset.Y) < this.Height)
                {
                    объектТекущий.Opacity = 0.5;
                    Point mousePos = Mouse.GetPosition(this);
                    объектТекущий.Margin = new Thickness(mousePos.X - offset.X, mousePos.Y - offset.Y, 0, 0);

                    if (объектТекущий.ЕстьСвязи)
                    {
                        foreach (Связь arrow in объектТекущий.Связи)
                        {
                            if (arrow.ОбъектПредыдущий == объектТекущий) { arrow.X1 = mousePos.X - offset.X + объектТекущий.Width; arrow.Y1 = mousePos.Y - offset.Y + объектТекущий.Height / 2;}
                            if (arrow.ОбъектСледующий == объектТекущий) {  arrow.X2 = mousePos.X - offset.X; arrow.Y2 = mousePos.Y - offset.Y + объектТекущий.Height / 2;}
                        }

                    }
                }
            }

        }
        private void ObjectSelected()
        {
            if (this.объектТекущий != null && объектВыбранный != объектТекущий)
            {
                ItSelect();

                объектВыбранный = объектТекущий;
                объектВыбранный.Chrome.Stroke = Brushes.Black;
                
                string s = null ;
                foreach (БазовыйОбъект obj in объектВыбранный.ОбъектыПредыдущие)
                {
                    s += obj.Наименование+' ';
                }
                s += "::";
                foreach (БазовыйОбъект obj in объектВыбранный.ОбъектыСледующие)
                {
                    s += ' '+obj.Наименование  ;
                }
                s += "="+ объектВыбранный.Связи.Count.ToString() ;
                tb_Stat.Text = s;
                tb_type_name.Text = объектВыбранный.Наименование;
            }
        }
        private void ObjectEnd()
        {
            if (this.объектТекущий != null)
            {
                объектТекущий.Opacity = 1;
                this.объектТекущий = null;
            }
        }
        private void ObjectMove(object obj)
        {
            if (obj is БазовыйОбъект)
            {
                объектТекущий = obj as БазовыйОбъект;

                offset = Mouse.GetPosition(объектТекущий);
                AreaGrid.Children.Remove(объектТекущий);

                AreaGrid.Children.Add(объектТекущий);
                ObjectPosition();
            }
            
        }
        //-------------------------------------------
        #endregion
        #region Arrow
        public void ArrowSelect(object obj)
        {
            if (obj !=текущаяСвязь&& obj is Связь)
            {
                    текущаяСвязь = obj as Связь;
                     
            }
        }
        public void ArrowBreak()
        {
            if (текущаяСвязь != null)
            {
                if (текущаяСвязь.ОбъектПредыдущий != null) { текущаяСвязь.ОбъектПредыдущий.DeleteArrow(текущаяСвязь); }
                if (текущаяСвязь.ОбъектСледующий != null) { текущаяСвязь.ОбъектСледующий.DeleteArrow(текущаяСвязь); }
                AreaGrid.Children.Remove(текущаяСвязь); текущаяСвязь = null;
                

            }
        }
        public void ArrowBegin()
        {
            if (объектТекущий != null) { объектВыбранный=объектТекущий ;объектТекущий = null; }
            if (объектВыбранный != null)
            {

                
                текущаяСвязь = new Связь();
                offset.X = объектВыбранный.Margin.Left + объектВыбранный.Width; offset.Y = объектВыбранный.Margin.Top + объектВыбранный.Height / 2;
                текущаяСвязь.Stroke = Brushes.Red;
                текущаяСвязь.StrokeThickness = 3;
                текущаяСвязь.X1 = offset.X;
                текущаяСвязь.Y1 = offset.Y;
                ArrowEndChange();
                
                AreaGrid.Children.Add(текущаяСвязь);
                Panel.SetZIndex(текущаяСвязь, -1);
                текущаяСвязь.ОбъектПредыдущий = объектВыбранный;
                объектВыбранный.AddArrows(текущаяСвязь);
            }

        }
       
        private void ArrowEnd()
        {
            if (текущаяСвязь != null && объектВыбранный != null)
            {
                текущаяСвязь.ОбъектПредыдущий.Opacity = 1;
                текущаяСвязь = null;

            }
        }
        private void ArrowMove(bool move)
        {
            if (текущаяСвязь != null)
            {
                if (move)
                {

                    ArrowEndChange();
                    
                }
                else
                {
                    
                    текущаяСвязь = null;
                    
                }
            }
        }
        private void Linked(object obj)
        {
            if (текущаяСвязь != null && obj is БазовыйОбъект)
            {

                БазовыйОбъект curObj = obj as БазовыйОбъект;
                if (ArrowSetLink(curObj) != null)
                {

                   
                    // если не нарушает условий страшной логики
                    текущаяСвязь.ОбъектПредыдущий = объектВыбранный;
                    текущаяСвязь.ОбъектСледующий = curObj;
                    curObj.AddArrows(текущаяСвязь);
                    текущаяСвязь.X2 = curObj.Margin.Left; текущаяСвязь.Y2 = curObj.Margin.Top + curObj.Height / 2;
                }
                else ArrowBreak();
            }
        }
        private БазовыйОбъект ArrowSetLink(БазовыйОбъект obj)
        {
            // страшная логика по условиям по тз
                if (текущаяСвязь.ОбъектПредыдущий != obj&&
                    !(obj is Генератор) && !(текущаяСвязь.ОбъектПредыдущий is Терминал)
                    )
                {
                    if (текущаяСвязь.ОбъектПредыдущий is Генератор) 
                    {
                        if (obj is Блокиратор || obj is Накопитель) return obj;
                        
                    }
                    if (текущаяСвязь.ОбъектПредыдущий is Блокиратор)
                    {
                        if (obj is Накопитель || obj is Процессор || obj is Терминал) return obj;
                        
                    }
                    if (текущаяСвязь.ОбъектПредыдущий is Накопитель)
                    {
                        if (obj is Блокиратор || obj is Процессор) return obj;
                        
                    }
                    if(текущаяСвязь.ОбъектПредыдущий is Процессор)
                    {
                        if (obj is Блокиратор || obj is Накопитель || obj is Терминал) return obj;
                        
                    }

                }

                return null;
        }
        private void ArrowAddMoreLink()
        {
            Связь newArrow = new Связь();
            Point mousePos = Mouse.GetPosition(this);
            newArrow.X1 = mousePos.X;
          //  newArrow.Y1=current_arrow.Margin.Top+
            //(x-x3)/[-(у2-y1)]=(y-y3)/(x2-x1)
        }
        #endregion
        #region Single
        private void ItSelect()
        {
            if (объектВыбранный != null) { объектВыбранный.Chrome.Stroke = Brushes.Red; объектВыбранный = null; }


        }
        private void ArrowEndChange()
        {
            Point mousePos = Mouse.GetPosition(this);
            текущаяСвязь.X2 = mousePos.X;
            текущаяСвязь.Y2 = mousePos.Y;
                    //if (current_arrow.X2 < mousePos.X) current_arrow.X2 = mousePos.X - 5;
                    //else { current_arrow.X2 = mousePos.X + 5; }

                    //if (current_arrow.Y2 < mousePos.Y) { current_arrow.Y2 = mousePos.Y - 5; }
                    //else { current_arrow.Y2 = mousePos.Y + 5; }
        }

        

        #endregion

        #region Pathes
       
        
        

        #endregion Pathes

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
        public void Run()
        {
            IsRun = true;
            while (IsRun)
            {
                foreach (БазовыйОбъект obj in Объекты)
                {
                    
                }
            }
        }
        public void Stop()
        {
            IsRun = false;
        }
    }
}
