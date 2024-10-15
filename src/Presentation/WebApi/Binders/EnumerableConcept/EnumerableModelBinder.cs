using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApi.Binders.EnumerableConcept
{
    public class EnumerableModelBinder<T> : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult == ValueProviderResult.None)
                return Task.CompletedTask;

            var values = valueProviderResult.FirstValue?.Split(",", StringSplitOptions.RemoveEmptyEntries);
            if (values == null || values.Length == 0)
            {
                bindingContext.Result = ModelBindingResult.Success(Array.Empty<T>());
                return Task.CompletedTask;
            }

            try
            {
                var convertedValues = values.Select(value => (T)Convert.ChangeType(value, typeof(T))).ToList();
                bindingContext.Result = ModelBindingResult.Success(convertedValues);
            }
            catch
            {
                bindingContext.Result = ModelBindingResult.Failed();
            }

            return Task.CompletedTask;
        }
    }
}