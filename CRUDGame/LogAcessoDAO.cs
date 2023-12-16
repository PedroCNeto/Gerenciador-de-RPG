using System;

namespace CRUDGame
{
    internal class LogAcessoDAO
    {
        internal static void RegistrarAcesso(Usuario userAutenticado, DateTime now)
        {
            try
            {
                LogAcesso registro = new LogAcesso();
                registro.UsuarioId = userAutenticado.IdUsuario;
                registro.DataHoraAcesso = now;
                using (var ctx = new RPGDBEntities())
                {
                    ctx.LogAcessoes.Add(registro);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}