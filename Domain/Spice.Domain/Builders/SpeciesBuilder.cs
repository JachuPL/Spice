namespace Spice.Domain.Builders
{
    public class SpeciesBuilder
    {
        private string _name;
        private string _latinName;
        private string _description;

        public static implicit operator Species(SpeciesBuilder builder)
        {
            return new Species(builder._name, builder._latinName, builder._description);
        }

        public SpeciesBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public SpeciesBuilder WithLatinName(string latinName)
        {
            _latinName = latinName;
            return this;
        }

        public SpeciesBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }
    }
}