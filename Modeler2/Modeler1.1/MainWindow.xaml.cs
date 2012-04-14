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
using Modeler1._1.Primitives;

namespace Modeler1._1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ГлавноеОкно : Window
    {
        private bool ShiftIsPassed;
        

        public ГлавноеОкно()
        {
            InitializeComponent();
            
        }


        
        

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift) { ShiftIsPassed = true; }
        }

       

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift) { ShiftIsPassed = false; area1.ArrowBreak(); }
            if (e.Key == Key.Delete) { area1.ObjectDelete(); }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ShiftIsPassed) { area1.ArrowBegin();  }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            area1.AreaGrid.Children.Clear();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            area1.ObjectAdd(new Блокиратор());
           
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            area1.ObjectAdd(new Процессор());

           // Analisator.Analisator a = new Analisator.Analisator();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            area1.ObjectAdd(new Накопитель());
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            area1.ObjectAdd(new Терминал());
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            МатрицаПутей p = new МатрицаПутей();
            foreach (var s in area1.Объекты)
            {
               if(s is Генератор) p.GeneratePathes(s);
            }
            string text=null;
            foreach (var s in p.Пути)
            {
                string str = null;
                foreach (var o in s.Объекты)
                {
                    str += o.Наименование + ' ';
                }
                text+=str+'\n';
            }
            tb_stat.Text +='\n'+ text;
            
            
        }

        private void area1_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            area1.ObjectAdd(new Генератор());
        }

        private void MenuItem_Click_7(object sender, RoutedEventArgs e)
        {
          

        }

       
    }
   
}
