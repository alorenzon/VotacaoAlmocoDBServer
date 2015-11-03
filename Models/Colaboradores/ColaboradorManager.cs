using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VotacaoAlmoco.Models.Colaboradores
{
    public class ColaboradorManager
    {
        private List<Colaborador> _colaboradores = new List<Colaborador>
        {
            new Colaborador {ID = 1, Nome = "Alexandre Lorenzon"},
            new Colaborador {ID = 2, Nome = "João da Silva"},
            new Colaborador {ID = 3, Nome = "Gilberto Oliveira"},
            new Colaborador {ID = 4, Nome = "Maria Terezinha de Jesus"},
            new Colaborador {ID = 5, Nome = "Joana Ferreira"},
            new Colaborador {ID = 6, Nome = "Gabriela Passos"},
        };

        public List<Colaborador> GetAll()
        {
            return _colaboradores;
        }

        public Colaborador GetColaboradorByID(int ID)
        {
            return _colaboradores.Find(c => c.ID == ID);
        }
    }
}