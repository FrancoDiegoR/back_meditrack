using Microsoft.EntityFrameworkCore;
using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Application.CommandServices;
using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Domain.Model.Aggregates;
using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Domain.Model.Commands;
using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Domain.Model.Errors;
using TechnoByteLambders.MediTrackSensor.Platform.Establishments.Domain.Repositories;
using TechnoByteLambders.MediTrackSensor.Platform.Shared.Application.Patterns;
using TechnoByteLambders.MediTrackSensor.Platform.Shared.Domain.Repositories;

namespace TechnoByteLambders.MediTrackSensor.Platform.Establishments.Application.Internal.CommandServices;

public class OperatorCommandService(
    IOperatorRepository operatorRepository,
    IEstablishmentRepository establishmentRepository,
    IUnitOfWork unitOfWork) : IOperatorCommandService
{
    public async Task<Result<Operator, string>> Handle(CreateOperatorCommand command, CancellationToken cancellationToken = default)
    {
        var establishment = await establishmentRepository.FindByIdAsync(command.EstablishmentId, cancellationToken);
        if (establishment is null)
            return new Result<Operator, string>.Failure(EstablishmentsErrors.EstablishmentNotFound.Description);

        var op = new Operator(0, command.Schedule, command.EstablishmentId,
            new Domain.Model.ValueObjects.UserId(command.UserId));
        try
        {
            await operatorRepository.AddAsync(op, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<Operator, string>.Success(op);
        }
        catch (OperationCanceledException) { return new Result<Operator, string>.Failure(EstablishmentsErrors.OperatorCreationFailed.Description); }
        catch (DbUpdateException) { return new Result<Operator, string>.Failure(EstablishmentsErrors.OperatorCreationFailed.Description); }
        catch (Exception) { return new Result<Operator, string>.Failure(EstablishmentsErrors.OperatorCreationFailed.Description); }
    }

    public async Task<Result<Operator, string>> Handle(UpdateOperatorCommand command, CancellationToken cancellationToken = default)
    {
        var op = await operatorRepository.FindByIdAsync(command.Id, cancellationToken);
        if (op is null)
            return new Result<Operator, string>.Failure(EstablishmentsErrors.OperatorNotFound.Description);

        op.UpdateSchedule(command.Schedule);
        try
        {
            operatorRepository.Update(op);
            await unitOfWork.CompleteAsync(cancellationToken);
            return new Result<Operator, string>.Success(op);
        }
        catch (OperationCanceledException) { return new Result<Operator, string>.Failure(EstablishmentsErrors.OperatorCreationFailed.Description); }
        catch (DbUpdateException) { return new Result<Operator, string>.Failure(EstablishmentsErrors.OperatorCreationFailed.Description); }
        catch (Exception) { return new Result<Operator, string>.Failure(EstablishmentsErrors.OperatorCreationFailed.Description); }
    }

    public async Task<Result<bool, string>> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var op = await operatorRepository.FindByIdAsync(id, cancellationToken);
        if (op is null)
            return new Result<bool, string>.Failure(EstablishmentsErrors.OperatorNotFound.Description);

        operatorRepository.Remove(op);
        await unitOfWork.CompleteAsync(cancellationToken);
        return new Result<bool, string>.Success(true);
    }
}
