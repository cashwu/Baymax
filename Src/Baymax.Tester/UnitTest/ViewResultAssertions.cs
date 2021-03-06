﻿using ExpectedObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Baymax.Tester.UnitTest
{
    public class ViewResultAssertions<TController> where TController : Controller
    {
        private readonly ViewResult _viewResult;

        public ViewResultAssertions(ViewResult viewResult)
        {
            _viewResult = viewResult;
        }

        public ViewResultAssertions<TController> WithModel<T>(T expected)
        {
            _viewResult.Model.Should().BeOfType<T>();

            expected.ToExpectedObject().ShouldEqual(_viewResult.Model);

            return this;
        }

        public ViewResultAssertions<TController> WithViewName(string viewName)
        {
            _viewResult.ViewName.Should().Be(viewName);

            return this;
        }

        public ViewResultAssertions<TController> WithViewBag(string key, object expectedValue)
        {
            return WithViewData(key, expectedValue);
        }

        public ViewResultAssertions<TController> WithViewData(string key, object expectedValue)
        {
            expectedValue.ToExpectedObject().ShouldEqual(_viewResult.ViewData[key]);

            return this;
        }
        
        public ViewResultAssertions<TController> WithNotTempData()
        {
            _viewResult.TempData.Should().BeNull();

            return this;
        }

        public ViewResultAssertions<TController> WithTempData(string key, object expectedValue)
        {
            expectedValue.ToExpectedObject().ShouldEqual(_viewResult.TempData[key]);

            return this;
        }
    }
}
