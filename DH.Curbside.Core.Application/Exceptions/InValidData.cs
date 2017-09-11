using System;

namespace DH.Curbside.Core.Application.Exceptions
{
    public class InValidData : Exception
    {
        public InValidData(string message)
        : base(message) { }
    }
}
