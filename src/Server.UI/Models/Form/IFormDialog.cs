using CleanArchitecture.Blazor.Application.Common.Interfaces.Caching;

namespace CleanArchitecture.Blazor.Server.UI.Models.Form;

public interface IFormDialog<TCommand> where TCommand : ICacheInvalidatorRequest<Result<int>>
{
    Action? Refresh { get; }
    TCommand Model { get; set; }
}
