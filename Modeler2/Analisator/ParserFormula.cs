using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Analizator
{
    [Serializable()]
    public class ParserFormula : IParserFormula
    {
        public string Expression { get; set; }
        public  ParserVars variables { get; set; }
        public string Variables { get { return variables.ToString(); } set { variables.SetVariables(value); } }
        public string Name { get; set; }
        public delegate double Function(double[] vars);
        public bool IsSys { get; private set; }
        public string Call { get { return GetCall(); } }

        private string GetCall()
        {
            string str = Name + '(';
            if (variables.Count > 0)
            {
                foreach (var v in variables.GetVariables())
                {
                    str += v.GlobalName.ToString() + ',';
                }
                str = str.Remove(str.Length - 1, 1);    
            }

            
            str+= ')';
            return str;
        }
        private Function Fun;
        public string Description { get; set; }
        #region Eval
        public double Eval( double  var1)
        {   
            if (Fun == null)
            {
                var p = new Parser();
                variables[0].Value = var1;

                variables.RegVariable(p);
                p.SetExpr(Expression);
                return  p.Eval();
            }
            else
            {
                return Fun(new double[]{var1});
            }
        }

        public double Eval(double var1, double var2)
        {
            if (Fun == null)
            {
                var p = new Parser();
                double res = 0;

                variables[0].Value = var1;
                variables[1].Value = var2;
                variables.RegVariable(p);
                p.SetExpr(Expression);
                res = p.Eval();


                return res;
            }
            else
            {
                return Fun(new double[] { var1,var2 });
            }
        }
        public double Eval(double var1, double var2, double var3)
        {
            if (Fun == null)
            {
                var p = new Parser();
                double res = 0;

                variables[0].Value = var1;
                variables[1].Value = var2;
                variables[2].Value = var3;
                variables.RegVariable(p);
                p.SetExpr(Expression);
                res = p.Eval();


                return res;
            }
            else
            {
                return Fun(new double[] { var1, var2 , var3 });
            }
        }
        public double Eval(double var1, double var2, double var3, double var4)
        {
            if (Fun == null)
            {
                var p = new Parser();
                double res = 0;

                variables[0].Value = var1;
                variables[1].Value = var2;
                variables[2].Value = var3;
                variables[3].Value = var4;
                variables.RegVariable(p);
                p.SetExpr(Expression);
                res = p.Eval();


                return res;
            }
            else
            {
                return Fun(new double[] { var1, var2, var3,var4 });
            }
        }
        public double Eval(double var1, double var2, double var3, double var4, double var5)
        {
            if (Fun == null)
            {
                var p = new Parser();
                double res = 0;

                variables[0].Value = var1;
                variables[1].Value = var2;
                variables[2].Value = var3;
                variables[3].Value = var4;
                variables[4].Value = var5;
                variables.RegVariable(p);
                p.SetExpr(Expression);
                res = p.Eval();


                return res;
            }
            else
            {
                return Fun(new double[] { var1, var2, var3,var4,var5 });
            }
        }
        #endregion Eval

        public ParserFormula(string Name, string Expression, params ParserVar[] Variables)
        {
            this.IsSys = false;
            this.Name = Name;
            this.Expression = Expression;
            this.variables = new ParserVars();
            foreach (var v in Variables)
            {
                this.variables.AddVariable(v);
            }
        }
        public ParserFormula(string Name,  Function fun, params ParserVar[] Variables) : this(Name,"[Системная функция]",Variables)
        {
            this.IsSys = true;
            Fun = fun;
        }
    }

    
    public interface IParserFormula
    {
         string Expression { get; set; }
         string Variables { get; set; }
         ParserVars variables { get; set; }
         bool IsSys { get; }
         string Name { get; set; }
         double Eval(double v);
         double Eval(double v, double v1);
         double Eval(double v, double v1, double v2);
         double Eval(double v, double v1, double v2, double v3);
         double Eval(double v, double v1, double v2, double v3, double v4);
         

    }
}
