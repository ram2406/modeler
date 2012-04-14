using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace MainLogic
{
    [Serializable]
    public class Pathes: IEnumerable<Path>
    {
        private List<Path> pathes;
        public Pathes()
        {
            this.pathes = new List<Path>();
        }
        public List<Path> Generete(IPrimitiveLink startPrimitive)
        {
            foreach (var link in startPrimitive.GetNextLinks())
            {
                GeneratePath(link, new Path());
            }
            return this.pathes;
        }
        public List<Path> Generete(params IPrimitiveLink[] startPrimitive)
        {
            foreach (var primitive in startPrimitive)
            {
                foreach (var link in primitive.GetNextLinks())
                {
                    GeneratePath(link, new Path());
                }
            }
            return this.pathes;
        }

        public void Add(Path path)
        {
            this.pathes.Add(path);
        }
        public void AddRange(IEnumerable<Path> pathes)
        {
            this.pathes.AddRange(pathes);
        }
        public IEnumerator<Path> GetEnumerator()
        {
            return pathes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void GeneratePath(ILink startLink, Path path)
        {
            path.Add(startLink);
            var links = startLink.ComingPrimitive.GetNextLinks();
            if (links.Count > 0)
                foreach (var link in links)
                {
                    Path newPath = new Path();
                    
                    newPath.AddRange(path);
                    GeneratePath(link, newPath);
                }
            else
            {
                this.Add(path);
            }

        }


        public override string ToString()
        {
            string str = string.Empty;
            foreach (var item in this)
            {
                str+="Путь " +item.id+"="+ item.ToString()+"\n";
            }

            return str;
        }
        
        

    }

    
    [Serializable]
    public class Path  : BaseObject , IEnumerable<ILink>    
    {
        List<ILink> Links;

        public Path():base(BaseObjectTypes.Path)
        {
            Links = new List<ILink>();
        }

        public IEnumerator<ILink> GetEnumerator()
        {
            return Links.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        
        public void Add(ILink link)
        {
            Links.Add(link);
        }

        
        public void AddRange(IEnumerable<ILink> links)
        {
            Links.AddRange(links);
        }

        public int id;
        
        public override string ToString()
        {
            string str = "Path:";
            
            foreach (var item in Links)
            {
                str += item.ToString() + "--";
            }
            return str.Remove(str.Length - 2, 2);
            
            
        }    
    }
   
        
}
