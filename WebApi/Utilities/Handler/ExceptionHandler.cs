﻿namespace WebApi.Utilities.Handler
{
    public class ExceptionHandler : Exception
    {
        public ExceptionHandler(string message): base(message) { } //constructor exception handler
    }
}
