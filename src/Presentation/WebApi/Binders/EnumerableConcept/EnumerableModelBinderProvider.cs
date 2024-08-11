using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApi.Binders.EnumerableConcept
{
    public class EnumerableModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (context.Metadata.IsEnumerableType
                && context.Metadata.ElementType?.IsEnum != true
                && context.Metadata.ElementType != typeof(IFormFile))
                return new BinderTypeModelBinder(typeof(EnumerableModelBinder<>).MakeGenericType(context.Metadata.ElementType!));

            return null;
        }
    }
}
