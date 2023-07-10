using PublicUtilitiesData;
using PublicUtilitiesData.Models;
using System;
using System.Collections.Generic;

namespace PublicUtilitiesTestTaskConsole
{
    public class StartDataLoader : IStartDataLoader
    {
        public IEnumerable<object> Load()
        {
            List<object> data = new List<object>();

            Consumer consumer = new Consumer("Иван Иванов");
            data.Add(consumer);

            Unit unit = new Unit("");
            Unit unitm3 = new Unit("куб м.");
            Unit unitkvt = new Unit("кВт/ч");
            Unit unitgkal = new Unit("Гкал");
            data.Add(unit);
            data.Add(unitm3);
            data.Add(unitkvt);
            data.Add(unitgkal);

            Calculator DefaultCalculator = new Calculator();
            Calculator GVSECalculator = new Calculator();
            Calculator EECalculator = new Calculator();
            data.Add(DefaultCalculator);
            data.Add(GVSECalculator);
            data.Add(EECalculator);

            ServiceBase serviceBaseHVS = new ServiceBase("ХВС");
            ServiceBase serviceBaseGVS = new ServiceBase("ГВС");
            ServiceBase serviceBaseEE = new ServiceBase("ЭЭ");
            data.Add(serviceBaseHVS);
            data.Add(serviceBaseGVS);
            data.Add(serviceBaseEE);

            MetersType metersHVS = new MetersType(serviceBaseHVS.Name, serviceBaseHVS);
            MetersType metersGVS = new MetersType(serviceBaseGVS.Name, serviceBaseGVS);
            MetersType metersEED = new MetersType("ЭЭ День", serviceBaseEE);
            MetersType metersEEN = new MetersType("ЭЭ Ночь", serviceBaseEE);
            data.Add(metersHVS);
            data.Add(metersGVS);
            data.Add(metersEED);
            data.Add(metersEEN);

            Service serviceHVS = new Service(serviceBaseHVS, metersHVS.Name, unitm3);
            Service serviceGVS = new Service(serviceBaseGVS, "ГВС Теплоноситель", unitm3);
            Service serviceGVSE = new Service(serviceBaseGVS, "ГВС Тепловая энергия", unitgkal);
            Service serviceEE = new Service(serviceBaseEE, serviceBaseEE.Name, unitkvt);
            Service serviceEED = new Service(serviceBaseEE, metersEED.Name, unitkvt);
            Service serviceEEN = new Service(serviceBaseEE, metersEEN.Name, unitkvt);
            data.Add(serviceHVS);
            data.Add(serviceGVS);
            data.Add(serviceGVSE);
            data.Add(serviceEE);
            data.Add(serviceEED);
            data.Add(serviceEEN);

            ServiceMetersType serviceMetersTypeHVS = new ServiceMetersType(serviceHVS, metersHVS);
            ServiceMetersType serviceMetersTypeGVS = new ServiceMetersType(serviceGVS, metersGVS);
            ServiceMetersType serviceMetersTypeGVSE = new ServiceMetersType(serviceGVSE, metersGVS);
            ServiceMetersType serviceMetersTypeEEDD = new ServiceMetersType(serviceEED, metersEED);
            ServiceMetersType serviceMetersTypeEENN = new ServiceMetersType(serviceEEN, metersEEN);
            data.Add(serviceMetersTypeHVS);
            data.Add(serviceMetersTypeGVS);
            data.Add(serviceMetersTypeGVSE);
            data.Add(serviceMetersTypeEEDD);
            data.Add(serviceMetersTypeEENN);

            Tariff tariffHVS = new Tariff(serviceHVS.Name, serviceHVS, 35.78m, 4.85m);
            Tariff tariffGVS = new Tariff(serviceGVS.Name, serviceGVS, 35.78m, 4.01m);
            Tariff tariffGVSE = new Tariff(serviceGVSE.Name, serviceGVSE, 998.69m, 0.05349m);
            Tariff tariffEE = new Tariff(serviceEE.Name, serviceEE, 4.28m, 164m);
            Tariff tariffEED = new Tariff(serviceEED.Name, serviceEED, 4.9m, 0m);
            Tariff tariffEEN = new Tariff(serviceEEN.Name, serviceEEN, 2.31m, 0m);
            data.Add(tariffHVS);
            data.Add(tariffGVS);
            data.Add(tariffGVSE);
            data.Add(tariffEE);
            data.Add(tariffEED);
            data.Add(tariffEEN);

            ConsumerTariff consTariffHVS = new ConsumerTariff(consumer, serviceHVS, tariffHVS);
            ConsumerTariff consTariffGVS = new ConsumerTariff(consumer, serviceGVS, tariffGVS);
            ConsumerTariff consTariffGVSE = new ConsumerTariff(consumer, serviceGVSE, tariffGVSE);
            ConsumerTariff consTariffEE = new ConsumerTariff(consumer, serviceEE, tariffEE);
            ConsumerTariff consTariffEED = new ConsumerTariff(consumer, serviceEED, tariffEED);
            ConsumerTariff consTariffEEN = new ConsumerTariff(consumer, serviceEEN, tariffEEN);
            data.Add(consTariffHVS);
            data.Add(consTariffGVS);
            data.Add(consTariffGVSE);
            data.Add(consTariffEE);
            data.Add(consTariffEED);
            data.Add(consTariffEEN);

            TariffCalculators tariffCalculatorGVSE = new TariffCalculators(tariffGVSE, GVSECalculator);
            TariffCalculators tariffCalculatorEE = new TariffCalculators(tariffEE, EECalculator);
            TariffCalculators tariffCalculatorEED = new TariffCalculators(tariffEED, EECalculator);
            TariffCalculators tariffCalculatorEEN = new TariffCalculators(tariffEEN, EECalculator);
            data.Add(tariffCalculatorGVSE);
            data.Add(tariffCalculatorEE);
            data.Add(tariffCalculatorEED);
            data.Add(tariffCalculatorEEN);

            return data;
        }
    }
}
