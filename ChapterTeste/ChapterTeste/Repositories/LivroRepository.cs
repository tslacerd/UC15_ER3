using ChapterTeste.Contexts;
using ChapterTeste.Models;

namespace ChapterTeste.Repositories
{
    public class LivroRepository
    {
        private readonly ChapterContext _context;

        public LivroRepository(ChapterContext context)
        {

            _context = context;

        }

        public List<Livro> Listar()
        {
            return _context.Livros.ToList();
        }
        public Livro BuscaPorId(int id)
        {
            return _context.Livros.Find(id);

        }

        public void Cadastrar(Livro livro)
        {
           _context.Livros.Add(livro);
           _context.SaveChanges();
        }

        public void Deletar(int id)
        {
            Livro livro = _context.Livros.Find(id);

            if (livro != null)
            { 
                _context.Livros.Remove(livro);

                _context.SaveChanges();
            }
                       
        }
        public void Atualizar(int id, Livro livro)
        {
            Livro livrobuscado = _context.Livros.Find(id);
            if (livrobuscado != null)
            {
                livrobuscado.Titulo = livro.Titulo;
                livrobuscado.QuantidadePaginas = livro.QuantidadePaginas;
                livrobuscado.Disponivel = livro.Disponivel;
            }
            _context.Livros.Update(livrobuscado);
            _context.SaveChanges();

        }
    }
}
