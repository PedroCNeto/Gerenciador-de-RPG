using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace CRUDGame
{
    internal class PersonagemDAO
    {
        internal static Personagen ListarPersonagens(int id)
        {
            Personagen personagem = null;

            using (var ctx = new RPGDBEntities())
            {
                personagem = ctx.Personagens.FirstOrDefault(
                        x => x.IdPersonagem == id
                    );
            }

            return personagem;
        }

        public static List<Personagen> ListarPersonagens()
        {
            List<Personagen> personagems = null;
            try
            {
                using (var ctx = new RPGDBEntities())
                {
                    personagems = ctx.Personagens.OrderBy(
                        x => x.NomePersonagem).ToList();
                }
            }
            catch (Exception ex)
            {
            }
            return personagems;
        }

        internal static string CadastrarPersonagem(Personagen novoPersonagem)
        {
            string mensagem = "";

            try
            {
                using (var ctx = new RPGDBEntities())
                {
                    ctx.Personagens.Add(novoPersonagem);
                    ctx.SaveChanges();
                }
                mensagem = "Personagem " + novoPersonagem.NomePersonagem + " cadastrada com suceso!";
            }
  
            catch (Exception ex)
            {
                mensagem = "Ocorreu um erro: " + ex.Message;
            }
            return mensagem;
        }

        internal static string AlterarPersonagem(Personagen novoPersonagem)
        {
            string mensagem = "";
            //Tratamento de erros
            try
            {
                using (RPGDBEntities ctx = new RPGDBEntities())
                {
                    Personagen personagem = ctx.Personagens.FirstOrDefault(
                        x => x.IdPersonagem == novoPersonagem.IdPersonagem
                     );

                    personagem.NomePersonagem = novoPersonagem.NomePersonagem;
                    personagem.Forca = novoPersonagem.Forca;
                    personagem.Destreza = novoPersonagem.Destreza;
                    personagem.Sabedoria = novoPersonagem.Sabedoria;
                    personagem.Constituicao = novoPersonagem.Constituicao;
                    personagem.Inteligencia = novoPersonagem.Inteligencia;
                    personagem.Carisma = novoPersonagem.Carisma;
                    personagem.Peso = novoPersonagem.Peso;
                    personagem.Carisma = novoPersonagem.Carisma;
                    personagem.Altura = novoPersonagem.Altura;
                    personagem.CorCabelo = novoPersonagem.CorCabelo;
                    personagem.EstiloCabelo = novoPersonagem.EstiloCabelo;
                    personagem.CorOlho = novoPersonagem.CorOlho;
                    personagem.CorPele = novoPersonagem.CorPele;
                    personagem.DataNasc = novoPersonagem.DataNasc;
                    personagem.Nivel = novoPersonagem.Nivel;
                    personagem.Sexo = novoPersonagem.Sexo;
                    personagem.RacaID = novoPersonagem.RacaID;
                    personagem.SubclasseID = novoPersonagem.SubclasseID;





                    ctx.SaveChanges();

                    mensagem = "Personagem " + personagem.NomePersonagem + " alterada com sucesso!";
                }
            }
            catch (Exception ex)
            {
                mensagem = ex.Message;
            }

            return mensagem;
        }

        internal static Personagen Remover(int idPersonagem)
        {
            Personagen person = null;

            using (var ctx = new RPGDBEntities())
            {
                person = ctx.Personagens.FirstOrDefault(
                        x => x.IdPersonagem == idPersonagem
                     );
                ctx.Personagens.Remove(person);
                ctx.SaveChanges();
            }


            return person;
        }
    }
}