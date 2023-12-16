using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace CRUDGame
{
    public class SubclasseDAO
    {
        public static string CadastrarSubclasse(Subclass novaSubclasse)
        {
            string mensagem = "";

            try
            {
                using (var ctx = new RPGDBEntities())
                {
                    ctx.Subclasses.Add(novaSubclasse);
                    ctx.SaveChanges();
                }
                mensagem = "Subclasse " + 
                    novaSubclasse.Descricao + " cadastrada com suceso!";
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2601 || ex.Number == 2627)
                {
                    mensagem = "A Subclasse " + novaSubclasse.Descricao + " já existe.";
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
                    mensagem = "A Subclasse " + novaSubclasse.Descricao + " já existe.";
                }
                else
                {
                    mensagem = "Ocorreu um erro: " + ex.Message;
                }
            }

            return mensagem;
        }

        internal static List<Subclass> ListarSubclasses()
        {
            List<Subclass> subClasses = null;
            try
            {
                using (var ctx = new RPGDBEntities())
                {
                    subClasses = ctx.Subclasses.OrderBy(x => x.Descricao).ToList();
                }
            }
            catch (Exception ex)
            {
            }
            return subClasses;
        }

        public static Subclass ListarSubclasse(int id)
        {
            Subclass subclasse = null;

            using (var ctx = new RPGDBEntities())
            {
                subclasse = ctx.Subclasses.FirstOrDefault(
                        x => x.IdSubclasse == id
                    );
            }

            return subclasse;
        }

        public static Subclass ListarSubclasseNome(string nome)
        {
            Subclass subclasse = null;

            using (var ctx = new RPGDBEntities())
            {
                subclasse = ctx.Subclasses.FirstOrDefault(
                        x => x.Descricao == nome
                    );
            }

            return subclasse;
        }

        public static Subclass Remover(int idSubclasse)
        {
            Subclass sub = null;

            using (var ctx = new RPGDBEntities())
            {
                sub = ctx.Subclasses.FirstOrDefault(
                        x => x.IdSubclasse == idSubclasse
                     );
                ctx.Subclasses.Remove(sub);
                ctx.SaveChanges();
            }


            return sub;
        }

        public static string AlterarSubclasse(Subclass novaSubclasse)
        {
            string mensagem = "";
            //Tratamento de erros
            try
            {
                using (RPGDBEntities ctx = new RPGDBEntities())
                {
                    Subclass subclasse = ctx.Subclasses.FirstOrDefault(
                        x => x.IdSubclasse == novaSubclasse.IdSubclasse
                     );

                    subclasse.Descricao = novaSubclasse.Descricao;
                    subclasse.ClasseID = novaSubclasse.ClasseID;
                    ctx.SaveChanges();

                    mensagem = "Subclasse " + subclasse.Descricao + " alterada com sucesso!";
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