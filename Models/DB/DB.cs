using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using VotacaoAlmoco.Models.Resultados;
using VotacaoAlmoco.Models.Votos;
using VotacaoAlmoco.Models.Restaurantes;
using VotacaoAlmoco.Models.Colaboradores;

namespace VotacaoAlmoco.Models.Database
{
    public static class DB
    {

        //variaveis globais da classe
        private static string CaminhoApp = Directory.GetCurrentDirectory();
        private static string NomeDB = "//DbVotacoes.xml";

        //Verifica se base já está criada
        private static bool DbExist()
        {
            if (File.Exists(Directory.GetCurrentDirectory() + "//DbVotacoes.xml"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Cria o arquivo de base
        private static void CriaDb()
        {
            string path = Path.Combine(CaminhoApp + NomeDB);
            StreamWriter file = new StreamWriter(path);
            file.Close();
        }

        //Salva os resultados no Db
        public static void SalvarVoto(Voto voto)
        {
            //Cria a variavel que irá manipular o DB
            XDocument xDoc;
            //Cria o elemento que contera a votacao
            XElement elementVotacao = new XElement("Votacao");
            //Cria elemento que contera o restaurante
            XElement elementRestaurante = new XElement("Restaurante");

            //Antes de salvar verifica se base existe, senão cria
            if (DbExist())
            {
                //Carrega o documento
                xDoc = XDocument.Load(CaminhoApp + NomeDB);
                
                //Busca a votacao do dia
                List<XElement> elementsVotacao = (from el in xDoc.Descendants("Votacao")
                                     where (string)el.Attribute("DataVotacao").Value == voto.DataVoto.ToString("dd/MM/yyyy")
                                     select el).ToList();

                //Se já existe algum voto no dia especificado > adiciona > senao > cria a votacao
                if (elementsVotacao != null && elementsVotacao.Count > 0)
                {
                    //Verifica se o restaurante já foi votado
                    List<XElement> elementsRestaurante = elementsVotacao.Descendants("Restaurante").ToList();
                    var xRestaurante = elementsRestaurante.Find(element => element.Attribute("IDRestaurante").Value == voto.Restaurante.ID.ToString());

                    //Se ja foi votado > adicionar 1 voto > senao > insere e computa voto
                    if (xRestaurante != null)
                    {
                        //Selecionar a votacao e adiciona um voto à quantidade
                        xRestaurante.Attribute("QtdVotos").Value = (Int32.Parse(xRestaurante.Attribute("QtdVotos").Value) + 1).ToString();

                        //Cria o elemento colaborador
                        XElement elementColaborador = new XElement("Colaborador");
                        elementColaborador.Value = voto.Colaborador.ID.ToString();

                        //Adiciona os elementos Child
                        xRestaurante.Add(elementColaborador);
                    }
                    else
                    {
                        //Adiciona os atributos ao elemento restaurante
                        elementRestaurante.SetAttributeValue("IDRestaurante", voto.Restaurante.ID.ToString());
                        elementRestaurante.SetAttributeValue("QtdVotos", 1);

                        //Cria o elemento colaborador
                        XElement elementColaborador = new XElement("Colaborador");
                        elementColaborador.Value = voto.Colaborador.ID.ToString();

                        //Adiciona os elementos Child
                        elementRestaurante.Add(elementColaborador);
                        elementsVotacao[0].Add(elementRestaurante);
                    }
                }
                else if (elementsVotacao.Count == 0)
                {
                    elementVotacao.SetAttributeValue("DataVotacao", voto.DataVoto.ToString("dd/MM/yyyy"));

                    //Adiciona os atributos ao elemento restaurante
                    elementRestaurante.SetAttributeValue("IDRestaurante", voto.Restaurante.ID.ToString());
                    elementRestaurante.SetAttributeValue("QtdVotos", 1);

                    //Cria o elemento colaborador
                    XElement elementColaborador = new XElement("Colaborador");
                    elementColaborador.Value = voto.Colaborador.ID.ToString();

                    //Adiciona os elementos Child
                    elementRestaurante.Add(elementColaborador);
                    elementVotacao.Add(elementRestaurante);

                    //Adiciona o elemento
                    xDoc.Root.Add(elementVotacao);
                }    
            }
            else
            {
                //Cria o arquivo da base de dados
                CriaDb();

                //Instancia o documento e cria o elemento root
                xDoc = new XDocument(new XElement("Root"));
                elementVotacao.SetAttributeValue("DataVotacao", voto.DataVoto.ToString("dd/MM/yyyy"));

                //Adiciona os atributos ao elemento restaurante
                elementRestaurante.SetAttributeValue("IDRestaurante", voto.Restaurante.ID.ToString());
                elementRestaurante.SetAttributeValue("QtdVotos", 1);

                //Cria o elemento colaborador
                XElement elementColaborador = new XElement("Colaborador");
                elementColaborador.Value = voto.Colaborador.ID.ToString();

                //Adiciona os elementos Child
                elementRestaurante.Add(elementColaborador);
                elementVotacao.Add(elementRestaurante);

                //Adiciona o elemento
                xDoc.Root.Add(elementVotacao);
            }
            
            //Salva os dados no arquivo
            xDoc.Save(Path.Combine(CaminhoApp + NomeDB));
        }

        public static List<Resultado> BuscaResultado(DateTime dataVotacao)
        {
            //Carrega o documento
            XDocument xDoc = XDocument.Load(CaminhoApp + NomeDB);
            
            //Busca a votacao do dia solicitado
            IEnumerable<XElement> elementsVotacao = from el in xDoc.Descendants("Votacao")
                                                    where (string)el.Attribute("DataVotacao") == dataVotacao.ToString("dd/MM/yyyy")
                                                    select el;

            //Se já existe algum voto no dia selecionado > monta objeto > senao > retorna objeto nulo
            if (elementsVotacao.Count() > 0)
            {

                //Seleciona todos os elementos restaurante
                var elementsRestaurante = elementsVotacao.Descendants("Restaurante"); //.Where(element => element.Name == "Restaurante");

                //Instacia objeto de retorno (resultado)
                List<Resultado> listaResultado = new List<Resultado>();

                //Instacia objeto utilzado na pesquisa
                RestauranteManager restauranteManager = new RestauranteManager();

                foreach (var restaurante in elementsRestaurante)
                {
                    //Instacia o objeto que receberá o resultado
                    Resultado resultado = new Resultado();

                    //Seta a quantidade de votos do restaurante
                    resultado.QuantidadeVotos = Int32.Parse(restaurante.Attribute("QtdVotos").Value.ToString());

                    //Seta a data de votacao
                    resultado.DataVotacao = dataVotacao;

                    //Pega o ID do restaurante
                    int idRestaurante = Int32.Parse(restaurante.Attribute("IDRestaurante").Value.ToString());
                    
                    //Instacia o objeto restaurante
                    Restaurante restauranteObj = new Restaurante();
                    restauranteObj = restauranteManager.GetRestauranteByID(idRestaurante);

                    //adiciona o restaurante ao resultado
                    resultado.Restaurante = restauranteObj;

                    //Seleciona os colaboradores que votaram no restaurante
                    var elementColaboradores = restaurante.Descendants("Colaborador");

                    //Instancia objeto utilizado na pesquisa
                    ColaboradorManager colaboradorManager = new ColaboradorManager();

                    foreach (var colaborador in elementColaboradores)
                    {
                        //Pega o ID do colaborador
                        int idColaborador = Int32.Parse(colaborador.Value.ToString());

                        //Instacia o objeto colaborador
                        Colaborador colaboradorObj = new Colaborador();
                        colaboradorObj = colaboradorManager.GetColaboradorByID(idColaborador);

                        //Adiciona o colaborador ao resultado
                        resultado.Colaboradores.Add(colaboradorObj);
                        
                    }

                    //Adiciona resultado a lista
                    listaResultado.Add(resultado);
                }

                //Retorna lista de resultado
                return listaResultado;
            }
            else
            {
                //Se nao encontrou a votacao, retorna nulo
                return null;
            }
        }
    }
}