using Ardalis.Result;
using AutoMapper;
using MediatR;
using MyApp.Application.Commands.Backups;
using MyApp.Application.Dtos.Backups;
using MyApp.Application.Specifications.Backups;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.Backups
{
    public class RestoreBackupCommandHandler : IRequestHandler<RestoreBackupCommand, Result<BackupDto>>
    {
        private readonly IAsyncRepository<BackupEntity> _backupRepository;
        private readonly IMapper _mapper;
        private readonly IPostgresBackupService _backupService;
        private const string contenedor = "backups";
        public RestoreBackupCommandHandler(IAsyncRepository<BackupEntity> backupRepository, IMapper mapper, IPostgresBackupService backupService)
        {
            this._backupRepository = backupRepository;
            this._mapper = mapper;
            this._backupService = backupService;
        }
        public async  Task<Result<BackupDto>> Handle(RestoreBackupCommand request, CancellationToken cancellationToken)
        {

            var backup = await _backupRepository.FirstOrDefaultAsync(new GetBackupByIdSpecification(request.Id) , cancellationToken);
            if (backup == null)
            {
                return Result.NotFound("Backup not exist");
            }

            try
            {
                var url = await _backupService.RestaurarBackupAsync(backup.Url);
                backup.Url = url;
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }

            var backupDto = _mapper.Map<BackupDto>(backup);
            

            return Result<BackupDto>.Success(backupDto);

        }
    }
}
