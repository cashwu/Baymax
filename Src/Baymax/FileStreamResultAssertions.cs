using System;
using System.IO;
using ExpectedObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Baymax
{
    public class FileStreamResultAssertions<TController>
    {
        private readonly FileStreamResult _fileStreamResult;
        private readonly TController _controller;

        public FileStreamResultAssertions(FileStreamResult fileStreamResult, TController controller)
        {
            this._fileStreamResult = fileStreamResult;
            this._controller = controller;
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