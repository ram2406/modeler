using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.CodeDom;
using Analizator;
using MainLogic.Primitives;

namespace MainLogic
{
    /// <summary>
    /// Типы примитивов
    /// </summary>
    public enum PrimitiveTypes // индекс обязателен 
    {Generator=0,Processor=1,Drive=2,Blocker=3,Terminal=4, Prototype=5}

    [Serializable]
    public class Primitive:BaseObject,IPrimitive
    {
        #region Properties & Fields
        //public string PrimitiveName { get; private set; }

        [Editor("Открыт", false)]
        [Variable("OPEN")]
        public virtual bool IsOpen
        {
            get;
            set;
        }
        
        [Editor("Тип примитива")]
        //[Variable("TYPE")]
        public PrimitiveTypes PrimitiveType { get; private set; }
        //public int PrimitiveId { get; private set; }
        [Editor("Выражение")]
        //[Variable("EXPRESSION")]
        public string Expression { get;  set; }
        
        


        [Editor("Использовал такт", false)]
        [Variable("USETACT")]
        public bool UseTact
        {
            get;
            set;
        }
        [Editor("Количество обработанных заявок", false)]
        [Variable("COUNTR_HANDLE")]
        public int TotalHandledReplyCount { get; protected set; }


        public ParserVars Variables { get { return GetVariables(); } }

