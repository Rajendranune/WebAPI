using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class ProductRepository : DocumentDb
    {
        private static DocumentClient client;
        public static void Initialize()
        {
            client = new DocumentClient(new Uri(ConfigurationManager.AppSettings["endpoint"]), ConfigurationManager.AppSettings["authKey"]);
        }
        //each repo can specify it's own database and document collection
        public ProductRepository() : base("TillPosdb", "Product") { }

        public Task<List<ProductModel>> GetProductAsync()
        {
            return Task<List<ProductModel>>.Run(() =>
                Client.CreateDocumentQuery<ProductModel>(Collection.DocumentsLink)
                .ToList());
        }

        public Task<ProductModel> GetProductAsync(string id)
        {
            return Task<CategoryModel>.Run(() =>
                Client.CreateDocumentQuery<ProductModel>(Collection.DocumentsLink)
                .Where(p => p.Id == id)
                .AsEnumerable()
                .FirstOrDefault());
        }
        public Task<ProductModel> CheckSubCategoryAsync(string id)
        {
            return Task<CategoryModel>.Run(() =>
                Client.CreateDocumentQuery<ProductModel>(Collection.DocumentsLink)
                .Where(p => p.SubCategoryid == id)
                .AsEnumerable()
                .FirstOrDefault());
        }

        public Task<ResourceResponse<Document>> CreateProduct(ProductModel Model)
        {
            return Client.CreateDocumentAsync(Collection.DocumentsLink, Model);
        }

        public Task<ResourceResponse<Document>> UpdateProductAsync(ProductModel Model)
        {
            var doc = Client.CreateDocumentQuery<Document>(Collection.DocumentsLink)
                .Where(d => d.Id == Model.Id.ToString())
                .AsEnumerable() // why the heck do we need to do this??
                .FirstOrDefault();

            return Client.ReplaceDocumentAsync(doc.SelfLink, Model);
        }

        public Task<ResourceResponse<Document>> DeleteProductAsync(string id)
        {
            var doc = Client.CreateDocumentQuery<Document>(Collection.DocumentsLink)
                .Where(d => d.Id == id)
                .AsEnumerable()
                .FirstOrDefault();

            return Client.DeleteDocumentAsync(doc.SelfLink);
        }
    }
}