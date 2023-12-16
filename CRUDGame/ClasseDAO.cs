using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace CRUDGame
{
    public class ClasseDAO
    {
        public static string CadastrarClasse(Class novaClasse)
        {
            string mensagem = "";
            //Tratamento de erros
            try
            {
                using (RPGDBEntities ctx = new RPGDBEntities())
                {
                    //Cadastrando uma nova classe
                    ctx.Classes.Add(novaClasse);
                    //Salvando as alterações no BD
                    ctx.SaveChanges();
                    mensagem = "Classe " + novaClasse.Descricao + 
                        " cadastrada com sucesso!";
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2601 || ex.Number == 2627)
                {
                    mensagem = "A classe " + novaClasse.Descricao + " já existe.";
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
                    mensagem = "A classe " + novaClasse.Descricao + " já existe.";
                }
                else
                {
                    mensagem = "Ocorreu um erro: " + ex.Message;
                }
            }

            return mensagem;
        }

        /// <summary>
        /// Retorna uma classe, de acordo com o id.
        /// </summary>
        /// <param name="classeID"></param>
        /// <returns></returns>
        public static Class ListarClasses(int classeID)
        {
            Class classe = null;

            using (var ctx = new RPGDBEntities())
            {
                classe = ctx.Classes.FirstOrDefault(
                        x => x.IdClasse == classeID
                    );
            }
            
            return classe;
        }

        /// <summary>
        /// Retorna todas as classes cadastradas!
        /// </summary>
        /// <returns></returns>
        public static List<Class> ListarClasses()
        {
            List<Class> classes = null;
            try
            {
                using (var ctx = new RPGDBEntities())
                {
                    classes = ctx.Classes.OrderBy(
                        x => x.Descricao).ToList();
                }
            }
            catch (Exception ex)
            {
            }
            return classes;
        }

        public static Class Remover(int idClasse)
        {
            Class classe = null;

            using (var ctx = new RPGDBEntities())
            {
                classe = ctx.Classes.FirstOrDefault(
                        x => x.IdClasse == idClasse
                     );
                ctx.Classes.Remove(classe);
                ctx.SaveChanges();
            }
            return classe;
        }

        public static string AlterarClasse(Class novaClasse)
        {
            string mensagem = "";
            //Tratamento de erros
            try
            {
                using (RPGDBEntities ctx = new RPGDBEntities())
                {
                    Class classe = ctx.Classes.FirstOrDefault(
                        x => x.IdClasse == novaClasse.IdClasse
                     );

                    classe.Descricao = novaClasse.Descricao;
                    ctx.SaveChanges();

                    mensagem = "Classe " + novaClasse.Descricao + " alterada com sucesso!";
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