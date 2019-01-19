namespace Spice.Domain.Builders
{
    public class FieldBuilder
    {
        private string _name;
        private string _description;
        private double _latitude;
        private double _longtitude;

        internal FieldBuilder()
        {
        }

        public static implicit operator Field(FieldBuilder builder)
        {
            return new Field(builder._name, builder._description, builder._latitude, builder._longtitude);
        }

        public FieldBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public FieldBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public FieldBuilder OnLatitude(double latitude)
        {
            _latitude = latitude;

            return this;
        }

        public FieldBuilder OnLongtitude(double longtitude)
        {
            _longtitude = longtitude;
            return this;
        }
    }
}