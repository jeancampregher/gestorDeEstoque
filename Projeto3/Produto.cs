using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto3
{
    [System.Serializable]
    abstract class Produto //classe pai, não vou conseguir criar instâncias dela.
    {
        public string nome;
        public float preco;
    }
}
