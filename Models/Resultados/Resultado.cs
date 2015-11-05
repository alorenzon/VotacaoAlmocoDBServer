using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VotacaoAlmoco.Models.Restaurantes;
using VotacaoAlmoco.Models.Colaboradores;
using VotacaoAlmoco.Models.Database;

namespace VotacaoAlmoco.Models.Resultados
{
    public class Resultado
    {
        public Restaurante Restaurante { get; set; }
        private List<Colaborador> _colaboradores = new List<Colaborador>();
        public List<Colaborador> Colaboradores
        {
            get { return _colaboradores; }
        }
        public DateTime DataVotacao { get; set; }
        public int QuantidadeVotos { get; set; }
        
        public List<Resultado> LerResultado(DateTime dataVotacao)
        {
            //Verifica se o parametro foi passado corretamente
            if (dataVotacao != null)
            {
                //Instancia e pesquisa os resultados da votacao
                List<Resultado> listaResultado = new List<Resultado>();
                List<Resultado> listaResultadoOrdenado = new List<Resultado>();
                listaResultado = DB.BuscaResultado(dataVotacao);

                //Ordena a lista para deixar o campeao em primeiro lugar
                if (listaResultado != null)
                {
                    listaResultadoOrdenado = listaResultado.OrderByDescending(r => r.QuantidadeVotos).ToList();    
                }
                
                //Retorna lista ordenada
                return listaResultadoOrdenado;
                                
            }
            else
            {
                //senao retorna nulo
                return null;
            }
            
        }

    }
}