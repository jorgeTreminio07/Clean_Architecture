using Ardalis.Result;
using AutoMapper;
using MediatR;
using MyApp.Application.Commands.Backups;
using MyApp.Application.Dtos.Backups;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.Backups
{
    public class DeleteBackupCommandHandler : IRequestHandler<DeleteBackupCommand, Result<BackupDto>>
    {
        private readonly IAsyncRepository<BackupEntity> _backupRepository;
        private readonly IMapper _mapper;
        private readonly IPostgresBackupService _backupService;

        public DeleteBackupCommandHandler(IAsyncRepository<BackupEntity> backupRepository, IMapper mapper, IPostgresBackupService backupService)
        {
            this._backupRepository = backupRepository;
            this._mapper = mapper;
            this._backupService = backupService;
        }
        public async Task<Result<BackupDto>> Handle(DeleteBackupCommand request, CancellationToken cancellationToken)
        {
            var backup = await _backupRepository.GetByIdAsync(request.Id);
            if(backup == null)
            {
                return Result.NotFound("Backup not exist");
            }

            await _backupRepository.DeleteAsync(backup, cancellationToken);
            var message = _backupService.DeleteBackup(backup.Url);

            var backupDto = _mapper.Map<BackupDto>(backup);
            return Result.Success(backupDto);

        }
    }
}
