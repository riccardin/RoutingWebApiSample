using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB.Routing.Api.Helpers
{
    public class UnprocesseableEntityObjectResult : ObjectResult
    {
        //public UnprocesseableEntityObjectResult(object error) : base(error)
        //{
        //    StatusCode = 422;
        //}

        public UnprocesseableEntityObjectResult(ModelStateDictionary modelState) 
            : base(new SerializableError(modelState))
        {

            if (modelState == null) {
                throw new ArgumentNullException(nameof(modelState));
            }

            StatusCode = 422;
        }

    }
}
