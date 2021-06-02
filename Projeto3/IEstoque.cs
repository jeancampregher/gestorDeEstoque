using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto3
{
    interface IEstoque
    { //uma implementação para todas as variações de produtos
        void Exibir();
        void AdicionarEntrada(); //adicionar um novo item no estoque, ou nova vaga em curso
        void AdicionarSaida(); //registrar vendas nesses produtos
    }
}
