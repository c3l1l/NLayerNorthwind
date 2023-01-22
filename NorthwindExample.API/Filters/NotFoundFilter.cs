using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NorthwindExample.Core.DTOs;
using NorthwindExample.Core.Models;
using NorthwindExample.Core.Services;

namespace NorthwindExample.API.Filters
{
    public class NotFoundFilter<T>:IAsyncActionFilter where T: BaseEntity
    {
        private readonly IService<T> _service;

        public NotFoundFilter(IService<T> service) // ONEMLI !! Eger bir filtera Constructorda servis yada DI kullanirska bunu ServiceFilter uzerinden kullanmaliyiz. ve Program.cs'ye eklemeliyiz.        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            var idValue = context.ActionArguments.Values.FirstOrDefault(); //GetById(int id) metodundaki id parametresini almak icin kullandik....

            if (idValue == null)
            {
                await next.Invoke();
                return;
            }
            var id = (int)idValue;
            var anyEntity = await _service.GetByIdAsync(id);
           // var anyEntity = await _service.AnyAsync(x=>x.Id==id);

            if (anyEntity!=null)
            {
                await next.Invoke();
                return;
            }
            context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(404, $"{typeof(T).Name} ({id}) not found"));
        }
    }
}
