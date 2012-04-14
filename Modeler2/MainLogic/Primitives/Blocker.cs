using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainLogic.Primitives
{
    [Serializable]
    public class Blocker : Primitive
    {
        [Editor("Если выражение верно")]
        public string PrimetiveTrue 
        { 
            get { return primTrue != null ? primTrue.Name : "примитив не указан"; }
            set 
            { 
                var query = GetNextLinks();
                if(query.Count>0)
                {
                   var q = query.Where(o=>(o.ComingPrimitive as IPrimitive).Name == value);
                    if(q.Count()>0)
                    {
                       primTrue =  q.First().ComingPrimitive as IPrimitive ;
                    }
                    else primTrue = null;

                }
                else primTrue = null;
                 
            }
        }
        [Editor("Если выражение не верно")]
        public string PrimetiveFalse 
        { 
            get { return primFalse != null ? primFalse.Name : "примитив не указан"; }
            set
            {
                var query = GetNextLinks();
                if (query.Count > 0)
                {
                    var q = query.Where(o => (o.ComingPrimitive as IPrimitive).Name == value);
                    if (q.Count() > 0)
                    {
                        primFalse = q.First().ComingPrimitive as IPrimitive;
                    }
                    else primFalse = null;

                }
                else primFalse = null;

            } 
        }

        private IPrimitive primTrue;
        private IPrimitive primFalse;

        public Blocker()
            : base(PrimitiveTypes.Blocker)
        {
            //this.Name = "Блокиратор"+this.Id;
            //this.Variables = this.GetVariables();
        }

        public override bool  GetState()
        {
            return true;

        }
        public override bool Handle(IReply reply)
        {

            this.TotalHandledReplyCount++;
            if(Owner.Instance.EvalBoolean(Expression))

            return reply.Move(this, primTrue);
            else return reply.Move(this, primFalse);
        }
        public override bool Work()
        {
            return true;
        }

        
    }
}
