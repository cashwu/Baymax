﻿using FluentAssertions;
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
    }
}