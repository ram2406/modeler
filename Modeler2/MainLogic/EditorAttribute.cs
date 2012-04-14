using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainLogic
{
     [AttributeUsage(AttributeTargets.Field |
       AttributeTargets.Property,
       AllowMultiple = true)]
    public class EditorAttribute : Attribute
    {
        public string FullName { get; private set; }
        public bool CanWrite { get; private set; }
        public EditorAttribute(string fullname, bool canWrite) 
        {
            this.FullName = fullname;
            this.CanWrite = canWrite;
        }
        public EditorAttribute(string fullname) : this(fullname,true)
        {
            
        }
        public override string ToString()
        {
            return FullName.ToString();
        }
    }
     [AttributeUsage(
       AttributeTargets.Field |
       AttributeTargets.Property,
       AllowMultiple = true)]
     public class VariableAttribute : Attribute
     {
         public string LocalName    { get; private set; }
         public bool CanAssign { get; private set; }

         public VariableAttribute(string LocalName): this(LocalName,false)
         {
             
         }
         public VariableAttribute(string LocalName, bool CanAssign)
             
         {
             this.LocalName = LocalName;
             this.CanAssign = CanAssign ;
             
         }
         public override string ToString()
         {
             return LocalName;
         }
     }
}
