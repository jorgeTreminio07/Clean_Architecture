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
    public class AddBackupCommandHandler : IRequestHandler<AddBackupCommand, Result<BackupDto>>
    {
        private readonly IAsyncRepository<BackupEntity> _backupRepository;
        private readonly IMapper _mapper;
        private readonly IPostgresBackupService _backupService;
        private const string contenedor = "backups";

        public AddBackupCommandHandler(IAsyncRepository<BackupEntity> backupRepository, IMapper mapper, IPostgresBackupService backupService)
        {
            this._backupRepository = backupRepository;
            this._mapper = mapper;
            this._backupService = backupService;
        }
        public async Task<Result<BackupDto>> Handle(AddBackupCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // 1. Generar el backup y obtener la URL
                var url = await _backupService.GenerarBackupAsync(contenedor);

                // 2. Crear entidad
                var backup = new BackupEntity
                {
                    Name = $"Backup_{DateTime.Now:yyyyMMdd_HHmmss}",
                    Url = url
                };
                
                // 3. Guardar en base de datos
                await _backupRepository.AddAsync(backup, cancellationToken);

                // 4. Mapear a DTO
                var backupDto = _mapper.Map<BackupDto>(backup);

                // 5. Devolver resultado exitoso
                return Result.Success(backupDto);
            }
            catch (Exception ex)
            {
                // En caso de error, retornar fallo
                return Result.Error($"Error al generar el backup: {ex.Message}");
            }
        }
    }
}
