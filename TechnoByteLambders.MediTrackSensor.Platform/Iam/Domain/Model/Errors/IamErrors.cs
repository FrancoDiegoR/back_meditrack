using TechnoByteLambders.MediTrackSensor.Platform.Shared.Domain.Model;

namespace TechnoByteLambders.MediTrackSensor.Platform.Iam.Domain.Model.Errors;

public static class IamErrors
{
    public static readonly Error InvalidCredentials = new("Iam.InvalidCredentials", "Invalid email or password.");
    public static readonly Error EmailAlreadyTaken = new("Iam.EmailAlreadyTaken", "The specified email is already taken.");
    public static readonly Error UserNotFound = new("Iam.UserNotFound", "User not found.");
    public static readonly Error AdminNotFound = new("Iam.AdminNotFound", "Admin not found.");
    public static readonly Error UserCreationFailed = new("Iam.UserCreationFailed", "An error occurred while creating the user.");
    public static readonly Error AdminCreationFailed = new("Iam.AdminCreationFailed", "An error occurred while creating the admin.");
}
