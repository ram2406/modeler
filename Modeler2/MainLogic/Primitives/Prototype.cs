using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MainLogic;

namespace MainLogic.Primitives
{
    [Serializable]
    public class Prototype :Primitive, IPrimitive
    {
        

        private Prototype(): base(PrimitiveTypes.Prototype)
        {
             
        }
        public static IPrimitive Create()
        {
            IPrimitive prim = new Prototype();
            prim.Expression=("1==1");
            return prim;
        }
    }
}
