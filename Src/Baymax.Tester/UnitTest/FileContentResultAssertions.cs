using System;
using ExpectedObjects;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Baymax.Tester.UnitTest
{
    public class FileContentResultAssertions<TController>
    {
        private readonly FileContentResult _fileContentResult;

        public FileContentResultAssertions(FileContentResult fileContentResult)
        {
            this._fileContentResult = fileContentResult;
        }

        public FileContentResultAssertions<TController> WithContentType(string expectedContentType)
        {
            this._fileContentResult.ContentType.Should().Be(expectedContentType);

            return this;
        }
        
        public FileContentResultAssertions<TController> WithFileContents(byte[] expectedFileContents)
        {
            expectedFileContents.ToExpectedObject().ShouldEqual(_fileContentResult.FileContents);

            return this;
        }
        
        public FileContentResultAssertions<TController> WithFileDownloadName(string expectedFileDownloadName)
        {
            _fileContentResult.FileDownloadName.Should().Be(expectedFileDownloadName);

            return this;
        }
        
        public FileContentResultAssertions<TController> WithLastModified(DateTimeOffset expectedLastModified)
        {
            _fileContentResult.LastModified.Value.Should().Be(expectedLastModified);

            return this;
        }
    }
}
