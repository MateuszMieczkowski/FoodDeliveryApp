namespace UI.API.Requests;

public record RegisterRequest(string FirstName, string LastName, string Email, string Password, string RoleName = "user", int? ManagedRestaurantId = null);