using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainLogic
{
    /// <summary>
    /// Типы базовых объектов
    /// </summary>
    public enum BaseObjectTypes //индекс обязателен 
    {  Reply=0, Link=1, Path=2, Rule=3 , Primitive=4 }
    [Serializable]
    public  class BaseObject : IBaseObject, IComparable
    {
        
        public long Id { get;protected set; }
        [Editor("Наименование")]
        public string Name { get; set; }
        [Editor("Тип объекта")]
        public BaseObjectTypes ObjectType { get; private set; }
        [Editor("Описание")]
        public string Description { get; set; }

        public override string ToString()
        {
            return Name;
        }
        public BaseObject(BaseObjectTypes objectType)
        {
            this.ObjectType = objectType;
            this.Id = ++Owner.Instance.GlobalIdentifiers[objectType.GetHashCode()][0];
            this.Name = this.ObjectType.ToString() + this.Id;
        }
        public virtual void Add()
        {
            Owner.Instance.AddObject(this);
        }
        public virtual bool Remove()
        {
            return Owner.Instance.RemoveObject(this);
        }
        public int CompareTo(object obj)
        {
            
            if(obj is IBaseObject) 
            {
                var b = obj as IBaseObject;
                return this.Name.CompareTo(b.Name);
            }
            return 0;
            
        }
    }
    public interface IBaseObject
    {
        long Id { get; }
        string Name { get; set; }
        BaseObjectTypes ObjectType { get; }
        void Add();
        bool Remove();
    }


}
