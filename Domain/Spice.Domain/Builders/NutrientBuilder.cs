namespace Spice.Domain.Builders
{
    public class NutrientBuilder
    {
        private string _name;
        private string _description;
        private string _dosageUnits;

        public static implicit operator Nutrient(NutrientBuilder builder)
        {
            return new Nutrient(builder._name, builder._description, builder._dosageUnits);
        }

        public NutrientBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public NutrientBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public NutrientBuilder WithDosageUnits(string dosageUnits)
        {
            _dosageUnits = dosageUnits;
            return this;
        }
    }
}