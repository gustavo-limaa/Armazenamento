using System;
using System.Collections.Generic;
using System.Linq; // Essencial para usar o Count, Any, etc.

public class RelatorioService
{
    private readonly List<Chamado> _chamados;

    // O construtor recebe a lista que veio do arquivo
    public RelatorioService(List<Chamado> chamados)
    {
        _chamados = chamados;
    }

    public void GerarRelatorio()
    {
        Console.WriteLine("\n📊 === DASHBOARD DE CHAMADOS ===");

        // 1. Total (LINQ Count)
        Console.WriteLine($"Total de Registros: {_chamados.Count}");

        // 2. Filtro de Alta Prioridade (LINQ Where + Count)
        var totalAlta = _chamados.Count(c => c.prioridadechamado == Chamado.Prioridade.Alta);
        Console.WriteLine($"Críticos (Alta Prioridade): {totalAlta}");

        // 3. Busca por palavra-chave (LINQ Any)
        if (_chamados.Any(c => c.Descricao.ToLower().Contains("gato")))
        {
            Console.WriteLine("💡 Nota: Existem chamados pendentes sobre 'gatos'.");
        }

        Console.WriteLine("=================================\n");
    }
}