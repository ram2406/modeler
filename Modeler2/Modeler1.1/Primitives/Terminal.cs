using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modeler1._1.Primitives
{
    /// <summary>
    /// Терминал - это примитив 
    /// хранящий выходную информацию о обработанных заявках
    /// </summary>
    class Терминал:БазовыйОбъект
    {
        public string Stat { get; private set; }
        public int CountTackts { get;private set; }
        

        public Терминал()
        {
            Тип = PrimitivesTypes.Терминал;
            //tb_type.Text = PrimitiveType.ToString(); 
            this.SetPush(TerminalPush);
            this.SetStep(TerminalStep);
            imageName = "Терминал.png";
        }
        bool TerminalPush()
        {
            return false;
        }
        void TerminalStep()
        {
            CountTackts++;
        }
        
    }
}
