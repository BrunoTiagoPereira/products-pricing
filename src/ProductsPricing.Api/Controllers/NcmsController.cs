using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductsPricing.Api.ApiResponses;
using ProductsPricing.Api.NcmImport;
using ProductsPricing.Core.Transaction;
using ProductsPricing.Domain.Contracts.Repositories;
using ProductsPricing.Domain.Ncms.Entities;
using System.Text.Json;

namespace ProductsPricing.Api.Controllers
{
    [ApiController]
    [Route("api/ncms")]
    public class NcmsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly INcmRepository _ncmRepository;
        private readonly IUnitOfWork _uow;

        public NcmsController(IMediator mediator, INcmRepository ncmRepository, IUnitOfWork uow)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _ncmRepository = ncmRepository ?? throw new ArgumentNullException(nameof(ncmRepository));
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        [HttpPost]
        [Route("import-file")]
        public async Task<ApiResponse> Ncm()
        {
            var ncmPath = @"C:\Users\bruno\Downloads\Tabela_NCM_20220901.json";
            var ncmFileData = System.IO.File.ReadAllText(ncmPath);
            var ncmData = JsonSerializer.Deserialize<NCM>(ncmFileData);

            var ncmCodes = ncmData.Nomenclaturas
                .Select(x => new
                {
                    Codigo = x.Codigo.Replace(".", "").Trim(),
                    Descricao = x.Descricao.Replace("-", "").Replace("<i>", "").Replace("</i>", "").Replace(":", "").Replace("<sup>", "").Replace("</sup>", "").Replace("<sub>", "").Replace("</sub>", "").Trim()
                })
                .OrderBy(x => x.Codigo)
                .ToList();

            foreach (var ncm in ncmCodes)
            {
                var n = new Ncm(ncm.Codigo, ncm.Descricao);
                await _ncmRepository.AddAsync(n);
            }

            await _uow.CommitAsync();

            return ApiResponse.Success();
        }
    }
}