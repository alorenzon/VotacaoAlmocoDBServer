using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VotacaoAlmoco.Models.Restaurantes;
using VotacaoAlmoco.Models.Colaboradores;
using VotacaoAlmoco.Models.Database;
using VotacaoAlmoco.Models.Resultados;

namespace VotacaoAlmoco.Models.Votos
{
    public class Voto
    {
        public Restaurante Restaurante { get; set; }
        public Colaborador Colaborador { get; set; }
        public DateTime DataVoto { get; set; }

        //Computado o voto dado pelo colaborador
        public void ComputaVoto()
        {
            //Chama model que organiza o db
            DB.SalvarVoto(this);
        }

    }
}