using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace CRUDGame
{
    internal class CoresDAO
    {
        internal static List<Core> ListarCores()
        {
            List<Core> cores = null;
            try
            {
                using (var ctx = new RPGDBEntities())
                {
                    cores = ctx.Cores.OrderBy(
                        x => x.Descricao).ToList();
                }
            }
            catch (Exception ex)
            {
            }
            return cores;
        }

        public static Core ListarCores(int corID)
        {
            Core cor = null;

            using (var ctx = new RPGDBEntities())
            {
                cor = ctx.Cores.FirstOrDefault(
                        x => x.IdCor == corID
                    );
            }

            return cor;
        }

        internal static string CadastrarCor(Core novaCor)
        {
            string mensagem = "";
            //Tratamento de erros
            try
            {
                using (RPGDBEntities ctx = new RPGDBEntities())
                {
                    //Cadastrando uma nova classe
                    ctx.Cores.Add(novaCor);
                    //Salvando as alterações no BD
                    ctx.SaveChanges();
                    mensagem = "Cor " + novaCor.Descricao +
                        " cadastrada com sucesso!";
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2601 || ex.Number == 2627)
                {
                    mensagem = "A Cor " + novaCor.Descricao + " já existe.";
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
                    mensagem = "A Cor " + novaCor.Descricao + " já existe.";
                }
                else
                {
                    mensagem = "Ocorreu um erro: " + ex.Message;
                }
            }

            return mensagem;
        }

        internal static Core Remover(int idCor)
        {
            Core cor = null;

            using (var ctx = new RPGDBEntities())
            {
                cor = ctx.Cores.FirstOrDefault(
                        x => x.IdCor == idCor
                     );
                ctx.Cores.Remove(cor);
                ctx.SaveChanges();
            }
            return cor;
        }

        internal static string AlterarCor(Core novaCor)
        {
            string mensagem = "";
            //Tratamento de erros
            try
            {
                using (RPGDBEntities ctx = new RPGDBEntities())
                {
                    Core cor = ctx.Cores.FirstOrDefault(
                        x => x.IdCor == novaCor.IdCor
                     );

                    cor.Descricao = novaCor.Descricao;
                    ctx.SaveChanges();

                    mensagem = "Cor " + novaCor.Descricao + " alterada com sucesso!";
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