using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUDGame
{
    public partial class Personagem_Habilidade
    {
        //Retorna a classe dessa subclasse
        private Personagen getPersonagem;
        private Habilidade getHabilidade;

        public Personagen GetPersonagem
        {
            get
            {
                getPersonagem = PersonagemDAO.ListarPersonagens(Personagens_IdPersonagem);
                return getPersonagem;
            }
        }

        public Habilidade GetHabilidade
        {
            get
            {
                getHabilidade = HabilidadeDAO.ListarHabilidades(Habilidades_IdHabilidade);
                return getHabilidade;
            }
        }
    }
}