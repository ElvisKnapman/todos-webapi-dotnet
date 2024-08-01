using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Todos.Api.DTOs
{
    public class TodoUpdateDto
    {
        public string? Title { get; set; }
        public bool IsComplete { get; set; }
    }
}