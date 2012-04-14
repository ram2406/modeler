using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Analizator
{
    public sealed class Analizator  //один для всех
    {
        #region Properties & Fields
        public  Parser Parser { get; private set; }
             
        public List<IParserFormula> Formulas { get; private set; }

        /// <summary>The one and only instance of the Singleton class.</summary>
        private static readonly Lazy<Analizator> _instance =
            new Lazy<Analizator>(() => new Analizator());


        /// <summary>Gets the singleton instance.</summary>
        public static Analizator Instance { get { return Analizator._instance.Value; } }

        private Analizator()
        {
            Formulas = new List<IParserFormula>();
            Parser = new Parser();
            string path = Path.Combine( Directory.GetCurrentDirectory(),"Formulas");
            if( Directory.Exists(path))
            {
                var fs = Directory.GetFiles(path, "*.mf",SearchOption.TopDirectoryOnly);
                foreach (var f in fs)
                {
                    Stream TestFileStream = File.OpenRead(f);
                    BinaryFormatter deserializer = new BinaryFormatter();
                    ParserFormula formula = (ParserFormula)deserializer.Deserialize(TestFileStream,null);
                    var fl = new ParserFormula(formula.Name, formula.Expression, formula.variables.GetVariables().ToArray());
                    TestFileStream.Dispose();
                    AddNewFormula(fl);
                     
                }
            }
            AddSysFormulas();
        }

        private void AddSysFormulas()
        {
            this.AddNewFormula(new ParserFormula("random", (o1 =>
            {
                int i = int.Parse(o1[0].ToString());
                int j = int.Parse(o1[1].ToString());
                return new Random().Next(i, j);
            }), new ParserVar("min", 0), new ParserVar("max", 10)
                                                    )
                         );
            this.AddNewFormula(new ParserFormula("mod", (o1 =>
            {
                double i = double.Parse(o1[0].ToString());
                double j = double.Parse(o1[1].ToString());
                return (i % j);
            }), new ParserVar("v1", 0), new ParserVar("v2", 10)
                                               )
                    );
        }

        #endregion

        #region Methods

        public bool EvalBoolean(string exp)
        {
            return Eval(exp)>0;
        }
        public double Eval(string exp)
        {
            
            double d = 0;
            this.Parser.SetExpr(exp);

            try {   d = this.Parser.Eval(); }
            catch (ParserException ex)
            {
                Log.SetMessage(string.Format("Ошибка в вырожении {0}, {1}", ex.Expression,ex.Message));
            }
            
            return d;
        }

        public bool AddNewFormula(ParserFormula formula)
        {
            //проверка на совподении имени
            foreach (var v in Formulas)
            {
                if (v.Name == formula.Name) { return false; }
            }
            //регистрируем функцию
            switch (formula.variables.Count)
            {
                case 0: Log.SetMessage("Количество параметров = 0, " + formula.Name); break;
                case 1: Parser.DefineFun(formula.Name, new Parser.Fun1Delegate(formula.Eval)); break;
                case 2: Parser.DefineFun(formula.Name, new Parser.Fun2Delegate(formula.Eval)); break;
                case 3: Parser.DefineFun(formula.Name, new Parser.Fun3Delegate(formula.Eval)); break;
                case 4: Parser.DefineFun(formula.Name, new Parser.Fun4Delegate(formula.Eval)); break;
                case 5: Parser.DefineFun(formula.Name, new Parser.Fun5Delegate(formula.Eval)); break;
                default: Log.SetMessage("Первышено количество параметров, " + formula.Name); return false;
            }
            //если все прошло успешно
            Formulas.Add(formula);
            
            return true;
        }
        public bool UpdateFormulas()
        {
            foreach(var formula in Formulas)
            switch (formula.variables.Count)
            {
                case 0: Log.SetMessage("Количество параметров = 0, " + formula.Name); break;
                case 1: Parser.DefineFun(formula.Name, new Parser.Fun1Delegate(formula.Eval)); break;
                case 2: Parser.DefineFun(formula.Name, new Parser.Fun2Delegate(formula.Eval)); break;
                case 3: Parser.DefineFun(formula.Name, new Parser.Fun3Delegate(formula.Eval)); break;
                case 4: Parser.DefineFun(formula.Name, new Parser.Fun4Delegate(formula.Eval)); break;
                case 5: Parser.DefineFun(formula.Name, new Parser.Fun5Delegate(formula.Eval)); break;
                default: Log.SetMessage("Первышено количество параметров, " + formula.Name); return false;
            }
            return true;
        }
        
        #endregion

    }
}
