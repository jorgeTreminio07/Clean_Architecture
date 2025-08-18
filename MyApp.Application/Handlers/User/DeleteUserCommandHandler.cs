using Ardalis.Result;
using MediatR;
using MyApp.Application.Commands.User;
using MyApp.Application.Interface.Archivos;
using MyApp.Application.Specifications.User;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application.Handlers.User
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<Guid>>
    {
        private readonly IAsyncRepository<UserEntity> _userRepository;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private const string contenedor = "UserAvatar";

        public DeleteUserCommandHandler(IAsyncRepository<UserEntity> userRepository, IAlmacenadorArchivos almacenadorArchivos)
        {
            this._userRepository = userRepository;
            this.almacenadorArchivos = almacenadorArchivos;
        }
        public async Task<Result<Guid>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FirstOrDefaultAsync(new GetUserByIdSpecification(request.Id), cancellationToken);
            if(user == null)
            {
                return Result.NotFound("User not exist");
            }

            user.IsDelete = true;
            await _userRepository.UpdateAsync(user, cancellationToken);

            await almacenadorArchivos.Borrar(user.Avatar, contenedor);

            return Result.Success(user.Id);
        }
    }
}
