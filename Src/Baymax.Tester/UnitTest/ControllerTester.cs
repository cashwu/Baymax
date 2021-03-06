﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Baymax.Tester.UnitTest
{
    public class ControllerTester<TController> where TController : Controller
    {
        private readonly TController _controller;

        internal ControllerTester(TController controller)
        {
            _controller = controller;
        }

        public ActionResultAssertions<TController> Action(Func<TController, IActionResult> func)
        {
            return new ActionResultAssertions<TController>(func?.Invoke(_controller), _controller);
        }

        public ActionResultAssertions<TController> Action(Func<TController, Task<IActionResult>> func)
        {
            var result = func?.Invoke(_controller).GetAwaiter().GetResult();

            return new ActionResultAssertions<TController>(result, _controller);
        }
    }
}