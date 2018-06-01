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
    }
}