using System;
using System.Collections.Generic;
using System.Linq;

namespace CRUDGame
{
    internal class PerfilUsuarioDAO
    {
        internal static List<PerfilUsuario> ListarPerfis()
        {
            List<PerfilUsuario> perfis = null;
            try
            {
                using (var ctx = new RPGDBEntities())
                {
                    perfis = ctx.PerfilUsuarios.OrderBy(x => x.Descricao).ToList();
                }

            }
            catch (Exception ex)
            {
            }

            return perfis;
        }

        internal static PerfilUsuario ObterPerfil(int perfilUsuarioId)
        {
            PerfilUsuario perfil = null;

            using (var ctx = new RPGDBEntities())
            {
                perfil = ctx.PerfilUsuarios.FirstOrDefault(
                        x => x.IdPerfilUsuario == perfilUsuarioId
                    );
            }

            return perfil;
        }
    }
}