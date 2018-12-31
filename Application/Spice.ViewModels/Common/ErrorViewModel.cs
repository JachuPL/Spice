using System;

namespace Spice.ViewModels.Common
{
    public class ErrorViewModel
    {
        public string Error { get; }

        public ErrorViewModel(string message)
        {
            Error = message;
        }

        public ErrorViewModel(Exception exception)
        {
            Error = exception.Message;
        }
    }
}