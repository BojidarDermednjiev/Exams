using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Artillery.DataProcessor
{
    using Artillery.Data.Models;
    using Artillery.Data.Models.Enums;
    using Data;

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
            throw new NotImplementedException();
        }
    }
}
