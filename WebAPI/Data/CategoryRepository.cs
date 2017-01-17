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
    public class CategoryRepository : DocumentDb
    {
        private static DocumentClient client;
        public static void Initialize()
        {
            client = new DocumentClient(new Uri(ConfigurationManager.AppSettings["endpoint"]), ConfigurationManager.AppSettings["authKey"]);
        }
        //each repo can specify it's own database and document collection
        public CategoryRepository() : base("TillPosdb", "Category") { }

        public Task<List<CategoryModel>> GetCategoryAsync()
        {
            return Task<List<CategoryModel>>.Run(() =>
                Client.CreateDocumentQuery<CategoryModel>(Collection.DocumentsLink)
                .ToList());
        }

        public Task<CategoryModel> GetCategoryAsync(string id)
        {
            return Task<CategoryModel>.Run(() =>
                Client.CreateDocumentQuery<CategoryModel>(Collection.DocumentsLink)
                .Where(p => p.Id == id)
                .AsEnumerable()
                .FirstOrDefault());
        }

        public Task<ResourceResponse<Document>> CreateCategory(CategoryModel CategoryModel)
        {
            return Client.CreateDocumentAsync(Collection.DocumentsLink, CategoryModel);
        }

        public Task<ResourceResponse<Document>> UpdateCategoryAsync(CategoryModel CategoryModel)
        {
            var doc = Client.CreateDocumentQuery<Document>(Collection.DocumentsLink)
                .Where(d => d.Id == CategoryModel.Id.ToString())
                .AsEnumerable() // why the heck do we need to do this??
                .FirstOrDefault();

            return Client.ReplaceDocumentAsync(doc.SelfLink, CategoryModel);
        }

        public Task<ResourceResponse<Document>> DeleteCategoryAsync(string id)
        {
            var doc = Client.CreateDocumentQuery<Document>(Collection.DocumentsLink)
                .Where(d => d.Id == id)
                .AsEnumerable()
                .FirstOrDefault();

            return Client.DeleteDocumentAsync(doc.SelfLink);
        }

    }
}