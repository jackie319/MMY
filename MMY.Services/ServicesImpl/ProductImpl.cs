using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JK.Framework.Core.Data;
using MMY.Data.Model;
using MMY.Services.IServices;

namespace MMY.Services.ServicesImpl
{
    public class ProductImpl:IProduct
    {
        private IRepository<Product> _productRepository;

        private IRepository<ProductV> _productVRepository;
        public ProductImpl(IRepository<Product> productRepository, IRepository<ProductV> productVRepository)
        {
            _productRepository = productRepository;
            _productVRepository = productVRepository;
        }
    }
}
