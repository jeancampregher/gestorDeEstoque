using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Projeto3
{
    class Program
    {

        static List<IEstoque> produtos = new List<IEstoque>(); //criando uma lista que vai aceitar qualquer tipo de dado que respeita o contrato de estoque
        enum Menu { Listar = 1, Adicionar, Remover, Entrada, Saida, Sair }

        static void Main(string[] args)
        {
            Carregar();
            bool escolheuSair = false;
            while (escolheuSair == false)
            {
                Console.WriteLine("Sistema de estoque");
                Console.WriteLine("1-Listar\n2-Adicionar\n3-Remover\n4-Registrar Entrada\n5-Registrar Saida\n6-Sair");
                string opStr = Console.ReadLine();
                int opInt = int.Parse(opStr);

                if (opInt > 0 && opInt < 7)
                {
                    Menu escolha = (Menu)opInt;

                    switch (escolha)
                    {
                        case Menu.Listar:
                            Listagem();
                            break;
                        case Menu.Adicionar:
                            Cadastro();
                            break;
                        case Menu.Remover:
                            Remover();
                            break;
                        case Menu.Entrada:
                            Entrada();
                            break;
                        case Menu.Saida:
                            Saida();
                            break;
                        case Menu.Sair:
                            escolheuSair = true; //fechar o loop e encerrar o programa
                            break;
                    }
                }
                else
                {
                    escolheuSair = true;
                }
                Console.Clear();
            }

        }

        static void Listagem()
        {
            Console.WriteLine("Lista de produtos");
            int i = 0;
            foreach (IEstoque produto in produtos) //vai percorrer todos os produtos na lista de produtos
            {
                Console.WriteLine("ID: " + i);
                produto.Exibir();
                i++;
            }
            Console.ReadLine(); //para finalizar essa função, pressionar ENTER
        }

        static void Remover()
        {
            Listagem();
            Console.WriteLine("Digite o ID do elemento que você quer remover: ");
            int id = int.Parse(Console.ReadLine());
            if (id >= 0 && id <produtos.Count)//verificando se o indice é válido
            {
                produtos.RemoveAt(id);
                Salvar();
            }
        }

        static void Entrada()
        {
            Listagem();
            Console.WriteLine("Digite o ID do elemento que você quer dar entrada: ");
            int id = int.Parse(Console.ReadLine());
            if (id >= 0 && id < produtos.Count)//verificando se o indice é válido
            {
                produtos[id].AdicionarEntrada(); //vai chamar todos os produtos do tipo estoque
                Salvar();
            }
        }

        static void Saida()
        {
            Listagem();
            Console.WriteLine("Digite o ID do elemento que você quer dar baixa: ");
            int id = int.Parse(Console.ReadLine());
            if (id >= 0 && id < produtos.Count)//verificando se o indice é válido
            {
                produtos[id].AdicionarSaida(); 
                Salvar();
            }
        }

        static void Cadastro() //método estático
        {
            Console.WriteLine("Cadastro de Produto");
            Console.WriteLine("1-Produto Físico\n2-Ebook\n3-Curso");
            string opStr = Console.ReadLine();
            int escolhaInt = int.Parse(opStr);
            switch (escolhaInt)
            {
                case 1:
                    CadastrarPFisico();
                    break;
                case 2:
                    CadastrarEbook();
                    break;
                case 3:
                    CadastrarCurso();
                    break;
            }
        }

        static void CadastrarPFisico() // cadastrando produto físico
        {
            Console.WriteLine("Cadastrando produto físico");
            Console.WriteLine("Nome: ");
            string nome = Console.ReadLine();
            Console.WriteLine("Preço: ");
            float preco = float.Parse(Console.ReadLine()); //convertendo para float
            Console.WriteLine("Frete: ");
            float frete = float.Parse(Console.ReadLine()); //convertando para float
            ProdutoFisico pf = new ProdutoFisico(nome, preco, frete);
            produtos.Add(pf); //polimorfismo de tipo
            Salvar();
        }

        static void CadastrarEbook() // cadastrando produto físico
        {
            Console.WriteLine("Cadastrando Ebook");
            Console.WriteLine("Nome: ");
            string nome = Console.ReadLine();
            Console.WriteLine("Preço: ");
            float preco = float.Parse(Console.ReadLine()); //convertendo para float
            Console.WriteLine("Autor: ");
            string autor = Console.ReadLine();

            Ebook eb = new Ebook(nome, preco, autor); //três informações do usuário
            produtos.Add(eb);
            Salvar();

        }

        static void CadastrarCurso() // cadastrando produto físico
        {
            Console.WriteLine("Cadastrando Curso");
            Console.WriteLine("Nome: ");
            string nome = Console.ReadLine();
            Console.WriteLine("Preço: ");
            float preco = float.Parse(Console.ReadLine()); //convertendo para float
            Console.WriteLine("Autor: ");
            string autor = Console.ReadLine();

            Curso cs = new Curso(nome, preco, autor);
            produtos.Add(cs);
            Salvar();
        }

        static void Salvar() //salvar os dados e listar
        {
            FileStream stream = new FileStream("produtos.dat", FileMode.OpenOrCreate); //abre ou cria um arquivo
            BinaryFormatter encoder = new BinaryFormatter();


            encoder.Serialize(stream, produtos);

            stream.Close(); //sempre fechar a stream

        }

        static void Carregar()
        {
            FileStream stream = new FileStream("produtos.dat", FileMode.OpenOrCreate); //abre ou cria um arquivo
            BinaryFormatter encoder = new BinaryFormatter();

            try
            {
                produtos = (List<IEstoque>)encoder.Deserialize(stream);

                if (produtos == null)
                {
                    produtos = new List<IEstoque>();
                }
            }
            catch(Exception e)
            {
                produtos = new List<IEstoque>(); //se der erro no processo acima, vai ser gerado uma nova lista novinha
            }

            stream.Close(); //sempre fechar
        }

    }
}
