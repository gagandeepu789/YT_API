using YT_VillaApi.Models.Dto;

namespace YT_VillaApi.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> villalist = new List<VillaDTO>
        {


                new VillaDTO { Id = 1, Name = "pool View",sqft=100,Occupancy=4 },
                new VillaDTO { Id = 2, Name = "Beach view",sqft=123,Occupancy=6 }
          };
    }
}