﻿using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Baymax.Tester.UnitTest
{
    public class RedirectResultAssertions<TController> where TController : Controller
    {
        private readonly RedirectResult _redirectResult;

        public RedirectResultAssertions(RedirectResult redirectResult)
        {
            _redirectResult = redirectResult;
        }
        
        public RedirectResultAssertions<TController> WithUrl(string expectedUrl)
        {
            _redirectResult.Url.Should().Be(expectedUrl);

            return this;
        }
        
        public RedirectResultAssertions<TController> WithPermanent(bool expectedPermanent)
        {
            _redirectResult.Permanent.Should().Be(expectedPermanent);

            return this;
        }
        
        
        public RedirectResultAssertions<TController> WithPreserveMethod(bool expectedPreserveMethod)
        {
            _redirectResult.PreserveMethod.Should().Be(expectedPreserveMethod);

            return this;
        }
    }
}
