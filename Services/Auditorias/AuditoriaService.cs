using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiCadastro.Data;
using WebApiCadastro.Models.Auditorias;

namespace WebApiCadastro.Services.Auditorias
{
    public class AuditoriaService : IAuditoriaService
    {
        private readonly AppDbContext _context;

        public AuditoriaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Auditoria>> BuscarAuditoria()
        {
            var auditoria = await _context.Auditorias.OrderByDescending(d => d.Data ).ToListAsync();
            return auditoria;
        }

        public async Task RegistrarAuditoriaAsync(string acao, string usuarioId, string dadosAlterados)
        {
            var auditoria = new Auditoria
            {
                Acao = acao,
                UsuarioId = usuarioId,
                DadosAlterados = dadosAlterados
            };

            _context.Auditorias.Add(auditoria);
            await _context.SaveChangesAsync();
        }
    }
}