using GestorEvento.Domain.Entities;
using GestorEvento.Infrastructure.Core;
using GestorEvento.Infrastructure.Interfaces;
using GestorEvento.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GestorEvento.Infrastructure.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(GestorDbcontext context) : base(context) { }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            return await Context.Usuarios.FindAsync(id);
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
            return await Context.Usuarios.ToListAsync();
        }

        public async Task<int> AddUsuarioAsync(Usuario usuario)
        {
            return await AddAsync(usuario); 
        }

        public async Task<bool> UpdateUsuarioAsync(Usuario usuario)
        {
            return await UpdateAsync(usuario); 
        }

        public async Task<bool> DeleteUsuarioAsync(int id)
        {
            return await DeleteAsync(id);
        }
        public async Task<Usuario> GetByCorreoAsync(string correo)
        {
            return await Context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);
        }
    }
}
