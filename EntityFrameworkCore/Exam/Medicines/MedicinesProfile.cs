namespace Medicines
{
    using AutoMapper;

    using Data.Models;
    using DataProcessor.ImportDtos;
    using DataProcessor.ExportDtos;

    public class MedicinesProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE OR RENAME THIS CLASS
        public MedicinesProfile()
        {
            // Pharmacy 
            this.CreateMap<ImportPharmacyDto, Pharmacy>();
            this.CreateMap<Pharmacy, ExportPharmacyDto>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.PhoneNumber));
            // Medicine 
            this.CreateMap<ImportMedicinesDto, Medicine>();
            this.CreateMap<Medicine, ExportMedicinesFromDesiredCategoryInNonStopPharmaciesDto>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Category))
                .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Price));
            // Patient 
            this.CreateMap<ImportPatientDto, Patient>();
            // PatientMedicine 
        }
    }
}
