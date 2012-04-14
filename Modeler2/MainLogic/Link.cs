using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MainLogic.Primitives;

namespace MainLogic
{
    [Serializable]
    public class Link:BaseObject, ILink
    {
        #region Properties & Fields
        public IPrimitiveLink ComingPrimitive   { get; private set; }
        public IPrimitiveLink OutgoingPrimitive  { get; private set; }
        //private IAreaLink _area;
        #endregion

        #region Constructors
        private Link():base(BaseObjectTypes.Link) {  
            
        }
        public static Link Create(IPrimitiveLink outPrimitive, IPrimitiveLink inPrimitive )
        {
            Link l = new Link();
            l.ComingPrimitive = inPrimitive;
            l.OutgoingPrimitive = outPrimitive;
            if (l.Validate())
            {
                l.Description = l.ToString();
                Owner.Instance.AddObject(l);
                Owner.Instance.RefreshPathes();
                return l;
            }
            else return null;
            
        }
        #endregion 

        #region Methods
                
        public override bool Equals(object obj)
        {
            if (obj is ILink)
            {
                var link = obj as ILink;
                return  link.ComingPrimitive == this.ComingPrimitive && 
                        link.OutgoingPrimitive == this.OutgoingPrimitive;
            }
            return base.Equals(obj);
        }
        public override string ToString()
        {
            return this.Name+'('+ (this.OutgoingPrimitive.ToString() +"->"+this.ComingPrimitive.ToString())+')';
        }
        public bool Validate()
        {
            if (this.OutgoingPrimitive is Prototype || this.ComingPrimitive is Prototype) 
                return true;
            if (this.OutgoingPrimitive is Terminal || this.ComingPrimitive is Generator) return false;
            if (this.OutgoingPrimitive is Generator)
            {
                if (this.ComingPrimitive is Blocker || this.ComingPrimitive is Drive) return true;

            }
            else
            if (this.OutgoingPrimitive is Blocker)
            {
                if (this.ComingPrimitive is Drive || this.ComingPrimitive is Processor || this.ComingPrimitive is Terminal) return true;

            }
            else
            if (this.OutgoingPrimitive is Drive)
            {
                if (this.ComingPrimitive is Blocker || this.ComingPrimitive is Processor) return true;

            }
            else
            if (this.OutgoingPrimitive is Processor)
            {
                if (this.ComingPrimitive is Blocker || this.ComingPrimitive is Drive || this.ComingPrimitive is Terminal) return true;

            }
            return false;
        }
        public bool Delete()
        {
            return Owner.Instance.RemoveObject(this);
        }
        #endregion
    }
    public interface ILink
    {
        /// <summary>
        /// Связь с примитивом конец
        /// </summary>
         IPrimitiveLink ComingPrimitive { get; }
        /// <summary>
        /// связь с прмитивом начало
        /// </summary>
         IPrimitiveLink OutgoingPrimitive { get;  }
        /// <summary>
        /// Проверка
        /// </summary>
        /// <returns></returns>
         bool Validate();
        /// <summary>
        /// Удалить связзь
        /// </summary>
        /// <returns></returns>
         bool Delete();
    }
    
}
