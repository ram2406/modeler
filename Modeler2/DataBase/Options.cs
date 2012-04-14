using System;


namespace DataBase
{
    public class Options
    {
        #region Properties & Fields
        public string Path { get; private set;}
        #endregion
        #region Constructors 
        #endregion

        #region Methods 
        public bool SetNewPath(string path)
        {
            if (!System.IO.File.Exists(path)) return false;
            else
            {
                this.Path = path;
                return true;
            }
        }
        #endregion



    }
}
