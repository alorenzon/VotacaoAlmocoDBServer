using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VotacaoAlmoco.Models.Resultados;

namespace VotacaoAlmoco.Controllers.Resultados
{
    public class ResultadoController : Controller
    {
        //Objeto para retorno vazio
        struct RetornoVazio
        {
            public string mensagem1;
            public string mensagem2;
        }

        //
        // GET: /Resultado/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult BuscaResultado(string dataVotacao)
        {
            DateTime data;

            //Verifica se parametro foi enviado corretamente
            if (!string.IsNullOrEmpty(dataVotacao))
            {
                data = Convert.ToDateTime(dataVotacao);    
            }
            else
            {
                //Monta objeto de retorno
                RetornoVazio ret = new RetornoVazio();
                ret.mensagem1 = "Data inválida!";
                ret.mensagem2 = "A data solicitada está no formato incorreto";

                return Json(ret, JsonRequestBehavior.AllowGet);
            }

            Resultado resultado = new Resultado();
            List<Resultado> listaResultado = new List<Resultado>();

            //Busca os resultados da votacao
            listaResultado = resultado.LerResultado(data);

            //Verifica se lista esta vazia
            if (listaResultado.Count < 1)
            {
                //Monta objeto de retorno
                RetornoVazio ret = new RetornoVazio();
                ret.mensagem1 = "Nenhuma votação foi encontrada!";
                ret.mensagem2 = "Não foi possível encontrar a votação do dia selecionado";

                return Json(ret, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(listaResultado, JsonRequestBehavior.AllowGet);
            }
            
        }

    }
}
