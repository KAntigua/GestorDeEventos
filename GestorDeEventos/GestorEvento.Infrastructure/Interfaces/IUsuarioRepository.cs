using GestorEvento.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorEvento.Infrastructure.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> GetByIdAsync(int id);
        Task<Usuario> GetByCorreoAsync(string correo);  
        Task<List<Usuario>> GetAllAsync();
        Task<int> AddUsuarioAsync(Usuario usuario);
        Task<bool> UpdateUsuarioAsync(Usuario usuario);
        Task<bool> DeleteUsuarioAsync(int id);
    }

}
