namespace WebApp.Domain.Models.CatalogModels
{
    public class CatalogItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal price { get; set; }
        public string PictureFileName { get; set; }
        public string PictureUrl { get; set; }



        public int CatalogTypeId { get; set; }
        public CatalogType CatalogType { get; set; }



        public int CatalogBrandId { get; set; }
        public CatalogBrand CatalogBrand { get; set; }


        public CatalogItem()
        {

        }
    }


}
