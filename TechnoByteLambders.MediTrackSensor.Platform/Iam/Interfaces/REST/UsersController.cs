using Microsoft.AspNetCore.Mvc;
using TechnoByteLambders.MediTrackSensor.Platform.Iam.Application.CommandServices;
using TechnoByteLambders.MediTrackSensor.Platform.Iam.Application.QueryServices;
using TechnoByteLambders.MediTrackSensor.Platform.Iam.Domain.Model.Aggregates;
using TechnoByteLambders.MediTrackSensor.Platform.Iam.Domain.Model.Commands;
using TechnoByteLambders.MediTrackSensor.Platform.Iam.Domain.Model.Queries;
using TechnoByteLambders.MediTrackSensor.Platform.Iam.Interfaces.REST.Resources;
using TechnoByteLambders.MediTrackSensor.Platform.Iam.Interfaces.REST.Transform;
using TechnoByteLambders.MediTrackSensor.Platform.Shared.Application.Patterns;

namespace TechnoByteLambders.MediTrackSensor.Platform.Iam.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
public class UsersController(IUserCommandService userCommandService, IUserQueryService userQueryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var users = await userQueryService.Handle(new GetAllUsersQuery(), ct);
        return Ok(users.Select(UserResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var user = await userQueryService.Handle(new GetUserByIdQuery(id), ct);
        if (user is null) return NotFound();
        return Ok(UserResourceFromEntityAssembler.ToResourceFromEntity(user));
    }

    [HttpPost]
    public async Task<IActionResult> SignUp([FromBody] SignUpResource resource, CancellationToken ct)
    {
        var command = SignUpCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await userCommandService.Handle(command, ct);
        if (result is Result<User, string>.Failure f)
            return BadRequest(new { error = f.Error });
        var success = (Result<User, string>.Success)result;
        return CreatedAtAction(nameof(GetById), new { id = success.Value.Id },
            UserResourceFromEntityAssembler.ToResourceFromEntity(success.Value));
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] SignInResource resource, CancellationToken ct)
    {
        var result = await userCommandService.Handle(new SignInCommand(resource.Email, resource.Password), ct);
        if (result is Result<(User User, string Token), string>.Failure failure)
            return Unauthorized(new { error = failure.Error });
        var success = (Result<(User User, string Token), string>.Success)result;
        var (user, token) = success.Value;
        return Ok(new AuthResource(UserResourceFromEntityAssembler.ToResourceFromEntity(user), token));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserResource resource, CancellationToken ct)
    {
        var result = await userCommandService.Handle(
            new UpdateUserCommand(id, resource.Name, resource.Phone, resource.JobTitle, resource.Photo), ct);
        if (result is Result<User, string>.Failure f)
            return BadRequest(new { error = f.Error });
        var success = (Result<User, string>.Success)result;
        return Ok(UserResourceFromEntityAssembler.ToResourceFromEntity(success.Value));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var result = await userCommandService.DeleteAsync(id, ct);
        if (result is Result<bool, string>.Failure f)
            return NotFound(new { error = f.Error });
        return NoContent();
    }
}
