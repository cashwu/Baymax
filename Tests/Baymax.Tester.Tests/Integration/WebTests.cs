using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Baymax.Tester.Integration;
using Baymax.Tester.Web;
using Baymax.Tester.Web.Controllers;
using ExpectedObjects;
using FluentAssertions;
using Xunit;

namespace Baymax.Tester.Tests.Integration
{
    public class WebTests : TestBase, IDisposable
    {
        public WebTests(ApplicationFactory<Startup, AppDbContext> factory) : base(factory)
        {
        }

        [Fact]
        public void GetAll()
        {
            Factory.DbOperator(db =>
            {
                db.Info.Add(new Info { Id = 1, Name = "Test123" });
                db.Info.Add(new Info { Id = 2, Name = "Test456" });
                db.SaveChanges();
            });
            
            var result = Factory.HttpClient.GetHttpResult<List<Info>>("/api/values");

            new List<Info>
                    {
                        new Info { Id = 1, Name = "Test123" },
                        new Info { Id = 2, Name = "Test456" }
                    }.ToExpectedObject()
                     .ShouldEqual(result);
        }

        [Fact]
        public void Post_Data()
        {
            var data = new Info { Id = 99, Name = "Cash" };
            var result = Factory.HttpClient.PostHttpResult<PostRespDto, Info>("/api/post", data);

            new PostRespDto { Id = 99 }.ToExpectedObject().ShouldEqual(result);

            Factory.DbOperator(db =>
            {
                var info = db.Info.FirstOrDefault(a => a.Id == 99);

                new Info { Id = 99, Name = "Cash" }.ToExpectedObject()
                                                   .ShouldEqual(info);
            });
        }

        [Fact]
        public void Post_NotData()
        {
            var result = Factory.HttpClient.PostHttpResult<PostRespDto>("/api/post1");

            new PostRespDto { Id = 999 }.ToExpectedObject().ShouldEqual(result);
        }
        
        [Fact]
        public void Put()
        {
            Factory.DbOperator(db =>
            {
                db.Info.Add(new Info { Id = 66, Name = "666" });
                db.SaveChanges();
            });

            var putData = new Info { Id = 66, Name = "Cash"};
            var result = Factory.HttpClient.PutHttpResult<PutRespDto, Info>($"/api/put/{putData.Id}", putData);

            new PutRespDto { Id = putData.Id }.ToExpectedObject().ShouldEqual(result);

            Factory.DbOperator(db =>
            {
                var info = db.Info.FirstOrDefault(a => a.Id == putData.Id);
                putData.ToExpectedObject().ShouldEqual(info);
            });
        }
        
        [Fact]
        public void Delete_NoContent()
        {
            var info = new Info { Id = 88, Name = "Cash" };
            Factory.DbOperator(db =>
            {
                db.Info.Add(info);
                db.SaveChanges();
            });
            
            var result = Factory.HttpClient.DeleteHttpResult($"/api/delete/{info.Id}");

            result.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
        
        [Fact]
        public void Delete_Result()
        {
            var info = new Info { Id = 77, Name = "Cash" };
            Factory.DbOperator(db =>
            {
                db.Info.Add(info);
                db.SaveChanges();
            });
            
            var result = Factory.HttpClient.DeleteHttpResult<DeleteRespDto>($"/api/delete1/{info.Id}");

            new DeleteRespDto { Id = 77 }.ToExpectedObject().ShouldEqual(result);
        }
        
        public void Dispose()
        {
            Clear();
        }
        
        private void Clear()
        {
            Factory.DbOperator(db =>
            {
                db.Info.RemoveRange(db.Info);
                db.SaveChanges();
            });
        }
    }
}