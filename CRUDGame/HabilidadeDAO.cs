using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace CRUDGame
{
    public class HabilidadeDAO
    {
        public static string CadastrarHabilidade(Habilidade novaHabilidade)
        {
            string mensagem = "";
            try
            {
                using (var ctx = new RPGDBEntities())
                {
                    Habilidade habilidade = ctx.Habilidades.Add(novaHabilidade);
                    ctx.SaveChanges();
                }

                mensagem = "Habilidade " + novaHabilidade.Descricao
                    + " cadastrada com sucesso!";
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2601 || ex.Number == 2627)
                {
                    mensagem = "A habilidade " + novaHabilidade.Descricao + " já existe.";
                }
                else
                {
                    mensagem = "Ocorreu um erro: " + ex.Message;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("An error occurred while updating the entries"))
                {
                    mensagem = "A habilidade " + novaHabilidade.Descricao + " já existe.";
                }
                else
                {
                    mensagem = "Ocorreu um erro: " + ex.Message;
                }
            }

            return mensagem;
        }

        public static Habilidade ListarHabilidades(int habilidadeID)
        {
            Habilidade habilidade = null;

            using (var ctx = new RPGDBEntities())
            {
                habilidade = ctx.Habilidades.FirstOrDefault(
                        x => x.IdHabilidade == habilidadeID
                    );
            }

            return habilidade;
        }

        /// <summary>
        /// Retorna todas as classes cadastradas!
        /// </summary>
        /// <returns></returns>
        public static List<Habilidade> ListarHabilidades()
        {
            List<Habilidade> habilidades = null;
            try
            {
                using (var ctx = new RPGDBEntities())
                {
                    habilidades = ctx.Habilidades.OrderBy(
                        x => x.Descricao).ToList();
                }
            }
            catch (Exception ex)
            {
            }
            return habilidades;
        }

        public static Habilidade Remover(int idHabilidade)
        {
            Habilidade habilidade = null;

            using (var ctx = new RPGDBEntities())
            {
                habilidade = ctx.Habilidades.FirstOrDefault(
                        x => x.IdHabilidade == idHabilidade
                     );
                ctx.Habilidades.Remove(habilidade);
                ctx.SaveChanges();
            }
            return habilidade;
        }

        public static string AlterarHabilidade(Habilidade novaHabilidade)
        {
            string mensagem = "";
            //Tratamento de erros
            try
            {
                using (RPGDBEntities ctx = new RPGDBEntities())
                {
                    Habilidade habilidade = ctx.Habilidades.FirstOrDefault(
                        x => x.IdHabilidade == novaHabilidade.IdHabilidade
                     );

                    habilidade.Descricao = novaHabilidade.Descricao;
                    ctx.SaveChanges();

                    mensagem = "Habilidade " + novaHabilidade.Descricao + " alterada com sucesso!";
                }
            }
            catch (Exception ex)
            {
                mensagem = ex.Message;
            }

            return mensagem;
        }

    }
}