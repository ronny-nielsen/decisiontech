using System.Collections.Generic;
using System.Linq;

namespace DecisionTech.Cart.Dtos
{
    public class CommandResult<T> where T : class
    {
        public List<string> Errors = new List<string>();

        public bool Success => !Errors.Any();

        public T Model { get; set; }
    }
}