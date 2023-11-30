namespace Artillery.DataProcessor
{
    using AutoMapper;
    using Newtonsoft.Json;
    using AutoMapper.QueryableExtensions;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using ExportDto;
    using Utilities;


    public class Serializer
    {
        public static string ExportShells(ArtilleryContext context, double shellWeight)
        {
            var shells = context.Shells
                .Where(s => s.ShellWeight > shellWeight)
                .Select(s => new
                {
                    s.ShellWeight,
                    s.Caliber,
                    Guns = s.Guns
                        .Where(g =>/* g.GunType.ToString() == "AntiAircraftGun"*/ (int)g.GunType == 3)
                        .Select(g => new
                        {
                            GunType = g.GunType.ToString(),
                            GunWeight = g.GunWeight,
                            BarrelLength = g.BarrelLength,
                            Range = g.Range > 3000 ? "Long-range" : "Regular range"
                        })
                        .OrderByDescending(c => c.GunWeight)
                        .ToArray()
                })
                .AsNoTracking()
                .OrderBy(s => s.ShellWeight)
                .ToArray();
            return JsonConvert.SerializeObject(shells, Formatting.Indented);
        }

        public static string ExportGuns(ArtilleryContext context, string manufacturer)
        {
            XmlHelper xmlHelper = new XmlHelper();
            IMapper mapper = InitializeAutoMapper();
            ExportGunDto[] exportGunDtos = context.Guns
                .Where(g => g.Manufacturer.ManufacturerName == manufacturer)
                .AsNoTracking()
                .ProjectTo<ExportGunDto>(mapper.ConfigurationProvider)
                .OrderBy(g => g.BarrelLength)
                .ToArray();
            return xmlHelper.Serialize<ExportGunDto[]>(exportGunDtos, "Guns");
        }
        private static IMapper InitializeAutoMapper()
            => new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ArtilleryProfile>();
            }));
    }
}