        public virtual  IReply CurrentReply { get;  set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Закрытый конструктор
        /// </summary>
        /// <param name="area"></param>
        public Primitive(PrimitiveTypes primitiveType) : base (BaseObjectTypes.Primitive)
        {

            this.TotalHandledReplyCount = 0;
            this.Id = ++Owner.Instance
                .GlobalIdentifiers  [BaseObjectTypes.Primitive.GetHashCode()]
                                    [primitiveType.GetHashCode()+1];
            this.PrimitiveType = primitiveType;
            this.Expression = "1";
            this.Name = this.PrimitiveType.ToString() + this.Id;
            this.UseTact = false;
            this.IsOpen = true;
            

        }

        public Primitive() : this(PrimitiveTypes.Prototype) { }
        #endregion

        #region Methods 
        #region Static
        /// <summary>
        /// Создать
        /// </summary>
        /// <param name="area">ссылка на владельца</param>
        /// <returns></returns>
        public  static IPrimitive Create()
        {
            
            return  Create(PrimitiveTypes.Prototype);
        }
        public static IPrimitive Create(PrimitiveTypes primitiveType)
        {
            IPrimitive p;
            switch (primitiveType)
            {
                case PrimitiveTypes.Blocker: p  = new Blocker(); break;
                case PrimitiveTypes.Drive:p = new Drive() ; break;
                case PrimitiveTypes.Processor: p = new Processor(); break;
                case PrimitiveTypes.Generator: p = new Generator(); break;
                case PrimitiveTypes.Terminal: p = new Terminal(); break;
                default: p = Prototype.Create(); break;
            }
            
            Owner.Instance.AddObject(p);
            return p;
        }
        #endregion
        #region Virtual 
        /// <summary>
        /// Удалить
        /// </summary>
        /// <returns></returns>
        public virtual bool Delete()
        {
            return Owner.Instance.RemoveObject(this);
        }
        
        
        //устаревшее
        public virtual bool Validate()
        {

            foreach (IRulePrimitive rule in Owner.Instance.Rules)
            {
                if(!rule.Validate(this)) return false ;
            }
            return true;
        }
        /// <summary>
        /// Получает и закрывается
        /// </summary>
        /// <param name="reply"></param>
        /// <returns></returns>
        public virtual bool Handle(IReply reply)
        {
            this.TotalHandledReplyCount++;
                this.CurrentReply = reply;
                
                return true;
            
                 
        }
        /// <summary>
        /// Проработать если не использовал такт, открывается если обрабатывает
        /// </summary>
        /// <returns></returns>
        public virtual bool Work()
        {
            if (!UseTact)
            {
                this.UseTact = true;
                if (Owner.Instance.EvalBoolean(Expression))
                {
                    if (this.CurrentReply != null) //TODO: нужно обдумать
                        this.CurrentReply.Move(this);
                    
                    this.IsOpen = true;
                    return true;
                }
                else {  return false; }
            }
            else {  return false; }
        }

        public virtual bool GetState()
        {
            return this.IsOpen = Work();
        }

        #endregion
        #region Override
        
        #endregion
        #region Simple
        public List<ILink> GetNextLinks()
        {
           
            
            var query = (from o in Owner.Instance.Links where o.OutgoingPrimitive.Equals(this) select o);
            Debug.WriteLine("{0},{1}", query.Count<ILink>(), this.ToString() );
            return query.ToList<ILink>();
        }
        public List<ILink> GetPrevoisLinks()
        {
            var query = (from o in Owner.Instance.Links where o.ComingPrimitive.Equals(this)  select o);
            Debug.WriteLine("{0},{1}", query.Count<ILink>(), this.ToString());
            return query.ToList<ILink>();
        }
        public void SetExpression(string exp)
        {
            this.Expression = exp;
        }
        protected  ParserVars GetVariables()
        {
            var properties = this.GetType().GetProperties();
            ParserVars vars = new ParserVars();
            foreach (var property in properties)
            {
                   #region
                    VariableAttribute ar2 = null;
                    var arr2 = property.GetCustomAttributes(typeof(VariableAttribute), false);
                    if (arr2.Count() > 0)
                    {
                        ar2 = arr2[0] as VariableAttribute;

                        object value = property.GetValue(this, null);
                        string typeName = value.GetType().Name;
                        double dd=0 ;
                        if(value is bool)
                        {
                         dd = ((bool)value).GetHashCode();
                        }
                        else if(value is int)
                        {
                         dd = (int)value;
                        } 
                        else if(value is double)

                         dd = (double)value;  
                        
                        vars.AddVariable(new ParserVar(ar2.LocalName, dd , this.Name+"_"+ar2.LocalName));

                        Debug.WriteLine(property.GetValue(this, null));
                        Debug.WriteLine(property.Name);
                    }
                    
                    #endregion
                

                
            }
            return vars;
            //имя + локал , value

        }
        public void RefreshVariables()
        {
            var properties = this.GetType().GetProperties();
            
            foreach (var property in properties)
            {
                #region
                VariableAttribute ar2 = null;
                var arr2 = property.GetCustomAttributes(typeof(VariableAttribute), false);
                if (arr2.Count() > 0)
                {
                    ar2 = arr2[0] as VariableAttribute;
                    if (ar2.CanAssign)
                    {
                        object value = property.GetValue(this, null);
                        string typeName = value.GetType().Name;
                        double d = Variables.GetVariables().Where(o => o.Name == ar2.LocalName).First().pVar.Value;
                        if (value is bool)
                        {
                            property.SetValue(this, Convert.ToBoolean(d), null);
                        }
                        else if (value is int)
                        {
                            property.SetValue(this, Convert.ToInt32(d), null);
                        }
                        else if (value is double)

                            property.SetValue(this, (d), null);



                        Debug.WriteLine(property.GetValue(this, null));
                        Debug.WriteLine(property.Name);
                    }
                }

                #endregion



            }
        }
        #endregion
        #endregion

    }

    public interface IPrimitive:IPrimitiveLink , IPrimitiveReply, IBaseObject
    {
        string Expression { get; set; }
        
        //int PrimitiveId { get; }
        ParserVars Variables { get; }
        
        bool Work();
        bool UseTact { get; set; }
        bool GetState();
        IReply CurrentReply { get; set; }
    }
    public interface IPrimitiveLink
    {
        List<ILink> GetNextLinks();
        List<ILink> GetPrevoisLinks();
        PrimitiveTypes PrimitiveType { get; }
        bool IsOpen { get; set; }
        //string Name { get; set; }
        

    }
    public interface IPrimitiveReply
    {
        bool Handle(IReply reply);
    }


    
}
