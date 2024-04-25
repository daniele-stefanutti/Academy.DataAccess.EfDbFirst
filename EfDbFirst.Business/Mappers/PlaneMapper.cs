using EfDbFirst.Business.Dtos;
using EfDbFirst.DataAccess.Models;

namespace EfDbFirst.Business.Mappers;

internal static class PlaneMapper
{
    public static PlaneDto Map(PlaneDetail planeDetail)
        => new
        (
            ManufacturerName: planeDetail.ModelNumberNavigation.ManufacturerName,
            ModelNumber: planeDetail.ModelNumber,
            RegistrationNo: planeDetail.RegistrationNo
        );
}
