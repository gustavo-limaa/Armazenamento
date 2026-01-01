using System;


        IArmazenamento armazenamento;

        Console.WriteLine("===== Bem-vindo ao atendimento web =====");
        Console.WriteLine("Escolha o tipo de armazenamento:");
        Console.WriteLine("1 - Arquivo");
        Console.WriteLine("2 - Memória");

        var tipo = Console.ReadLine();

        if (tipo == "1")
            armazenamento = new RepositorioChamadoArquivo();
        else
            armazenamento = new RepositorioChamados();

        var sair = false;

        while (!sair)
        {
            Console.WriteLine("\n1 - Salvar chamado");
            Console.WriteLine("2 - Mostrar todos os chamados");
            Console.WriteLine("3 - Buscar chamado por ID");
            Console.WriteLine("4 - Gerar relatório de chamados");
    Console.WriteLine("0 - Sair");

            var opcao = Console.ReadLine();

            switch (opcao)
            {
        case "1":
            Console.WriteLine("Digite o ID do chamado:");
            // Adicionamos o ?? "" para garantir que NUNCA seja nulo
            var id = Console.ReadLine() ?? "";

            Console.WriteLine("Digite a descrição do chamado:");
            var descricao = Console.ReadLine() ?? "";

            // O seu IF já trata se está vazio, então o compilador fica feliz
            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(descricao))
            {
                Console.WriteLine("ID e descrição são obrigatórios.");
                break;
            }

            Console.WriteLine("Digite a prioridade (Alta, Media, Baixa):");
            var prioridadeInput = Console.ReadLine() ?? "Baixa";

            if (!Enum.TryParse(prioridadeInput, true, out Chamado.Prioridade prioridade))
            {
                prioridade = Chamado.Prioridade.Baixa;
            }

            var novoChamado = new Chamado(id, descricao, prioridade);
            bool salvo = armazenamento.Salvar(novoChamado);

            if (!salvo)
                Console.WriteLine("❌ Erro: ID já existe.");
            else
                Console.WriteLine("✅ Salvo!");
            break;
        case "2":
            // 1. Pegamos a lista que o repositório leu do arquivo
            var lista = armazenamento.ListarTodos();

            Console.WriteLine("\n--- LISTA DE CHAMADOS ---");

            // 2. Verificamos se há algo para mostrar
            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhum chamado encontrado no sistema.");
            }
            else
            {
                // 3. Este laço percorre a lista e imprime cada item
                foreach (var c in lista)
                {
                    Console.WriteLine($"ID: {c.Id} | Descrição: {c.Descricao} | Prioridade: {c.prioridadechamado}");
                }
            }
            Console.WriteLine("-------------------------\n");
            break;
           
        case "3":
            Console.WriteLine("Digite o ID:");
            var idBusca = Console.ReadLine() ?? "";
            var encontrado = armazenamento.BuscarPorId(idBusca);

            // O SEGREDO: Verificar se 'encontrado' não é nulo antes de usar
            if (encontrado != null)
            {
                Console.WriteLine($"Achamos: {encontrado.Descricao}");
            }
            else
            {
                Console.WriteLine("⚠️ Chamado não encontrado.");
            }
            break;
        case "4":
            var dadosAtuais = armazenamento.ListarTodos();
            var relatorio = new RelatorioService(dadosAtuais);
            relatorio.GerarRelatorio();
            break;






        case "0":
                    sair = true;
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    

