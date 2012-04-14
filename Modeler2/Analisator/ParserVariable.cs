using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analizator
{
    [Serializable()]
    public class ParserVar
    {
        public string Name { get; set; }
        public string GlobalName { get; private set; }
        
        public double Value 
        { 
            get 
            { return pVar.Value; } 
            set 
            {  pVar.Value = value; } 
        }
        public ParserVariable pVar { get;  set; }
        public ParserVar(string LocalName, double StartValue,string GlobalName)
        {
            this.GlobalName = GlobalName;
            this.Name = LocalName;
            
            this.pVar = new ParserVariable(StartValue);
        }
        public ParserVar(string LocalName, double StartValue) : this(LocalName,StartValue,LocalName)
        { 
            
        }
        
        public override string ToString()
        {
            return GlobalName;
        }
    }
    [Serializable()]
    public class ParserVars 
    {
        List<ParserVar> variables;
        
        public ParserVars()
        {
            
            variables = new List<ParserVar>(4);
            Message = "Проверка не производилась";
        }
        public bool AddVariable(ParserVar pvar)
        {
            variables.Add(pvar);
            return true;
        }
        public IEnumerable<ParserVar> GetVariables()
        {
            return variables;
        }
        public bool DeleteVariable(ParserVar pvar)
        {
            return variables.Remove(pvar);
        }
        public bool DeleteVariable(string localName)
        {
            var pvars = variables.Where(w => w.Name == localName).Select(o => o);
            if (pvars.Count<ParserVar>() > 0) { return variables.Remove(pvars.First()); }
            else return false;
            //return variables.Remove(pvar);
        }
        public bool RegVariable(Parser parser)
        {
            if (Check())
            {
                string str = string.Empty;
                try
                {

                    foreach (var f in variables)
                    {
                        str = f.GlobalName == string.Empty ? f.Name : f.GlobalName; parser.DefineVar(f.GlobalName == string.Empty ? f.Name : f.GlobalName, f.pVar);
                    }
                }
                catch (ParserException ex) { Log.SetMessage("Ошибка при объявлении переменной "+str+", "+ex.Message); }
                return true;
            }
            else return false;
        }
        public ParserVar this[int i] { get { return variables[i]; } }
        public ParserVar this[string  name] { get { return variables.Find(o=>o.Name == name); } }
        public int Count { get { return variables.Count; } }
        public void Clear() { variables = new List<ParserVar>(10); }
        public void SetVariables(List<ParserVar> vars) { variables = vars; }
        public void SetVariables(params ParserVar[] vars) { this.Clear(); variables.AddRange(vars); }
        public bool Check()
        {
           var r = variables.GroupBy(g => g.GlobalName).Where(w => w.Count<ParserVar>() > 1);
           if (r.Count() > 0) 
           { 
               Message = "Ошибка: есть одноименные переменные - " + r.Select(s => s.First().GlobalName).First(); 
               return false; 
           }
           else { Message = "Ошибок нет"; return true; }
        }
        public string Message { get; private set; }
        public override string ToString()
        {
            string str = string.Empty;
            foreach (string s in variables.Select(o => o.GlobalName))
            {
                str += s + ',';
            }
            
            return str;
        }
        public void SetVariables(string value)
        {
            
            var strs = value.Split(',');
            variables = new List<ParserVar>(strs.Length);
            foreach (string s in strs)
            {
                variables.Add(new ParserVar(s, 0,s));
            }
        }
    }
    
 }
