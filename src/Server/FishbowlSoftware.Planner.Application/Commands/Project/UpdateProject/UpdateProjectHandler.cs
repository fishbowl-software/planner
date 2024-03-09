using FishbowlSoftware.Planner.Application.Core;
using FishbowlSoftware.Planner.Domain.Entities;
using FishbowlSoftware.Planner.Domain.Persistence;
using FishbowlSoftware.Planner.Shared;

namespace FishbowlSoftware.Planner.Application.Commands;

internal class UpdateProjectHandler : RequestHandler<UpdateProjectCommand, Result>
{
    private readonly IUnitOfWork _uow;

    public UpdateProjectHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }
    
    protected override async Task<Result> HandleValidated(UpdateProjectCommand req, CancellationToken ct)
    {
        var project = await _uow.Repository<Project>().GetByIdAsync(req.Id!, false, "Client", "Survey");
        if (project is null)
        {
            return Result.CreateFailure($"Project with the ID '{req.Id}' does not exist");
        }

        UpdateProjectDetails(req, project);

        var clientResult = await UpdateClientIfNecessary(req, project);
        if (!clientResult.IsSuccess)
        {
            return clientResult;
        }

        _uow.Repository<Project>().Update(project);
        await _uow.SaveChangesAsync(ct);
        return Result.CreateSuccess();
    }

    private void UpdateProjectDetails(UpdateProjectCommand req, Project project)
    {
        if (!string.IsNullOrEmpty(req.Name) && req.Name != project.Name)
        {
            project.Name = req.Name;
        }

        if (req.Description != project.Description)
        {
            project.Description = req.Description;
        }
    }

    private async Task<Result> UpdateClientIfNecessary(UpdateProjectCommand req, Project project)
    {
        if (!string.IsNullOrEmpty(req.ClientId) && req.ClientId != project.ClientId)
        {
            var client = await _uow.Repository<Client>().GetByIdAsync(req.ClientId);
            if (client is null)
            {
                return Result.CreateFailure($"Client with the ID '{req.ClientId}' does not exist");
            }
            project.Client = client;
        }

        return Result.CreateSuccess();
    }
}
