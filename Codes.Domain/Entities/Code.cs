using System;
using Codes.Domain.Enumerations;

namespace Codes.Domain.Entities
{
    public class Code
    {
        public Guid Id { get; private set; }
        
        public CodeType CodeType { get; set; }
        
        public string Value { get; set; }
    }
}