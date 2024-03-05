using Nager.Date.Models;
using Nager.Date.ReligiousProviders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nager.Date.HolidayProviders
{
    /// <summary>
    /// France HolidayProvider
    /// </summary>
    internal class FranceHolidayProvider : IHolidayProvider, ISubdivisionCodesProvider
    {
        private readonly ICatholicProvider _catholicProvider;

        /// <summary>
        /// France HolidayProvider
        /// </summary>
        /// <param name="catholicProvider"></param>
        public FranceHolidayProvider(
            ICatholicProvider catholicProvider)
        {
            this._catholicProvider = catholicProvider;
        }

        /// <inheritdoc/>
        public IDictionary<string, string> GetSubdivisionCodes()
        {
            return new Dictionary<string, string>
            {
                { "FR-ARA", "Auvergne-Rhone-Alpes" },
                { "FR-BFC", "Bourgogne-Franche-Comte" },
                { "FR-BRE", "Bretagne" },
                { "FR-CVL", "Centre-Val de Loire" },
                { "FR-20R", "Corse" },
                { "FR-GES", "Grand-Est" },
                { "FR-HDF", "Hauts-de-France" },
                { "FR-IDF", "Ile-de-France" },
                { "FR-NOR", "Normandie" },
                { "FR-NAQ", "Nouvelle-Aquitaine" },
                { "FR-OCC", "Occitanie" },
                { "FR-PDL", "Pays-de-la-Loire" },
                { "FR-PAC", "Provence-Alpes-Cote-d'Azur" },
            };
        }

        /// <inheritdoc/>
        public IEnumerable<Holiday> GetHolidays(int year)
        {
            var countryCode = CountryCode.FR;

            var holidaySpecifications = new List<HolidaySpecification>
            {
                new HolidaySpecification
                {
                    Date = new DateTime(year, 1, 1),
                    EnglishName = "New Year's Day",
                    LocalName = "Jour de l'an",
                    HolidayTypes = HolidayTypes.Public
                },
                new HolidaySpecification
                {
                    Date = new DateTime(year, 5, 1),
                    EnglishName = "Labour Day",
                    LocalName = "Fête du Travail",
                    HolidayTypes = HolidayTypes.Public
                },
                new HolidaySpecification
                {
                    Date = new DateTime(year, 5, 8),
                    EnglishName = "Victory in Europe Day",
                    LocalName = "Victoire 1945",
                    HolidayTypes = HolidayTypes.Public
                },
                new HolidaySpecification
                {
                    Date = new DateTime(year, 7, 14),
                    EnglishName = "Bastille Day",
                    LocalName = "Fête nationale",
                    HolidayTypes = HolidayTypes.Public
                },
                new HolidaySpecification
                {
                    Date = new DateTime(year, 8, 15),
                    EnglishName = "Assumption Day",
                    LocalName = "Assomption",
                    HolidayTypes = HolidayTypes.Public
                },
                new HolidaySpecification
                {
                    Date = new DateTime(year, 11, 1),
                    EnglishName = "All Saints' Day",
                    LocalName = "Toussaint",
                    HolidayTypes = HolidayTypes.Public
                },
                new HolidaySpecification
                {
                    Date = new DateTime(year, 11, 11),
                    EnglishName = "Armistice Day",
                    LocalName = "Armistice 1918",
                    HolidayTypes = HolidayTypes.Public
                },
                new HolidaySpecification
                {
                    Date = new DateTime(year, 12, 25),
                    EnglishName = "Christmas Day",
                    LocalName = "Noël",
                    HolidayTypes = HolidayTypes.Public
                },
                this._catholicProvider.EasterMonday("Lundi de Pâques", year),
                this._catholicProvider.AscensionDay("Ascension", year),
                this._catholicProvider.WhitMonday("Lundi de Pentecôte", year)
            };

            var holidays = HolidaySpecificationProcessor.Process(holidaySpecifications, countryCode);
            return holidays.OrderBy(o => o.Date);

            //var items = new List<Holiday>();
            //items.Add(new Holiday(year, 1, 1, "Jour de l'an", "New Year's Day", countryCode, 1967));
            //items.Add(this._catholicProvider.EasterMonday("Lundi de Pâques", year, countryCode).SetLaunchYear(1642));
            //items.Add(new Holiday(year, 5, 1, "Fête du Travail", "Labour Day", countryCode));
            //items.Add(this._catholicProvider.AscensionDay("Ascension", year, countryCode));
            //items.Add(new Holiday(year, 5, 8, "Victoire 1945", "Victory in Europe Day", countryCode));
            //items.Add(this._catholicProvider.WhitMonday("Lundi de Pentecôte", year, countryCode));
            //items.Add(new Holiday(year, 7, 14, "Fête nationale", "Bastille Day", countryCode));
            //items.Add(new Holiday(year, 8, 15, "Assomption", "Assumption Day", countryCode));
            //items.Add(new Holiday(year, 11, 1, "Toussaint", "All Saints' Day", countryCode));
            //items.Add(new Holiday(year, 11, 11, "Armistice 1918", "Armistice Day", countryCode));
            //items.Add(new Holiday(year, 12, 25, "Noël", "Christmas Day", countryCode));

            //return items.OrderBy(o => o.Date);
        }

        /// <inheritdoc/>
        public IEnumerable<string> GetSources()
        {
            return new string[]
            {
                "https://en.wikipedia.org/wiki/Public_holidays_in_France",
                "https://en.wikipedia.org/wiki/ISO_3166-2:FR",
                "https://ec.europa.eu/taxation_customs/dds2/rd/publicholidays_consultation.jsp?Screen=0&Expand=true&Country=FR"
            };
        }
    }
}
