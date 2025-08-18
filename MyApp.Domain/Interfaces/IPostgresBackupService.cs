﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Domain.Interfaces
{
    public interface IPostgresBackupService
    {
        Task<string> GenerarBackupAsync(string contenedor);
        Task<string> RestaurarBackupAsync(string rutaRelativaArchivoDump);

        string DeleteBackup(string url);
    }
}
