using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUDGame
{
    public partial class Subclass
    {
        //Retorna a classe dessa subclasse
        private Class getClasse;

        public Class GetClasse
        {
            get {
                getClasse = ClasseDAO.ListarClasses(ClasseID);
                return getClasse; 
            }            
        }

    }
}