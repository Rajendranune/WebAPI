using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace WebAPI.Data
{
    public class responseRepository :  DocumentDb
    {
        private static DocumentClient client;
        public static void Initialize()
        {
            client = new DocumentClient(new Uri(ConfigurationManager.AppSettings["endpoint"]), ConfigurationManager.AppSettings["authKey"]);
        }
        //each repo can specify it's own database and document collection
        public responseRepository() : base("TestDb", "People") { }

        public Task<List<response>> GetPeopleAsync()
        {
            return Task<List<response>>.Run(() => 
                Client.CreateDocumentQuery<response>(Collection.DocumentsLink)
                .ToList());
        }

        public Task<response> GetresponseAsync(string id)
        {
            return Task<response>.Run(() => 
                Client.CreateDocumentQuery<response>(Collection.DocumentsLink)
                .Where(p => p.Id == id)
                .AsEnumerable()
                .FirstOrDefault());
        }

        public Task<ResourceResponse<Document>> Createresponse(response response)
        {
            return Client.CreateDocumentAsync(Collection.DocumentsLink, response);
        }

        public Task<ResourceResponse<Document>> UpdateresponseAsync(response response)
        {
            var doc = Client.CreateDocumentQuery<Document>(Collection.DocumentsLink)
                .Where(d => d.Id == response.Id)
                .AsEnumerable() // why the heck do we need to do this??
                .FirstOrDefault();

            return Client.ReplaceDocumentAsync(doc.SelfLink, response);
        }

        public Task<ResourceResponse<Document>> DeleteresponseAsync(string id)
        {
            var doc = Client.CreateDocumentQuery<Document>(Collection.DocumentsLink)
                .Where(d => d.Id == id)
                .AsEnumerable()
                .FirstOrDefault();

            return Client.DeleteDocumentAsync(doc.SelfLink);
        }

        public Task<List<response>> GetPeopleByLastNameAsync(string lastName)
        {
            return Task.Run(() =>
                Client.CreateDocumentQuery<response>(Collection.DocumentsLink)
                .Where(p => p.LastName == lastName)
                .ToList());
        }
    }
}