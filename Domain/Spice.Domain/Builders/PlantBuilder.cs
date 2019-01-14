using Spice.Domain.Plants;
using System.Data;

namespace Spice.Domain.Builders
{
    public class PlantBuilder
    {
        private string _name;
        private Species _species;
        private Field _field;
        private int _row;
        private int _column;
        private PlantState _state;

        public static implicit operator Plant(PlantBuilder builder)
        {
            if (builder._field is null)
            {
                throw new NoNullAllowedException("Field cannot be null");
            }

            if (builder._species is null)
            {
                throw new NoNullAllowedException("Species cannot be null");
            }

            return new Plant(builder._name, builder._species, builder._field, builder._row, builder._column, builder._state);
        }

        public PlantBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public PlantBuilder WithSpecies(Species species)
        {
            _species = species;
            return this;
        }

        public PlantBuilder WithField(Field field)
        {
            _field = field;
            return this;
        }

        public PlantBuilder InRow(int row)
        {
            _row = row;
            return this;
        }

        public PlantBuilder InColumn(int column)
        {
            _column = column;
            return this;
        }

        public PlantBuilder WithState(PlantState state)
        {
            _state = state;
            return this;
        }
    }
}