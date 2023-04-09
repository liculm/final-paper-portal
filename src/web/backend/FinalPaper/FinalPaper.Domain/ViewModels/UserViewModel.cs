using FinalPaper.Domain.Entities;

namespace FinalPaper.Domain.ViewModels;

public sealed record UserViewModel(User User, string JwtToken);