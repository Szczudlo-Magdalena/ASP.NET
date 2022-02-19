using System.Collections.Generic;
using LibApp.Dtos;
using LibApp.Models;

namespace LibApp.ViewModels
{
    public class RandomBookViewModel
    {
        public BookDto Book { get; set; }
        public List<CustomerDto> Customers { get; set; }
    }
}