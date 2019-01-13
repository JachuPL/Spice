namespace Spice.Domain.Builders
{
    public static class New
    {
        public static FieldBuilder Field => new FieldBuilder();
        public static NutrientBuilder Nutrient => new NutrientBuilder();
        public static SpeciesBuilder Species => new SpeciesBuilder();
        public static PlantBuilder Plant => new PlantBuilder();
    }
}