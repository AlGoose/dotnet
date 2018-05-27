using System.Collections.Generic;
using WebTavern.Models.EFModels;

namespace WebTavern.Models.ViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Drink> Drinks { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}