using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.JScript;
using System.Threading;
using Analizator;
using MainLogic.Primitives;
using MainLogic;

namespace ConsoleTestModeler2
{
    class Program
    {
        

        static void Main(string[] args)
        {
            Parser parser = new Parser();
            MainLogic.Pathes pathes = new MainLogic.Pathes();
            var area = MainLogic.Owner.Instance;
            {
                var p0 = Primitive.Create();
                
                MainLogic.IPrimitive p1 = null;
                for (int i = 0; i < 10; i++)
                {
                    var p = Primitive.Create();

                    
                    if ( i % 1 == 0) 
                    { 
                        MainLogic.Link.Create(p1??p0,p);
                    }
                    p1 = p;
                }

                MainLogic.Link.Create(p0,p1);
                pathes.Generete(p0);


                Console.WriteLine(MainLogic.Owner.Instance.Links.Count().ToString());
                Console.WriteLine(MainLogic.Owner.Instance.Primitives.Count().ToString());
                foreach (var l in MainLogic.Owner.Instance.Links)
                {
                    Console.WriteLine(l.ToString());
                }
                Console.WriteLine(pathes.ToString());
            }
            object o = new object();
            object o1 = new object();
            try
            {

                // System.CodeDom.Compiler.CodeParser parser = new System.CodeDom.Compiler.CodeParse;
                int n1 = 5, n2 = 10;
                var date = DateTime.Now;
                parser.SetExpr("5");
                Console.WriteLine(parser.Eval());
                Console.WriteLine(DateTime.Now - date);
                date = DateTime.Now;
                //Thread.Sleep(1000);

                var r1 = new ParserVariable(5);
                r1.Value = 10;
                parser.DefineVar("x1", r1);
                //parser.DefineVar("x1", new ParserVariable(1));
                r1.Value = 4;
                parser.SetExpr("x1 = 5");
                
                Console.WriteLine(parser.Eval());
                Console.WriteLine(r1.Value);
                Console.WriteLine(DateTime.Now - date);
                date = DateTime.Now;
                parser.SetExpr("5<1");
                Console.WriteLine(parser.Eval());
                Console.WriteLine(DateTime.Now - date);
                date = DateTime.Now;
                parser.SetExpr("5+1");
                Console.WriteLine(parser.Eval());
                Console.WriteLine(DateTime.Now - date);
                date = DateTime.Now;
                //parser.SetExpr("5+1");
                Console.WriteLine((1 + 5).ToString());
                Console.WriteLine(DateTime.Now - date);
                Console.WriteLine((o.GetHashCode()).ToString());
                Console.WriteLine(MainLogic.BaseObjectTypes.Reply.GetHashCode());
            }
            catch (ParserException ex) { Console.WriteLine(ex.Message); }
            Console.ReadKey();
        }
    }
}
