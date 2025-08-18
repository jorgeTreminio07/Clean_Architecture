using Ardalis.Result;
using AutoMapper;
using MediatR;
using MyApp.Application.Dtos.Backups;
using MyApp.Application.Queries.Backups;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.Backups
{
    public class GetAllBackupsQueryHandler : IRequestHandler<GetAllBackupsQuery, Result<List<BackupDto>>>
    {
        private readonly IAsyncRepository<BackupEntity> _backupRepository;
        private readonly IMapper _mapper;

        public GetAllBackupsQueryHandler(IAsyncRepository<BackupEntity> backupRepository, IMapper mapper)
        {
            this._backupRepository = backupRepository;
            this._mapper = mapper;
        }
        public async Task<Result<List<BackupDto>>> Handle(GetAllBackupsQuery request, CancellationToken cancellationToken)
        {
            var backups = await _backupRepository.ListAsync(cancellationToken);

            if (backups == null)
            {
                return Result.NotFound("No existen backups, realice uno");
            }

            var backupDto = _mapper.Map<List<BackupDto>>(backups);
            return Result.Success(backupDto);
        }
    }
}
