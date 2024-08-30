using Application.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

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
                && context.Metadata.ElementType != typeof(IFormFile)
                && context.Metadata.ElementType != typeof(ImageItem)
                && context.Metadata.ElementType != typeof(List<ImageItem>))
                return new BinderTypeModelBinder(typeof(EnumerableModelBinder<>).MakeGenericType(context.Metadata.ElementType!));

            return null;
        }
    }
}
