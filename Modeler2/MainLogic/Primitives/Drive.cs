using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainLogic.Primitives
{
    [Serializable]
    public class Drive : Primitive
    {
        [Editor("Объем")]
        [Variable("VOLUME")]
        public int Volume { get;  set; }
        [Editor("Текущее количество", false)]
        [Variable("COUNTR")]
        public int ReplyCount { get { return Replies.Count; } }

        [Editor("Группа")]
        public string GroupName { get; set; }

        private List<IReply> Replies;
        public Drive()
            : base(PrimitiveTypes.Drive)
        {
           
            //this.Name = "Накопитель"+this.Id;
            this.Volume = 4;
            this.Replies = new List<IReply>(this.Volume);
            this.Expression = string.Format("{0}_COUNTR<{0}_VOLUME",this.Name);
            //this.Variables = this.GetVariables();
        }
        public override bool Work()
        {
            if (Replies.Count > 0)
            {
                var reply = this.Replies.First();
                if (reply.Move(this))
                {
                    Replies.Remove(reply);

                }
            }
            if(Owner.Instance.EvalBoolean(Expression))
            {
                return true;
            }
             
            return false;
        }
        public override bool Handle(IReply reply)
        {
            Replies.Add(reply);

            return base.Handle(reply);
        }
    }
}
