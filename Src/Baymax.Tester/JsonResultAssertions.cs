using ExpectedObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Baymax.Tester
{
    public class JsonResultAssertions
    {
        private readonly JsonResult _jsonResult;

        public JsonResultAssertions(JsonResult jsonResult)
        {
            _jsonResult = jsonResult;
        }

        public JsonResultAssertions WithData<TModel>(TModel data) 
            where TModel : class 
        {
            _jsonResult.Value.Should().BeOfType<TModel>();
            data.ToExpectedObject().ShouldEqual(_jsonResult.Value as TModel);

            return this;
        }

        public JsonResultAssertions WithAnonymousData(object data)
        {
            _jsonResult.Value.ToObjectString().Should().Be(data.ToObjectString());

            return this;
        }
    }
}