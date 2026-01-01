
using static Chamado;

public class Chamado
    {
        public enum Prioridade
        {
            Alta,
            Media,
            Baixa

        }
    public string Id { get; set; }
    public string Descricao { get; set; }
    public Prioridade prioridadechamado { get; set; }
    public DateTime DataCriacao { get; set; }
    public Chamado() { }
    public Chamado(string id, string descricao, Prioridade prioridade)
    {
        Id = id;
        
        Descricao = descricao;
        
        prioridadechamado = prioridade;

        DataCriacao = DateTime.Now;
    }

}


