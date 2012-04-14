using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainLogic.Primitives
{
    [Serializable]
    public class Terminal : Primitive
    {
        [Editor("Текущее время", false)]
        [Variable("TICK")]
        public int CurrentTime { get;  set; }

        [Editor("Количество заявок",false)]
        [Variable("COUNTR")]
        public int CurrentReplyCount { get; private set; }


        [Editor("Количество не обработанных заявок", false)]
        [Variable("COUNTR_NOTHANDLE")]
        public int TotalNotHandledReplyCount { get; protected set; }


        [Editor("Количество путей",false)]
        [Variable("COUNTP")]
        public int PathCount { get; private set; }

        
        public Terminal() : base(PrimitiveTypes.Terminal) 
        {
            
            Owner.Instance.CurrentTerminal = this;
            this.Expression = string.Format("{0}_TICK <= 100",this.Name);
            //this.Name = "Терминал";
            //this.Variables = this.GetVariables();
        }
        public override bool Handle(IReply reply)
        {


            CurrentReplyCount++;
            if (reply.PrevoisPrimitive is Blocker)
            {
                TotalNotHandledReplyCount++;
            }
            else { TotalHandledReplyCount++; }
            return true;
        }
        public override bool Work()
        {
            return Owner.Instance.EvalBoolean(Expression);
        }
        
        
    }
}
