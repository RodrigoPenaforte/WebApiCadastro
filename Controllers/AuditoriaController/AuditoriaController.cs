using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApiCadastro.Models.Auditorias;
using WebApiCadastro.Services.Auditorias;

namespace WebApiCadastro.Controllers.AuditoriaController
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AuditoriaController : ControllerBase
    {
        private readonly IAuditoriaService _auditoriaService;
        public AuditoriaController(IAuditoriaService auditoriaService)
        {
            _auditoriaService = auditoriaService;
        }

        [HttpGet]
        public async Task<ActionResult> BuscarTodasAuditorias()
        {
            var auditoria = await _auditoriaService.BuscarAuditoria();

            return Ok(auditoria);
        }
    }
}