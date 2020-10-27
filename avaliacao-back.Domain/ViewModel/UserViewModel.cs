using System;
using System.Collections.Generic;
using avaliacao_back.Domain.Models;

namespace avaliacao_back.Domain.ViewModel
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public IList<PhoneViewModel> Phones { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public DateTime LastLogin { get; set; }
        public Guid Token { get; set; }
    }
}