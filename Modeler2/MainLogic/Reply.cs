using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MainLogic.Primitives;

namespace MainLogic
{
    [Serializable]
    class Reply:BaseObject ,IReply 
    {
        public int ReplyId { get; private set; }
        public string ReplyName { get; private set; }
        public Generator CurrentGenerator { get; private set; }
        public IPrimitive PrevoisPrimitive { get; private set; }
        public bool NotHandle { get; private set; }

        public Reply(Generator gen) :base(BaseObjectTypes.Reply)
        {
            this.ReplyId = ++Owner.Instance.GlobalIdentifiers[BaseObjectTypes.Reply.GetHashCode()][0];
            this.ReplyName = BaseObjectTypes.Reply.ToString() + this.ReplyId;
            this.CurrentGenerator = gen;
            this.NotHandle = false;
        }
        public IPrimitive[] GetNext(IPrimitive prim)
        {
            return (from p in this.CurrentGenerator.Preority
                        join
                        j in prim.GetNextLinks().Where(o=>prim.IsOpen).Select(s => s.ComingPrimitive)
                        on p equals j
                        select p).ToArray<IPrimitive>();
        }
        public bool Move(IPrimitive prim)
        {
            this.PrevoisPrimitive = prim;
            
            foreach (var p in this.CurrentGenerator.Preority)
            {
               var links =  prim.GetNextLinks();
               foreach( var p1 in  links.Select(s => s.ComingPrimitive))
               {
                   if(p == p1) 
                   {
                       System.Diagnostics.Debug.WriteLine(p.ToString());
                       if(p.GetState())
                       {
                           p.Handle(this);
                           prim.CurrentReply = null;
                           return true;
                       }
                   }
               }
            }
            return false;
            
            
        }
        public bool Move(IPrimitive prim, IPrimitive mprim)
        {
            this.PrevoisPrimitive = prim;

            
                var links = prim.GetNextLinks();
                foreach (var p1 in links.Select(s => s.ComingPrimitive))
                {
                    if (mprim == p1)
                    {
                        System.Diagnostics.Debug.WriteLine(p1.ToString());
                        if (mprim.GetState())
                        {
                            mprim.Handle(this);
                            prim.CurrentReply = null;
                            return true;
                        }
                    }
                }
            
            return false;


        }

    }
    public interface IReply
    {
        Generator CurrentGenerator { get; }
        bool Move(IPrimitive prim);
        bool Move(IPrimitive prim, IPrimitive mprim);
        IPrimitive PrevoisPrimitive { get; }
    }
}
