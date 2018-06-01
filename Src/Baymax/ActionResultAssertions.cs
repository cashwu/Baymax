using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Baymax
{
    public class ActionResultAssertions<TController> where TController : Controller
    {
        private readonly IActionResult _actionResult;
        private readonly TController _controller;

        public ActionResultAssertions(IActionResult actionResult, TController controller)
        {
            _actionResult = actionResult;
            _controller = controller;
        }

        public RedirectToActionResultAssertions<TController> ShouldBeRedirectToActionResult()
        {
            _actionResult.Should().BeOfType<RedirectToActionResult>();

            return new RedirectToActionResultAssertions<TController>(_actionResult as RedirectToActionResult, _controller);
        }

        public JsonResultAssertions<TController> ShouldBeJsonResult()
        {
            _actionResult.Should().BeOfType<JsonResult>();

            return new JsonResultAssertions<TController>(_actionResult as JsonResult, _controller);
        }
        
        public ViewResultAssertions<TController> ShouldBeViewResult()
        {
            _actionResult.Should().BeOfType<ViewResult>();

            return new ViewResultAssertions<TController>(_actionResult as ViewResult, _controller);
        }

        public ContentResultAssertions<TController> ShouldBeContentResult()
        {
            _actionResult.Should().BeOfType<ContentResult>();

            return new ContentResultAssertions<TController>(_actionResult as ContentResult, _controller);
        }

        public FileContentResultAssertions<TController> ShouldBeFileContentResult()
        {
            _actionResult.Should().BeOfType<ContentResult>();

            return new FileContentResultAssertions<TController>(_actionResult as FileContentResult, _controller);
        }

        public FileStreamResultAssertions<TController> ShouldBeFileStreamResult()
        {
            _actionResult.Should().BeOfType<FileStreamResult>();

            return new FileStreamResultAssertions<TController>(_actionResult as FileStreamResult, _controller);
        }

        public EmptyResultAssertions<TController> ShouldBeEmptyResult()
        {
            _actionResult.Should().BeOfType<EmptyResult>();

            return new EmptyResultAssertions<TController>(_actionResult as EmptyResult, _controller);
        }

        public PartialViewResultAssertions<TController> ShouldBePartialViewResult()
        {
            _actionResult.Should().BeOfType<PartialViewResult>();

            return new PartialViewResultAssertions<TController>(_actionResult as PartialViewResult, _controller);
        }

        public RedirectResultAssertions<TController> ShouldBeRedirectResult()
        {
            _actionResult.Should().BeOfType<RedirectResult>();

            return new RedirectResultAssertions<TController>(_actionResult as RedirectResult, _controller);
        }

        public ForbidResultAssertions<TController> ShouldBeForbidResult()
        {
            _actionResult.Should().BeOfType<ForbidResult>();
            
           return new ForbidResultAssertions<TController>(_actionResult as ForbidResult, _controller); 
        }
        
        public LocalRedirectResultAssertions<TController> ShouldBeLocalRedirectResult()
        {
            _actionResult.Should().BeOfType<LocalRedirectResult>();
            
           return new LocalRedirectResultAssertions<TController>(_actionResult as LocalRedirectResult, _controller); 
        }

        public RedirectToRouteResultAssertions<TController> ShouldBeRedirectToRouteResult()
        {
            _actionResult.Should().BeOfType<RedirectToRouteResult>();

            return new RedirectToRouteResultAssertions<TController>(_actionResult as RedirectToRouteResult, _controller);
        }
    }
}