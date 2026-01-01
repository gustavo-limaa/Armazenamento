public interface IArmazenamento
{
   bool Salvar(Chamado chamado);
    List<Chamado> ListarTodos();
    Chamado BuscarPorId(string id);
}


