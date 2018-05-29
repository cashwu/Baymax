using ExpectedObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Baymax
{
    public class JsonResultAssertions<TController> where TController : Controller
    {
        private readonly JsonResult _jsonResult;
        private readonly TController _controller;

        public JsonResultAssertions(JsonResult jsonResult, TController controller)
        {
            this._jsonResult = jsonResult;
            _controller = controller;
        }

        public JsonResultAssertions<TController> WithData<TModel>(TModel data) 
            where TModel : class 
        {
            _jsonResult.Value.Should().BeOfType<TModel>();
            data.ToExpectedObject().ShouldEqual(_jsonResult.Value as TModel);

            return this;
        }

        public JsonResultAssertions<TController> WithAnonymousData(object data)
        {
            _jsonResult.Value.ToObjectString().Should().Be(data.ToObjectString());

            return this;
        }
    }
}