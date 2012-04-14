using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modeler1._1.Primitives
{
    class Reply
    {
        public  Guid            ID     { get; set; }
        public  БазовыйОбъект   Owner  { get; set; }

        public Reply()
        {
            ID = Guid.NewGuid();
        }
             
    }
}
