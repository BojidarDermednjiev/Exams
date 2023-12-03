using Medicines.Data.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Medicines.DataProcessor
{
    using AutoMapper;

    using Data;

    public class Serializer
    {
        public static string ExportPatientsWithTheirMedicines(MedicinesContext context, string date)
        {
            throw new NotImplementedException();
        }

        public static string ExportMedicinesFromDesiredCategoryInNonStopPharmacies(MedicinesContext context, int medicineCategory)
        {
            throw new NotImplementedException();
        }
        private static IMapper InitializeAutoMapper()
            => new Mapper(new MapperConfiguration(cfg => { cfg.AddProfile<MedicinesProfile>(); }));
    }
}
