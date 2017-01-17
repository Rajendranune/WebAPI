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
    public class SubCategoryRepository : DocumentDb
    {
        private static DocumentClient client;
        public static void Initialize()
        {
            client = new DocumentClient(new Uri(ConfigurationManager.AppSettings["endpoint"]), ConfigurationManager.AppSettings["authKey"]);
        }
        //each repo can specify it's own database and document collection
        public SubCategoryRepository() : base("TillPosdb", "SubCategory") { }

        public Task<List<SubCategoryModel>> GetSubCategoryAsync()
        {
            return Task<List<SubCategoryModel>>.Run(() =>
                Client.CreateDocumentQuery<SubCategoryModel>(Collection.DocumentsLink)
                .ToList());
        }

        public Task<SubCategoryModel> GetSubCategoryAsync(string id)
        {
            return Task<SubCategoryModel>.Run(() =>
                Client.CreateDocumentQuery<SubCategoryModel>(Collection.DocumentsLink)
                .Where(p => p.Id == id)
                .AsEnumerable()
                .FirstOrDefault());
        }
        public Task<SubCategoryModel> CheckCategoryExistAsyn(string id)
        {
            return Task<SubCategoryModel>.Run(() =>
                Client.CreateDocumentQuery<SubCategoryModel>("dbs/pM9FAA==/colls/pM9FAIgIaAQ=/docs/")
                .Where(p => p.Categoryid == id)
                .AsEnumerable()
                .FirstOrDefault());
        }
        public Task<ResourceResponse<Document>> SubCreateCategory(SubCategoryModel Model)
        {
            return Client.CreateDocumentAsync(Collection.DocumentsLink, Model);
        }

        public Task<ResourceResponse<Document>> UpdateSubCategoryAsync(SubCategoryModel Model)
        {
            var doc = Client.CreateDocumentQuery<Document>(Collection.DocumentsLink)
                .Where(d => d.Id == Model.Id.ToString())
                .AsEnumerable() // why the heck do we need to do this??
                .FirstOrDefault();

            return Client.ReplaceDocumentAsync(doc.SelfLink, Model);
        }

        public Task<ResourceResponse<Document>> DeleteSubCategoryAsync(string id)
        {
            var doc = Client.CreateDocumentQuery<Document>(Collection.DocumentsLink)
                .Where(d => d.Id == id)
                .AsEnumerable()
                .FirstOrDefault();

            return Client.DeleteDocumentAsync(doc.SelfLink);
        }
        
    }
}