using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMY.Data.Model;
using MMY.Services.ServiceModel;

namespace MMY.Services.IServices
{
    public interface IProduct
    {
        IList<ProductV> GetProductVs(string productName, Guid? categoryGuid, ProductStatusEnum? status,
            bool? isSpecialOffer, bool? isRecommended,
            DateTime? timeCreatedBegin, DateTime? timeCreatedEnd, int skip, int take, out int total);

        IList<ProductV> GetAdminProductVs(string productName, Guid? categoryGuid, ProductStatusEnum? status,
            bool? isSpecialOffer, bool? isRecommended,
            DateTime? timeCreatedBegin, DateTime? timeCreatedEnd, int skip, int take, out int total);
        void  CreatedProduct(Product product, IList<ProductClassification> classifications, IList<ProductAlbum> albums);

        void UpdateProduct(Product product, IList<ProductClassification> classifications, IList<ProductAlbum> albums);

        void OnShelf(Guid productGuid);
        void OffShelf(Guid productGuid);

        void Delete(Guid productGuid);

        IList<ProductPurchaseRecords> GetProductPurchaseRecords(Guid? productGuid, Guid? supplierGuid, int skip,
            int take, out int total);

        void AddPurchaseRecords(ProductPurchaseRecords records);

        Product FindProduct(Guid productGuid);
        IList<Product> FindProducts(IList<Guid> productGuids);
        IList<ProductClassification> GetClassifications(Guid productGuid);
        IList<ProductAlbum> GetAlbums(Guid productGuid);
        IList<ProductV> HotList(int skip, int take, out int total);
        IList<ProductV> NewList(int skip, int take, out int total);
    }
}
