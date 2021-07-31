namespace API.Utils
{
    using BasicWrapperTool;
    using Microsoft.AspNetCore.Mvc;

    public abstract class BaseController : ControllerBase
    {
        protected IActionResult Error(string errorMessage)
        {
            return BadRequest(Envelope.Error(errorMessage));
        }

        protected IActionResult FromResult(Result result)
        {
            return result.IsSuccess
                ? Ok()
                : Error(result.Messages.ToString());
        }

        protected IActionResult FromResult<T>(Result<T> result)
        {
            return result.IsSuccess
                ? this.Ok<T>(result.Value)
                : this.Error(result.Messages.ToString());
        }

        protected new IActionResult Ok()
        {
            return base.Ok(Envelope.Ok());
        }

        protected IActionResult Ok<T>(T result)
        {
            return base.Ok(Envelope.Ok(result));
        }
    }
}