using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modeler1._1.Primitives
{
    /// <summary>
    /// Накопитель - это прмитив 
    /// хранящий в себе поступившие заявки
    /// имеет ограниченный объем
    /// </summary>
    class Накопитель:БазовыйОбъект
    {

        public bool Full { get; private set; }
        public int Size { get; set; }
        public int CurrentCount { get; private set; }

        public Накопитель()
        {
            Тип = PrimitivesTypes.Накопитель;
            //tb_type.Text = PrimitiveType.ToString();
            this.SetPop(DrivePop);
            this.SetPush(DrivePush);
            this.SetStep(DriveStep);
            imageName = "Накопитель.png";
        }
        private bool DrivePush()
        {
            return false;
            //добавить новый элемент
        }
        private bool DrivePop()
        {
            return false; 
            //вытолкнуть 
        }
        private void DriveStep()
        {
 
        }
    }
}
