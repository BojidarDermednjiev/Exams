//Resharper disable InconsistentNaming, CheckNamespace

using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;


using AutoMapper;
using AutoMapper.Configuration;
using Medicines;
using Medicines.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using NUnit.Framework;

[TestFixture]
public class Import_000_101
{
    private IServiceProvider serviceProvider;
    private static readonly Assembly CurrentAssembly = typeof(StartUp).Assembly;

    [SetUp]
    public void Setup()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MedicinesProfile>();
        });

        var uniqueDbName = $"Medicines_{Guid.NewGuid()}";

        this.serviceProvider = ConfigureServices<MedicinesContext>(uniqueDbName);
    }

    [Test]
    public void ImportPharmaciesTest()
    {
        var context = this.serviceProvider.GetService<MedicinesContext>();

        var inputXml =
            "<?xml version='1.0' encoding='UTF-8'?><Pharmacies><Pharmacy non-stop=\"true\"><Name>Vitality</Name><PhoneNumber>(123) 456-7890</PhoneNumber><Medicines><Medicine category=\"1\"><Name>Ibuprofen</Name><Price>8.50</Price><ProductionDate>2022-02-10</ProductionDate><ExpiryDate>2025-02-10</ExpiryDate><Producer>ReliefMed Labs</Producer></Medicine><Medicine category=\"4\"><Name>Lorazepam</Name><Price>25.30</Price><ProductionDate>2022-03-20</ProductionDate><ExpiryDate>2023-03-20</ExpiryDate><Producer>Central Pharma</Producer></Medicine><Medicine category=\"3\"><Name>Cetirizine</Name><Price>12.50</Price><ProductionDate>2021-11-10</ProductionDate><ExpiryDate>2023-11-10</ExpiryDate><Producer>GeneriCare Pharmaceuticals</Producer></Medicine><Medicine category=\"5\"><Name>Flu Vaccine</Name><Price>15.00</Price><ProductionDate>2022-08-01</ProductionDate><ExpiryDate>2023-02-01</ExpiryDate><Producer>ReliefMed Labs</Producer></Medicine><Medicine category=\"2\"><Name>Penicillin</Name><Price>22.00</Price><ProductionDate>2022-05-15</ProductionDate><ExpiryDate>2024-05-15</ExpiryDate><Producer>Central Pharma</Producer></Medicine><Medicine category=\"4\"><Name>Diazepam</Name><Price>18.75</Price><ProductionDate>2022-01-20</ProductionDate><ExpiryDate>2024-01-20</ExpiryDate><Producer>HealthCare Pharma</Producer></Medicine><Medicine category=\"1\"><Name>Acetaminophen</Name><Price>7.99</Price><ProductionDate>2021-12-12</ProductionDate><ExpiryDate>2024-12-12</ExpiryDate><Producer>GeneriCare Pharmaceuticals</Producer></Medicine><Medicine category=\"2\"><Name>Ciprofloxacin</Name><Price>19.20</Price><ProductionDate>2022-07-22</ProductionDate><ExpiryDate>2025-07-22</ExpiryDate><Producer>ReliefMed Labs</Producer></Medicine><Medicine category=\"3\"><Name>Chlorhexidine</Name><Price>9.80</Price><ProductionDate>2022-03-30</ProductionDate><ExpiryDate>2024-03-30</ExpiryDate><Producer>Central Pharma</Producer></Medicine><Medicine category=\"3\"><Name>Chlorhexidine</Name><Price>10.80</Price><ProductionDate>2022-03-30</ProductionDate><ExpiryDate>2024-03-30</ExpiryDate><Producer>HealthCare Pharma</Producer></Medicine><Medicine category=\"5\"><Name>Hepatitis B Vaccine</Name><Price>20.00</Price><ProductionDate>2022-06-14</ProductionDate><ExpiryDate>2023-06-14</ExpiryDate><Producer>HealthCare Pharma</Producer></Medicine><Medicine category=\"4\"><Name>Alprazolam</Name><Price>23.50</Price><ProductionDate>2022-04-18</ProductionDate><ExpiryDate>2025-04-18</ExpiryDate><Producer>GeneriCare Pharmaceuticals</Producer></Medicine><Medicine category=\"4\"><Name>Alprazolam</Name><Price>25.50</Price><ProductionDate>2022-04-18</ProductionDate><ExpiryDate>2025-04-18</ExpiryDate><Producer>GeneriCare Pharmaceuticals</Producer></Medicine><Medicine category=\"1\"><Name>Naproxen</Name><Price>10.00</Price><ProductionDate>2022-09-09</ProductionDate><ExpiryDate>2023-09-09</ExpiryDate><Producer>ReliefMed Labs</Producer></Medicine></Medicines></Pharmacy><Pharmacy non-stop=\"true\"><Name>GreenLeaf</Name><PhoneNumber>(456) 789-0123</PhoneNumber><Medicines><Medicine category=\"1\"><Name></Name><Price>9.00</Price><ProductionDate>2022-03-05</ProductionDate><ExpiryDate>2025-03-05</ExpiryDate><Producer>NextGen Pharma</Producer></Medicine><Medicine category=\"4\"><Name>Lorazepam</Name><Price>26.50</Price><ProductionDate>2022-04-22</ProductionDate><ExpiryDate>2023-04-22</ExpiryDate><Producer>Central Pharma</Producer></Medicine><Medicine category=\"2\"><Name>Amoxicillin</Name><Price>0.00</Price><ProductionDate>2022-05-18</ProductionDate><ExpiryDate>2024-05-18</ExpiryDate><Producer>GeneriCare Pharmaceuticals</Producer></Medicine><Medicine category=\"5\"><Name>Flu Vaccine</Name><Price>16.00</Price><ProductionDate>2022-09-01</ProductionDate><ExpiryDate>2023-03-01</ExpiryDate><Producer>HealthCare Pharma</Producer></Medicine><Medicine category=\"3\"><Name>Cetirizine</Name><Price>11.00</Price><ProductionDate>2021-10-15</ProductionDate><ExpiryDate>2023-10-15</ExpiryDate><Producer>ReliefMed Labs</Producer></Medicine><Medicine category=\"1\"><Name>Acetaminophen</Name><Price>-8.50</Price><ProductionDate>2021-11-11</ProductionDate><ExpiryDate>2024-11-11</ExpiryDate><Producer>NextGen Pharma</Producer></Medicine><Medicine category=\"4\"><Name>Zolpidem</Name><Price>19.95</Price><ProductionDate>2022-06-20</ProductionDate><ExpiryDate>2025-06-20</ExpiryDate><Producer>Central Pharma</Producer></Medicine><Medicine category=\"2\"><Name>Penicillin</Name><Price>21.00</Price><ProductionDate>2022-07-10</ProductionDate><ExpiryDate>2024-07-10</ExpiryDate><Producer>GeneriCare Pharmaceuticals</Producer></Medicine><Medicine category=\"3\"><Name>Chlorhexidine</Name><Price>10.50</Price><ProductionDate>2022-04-30</ProductionDate><ExpiryDate>2024-04-30</ExpiryDate><Producer>ReliefMed Labs</Producer></Medicine><Medicine category=\"5\"><Name>MMR Vaccine</Name><Price>22.00</Price><ProductionDate>2022-08-14</ProductionDate><ExpiryDate>2023-08-14</ExpiryDate><Producer>HealthCare Pharma</Producer></Medicine></Medicines></Pharmacy><Pharmacy non-stop=\"tru\"><Name>Harmony</Name><PhoneNumber>(789) 012-3456</PhoneNumber><Medicines><Medicine category=\"1\"><Name>Paracetamol</Name><Price>9.50</Price><ProductionDate>2022-03-01</ProductionDate><ExpiryDate>2025-03-01</ExpiryDate><Producer>Central Pharma</Producer></Medicine><Medicine category=\"2\"><Name>Azithromycin</Name><Price>18.20</Price><ProductionDate>2022-06-20</ProductionDate><ExpiryDate>2024-06-20</ExpiryDate><Producer>GeneriCare Pharmaceuticals</Producer></Medicine><Medicine category=\"3\"><Name>Betadine</Name><Price>7.99</Price><ProductionDate>2021-11-11</ProductionDate><ExpiryDate>2023-11-11</ExpiryDate><Producer>HealthCare Pharma</Producer></Medicine><Medicine category=\"4\"><Name>Oxazepam</Name><Price>22.75</Price><ProductionDate>2022-01-15</ProductionDate><ExpiryDate>2023-01-15</ExpiryDate><Producer>NextGen Pharma</Producer></Medicine><Medicine category=\"6\"><Name>Tetanus Vaccine</Name><Price>14.50</Price><ProductionDate>2022-10-05</ProductionDate><ExpiryDate>2023-10-05</ExpiryDate><Producer>ReliefMed Labs</Producer></Medicine><Medicine category=\"1\"><Name>Aspirin</Name><Price>8.25</Price><ProductionDate>2022-07-10</ProductionDate><ExpiryDate>2024-07-10</ExpiryDate><Producer>Central Pharma</Producer></Medicine></Medicines></Pharmacy><Pharmacy non-stop=\"false\"><Name>CareFirst, CareFirst, CareFirst, CareFirst, CareFirst, CareFirst</Name><PhoneNumber>(234) 567-8901</PhoneNumber><Medicines><Medicine category=\"1\"><Name>Naproxen</Name><Price>11.20</Price><ProductionDate>2022-07-07</ProductionDate><ExpiryDate>2021-07-07</ExpiryDate><Producer>NextGen Pharma</Producer></Medicine><Medicine category=\"2\"><Name>Ciprofloxacin</Name><Price>20.50</Price><ProductionDate>2022-08-15</ProductionDate><ExpiryDate>2024-08-15</ExpiryDate><Producer>GeneriCare Pharmaceuticals</Producer></Medicine><Medicine category=\"0\"><Name>Hydrogen Peroxide</Name><Price>5.75</Price><ProductionDate>2021-12-20</ProductionDate><ExpiryDate>2023-12-20</ExpiryDate><Producer>Central Pharma</Producer></Medicine><Medicine category=\"4\"><Name>Temazepam</Name><Price>24.30</Price><ProductionDate>2022-02-28</ProductionDate><ExpiryDate>2024-02-28</ExpiryDate><Producer>HealthCare Pharma</Producer></Medicine><Medicine category=\"5\"><Name>Varicella Vaccine</Name><Price>18.00</Price><ProductionDate>2022-09-10</ProductionDate><ExpiryDate>2023-09-10</ExpiryDate><Producer>ReliefMed Labs</Producer></Medicine></Medicines></Pharmacy><Pharmacy non-stop=\"false\"><Name>Pulse</Name><PhoneNumber>(567) 890-1234</PhoneNumber><Medicines><Medicine category=\"1\"><Name>Diclofenac</Name><Price>10.75</Price><ProductionDate>2022-08-15</ProductionDate><ExpiryDate>2025-08-15</ExpiryDate><Producer></Producer></Medicine><Medicine category=\"2\"><Name>Tetracycline</Name><Price>17.40</Price><ProductionDate>2022-05-22</ProductionDate><ExpiryDate>2024-05-22</ExpiryDate><Producer>GeneriCare Pharmaceuticals</Producer></Medicine><Medicine category=\"4\"><Name>Clonazepam</Name><Price>21.30</Price><ProductionDate>2022-02-07</ProductionDate><ExpiryDate>2023-02-07</ExpiryDate><Producer>NextGen Pharma</Producer></Medicine><Medicine category=\"5\"><Name>MMR Vaccine</Name><Price>19.50</Price><ProductionDate></ProductionDate><ExpiryDate>2023-09-01</ExpiryDate><Producer>ReliefMed Labs</Producer></Medicine></Medicines></Pharmacy><Pharmacy non-stop=\"true\"><Name>Serenity</Name><PhoneNumber>(890) 123-4567</PhoneNumber><Medicines><Medicine category=\"1\"><Name>Meloxicam</Name><Price>12.20</Price><ProductionDate>2022-07-30</ProductionDate><ExpiryDate>2024-07-30</ExpiryDate><Producer>Central Pharma</Producer></Medicine><Medicine category=\"2\"><Name>Erythromycin</Name><Price>16.85</Price><ProductionDate>2022-06-10</ProductionDate><ExpiryDate>2024-06-10</ExpiryDate><Producer>GeneriCare Pharmaceuticals</Producer></Medicine><Medicine category=\"3\"><Name>Iodine Solution</Name><Price>6.50</Price><ProductionDate></ProductionDate><ExpiryDate></ExpiryDate><Producer>HealthCare Pharma</Producer></Medicine><Medicine category=\"4\"><Name>Lithium Carbonate</Name><Price>23.00</Price><ProductionDate>2022-03-15</ProductionDate><ExpiryDate></ExpiryDate><Producer>NextGen Pharma</Producer></Medicine><Medicine category=\"5\"><Name>Pneumococcal Vaccine</Name><Price>20.75</Price><ProductionDate>2022-11-05</ProductionDate><ExpiryDate>2023-11-05</ExpiryDate><Producer>ReliefMed Labs</Producer></Medicine></Medicines></Pharmacy><Pharmacy non-stop=\"false\"><Name>PureLife</Name><PhoneNumber>(321) 654-9870</PhoneNumber><Medicines></Medicines></Pharmacy><Pharmacy non-stop=\"true\"><Name>Revive</Name><PhoneNumber>(654) 987-0123</PhoneNumber><Medicines><Medicine category=\"1\"><Name>Ketoprofen</Name><Price>11.50</Price><ProductionDate>2022-04-10</ProductionDate><ExpiryDate>2025-04-10</ExpiryDate><Producer>GeneriCare Pharmaceuticals</Producer></Medicine><Medicine category=\"2\"><Name>Clindamycin</Name><Price>15.30</Price><ProductionDate>2022-07-20</ProductionDate><ExpiryDate>2024-07-20</ExpiryDate><Producer>Central Pharma</Producer></Medicine><Medicine category=\"3\"><Name>Saline Solution</Name><Price>5.20</Price><ProductionDate>2021-10-15</ProductionDate><ExpiryDate>2023-10-15</ExpiryDate><Producer></Producer></Medicine><Medicine category=\"4\"><Name>Zopiclone</Name><Price>22.45</Price><ProductionDate>2022-02-18</ProductionDate><ExpiryDate>2024-02-18</ExpiryDate><Producer>NextGen Pharma</Producer></Medicine><Medicine category=\"5\"><Name></Name><Price>18.25</Price><ProductionDate>2022-10-01</ProductionDate><ExpiryDate>2023-10-01</ExpiryDate><Producer>ReliefMed Labs</Producer></Medicine><Medicine category=\"1\"><Name>Celecoxib</Name><Price>13.75</Price><ProductionDate>2022-08-22</ProductionDate><ExpiryDate>2025-08-22</ExpiryDate><Producer>Ge</Producer></Medicine><Medicine category=\"2\"><Name>Doxycycline</Name><Price>-0.01</Price><ProductionDate>2022-09-05</ProductionDate><ExpiryDate>2024-09-05</ExpiryDate><Producer>Central Pharma</Producer></Medicine></Medicines></Pharmacy><Pharmacy non-stop=\"false\"><Name>Panacea</Name><PhoneNumber>(987) 321-6540</PhoneNumber><Medicines><Medicine category=\"1\"><Name>Indomethacin</Name><Price>12.75</Price><ProductionDate>2022-08-08</ProductionDate><ExpiryDate>2024-08-08</ExpiryDate><Producer>HealthCare Pharma</Producer></Medicine><Medicine category=\"2\"><Name>Vancomycin</Name><Price>23.50</Price><ProductionDate>2022-10-10</ProductionDate><ExpiryDate>2025-10-10</ExpiryDate><Producer>NextGen Pharma, NextGen Pharma, NextGen Pharma, NextGen Pharma, NextGen Pharma, NextGen Pharma, NextGen Pharma, NextGen Pharma</Producer></Medicine><Medicine category=\"4\"><Name>Ambien</Name><Price>21.20</Price><ProductionDate>2022-01-22</ProductionDate><ExpiryDate>2022-01-22</ExpiryDate><Producer>ReliefMed Labs</Producer></Medicine></Medicines></Pharmacy><Pharmacy non-stop=\"false\"><Name>Healthwise</Name><PhoneNumber>(432) 765-0981</PhoneNumber><Medicines><Medicine category=\"3\"><Name>Hydrocortisone Cream</Name><Price>9.40</Price><ProductionDate>2022-06-15</ProductionDate><ExpiryDate>2024-06-15</ExpiryDate><Producer>Central Pharma</Producer></Medicine><Medicine category=\"5\"><Name>Rabies Vaccine</Name><Price>29.99</Price><ProductionDate>2022-07-25</ProductionDate><ExpiryDate>2023-07-25</ExpiryDate><Producer>GeneriCare Pharmaceuticals</Producer></Medicine><Medicine category=\"1\"><Name>Aleve (Naproxen)</Name><Price>10.50</Price><ProductionDate>2022-09-01</ProductionDate><ExpiryDate>2025-09-01</ExpiryDate><Producer>HealthCare Pharma</Producer></Medicine></Medicines></Pharmacy><Pharmacy non-stop=\"false\"><Name>V</Name><PhoneNumber>(765) 098-4321</PhoneNumber><Medicines><Medicine category=\"3\"><Name>Hydrocortisone Cream</Name><Price>9.40</Price><ProductionDate>2022-06-15</ProductionDate><ExpiryDate>2024-06-15</ExpiryDate><Producer>Central Pharma</Producer></Medicine><Medicine category=\"5\"><Name>Rabies Vaccine</Name><Price>29.99</Price><ProductionDate>2022-07-25</ProductionDate><ExpiryDate>2023-07-25</ExpiryDate><Producer>GeneriCare Pharmaceuticals</Producer></Medicine><Medicine category=\"1\"><Name>Aleve (Naproxen)</Name><Price>10.50</Price><ProductionDate>2022-09-01</ProductionDate><ExpiryDate>2025-09-01</ExpiryDate><Producer>HealthCare Pharma</Producer></Medicine></Medicines></Pharmacy><Pharmacy non-stop=\"true\"><Name>LifeSpring</Name><PhoneNumber>(098) 765-4321</PhoneNumber><Medicines><Medicine category=\"1\"><Name>Morphine</Name><Price>30.00</Price><ProductionDate>2022-05-10</ProductionDate><ExpiryDate>2024-05-10</ExpiryDate><Producer>GeneriCare Pharmaceuticals</Producer></Medicine><Medicine category=\"2\"><Name>Streptomycin</Name><Price>1000.01</Price><ProductionDate>2022-07-22</ProductionDate><ExpiryDate>2024-07-22</ExpiryDate><Producer>Central Pharma</Producer></Medicine><Medicine category=\"3\"><Name>Antiseptic Wipes</Name><Price>4.95</Price><ProductionDate>2021-09-15</ProductionDate><ExpiryDate>2023-09-15</ExpiryDate><Producer>HealthCare Pharma</Producer></Medicine><Medicine category=\"4\"><Name>Ativan (Lorazepam)</Name><Price>20.00</Price><ProductionDate>2022-03-01</ProductionDate><ExpiryDate>2025-03-01</ExpiryDate><Producer>NextGen Pharma</Producer></Medicine></Medicines></Pharmacy><Pharmacy non-stop=\"true\"><Name>MediTrust</Name><PhoneNumber>(135) 792-4680</PhoneNumber><Medicines><Medicine category=\"5\"><Name>HPV Vaccine, HPV Vaccine, HPV Vaccine, HPV Vaccine, HPV Vaccine, HPV Vaccine, HPV Vaccine, HPV Vaccine, HPV Vaccine, HPV Vaccine, HPV Vaccine, HPV Vaccine, HPV Vaccine, HPV Vaccine, HPV Vaccine</Name><Price>45.00</Price><ProductionDate>2022-08-12</ProductionDate><ExpiryDate>2023-08-12</ExpiryDate><Producer>ReliefMed Labs</Producer></Medicine><Medicine category=\"1\"><Name>Co</Name><Price>15.75</Price><ProductionDate>2022-10-05</ProductionDate><ExpiryDate>2024-10-05</ExpiryDate><Producer>GeneriCare Pharmaceuticals</Producer></Medicine></Medicines></Pharmacy></Pharmacies>\r\n";

        var actualOutput =
            Medicines.DataProcessor.Deserializer.ImportPharmacies(context, inputXml).TrimEnd();
        var expectedOutput =
            "Invalid Data!\r\nInvalid Data!\r\nInvalid Data!\r\nSuccessfully imported pharmacy - Vitality with 11 medicines.\r\nInvalid Data!\r\nInvalid Data!\r\nInvalid Data!\r\nInvalid Data!\r\nInvalid Data!\r\nSuccessfully imported pharmacy - GreenLeaf with 5 medicines.\r\nInvalid Data!\r\nInvalid Data!\r\nInvalid Data!\r\nInvalid Data!\r\nSuccessfully imported pharmacy - Pulse with 2 medicines.\r\nInvalid Data!\r\nInvalid Data!\r\nInvalid Data!\r\nSuccessfully imported pharmacy - Serenity with 2 medicines.\r\nSuccessfully imported pharmacy - PureLife with 0 medicines.\r\nInvalid Data!\r\nInvalid Data!\r\nInvalid Data!\r\nInvalid Data!\r\nSuccessfully imported pharmacy - Revive with 3 medicines.\r\nInvalid Data!\r\nInvalid Data!\r\nSuccessfully imported pharmacy - Panacea with 1 medicines.\r\nInvalid Data!\r\nSuccessfully imported pharmacy - Healthwise with 2 medicines.\r\nInvalid Data!\r\nInvalid Data!\r\nSuccessfully imported pharmacy - LifeSpring with 3 medicines.\r\nInvalid Data!\r\nInvalid Data!\r\nSuccessfully imported pharmacy - MediTrust with 0 medicines.";
        var assertContext = this.serviceProvider.GetService<MedicinesContext>();

        const int expectedMedicinesCount = 29;
        var actualMedicinesCount = assertContext.Medicines.Count();


        Assert.That(actualMedicinesCount, Is.EqualTo(expectedMedicinesCount),
            $"Inserted {nameof(context.Medicines)} count is incorrect!");


        Assert.That(actualOutput, Is.EqualTo(expectedOutput).NoClip,
            $"{nameof(Medicines.DataProcessor.Deserializer.ImportPharmacies)} output is incorrect!");
    }

    private static Type GetType(string modelName)
    {
        var modelType = CurrentAssembly
            .GetTypes()
            .FirstOrDefault(t => t.Name == modelName);

        Assert.IsNotNull(modelType, $"{modelName} model not found!");

        return modelType;
    }

    private static IServiceProvider ConfigureServices<TContext>(string databaseName)
        where TContext : DbContext
    {
        var services = ConfigureDbContext<TContext>(databaseName);

        var context = services.GetService<TContext>();

        try
        {
            context.Model.GetEntityTypes();
        }
        catch (InvalidOperationException ex) when (ex.Source == "Microsoft.EntityFrameworkCore.Proxies")
        {
            services = ConfigureDbContext<TContext>(databaseName, useLazyLoading: true);
        }

        return services;
    }

    private static IServiceProvider ConfigureDbContext<TContext>(string databaseName, bool useLazyLoading = false)
        where TContext : DbContext
    {
        var services = new ServiceCollection();

        services
            .AddDbContext<TContext>(
                options => options
                    .UseInMemoryDatabase(databaseName)
            );

        var serviceProvider = services.BuildServiceProvider();
        return serviceProvider;
    }
}