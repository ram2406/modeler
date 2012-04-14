using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MainLogic
{


    public abstract class Rule:BaseObject, IRule
    {
        //protected IAreaRule _area;
        public Rule():base (BaseObjectTypes.Rule)
        {
              Owner.Instance.AddObject(this);
        }
    }
    

    public class RuleLinkAtPrimitiveType : Rule, IRuleLink
    {

        PrimitiveTypes[] PrimitiveTypesHasNoIncomingLinks;
        PrimitiveTypes[] PrimitiveTypesHasNoOutgoingLinks;

        //закрываю непрсредственный конструктор
        private RuleLinkAtPrimitiveType():base() { }

        public static RuleLinkAtPrimitiveType Create(PrimitiveTypes[] noIncoming,PrimitiveTypes[] noOutgoing)
        {
            var rule = new RuleLinkAtPrimitiveType();
            rule.PrimitiveTypesHasNoIncomingLinks = noIncoming;
            rule.PrimitiveTypesHasNoOutgoingLinks = noOutgoing;
            return rule;
        }
         

        public bool Validate(ILink link)
        {
            foreach (var ptype in PrimitiveTypesHasNoIncomingLinks)
            {
                //входит ли тип в правило "не может иметь связи на входе"
                if (ptype == link.ComingPrimitive.PrimitiveType) return false;    
            }
            foreach (var ptype in PrimitiveTypesHasNoOutgoingLinks)
            {
                //входит ли тип в правило "не может иметь связи на выходе"
                if (ptype == link.OutgoingPrimitive.PrimitiveType) return false;
            }
            return true;
        }
    }
    public class RuleLinkAtPrimitive :Rule, IRuleLink
    {
        IPrimitiveLink[] PrimitivesHasNoIncomingLinks;
        IPrimitiveLink[] PrimitivesHasNoOutgoingLinks;

        //закрываю непрсредственный конструктор
        private RuleLinkAtPrimitive() { }

        public static RuleLinkAtPrimitive Create(IPrimitiveLink[] noIncoming, IPrimitiveLink[] noOutgoing)
        {
            var rule = new RuleLinkAtPrimitive();
            rule.PrimitivesHasNoIncomingLinks = noIncoming;
            rule.PrimitivesHasNoOutgoingLinks = noOutgoing;
            return rule;
        }


        public bool Validate(ILink link)
        {
            foreach (var primitive in PrimitivesHasNoIncomingLinks)
            {
                //входит ли примитив в правило "не может иметь связи на входе"
                if (primitive == link.ComingPrimitive) return false;
            }
            foreach (var primitive in PrimitivesHasNoOutgoingLinks)
            {
                //входит ли примитив в правило "не может иметь связи на выходе"
                if (primitive == link.OutgoingPrimitive) return false;
            }
            return true;
        }
    }
    public class RuleLinkCreate :Rule, IRuleLink
    {
        public bool Validate(ILink link)
        {
            
            foreach (var l in (Owner.Instance).Links)
            {
                if (!(l == link)) { return false; }
            }
            return true;
        }
    }
    

    public interface IRule
    {
        //совокупность
        
    }
    
    public interface IRuleLink :IRule
    {
        bool Validate(ILink link);
        
    }
    public interface IRulePrimitive : IRule
    {
        bool Validate(IPrimitive primitive);
    }
    public class RulePrimitive //: IRulePrimitive
    {
        //public 
    }
    public class RulePrimitiveAtType : RulePrimitive
    {
        public BaseObjectTypes PrimitiveType { get; set; }
    }
    public class RulePrimitiveAtInstance : RulePrimitiveAtType
    {
        public IPrimitive CurrentPrimitive { get; set; }
    }
    

}
