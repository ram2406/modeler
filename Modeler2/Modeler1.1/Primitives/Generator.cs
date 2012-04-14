using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modeler1._1.Primitives
{
    /// <summary>
    /// Генератор - это примитив 
    /// генерирующий заявки по определенному закону 
    /// или по временному интервалу
    /// </summary>
    class Генератор:БазовыйОбъект
    {
        // период поступления заявок

        public int Period { get; private set; }
        public int _period;
        private int Count { get; set; }
        // приоритет на процессоры

        public List<Процессор> PreopertyProcessors { get; private set; }

        public bool IsRun { get; private set; }

        public Генератор()
        {
            Тип = PrimitivesTypes.Генератор;
            PreopertyProcessors = new List<Процессор>();
            imageName = "Генератор.png";
            this.SetPush(GeneratorPush);
            this.SetPop(GeneratorPop);
            this.SetStep(GenerateStep);
        }
        public void SetPeriod(int per) 
        {
            Period = per;
        }


        bool GeneratorPop()
        {
            return true;
        }
        bool GeneratorPush()
        {
            return false;
        }
        void GenerateStep()
        {
            if (_period < Period)
            {
                _period++;
            }
            else
            {
                for (int i = 0; i < Count; i++)
                {
                    this.Pop();
                }
            }
        }

        
    }
}
