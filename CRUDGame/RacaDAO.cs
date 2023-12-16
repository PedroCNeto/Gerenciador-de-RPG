using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace CRUDGame
{
    public class RacaDAO
    {
        /// <summary>
        /// Cadastra um nova raça no banco de dados.
        /// </summary>
        /// <param name="novaRaca">Objeto do tipo Raca.</param>
        /// <returns>Mensagem com informações sobre a operação.</returns>
        public static string CadastrarRaca(Raca novaRaca)
        {
            string mensagem = "";
            try
            {
                using (var ctx = new RPGDBEntities())
                {
                    ctx.Racas.Add(novaRaca);
                    ctx.SaveChanges();
                }

                mensagem = "Raça " + novaRaca.Descricao
                    + " cadastrada com sucesso!";
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2601 || ex.Number == 2627)
                {
                    mensagem = "A raca " + novaRaca.Descricao + " já existe.";
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
                    mensagem = "A raca " + novaRaca.Descricao + " já existe.";
                }
                else
                {
                    mensagem = "Ocorreu um erro: " + ex.Message;
                }
            }

            return mensagem;
        }

        public static Raca ListarRacas(int racaID)
        {
            Raca raca = null;

            using (var ctx = new RPGDBEntities())
            {
                raca = ctx.Racas.FirstOrDefault(
                        x => x.IdRaca == racaID
                    );
            }

            return raca;
        }
        internal static List<Raca> ListarRacas()
        {
            List<Raca> racas = null;
            try
            {
                using (var ctx = new RPGDBEntities())
                {
                    racas = ctx.Racas.OrderBy(
                        x => x.Descricao).ToList();
                }
            }
            catch (Exception ex)
            {
            }
            return racas;
        }
        public static Raca Remover(int idRaca)
        {
            Raca raca = null;

            using (var ctx = new RPGDBEntities())
            {
                raca = ctx.Racas.FirstOrDefault(
                        x => x.IdRaca == idRaca
                     );
                ctx.Racas.Remove(raca);
                ctx.SaveChanges();
            }
            return raca;
        }

        public static string AlterarRaca(Raca novaRaca)
        {
            string mensagem = "";
            //Tratamento de erros
            try
            {
                using (RPGDBEntities ctx = new RPGDBEntities())
                {
                    Raca raca = ctx.Racas.FirstOrDefault(
                        x => x.IdRaca == novaRaca.IdRaca
                     );

                    raca.Descricao = novaRaca.Descricao;
                    ctx.SaveChanges();

                    mensagem = "Raça " + novaRaca.Descricao + " alterada com sucesso!";
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