using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Turnos.Context;
using Turnos.Models;

namespace Turnos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuarios>>> GetUsuarios()
        {
            // Llama a un procedimiento almacenado para obtener la lista de usuarios
            var usuarios = await _context.Usuarios.FromSqlRaw("EXEC GetUsuarios").ToListAsync();
            return usuarios;
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuarios>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios
                            .FromSqlRaw("SELECT * FROM Usuarios WHERE ID_USUARIO = {0}", id)
                            .SingleOrDefaultAsync();

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }


        // PUT: api/Usuarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarios(int id, Usuarios usuario)
        {
            // Verifica si el ID proporcionado es el correcto
            if (id != usuario.ID_USUARIO)
            {
                return BadRequest();
            }

            // Llama a un procedimiento almacenado para actualizar un usuario
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
            EXEC ActualizarUsuario 
            {usuario.ID_USUARIO}, 
            {usuario.USUARIO}, 
            {usuario.ESTADO}, 
            {usuario.ID_ROL}, 
            {usuario.ID_AREA}, 
            {usuario.NUMERO}, 
            {usuario.EXTENCION}, 
            {usuario.IdZona}, 
            {usuario.CELULAR}
            ");

            return NoContent();
        }


        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<Usuarios>> PostUsuarios(Usuarios usuario)
        {
            // Llama a un procedimiento almacenado para agregar un nuevo usuario
            await _context.Database.ExecuteSqlInterpolatedAsync($@"
            EXEC AgregarUsuario 
            @UsuarioNombre={usuario.USUARIO}, 
            @Estado={usuario.ESTADO}, 
            @RolId={usuario.ID_ROL}, 
            @AreaId={usuario.ID_AREA}, 
            @Numero={usuario.NUMERO}, 
            @Extension={usuario.EXTENCION}, 
            @ZonaId={usuario.IdZona}, 
            @Celular={usuario.CELULAR}
    ");

            // Recuperar el ID del usuario recién creado
            var createdUser = await _context.Usuarios.OrderByDescending(u => u.ID_USUARIO).FirstOrDefaultAsync();

            // Devolver el usuario creado
            return CreatedAtAction("GetUsuarios", new { id = createdUser.ID_USUARIO }, createdUser);
        }


        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarios(int id)
        {
            // Llama a un procedimiento almacenado para "eliminar" un usuario cambiando su estado
            await _context.Database.ExecuteSqlInterpolatedAsync($"EXEC DeleteUsuarios {id}");

            return NoContent();
        }
    }
}
