using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainLogic.Primitives
{
    [Serializable]
    public  class Generator:Primitive
    {
        [Editor("Период")]
        [Variable("PERIOD",true)]
        public int ReplyPeriod { get;  set; }
        [Editor("Количество заявок")]
        [Variable("COUNTR",true)]
        public int ReplyCount { get;  set; }

        [Editor("Приоритет")]
        public string PreorityList
        {
            get { return GetPreority(); }
            set { SetPreority(value); }
        }

        

        public List<IPrimitive> Preority { get; private set; }

        public Generator()
            : base(PrimitiveTypes.Generator)
        {
            //this.Name = "Генератор" + this.Id;
            this.ReplyPeriod = 2;
            this.ReplyCount = 5;
            this.Expression = string.Format( "(Terminal1_TICK == 0) + (mod(Terminal1_TICK,{0}_PERIOD)==0)",this.Name);
            PreorityList = string.Empty;
            Preority = new List<IPrimitive>();
            Analizator.ParserVariable v = new Analizator.ParserVariable();
            //this.Variables = this.GetVariables();
            
        }

        private bool MakePrerity(string value)
        {

            
                Preority = new List<IPrimitive>();
                string[] pr = value.Split(',');
                foreach (var s in pr)
                {
                    var query = Owner.Instance.Primitives.Where(o => o.Name == s);
                    if (query.Count() > 0)
                    {
                        Preority.Add(query.First());
                    }
                    else { Log.SetMessage("Ошибка не верный формат строки преоритетов"); return false; }
                }
                return true;
            
        }

        private string GetPreority()
        {
            string str = string.Empty;
            foreach (var el in Preority)
            {
                str += el.ToString() + ';';

            }
            return str;
        }
        private void SetPreority(string str)
        {
            string[] strs = str.Split(';');
            Preority = new List<IPrimitive>();
            foreach (var s in strs)
            {
               var query =  Owner.Instance.Primitives.Where(o => o.Name == s);
               if (query.Count<IPrimitive>() > 0) { this.Preority.Add(query.First()); }
               
            }
        }
        
        public void Pop()
        {
            
            if (Owner.Instance.EvalBoolean(this.Expression))
            {
                int m = 0;
                while (m < ReplyCount)
                {
                    new Reply(this).Move(this);
                    this.TotalHandledReplyCount++;
                    m++;
                }  
            }
        }
       
        
    }
}