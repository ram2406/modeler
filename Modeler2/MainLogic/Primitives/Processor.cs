using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainLogic.Primitives
{
    [Serializable]
    public class Processor : Primitive
    {
        [Editor("Время обработки")]
        [Variable("WORKTIME")]
        public int HandleTime { get;  set; }

        

        
        public Processor()
            : base(PrimitiveTypes.Processor)
        {
            this.TotalHandledReplyCount = 0;
            //this.Name = "Процессор" + this.Id;
            this.HandleTime = 0;
            this.Expression = string.Format("{0}_WORKTIME==5",Name);
            //this.Variables = this.GetVariables();   
        }
        public override bool Work()
        {
            if (this.CurrentReply == null) { return true; }
            if (UseTact) {return false;}

            this.UseTact = true;
            HandleTime++;
            if ( !Owner.Instance.EvalBoolean(Expression)) {return false;}
            
            if (this.CurrentReply != null) //TODO: нужно обдумать
                this.CurrentReply.Move(this);

            HandleTime=0;
            
                    
            return true;
            
        }
        
       
    }
}
