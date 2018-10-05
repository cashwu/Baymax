using System;
using System.IO;
using ExpectedObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Baymax.Tester.UnitTest
{
    public class FileStreamResultAssertions<TController>
    {
        private readonly FileStreamResult _fileStreamResult;

        public FileStreamResultAssertions(FileStreamResult fileStreamResult)
        {
            this._fileStreamResult = fileStreamResult;
        }
        
        public FileStreamResultAssertions<TController> WithContentType(string expectedContentType)
        {
            this._fileStreamResult.ContentType.Should().Be(expectedContentType);

            return this;
        }
        
        public FileStreamResultAssertions<TController> WithFileContents(Stream expectedStream)
        {
            expectedStream.ToExpectedObject().ShouldEqual(_fileStreamResult.FileStream);

            return this;
        }
        
        public FileStreamResultAssertions<TController> WithFileDownloadName(string expectedFileDownloadName)
        {
            _fileStreamResult.FileDownloadName.Should().Be(expectedFileDownloadName);

            return this;
        }
        
        public FileStreamResultAssertions<TController> WithLastModified(DateTimeOffset expectedLastModified)
        {
            _fileStreamResult.LastModified.Value.Should().Be(expectedLastModified);

            return this;
        }
    }
}
