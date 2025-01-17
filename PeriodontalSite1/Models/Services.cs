﻿
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeriodontalSite1.Models
{
    [Table("Services")]
    public class Services
    {
        [Key]
        public int ServicesId { get; set; }
        [Display (Name = "Наименование")]
        public string Name { get; set; }
        public string Description { get; set; }

        public int TypeId { get; set; }

        public int UnitId { get; set; }

        public  TypeServices Types { get; set; }
        public  Units Units { get; set;}

        public ICollection<Prices> Prices { get; set; }
        public Services()
        {
            var Prices = new List<Prices>();
        }



    }

    public class ResultEdit
    {
        public int ServicesId;
        public string Name;
        public Prices Price;
    }

    public class ConfirmEdit
    {
        public int ServicesId;
        public string Name;
        public Prices Prices;
    }
}