﻿namespace Spice.Domain.Builders
{
    public static class New
    {
        public static FieldBuilder Field => new FieldBuilder();
        public static SpeciesBuilder Species => new SpeciesBuilder();
    }
}