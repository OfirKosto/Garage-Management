using System;

namespace GarageManger.ConsoleUI
{
    class ReturnToMenuException : Exception
    {
        public override string Message => base.Message;
    }
}
