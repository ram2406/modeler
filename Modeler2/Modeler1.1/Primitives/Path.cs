using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modeler1._1.Primitives
{
    class Путь
    {
        public List<БазовыйОбъект> Объекты { get; set; }

        public Путь()
        {
            Объекты = new List<БазовыйОбъект>();
                
        }

        //public void GetPath(БазовыйОбъект sobj)
        //{
            
        //    GeneratePath(sobj);
        //}

        /// <summary>
        /// Идня простая
        /// Рекурсивный метод добавления объектов в Путь
        /// Затем дабовление его в коллекцию путей
        /// </summary>
        /// <param name="sobj"></param>
        /// <returns></returns>

        
    }
    class МатрицаПутей
    {
        public List<Путь> Пути { get; set; }
        private Путь current_path;

        public МатрицаПутей()
        {
            Пути = new List<Путь>();
            
        }

        internal void GeneratePathes(БазовыйОбъект obj)
        {
            Путь p = new Путь();
            GeneratePath(obj, p) ;
        }

        internal void GeneratePath(БазовыйОбъект obj, Путь p)
        {
            
            p.Объекты.Add(obj);
            foreach (var o in obj.ОбъектыСледующие)
            {
                var pt = new Путь();
                pt.Объекты.AddRange( p.Объекты);
                GeneratePath(o, pt);

            }
            if (!obj.ЕстьСледующиеОбъекты)
            {
                Пути.Add(p);
                
            }
            
        }
    }
}
