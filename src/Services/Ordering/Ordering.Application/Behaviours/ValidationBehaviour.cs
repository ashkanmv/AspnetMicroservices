using FluentValidation;
using MediatR;
using Ordering.Application.Exceptions;
using ValidationException = Ordering.Application.Exceptions.ValidationException;

namespace Ordering.Application.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResponse =
                    await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failed = validationResponse.SelectMany(v => v.Errors).Where(v => v != null).ToList();

                if (failed.Any())
                {
                    throw new ValidationException(failed);
                }
            }

            return await next();
        }
    }
}
