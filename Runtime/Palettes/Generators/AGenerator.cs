using System;

namespace LiteNinja.Colors.Palettes.Generators
{
    public abstract class AGenerator
    {
        private int _seed;
        protected Random _random;

        public AGenerator(int? seed)
        {
            _seed = seed ?? DateTime.Now.Millisecond;
            _random = new Random(_seed);
        }

        /// <summary>
        /// Reset the random generator with a new seed, if provided, otherwise with the current seed
        /// </summary>
        /// <param name="newSeed"></param>
        public void Reset(int? newSeed)
        {
            if (newSeed.HasValue)
            {
                _seed = newSeed.Value;
            }
            _random = new Random(_seed);
        }

        public abstract IPalette Generate(int count);
    }
}