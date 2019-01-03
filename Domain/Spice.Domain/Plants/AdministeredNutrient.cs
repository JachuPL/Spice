using System;

namespace Spice.Domain.Plants
{
    public class AdministeredNutrient
    {
        public Guid Id { get; set; }
        public Plant Plant { get; set; }
        public Nutrient Nutrient { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }

        protected AdministeredNutrient()
        {
        }

        internal AdministeredNutrient(Plant plant, Nutrient nutrient, double amount, DateTime date)
        {
            Plant = plant;
            Nutrient = nutrient;
            Amount = amount;
            Date = date;
        }
    }
}