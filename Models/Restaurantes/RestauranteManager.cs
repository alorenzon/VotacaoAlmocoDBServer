using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VotacaoAlmoco.Models.Resultados;

namespace VotacaoAlmoco.Models.Restaurantes
{
    public class RestauranteManager
    {
        private readonly List<Restaurante> _restaurantes = new List<Restaurante>
        {
            new Restaurante {
                ID = 1, 
                Nome = "Restaurante Pueblo", 
                Descricao = "Cozinha une ingredientes indígenas mexicanos e espanhóis, servidos com pimenta à parte, em ambiente colorido.",
                Endereco = "Av. Ijuí, 147 - Petrópolis",
                ValorAproximado = 26.30},
            new Restaurante {
                ID = 2, 
                Nome = "Restaurante Árabe Baalbek", 
                Descricao = "Culinária e cultura árabes em restaurante com decoração típica e delícias beduínas dispostas em buffet.",
                Endereco = "R. Dr. Timóteo, 272 - Floresta",
                ValorAproximado = 28.90},
            new Restaurante {
                ID = 3, 
                Nome = "Galeteria Casa do Marquês", 
                Descricao = "Cozinha brasileira oferece galeto e opções em casa espaçosa e informal, com sala própria para a criançada.",
                Endereco = "R. Marquês do Pombal, 1814 - Higienópolis",
                ValorAproximado = 22.90},
            new Restaurante {
                ID = 4, 
                Nome = "Via Imperatore - Restaurante", 
                Descricao = "Carnes grelhadas com buffet de massas, saladas, queijos e frutas, em espaço contemporâneo-rústico informal.",
                Endereco = "R. da República, 509 - Cidade Baixa",
                ValorAproximado = 16.90},
            new Restaurante {
                ID = 5, 
                Nome = "Sharin", 
                Descricao = "Cozinha indiana de aromas intensos, em ambiente ricamente decorado com temas típicos, com músico e dançarina.",
                Endereco = "R. Felipe Neri, 332 - Auxiliadora",
                ValorAproximado = 16.90},
            new Restaurante {
                ID = 6, 
                Nome = "Tirol", 
                Descricao = "Rodízio de grelhados e buffet, além de filés, frango e pescados, em restaurante familiar com área infantil.",
                Endereco = "R. José de Alencar, 520 - Menino Deus",
                ValorAproximado = 36.90},
            new Restaurante {
                ID = 7, 
                Nome = "Santo Antônio", 
                Descricao = "Grelhados na chapa, peixes, massas, doces e vinhos em casa familiar, fundada por imigrantes italianos em 1935.",
                Endereco = "R. Dr. Timóteo, 465 - Moinhos de Vento",
                ValorAproximado = 20.90},
            new Restaurante {
                ID = 8, 
                Nome = "Cavanhas", 
                Descricao = "Lanchonete simples serve bebidas variadas, hambúrgueres e pizzas diversas, com a opção de pratos executivos.",
                Endereco = "R. Gen. Lima e Silva, 274 - Cidade Baixa",
                ValorAproximado = 15.50},
            new Restaurante {
                ID = 9, 
                Nome = "Takêdo", 
                Descricao = "Gastronomia japonesa une ingredientes asiáticos e orientais em combinações harmoniosas e ambiente refinado.",
                Endereco = "Rua Carvalho Monteiro, 397 - Bela Vista",
                ValorAproximado = 49.90},
            new Restaurante {
                ID = 10, 
                Nome = "Churrascaria Galpão Crioulo", 
                Descricao = "Churrascaria de buffet variado, apresentações de danças do folclore gaúcho, em ambiente e móveis rústicos.",
                Endereco = "R. Otávio Francisco Caruso da Rocha, s/n - Centro Histórico",
                ValorAproximado = 55.00},
        };

        public List<Restaurante> GetAll()
        { 
            return _restaurantes;
        }

        public Restaurante GetRestauranteByID(int ID)
        {
            return _restaurantes.Find(r => r.ID == ID);
        }
        
        public List<Restaurante> CampeoesDaSemana()
        {
            //Instacia o objeto resultado
            Resultado resultado = new Resultado();

            //Pesquisa os restaurantes já votados
            List<Resultado> listaResultadoDay1 = new List<Resultado>();
            List<Resultado> listaResultadoDay2 = new List<Resultado>();
            List<Resultado> listaResultadoDay3 = new List<Resultado>();
            List<Resultado> listaResultadoDay4 = new List<Resultado>();
            List<Resultado> listaResultadoDay5 = new List<Resultado>();
            List<Resultado> listaResultadoDay6 = new List<Resultado>();

            //cria variavel para armazenar data atual
            var today = DateTime.Now;

            //Se for mais que 11 da manha, o dia de votacao atual eh o proximo
            if (today.Hour >= 11)
            {
                today = today.AddDays(1);
            }

            //Carrega as variaveis com os dias da semana
            var day1 = today.AddDays(-1);
            var day2 = today.AddDays(-2);
            var day3 = today.AddDays(-3);
            var day4 = today.AddDays(-4);
            var day5 = today.AddDays(-5);
            var day6 = today.AddDays(-6);

            //Pesquisa os resultados de cada data
            listaResultadoDay1 = resultado.LerResultado(day1);
            listaResultadoDay2 = resultado.LerResultado(day2);
            listaResultadoDay3 = resultado.LerResultado(day3);
            listaResultadoDay4 = resultado.LerResultado(day4);
            listaResultadoDay5 = resultado.LerResultado(day5);
            listaResultadoDay6 = resultado.LerResultado(day6);

            //Cria a lista de retorno
            List<Restaurante> restaurantesCampeoes = new List<Restaurante>();

            //Carrega a lista com os restaurantes campeoes, verificando se existe resultado
            if (listaResultadoDay1.Count > 0)
            {
                restaurantesCampeoes.Add(listaResultadoDay1.First().Restaurante);
            }

            if (listaResultadoDay2.Count > 0)
            {
                restaurantesCampeoes.Add(listaResultadoDay2.First().Restaurante);
            }

            if (listaResultadoDay3.Count > 0)
            {
                restaurantesCampeoes.Add(listaResultadoDay3.First().Restaurante);
            }

            if (listaResultadoDay4.Count > 0)
            {
                restaurantesCampeoes.Add(listaResultadoDay4.First().Restaurante);
            }

            if (listaResultadoDay5.Count > 0)
            {
                restaurantesCampeoes.Add(listaResultadoDay5.First().Restaurante);
            }

            if (listaResultadoDay6.Count > 0)
            {
                restaurantesCampeoes.Add(listaResultadoDay6.First().Restaurante);
            }

            //Retorna a lista
            return restaurantesCampeoes;

        }

        public List<Restaurante> GetRestaurantesCandidatos()
        {
            //Carrega lista com todos os restaurantes
            List<Restaurante> listaAllRestaurante = new List<Restaurante>();
            listaAllRestaurante = _restaurantes;

            //Cria lista dos restaurantes que ja venceram na semana
            List<Restaurante> listaRestaurantesCampeoesSemana = new List<Restaurante>();
            listaRestaurantesCampeoesSemana = CampeoesDaSemana();

            //Remove da lista os restaurantes ganhadores da semana
            for (int i = 0; i < listaRestaurantesCampeoesSemana.Count(); i++)
            {
               listaAllRestaurante.RemoveAll(r => r.ID == listaRestaurantesCampeoesSemana[i].ID);
            }

            //Retorna lista com os restaurantes que ainda nao ganharam na semana
            return listaAllRestaurante;
        }
    }
}