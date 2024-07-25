using FluentResults;

namespace Priemka.Application.Validator
{
    public interface IValidator<T>
    {
        Task<Result> Validate(T request, CancellationToken cancellationToken);
    }
}
