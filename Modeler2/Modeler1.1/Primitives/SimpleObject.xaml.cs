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
using Modeler1._1.Primitives.arrow;

namespace Modeler1._1.Primitives
{
    /// <summary>
    /// Interaction logic for SimpleObject.xaml
    /// </summary>
    /// 

    public enum PrimitivesTypes { Генератор, Накопитель, Блокиратор, Процессор, Терминал, БазовыйОбъект }
    public delegate bool MyFun();
    public partial class БазовыйОбъект : UserControl
    {


        #region Empty
        #endregion
        #region Properties

        public string imageName { get; set; }

        public string Наименование { get; set; }
        public PrimitivesTypes Тип { get; set; }

       
        public List<Связь> Связи { get; private set; }
        public bool ЕстьСвязи { get; private set; }

       

        public List<БазовыйОбъект> ОбъектыПредыдущие
        {
            get;
            private set;
        }
        public List<БазовыйОбъект> ОбъектыСледующие
        {
            get;
            private set;
        }

        public bool ЕстьПредыдущиеОбъекты { get { if (ОбъектыПредыдущие.Count > 0) return true; else return false; } }

        public bool ЕстьСледующиеОбъекты { get { if (ОбъектыСледующие.Count > 0) return true; else return false; } }

        

        protected MyFun _Push;
        protected MyFun _Pop;

        protected Action _Step;
        

        private ГлавноеОкно win;
        #endregion
        #region Main
        public БазовыйОбъект()
        {
            InitializeComponent();
            Тип = PrimitivesTypes.БазовыйОбъект;
            tb_type.Text = Тип.ToString();
            ОбъектыПредыдущие = new List<БазовыйОбъект>();
            ОбъектыСледующие = new List<БазовыйОбъект>();
            Связи = new List<Связь>();
            
           
        }
        #endregion
        #region Arrows
        public void AddArrows(Связь current_arrow)
        {
            if (current_arrow != null)
            {
                Связи.Add(current_arrow);

                if (current_arrow.ОбъектСледующий == this && current_arrow.ОбъектПредыдущий != null)
                {
                    ОбъектыПредыдущие.Add(current_arrow.ОбъектПредыдущий);
                    current_arrow.ОбъектПредыдущий.AddObjectEnd(current_arrow.ОбъектСледующий);
                }

                ЕстьСвязи = true;
            }
        }
        public void DeleteLinks(Связь current_arrow)
        {
            if (current_arrow != null)
            {
                if (current_arrow.ОбъектПредыдущий != null && current_arrow.ОбъектПредыдущий != this) { current_arrow.ОбъектПредыдущий.DeleteLinksEnd(current_arrow); }
                if (current_arrow.ОбъектСледующий != null && current_arrow.ОбъектСледующий != this) { current_arrow.ОбъектСледующий.DeleteLinksStart(current_arrow); }


            }

        }

        protected void AddObjectEnd(БазовыйОбъект obj)
        {
            ОбъектыСледующие.Add(obj);
        }
        protected void DeleteLinksStart(Связь arrow)
        {
            ОбъектыПредыдущие.Remove(arrow.ОбъектПредыдущий);
            arrow.ОбъектСледующий.DeleteArrow(arrow);

        }
        protected void DeleteLinksEnd(Связь arrow)
        {
            ОбъектыСледующие.Remove(arrow.ОбъектСледующий);
            arrow.ОбъектПредыдущий.DeleteArrow(arrow);
        }
        public void DeleteArrow(Связь arrow)
        {
            Связи.Remove(arrow);
        }

       
        #endregion
        #region Objects
        public void SetParameters()
        {
            con_menu_name.Header = Наименование;
            tb_type.Text = Тип.ToString();
        }

        #endregion
        #region Events 
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            
           
            
        }
        #endregion

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            win.area1.ArrowBegin();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            win = Window.GetWindow(this) as ГлавноеОкно;
            if (imageName != null) 
            { 
                Chrome.Fill = new ImageBrush() { ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/images/" + imageName)) }; 
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            win.area1.ObjectDelete();
        }
        public bool Push()
        {
            
            return _Push();
        }
        public bool Pop()
        {
            return _Pop();
        }
        protected void SetPush(MyFun push)     //задать функцию получения заявки 
        {
            _Push = push;

        }
        protected void SetPop(MyFun pop)       //задать функцию отдачи заявки
        {
            _Pop = pop;
        }
        protected void SetStep(Action step)    //задать функцию такта
        {
            this._Step = step; 
        }
    }
}
