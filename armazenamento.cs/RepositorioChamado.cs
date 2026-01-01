using System;
using System.Collections.Generic;
using System.Text;


public class RepositorioChamados : IArmazenamento
{
    // Chave: ID do chamado, Valor: O objeto Chamado completo
    private Dictionary<string, Chamado> _dados = new Dictionary<string, Chamado>();

    public bool Salvar(Chamado chamado)
    {
        if (!_dados.ContainsKey(chamado.Id))
        {
            _dados.Add(chamado.Id, chamado);
        }
        else
        {
            _dados[chamado.Id] = chamado; // Atualiza se já existir
        }
        return true;
    }

    public List<Chamado> ListarTodos()
    {
        // Aqui está o segredo: convertendo os VALORES do dicionário em uma lista
        return _dados.Values.ToList();
    }

    public Chamado BuscarPorId(string id)
    {
        _dados.TryGetValue(id, out var chamado);
        return chamado; // Retorna o chamado ou null se não achar
    }
}
