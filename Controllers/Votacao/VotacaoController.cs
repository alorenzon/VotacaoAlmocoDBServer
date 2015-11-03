using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VotacaoAlmoco.Models.Restaurantes;
using VotacaoAlmoco.Models.Colaboradores;
using VotacaoAlmoco.Models.Votos;
using VotacaoAlmoco.Models.Resultados;

namespace VotacaoAlmoco.Models.Votacao
{
    public class VotacaoController : Controller
    {
        //Verifica data da votacao
        public DateTime DataVotacao()
        {
            //Valida a data de votacao
            var today = DateTime.Now;

            if (today.Hour >= 11)
            {
               today = today.AddDays(1);
            }

            return today;
        }
        
        //Verifica se votacao esta aberta
        public bool VotacaoAberta()
        {
            //Cria os objetos de resultado
            Resultado resultado = new Resultado();
            List<Resultado> listaResultado = new List<Resultado>();

            //Verifico se nao tem votacao apos a data atual, caso o usuario altere o horario do SO
            DateTime dataPosterior = DataVotacao().AddDays(1);
            listaResultado = resultado.LerResultado(dataPosterior);

            if (listaResultado != null)
            {
                return false;
            }

            //Carrega lista de resultados
            listaResultado = resultado.LerResultado(DataVotacao());

            int totalVotos = 0;

            //Faz contagem total dos votos
            if (listaResultado != null)
            {
                for (int i = 0; i < listaResultado.Count(); i++)
                {
                    totalVotos += listaResultado[i].QuantidadeVotos;
                }

                //Busca quantidade total de colaboradores
                ColaboradorManager colaboradoresManager = new ColaboradorManager();
                List<Colaborador> listaColaboradores = new List<Colaborador>();
                listaColaboradores = colaboradoresManager.GetAll();

                //Verifica quantos votos ainda podem ser computados
                int votosRestantes = listaColaboradores.Count() - totalVotos;

                //Verifica a diferenca de votos entre o primeiro e o segundo colocado
                int difVotos = listaResultado[0].QuantidadeVotos - listaResultado[1].QuantidadeVotos;

                //Caso a diferenca seja superior a quantidade de votos restantes, encerra votacao
                if (difVotos > votosRestantes)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            else
            {
                return true;
            }
        }
        
        //
        // GET: /Votacao/
        public ActionResult Index()
        {
            if (VotacaoAberta())
            {
                return View();    
            }
            else
            {
                return "Votação encerrada";
            }
            
        }

        [HttpPost]
        public ActionResult ComputaVoto(int idRestaurante, int idColaborador)
        {
            
            if (!VotacaoAberta())
            {
               return Json("Votação encerrada!"); 
            }
            
            //Declara os objetos
            Voto voto = new Voto();
            Restaurante restaurante = new Restaurante();
            RestauranteManager restManager = new RestauranteManager();

            //Verifica se id passado eh valido
            restaurante = restManager.GetRestauranteByID(idRestaurante);

            //Adiciona o restaurante ao voto
            if (restaurante != null)
            {
                voto.Restaurante = restaurante;
            }
            else
            {
                return Json("Restaurante inválido!");
            }

            //Cria objeto colaborador
            Colaborador colaborador = new Colaborador();
            ColaboradorManager colabManager = new ColaboradorManager();

            //Verifica se colaborador existe
            colaborador = colabManager.GetColaboradorByID(idColaborador);

            //Adiciona o colaborador ao voto
            if (colaborador != null)
            {
                voto.Colaborador = colaborador;
            }
            else
            {
                return Json("Colaborador inválido!");
            }

            //Adiciona data de voto
            voto.DataVoto = DataVotacao();

            //Computa o voto do colaborador
            voto.ComputaVoto();

            //Retorno da funcao
            return Json("Voto computado com sucesso");
        }

    }
}
