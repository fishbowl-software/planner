﻿using FishbowlSoftware.Planner.Application.Core;
using FishbowlSoftware.Planner.Domain.Entities;
using FishbowlSoftware.Planner.Domain.Persistence;
using FishbowlSoftware.Planner.Shared;

namespace FishbowlSoftware.Planner.Application.Commands
{
    internal class UpdateClientHandler : RequestHandler<UpdateClientCommand, Result>
    {
        private readonly IUnitOfWork _uow;

        public UpdateClientHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        protected override async Task<Result> HandleValidated(
            UpdateClientCommand req, CancellationToken ct)
        {
            var user = await _uow.Repository<Client>()
                .GetAsync(i => i.Id == req.Id, false);

            if (user is null)
            {
                return Result.CreateError($"Could not find a user with ID {req.Id}");
            }

            if (!string.IsNullOrEmpty(req.Email))
            {
                user.Email = req.Email;
            }

            if (!string.IsNullOrEmpty(req.PhoneNumber))
            {
                user.PhoneNumber = req.PhoneNumber;
            }

            _uow.Repository<Client>().Update(user);
            await _uow.SaveChangesAsync(ct);
            return Result.CreateSuccess();
        }
    }
}