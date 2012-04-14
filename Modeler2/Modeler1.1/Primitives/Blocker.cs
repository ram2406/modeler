using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modeler1._1.Primitives
{
    /// <summary>
    /// Блокиратор - это прмитив ответственный за логику в коде
    /// по выполнению одного из условий он пропускает 
    /// или блокирует если условие не выполнилось
    /// </summary>
    class Блокиратор:БазовыйОбъект
    {
        
        public bool Open { get;   private set;  }

        public Блокиратор()
        {
            Тип = PrimitivesTypes.Блокиратор;
            //tb_type.Text = PrimitiveType.ToString() ;
            imageName = "Блокиратор.png";
            this.SetPop(BlockerPop);
            this.SetPush(BlockerPush);
            this.SetStep(BlockerStep);
        }
        bool BlockerPop()
        {
            return false;
        }
        bool BlockerPush()
        {
            return false;
        }
        void BlockerStep()
        {
            
        }
    }
}
