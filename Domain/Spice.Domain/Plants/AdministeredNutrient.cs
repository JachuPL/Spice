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

        public AdministeredNutrient(Plant plant, Nutrient nutrient, double amount)
        {
            Plant = plant;
            Nutrient = nutrient;
            Amount = amount;
            Date = DateTime.Now;
        }

        public AdministeredNutrient(Plant plant, Nutrient nutrient, double amount, DateTime date) : this(plant, nutrient, amount)
        {
            Date = date;
        }
    }
}