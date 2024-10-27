using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebUI.Binders.BooleanConcept
{
    public class BooleanBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var modelName = bindingContext.ModelName;

            var valueProviderResult =
                bindingContext.ValueProvider.GetValue(modelName);

            if (valueProviderResult == ValueProviderResult.None)
                return Task.CompletedTask;

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            switch (valueProviderResult.FirstValue)
            {
                case "on":
                case "true":
                    bindingContext.Result = ModelBindingResult.Success(true);
                    break;
                default:
                    bindingContext.Result = ModelBindingResult.Success(false);
                    break;
            }
            return Task.CompletedTask;
        }
    }
}