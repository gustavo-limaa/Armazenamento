using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.IO;

public class RepositorioChamadoArquivo : IArmazenamento
{
    private const string NomeArquivo = "chamados.json";

    public bool Salvar(Chamado chamado)
    {
        try
        {
            // 1. Busca os chamados existentes
            var chamados = ListarTodos();

            // 2. Verifica se o ID já existe (Regra que evita o erro vermelho no Program)
            if (chamados.Any(c => c.Id == chamado.Id))
            {
                return false; // Retorna falso para o Program avisar que o ID é duplicado
            }

            // 3. Adiciona e salva
            chamados.Add(chamado);
            var json = JsonSerializer.Serialize(chamados, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(NomeArquivo, json);

            return true; // Sucesso!
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro técnico ao salvar arquivo: {ex.Message}");
            return false;
        }
    }

    public List<Chamado> ListarTodos()
    {
        // Se o arquivo não existir, retorna lista vazia
        if (!File.Exists(NomeArquivo)) return new List<Chamado>();

        try
        {
            var json = File.ReadAllText(NomeArquivo);

            // Se o arquivo estiver vazio, o Deserialize pode falhar
            if (string.IsNullOrWhiteSpace(json)) return new List<Chamado>();

            // CONVERSÃO: O JsonSerializer já transforma o texto em List<Chamado>
            var listaDeChamados = JsonSerializer.Deserialize<List<Chamado>>(json);

            // Retorna a lista ou uma nova caso venha nulo
            return listaDeChamados ?? new List<Chamado>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao processar dados: {ex.Message}");
            return new List<Chamado>();
        }
    }

    public Chamado? BuscarPorId(string id)
    {
        return ListarTodos().FirstOrDefault(c => c.Id == id);
    }

  
}