using System;
using System.Collections.Generic;
using WebTavern.Models.EFModels;

namespace WebTavern.Models.ViewModels
{
    public class DrinkDetails
    {
        public Drink Drink { get; set; }
        public List<String> Ingredients { get; set; }
    }
}