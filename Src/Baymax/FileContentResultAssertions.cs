using Microsoft.AspNetCore.Mvc;

namespace Baymax
{
    public class FileContentResultAssertions<TController>
    {
        private readonly FileContentResult _fileContentResult;
        private readonly TController _controller;

        public FileContentResultAssertions(FileContentResult fileContentResult, TController controller)
        {
            this._fileContentResult = fileContentResult;
            this._controller = controller;
        }
    }
}