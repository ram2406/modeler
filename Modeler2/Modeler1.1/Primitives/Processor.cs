using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modeler1._1.Primitives
{
    /// <summary>
    /// Процессор(канал) - это прмитив
    /// обрабатывающий заявки по определенному закону 
    /// или интервалу времени
    /// </summary>
    class Процессор:БазовыйОбъект
    {

        public bool Busy { get; private set; }
        public int WorkTime { get; set; }

        public Процессор()
        {

            Тип = PrimitivesTypes.Процессор; 
            //tb_type.Text = PrimitiveType.ToString();
            imageName = "Процессор.png";
            this.SetPop(ProcessorPop);
            this.SetPush(ProcessorPush);
            this.SetStep(ProcessorStep);
        }
        bool ProcessorPop()
        {
            return false;
        }
        bool ProcessorPush()
        {
            return false;
        }
        void ProcessorStep()
        {
           
        }

    }
}
