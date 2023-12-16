using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUDGame
{
    public partial class Usuario
    {
        public PerfilUsuario GetPerfil
        {
            get
            {
                PerfilUsuario perfil = null;

                perfil = PerfilUsuarioDAO.ObterPerfil(this.PerfilUsuarioId);

                return perfil;
            }
        }
    }
}