using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using MainLogic.Primitives;


namespace MainLogic
{
    [Serializable]
    public sealed class Owner //один для всех
    {
        #region Properties & Fields
        internal int[][] GlobalIdentifiers;
        //internal int GlobalIdCount;
       
        private List<IBaseObject> Objects;
        public Analizator.ParserVars Variables { get; private set; }
            

        public Terminal CurrentTerminal { get;  set; }

        public event EventHandler ObjectsChanged;
        public event EventHandler TimeChanged;

        /// <summary>
        /// для отображения
        /// </summary>
        public string Name { get; private set; }

        #region Queries
        public IEnumerable<IPrimitive> Primitives 
        { get 
            { 
                return Objects.Where(o => o.ObjectType == BaseObjectTypes.Primitive)
                    .Select(o => o as IPrimitive); 
            } 
        }
        public IEnumerable<IRule> Rules 
        { 
            get 
            { 
                return Objects.Where(o => o.ObjectType == BaseObjectTypes.Rule)
                    .Select(o => o as IRule); 
            } 
        }
        public IEnumerable<ILink> Links 
        { get 
            { 
                return Objects.Where(o => o.ObjectType == BaseObjectTypes.Link)
                    .Select(o => o as ILink); 
            } 
        }
        #endregion

        #endregion
        #region Constructors
        private Owner()
        {
            ObjectsChanged = new EventHandler((s, e) => { Debug.WriteLine("Event -"); });
            Objects = new List<IBaseObject>(100);
            GlobalIdentifiers = new int[Enum.GetValues(typeof( BaseObjectTypes)).Length] [];
            foreach (var r in Enum.GetValues(typeof(BaseObjectTypes)))
            {
                var type = (BaseObjectTypes)r ;
                switch (type)
                {
                    case BaseObjectTypes.Primitive: GlobalIdentifiers[BaseObjectTypes.Primitive.GetHashCode()] = new int[Enum.GetValues(typeof(PrimitiveTypes)).Length+1]; break;
                    default: GlobalIdentifiers[r.GetHashCode()] = new int[1]; break;
                }
            }
            Variables = new Analizator.ParserVars();
                 
        }
        
	    
	    /// <summary>Gets the singleton instance.</summary>
        /// отклонение от одиночки - должен уметь создовать и загружать 
        public static Owner Instance { get; private set; }
        #endregion
        #region Methods

        public IEnumerable<IBaseObject> GetObjects()
        {
            return Objects.OrderBy(s =>s.Name);
        }

        public void RefreshPathes()
        {
            this.Objects.RemoveAll(p => p.ObjectType == BaseObjectTypes.Path);
            this.GlobalIdentifiers[BaseObjectTypes.Path.GetHashCode()][0] = 0;
            this.Objects.AddRange   ( new Pathes()
                                        .Generete(this.Primitives
                                        .Where(s=>s.PrimitiveType == PrimitiveTypes.Generator)
                                        .ToArray<IPrimitive>())
                                    );
        }
        public void Run()
        {
            if (this.CurrentTerminal != null)
            {
                this.CurrentTerminal.CurrentTime = 0;
                this.CurrentTerminal.UseTact = false;
                Analizator.Analizator.Instance.UpdateFormulas();
                Owner.Instance.SetVariables(Analizator.Analizator.Instance.Parser);
               // Analizator.Analizator.Instance.Parser 
                while (
                    this.CurrentTerminal.Work()
                    )
                {
                    foreach (var p in this.Primitives.Where(o=>o.PrimitiveType != PrimitiveTypes.Terminal))
                    {
                        
                        p.UseTact = false;
                        p.Work();
                    }
                    

                    foreach (Generator gen in this.Primitives.Where(s => s.PrimitiveType == PrimitiveTypes.Generator))
                    {


                        
                        
                        gen.Pop();
                        

                    }
                   Debug.WriteLine(  Analizator.Analizator.Instance.Eval("Terminal1_TICK"));
                    this.CurrentTerminal.CurrentTime++;
                 }
                Log.SetMessage("Цикл завершен");
            }
            else 
            {
                Log.SetMessage("Терминал не найден"); 
            }
        }
        public void Stop()
        {
 
        }
        public void Step(int i)
        {
 
        }
        public bool Validate()
        {
            //has Terminal
            if (this.CurrentTerminal == null) return false;
            
            return true;
        }
        public bool EvalBoolean(string Expression)
        {
            Owner.Instance.SetVariables(Analizator.Analizator.Instance.Parser);
            return Analizator.Analizator.Instance.EvalBoolean(Expression);
        }
        public bool AddObject(IBaseObject obj) 
        {
 
            if (!this.Objects.Contains(obj)) 
            {
                this.ObjectsChanged(this, new EventArgs()); 
                this.Objects.Add(obj); return true; 
            } 
            else return false; 
        }
        public bool RemoveObject(IBaseObject obj) 
        {
            this.ObjectsChanged(this, new EventArgs());
            return this.Objects.Remove(obj);
        }
        public static Owner SetNewOwner(string name)
        {
           return Instance = new Owner(){Name = name};
        }
        public static Owner SetOwner(Owner owner)
        {
           return Instance = owner;
        }
        #endregion
        public void SetVariables(Analizator.Parser p)
        {
            p.ClearVar();
            try
            {
                foreach (IPrimitive prim in this.Primitives)
                {

                    foreach (var v in prim.Variables.GetVariables())
                    {
                        p.DefineVar(v.GlobalName, v.pVar);
                    }
                }
            }
            catch (Analizator.ParserException ex) { Log.SetMessage("Ошибка в объявлении переменных"); }
            
        }
    }
   
    

}
