using System;
using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using ElasticsearchNet.Web.Entities;

namespace ElasticsearchNet.Web.Controllers
{
    [Route("api")]
    public class ApiController : Controller
    {
        private readonly IDatabase database;
        private readonly IMediator mediator;

        public ApiController(
            IDatabase database,
            IMediator mediator)
        {
            this.mediator = mediator;
            this.database = database;
        }

        // GET api values
        [HttpGet("{index}/{type}")]
        public async Task<IActionResult> Get(string index, string type)
        {
            var result = await this.mediator.Send(new GetEntitiesRequest(index, type));

            throw new Exception("Haha");

            return this.Ok(result);
        }

        // GET api/values/5
        [HttpGet("{index}/{type}/{id}")]
        public async Task<IActionResult> Get(string index, string type, string id)
        {
            return this.Ok(await this.mediator.Send(new GetEntityRequest(index, type, id)));
        }

        // POST api/values
        [HttpPost("{index}/{type}")]
        public async Task<IActionResult> Post(string index, string type, [FromBody]JObject content)
        {
            return this.Ok(await this.mediator.Send(new StoreEntitiesRequest(index, type, content)));
        }

        // PUT api/values/5
        [HttpPut("{index}/{type}/{id}")]
        public IActionResult Put(string index, string type, string id, [FromBody]JObject content)
        {
            if (this.database.StoreDocument(index, type, new Document(content)))
            {
                return this.Ok();
            }

            return this.StatusCode(500);
        }

        // DELETE api/values/5
        [HttpDelete("{index}/{type}/{id}")]
        public IActionResult Delete(string index, string type, string id)
        {
            if (this.database.RemoveDocument(index, type, id))
            {
                return this.Ok();
            }

            return this.StatusCode(500);
        }
    }

    public class Document : IDocument
    {
        public Document(JObject content)
        {
            this.Content = content;
        }

        public JObject Content { get; }
    }
}
