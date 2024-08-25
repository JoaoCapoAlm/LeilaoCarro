using FluentValidation;
using LeilaoCarro.Exceptions;

namespace LeilaoCarro.Helpers
{
    public static class FluentValidationExtension
    {
        public static void ValidateAndThrowApp<T>(this IValidator<T> validator, T instance, string message)
        {
            var result = validator.Validate(instance);

            if (!result.IsValid)
                throw new AppException(message, result.Errors);
        }

        public static async Task ValidateAndThrowAppAsync<T>(this IValidator<T> validator, T instance, string errorMessage)
        {
            var validation = await validator.ValidateAsync(instance);

            if (!validation.IsValid)
                throw new AppException(errorMessage, validation.Errors);
        }
    }
}
