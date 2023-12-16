using System;
using System.Collections.Generic;
using System.Linq;

namespace CRUDGame
{
    internal class HabilidadesPersonagensDAO
    {
        internal static List<Personagem_Habilidade> ListarCombinacoes()
        {
            List<Personagem_Habilidade> habsperson = null;
            try
            {
                using (var ctx = new RPGDBEntities())
                {
                    habsperson = ctx.Personagem_Habilidade.OrderBy(
                        x => x.IdPersonHab).ToList();
                }
            }
            catch (Exception ex)
            {
            }
            return habsperson;
        }

        internal static string CadastrarHabsPerson(Personagem_Habilidade novaPersonHab)
        {

            string mensagem = "";

            try
            {
                using (var ctx = new RPGDBEntities())
                {
                    Personagem_Habilidade personHab = ctx.Personagem_Habilidade.Add(novaPersonHab);
                    ctx.SaveChanges();


                }
                Personagen person = PersonagemDAO.ListarPersonagens(novaPersonHab.Personagens_IdPersonagem);
                Habilidade hab = HabilidadeDAO.ListarHabilidades(novaPersonHab.Habilidades_IdHabilidade);
                mensagem = "Habilidade " + hab.Descricao + " cadastrada no personagem " + person.NomePersonagem + " com sucesso!";
            }
            catch (Exception ex)
            {
                    mensagem = "Ocorreu um erro: " + ex.Message;
            }

            return mensagem;
        }

        internal static Personagem_Habilidade ListarCombinacoes(int id)
        {
            Personagem_Habilidade personagem_Habilidade = null;

            using (var ctx = new RPGDBEntities())
            {
                personagem_Habilidade = ctx.Personagem_Habilidade.FirstOrDefault(
                        x => x.IdPersonHab == id
                    );
            }

            return personagem_Habilidade;
        }

        internal static string AlterarHabsPerson(Personagem_Habilidade novaPersonHab)
        {
            string mensagem = "";
            //Tratamento de erros
            try
            {
                using (RPGDBEntities ctx = new RPGDBEntities())
                {
                    Personagem_Habilidade personhabilidade = ctx.Personagem_Habilidade.FirstOrDefault(
                        x => x.IdPersonHab == novaPersonHab.IdPersonHab
                     );

                    personhabilidade.Habilidades_IdHabilidade = novaPersonHab.Habilidades_IdHabilidade;
                    personhabilidade.Personagens_IdPersonagem = novaPersonHab.Personagens_IdPersonagem;

                    ctx.SaveChanges();

                    Personagen person = PersonagemDAO.ListarPersonagens(personhabilidade.Personagens_IdPersonagem);
                    Habilidade hab = HabilidadeDAO.ListarHabilidades(personhabilidade.Habilidades_IdHabilidade);
                    mensagem = "Habilidade " + hab.Descricao + " alterada no personagem " + person.NomePersonagem + " com sucesso!";
                }
            }
            catch (Exception ex)
            {
                mensagem = ex.Message;
            }

            return mensagem;
        }

        internal static Personagem_Habilidade Remover(int id)
        {
            Personagem_Habilidade personagem_Habilidade = null;

            using (var ctx = new RPGDBEntities())
            {
                personagem_Habilidade = ctx.Personagem_Habilidade.FirstOrDefault(
                        x => x.IdPersonHab == id
                     );
                ctx.Personagem_Habilidade.Remove(personagem_Habilidade);
                ctx.SaveChanges();
            }
            return personagem_Habilidade;
        }
    }
}