using CatalogService.API.Core.Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.API.Infrastructure.Context
{
    public class CatalogContextSeed
    {

        public async Task SeedAsync(CatalogContext context, IWebHostEnvironment env, ILogger<CatalogContextSeed> logger)
        {
            var policy = Policy.Handle<SqlException>().
                WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                onRetry: (exception, timeSpan, retry, ctx) =>
                 {
                     logger.LogWarning($" Exception {exception} with message  detected on attempt {retry} of");
                 }
                );

            var setupDirPath = Path.Combine(env.ContentRootPath, "Infrastructure", "Setup", "SeedFiles");
            var picturePath = "Pics";

            await policy.ExecuteAsync(() => ProcessSeeding(context, setupDirPath, picturePath, logger));
        }




        private async Task ProcessSeeding(CatalogContext context, string setupDirPath, string picturePath, ILogger logger)
        {
            if (!context.CatalogBrands.Any())
            {
                await context.CatalogBrands.AddRangeAsync(GetCatalogBrandsFromFile(setupDirPath));

                await context.SaveChangesAsync();
            }

            if (!context.CatalogTypes.Any())
            {
                await context.CatalogTypes.AddRangeAsync(GetCatalogTypesFromFile(setupDirPath));

                await context.SaveChangesAsync();
            }

            if (!context.CatalogItems.Any())
            {
                await context.CatalogItems.AddRangeAsync(GetCatalogItemsFromFile(setupDirPath, context));

                await context.SaveChangesAsync();

                GetCatalogItemPicture(setupDirPath, picturePath);
            }
        }




        private IEnumerable<CatalogBrand> GetCatalogBrandsFromFile(string contentPath)
        {
            IEnumerable<CatalogBrand> GetPreconfiguredCatalogBrands()
            {
                return new List<CatalogBrand>()
                {
                    new CatalogBrand(){ Brand = "Azure"},
                    new CatalogBrand(){ Brand = ".NET"},
                    new CatalogBrand(){ Brand = "Vizual Studio"},
                    new CatalogBrand(){ Brand = "SQL Server"},
                    new CatalogBrand(){ Brand = "Other"}
                };
            }

            string fileName = Path.Combine(contentPath, "BrandsTextFile.txt");
            if (!File.Exists(fileName))
            {
                return GetPreconfiguredCatalogBrands();
            }

            var fileContent = File.ReadAllLines(fileName);

            var list = fileContent.Select(i => new CatalogBrand()
            {
                Brand = i.Trim('"')
            }).Where(i => i != null);

            return list ?? GetPreconfiguredCatalogBrands();
        }




        private IEnumerable<CatalogType> GetCatalogTypesFromFile(string contentPath)
        {
            IEnumerable<CatalogType> GetPreconfiguredCatalogTypes()
            {
                return new List<CatalogType>()
                {
                    new CatalogType(){ Type = "Mug"},
                    new CatalogType(){ Type = "T-Shirt"},
                    new CatalogType(){ Type = "Sheet"},
                    new CatalogType(){ Type = "USB Memory Stick"},
                };
            }

            string fileName = Path.Combine(contentPath, "CatalogTypes.txt");
            if (!File.Exists(fileName))
            {
                return GetPreconfiguredCatalogTypes();
            }

            var fileContent = File.ReadAllLines(fileName);

            var list = fileContent.Select(i => new CatalogType()
            {
                Type = i.Trim('"')
            }).Where(i => i != null);

            return list ?? GetPreconfiguredCatalogTypes();
        }



        private IEnumerable<CatalogItem> GetCatalogItemsFromFile(string contentPath, CatalogContext context)
        {
            IEnumerable<CatalogItem> GetPreconfiguredItems()
            {
                var list = new List<CatalogItem>();
                list.Add(new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 5, AvailableStock = 100, Description = "dsjadiljashdas", Name = "Item1", PictureFileName = "dsadasdas", PictureUrl = "dsadasdasdas", Price = 500 });
                list.Add(new CatalogItem { CatalogTypeId = 1, CatalogBrandId = 2, AvailableStock = 100, Description = "dsjadiljashdas", Name = "Item1", PictureFileName = "dsadasdas", PictureUrl = "dsadasdasdas", Price = 800 });
                list.Add(new CatalogItem { CatalogTypeId = 1, CatalogBrandId = 5, AvailableStock = 100, Description = "dsjadiljashdas", Name = "Item1", PictureFileName = "dsadasdas", PictureUrl = "dsadasdasdas", Price = 500 });
                list.Add(new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 2, AvailableStock = 100, Description = "dsjadiljashdas", Name = "Item1", PictureFileName = "dsadasdas", PictureUrl = "dsadasdasdas", Price = 300 });
                list.Add(new CatalogItem { CatalogTypeId = 3, CatalogBrandId = 2, AvailableStock = 100, Description = "dsjadiljashdas", Name = "Item1", PictureFileName = "dsadasdas", PictureUrl = "dsadasdasdas", Price = 200 });
                list.Add(new CatalogItem { CatalogTypeId = 2, CatalogBrandId = 5, AvailableStock = 100, Description = "dsjadiljashdas", Name = "Item1", PictureFileName = "dsadasdas", PictureUrl = "dsadasdasdas", Price = 600 });

                return list;
            }

            string filename = Path.Combine(contentPath, "CatalogItems.txt");

            if (!File.Exists(filename))
            {
                return GetPreconfiguredItems();
            }

            var catalogTypeIdLookup = context.CatalogTypes.ToDictionary(ct => ct.Type, ct => ct.Id);
            var catalogBrandIdLookup = context.CatalogBrands.ToDictionary(ct => ct.Brand, ct => ct.Id);

            var fileContent = File.ReadAllLines(filename)
                                  .Skip(1)
                                  .Select(x => x.Split(','))
                                  .Select(i => new CatalogItem()
                                  {
                                      CatalogTypeId = catalogTypeIdLookup[i[0]],
                                      CatalogBrandId = catalogBrandIdLookup[i[1]],
                                      Description = i[2].Trim('"').Trim(),
                                      Name = i[3].Trim('"').Trim().ToString(),
                                      Price = Decimal.Parse(i[4].Trim('"').Trim(), System.Globalization.NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture),
                                      PictureFileName = i[5].Trim('"').Trim().ToString(),
                                      AvailableStock = string.IsNullOrEmpty(i[6]) ? 0 : int.Parse(i[6]),
                                      OnReader = Convert.ToBoolean(i[7])
                                  });

            return fileContent;
        }



        private void GetCatalogItemPicture(string contentPath, string picturePath)
        {
            picturePath ??= "Pics";
            if (picturePath != null)
            {
                DirectoryInfo directory = new DirectoryInfo(picturePath);
                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }

                string zipFileCatalogItemPictures = Path.Combine(contentPath, "CatalogItems.zip");
                ZipFile.ExtractToDirectory(zipFileCatalogItemPictures, picturePath);
            }
        }




    }
}
