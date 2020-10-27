using System.Collections.Generic;
using avaliacao_back.Domain.Models;

namespace avaliacao_back.Domain.ViewModel
{
    public class UserCreationViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IList<Phone> Phones { get; set; }
    }
}