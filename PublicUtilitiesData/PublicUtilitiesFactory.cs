using PublicUtilitiesData.Models;
using System;
using System.Collections.Generic;

namespace PublicUtilitiesData
{
    public class PublicUtilitiesFactory
    {
        private ICalculatorsLoader _calculatorsLoader;
        private IStartDataLoader _startDataLoader;

        internal PublicUtilitiesFactory(string connectionString)
        {
            PUDbContext.SetConnectionString(connectionString);
        }

        public PublicUtilitiesFactory WithCalculatorLoader(ICalculatorsLoader calculatorsLoader)
        {
            if (_calculatorsLoader != null)
                throw new InvalidOperationException("Adden more than one loader");

            _calculatorsLoader = calculatorsLoader;

            return this;
        }

        public PublicUtilitiesFactory WithDefaultCalculatorLoader()
        {
            if (_calculatorsLoader != null)
                throw new InvalidOperationException("Adden more than one calculator loader");

            _calculatorsLoader = new DefaultCalculatorsLoader();

            return this;
        }

        public PublicUtilitiesFactory WithStartDataLoader(IStartDataLoader startDataLoader)
        {
            if (_startDataLoader != null)
                throw new InvalidOperationException("Adden more than one start data loader");

            _startDataLoader = startDataLoader;
            return this;
        }

        public PublicUtilities Init()
        {
            using (var context = new PUDbContext())
            {
                if (context.Database.EnsureCreated())
                {
                    context.AddRange(_startDataLoader.Load());
                    context.SaveChanges();
                }
            }

            return new PublicUtilities(_calculatorsLoader);
        }
    }
}
