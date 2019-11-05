//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace BrokerageAccountApi.ClientSearch
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ClientSearchController : ControllerBase
//    {
//        /// <summary>
//        /// Gets a list of all clients
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet]
//        public IActionResult Get()
//        {
//            var results = new List<ClientSearchResultModel>()
//            {
//                new ClientSearchResultModel() { ClientId = 1, FirstName = "Carrie", LastName = "Wilson", StreetAddress = "120 Cedar Ave", City = "Milwaukee", StateCode = "WI", ZipCode = "53202", ActiveAccounts = 2, CurrentAccountsValue = 492_828 },
//                new ClientSearchResultModel() { ClientId = 1, FirstName = "Terry", LastName = "Bradley", StreetAddress = "3382 Meadow Ave", City = "Green Bay", StateCode = "WI", ZipCode = "55382", ActiveAccounts = 1, CurrentAccountsValue =  117_282},
//                new ClientSearchResultModel() { ClientId = 1, FirstName = "Sam", LastName = "Montgomery", StreetAddress = "557 Oak Terrace Rd", City = "Whitefish Bay", StateCode = "WI", ZipCode = "53202", ActiveAccounts = 1, CurrentAccountsValue = 212_432 },
//                new ClientSearchResultModel() { ClientId = 1, FirstName = "Anne", LastName = "Harrison", StreetAddress = "2382 Jefferson Ave", City = "Shorewood", StateCode = "WI", ZipCode = "53202", ActiveAccounts = 1, CurrentAccountsValue = 302_345 },
//                new ClientSearchResultModel() { ClientId = 1, FirstName = "Reggie", LastName = "McGee", StreetAddress = "7392 Fairway Ave", City = "Northbrook", StateCode = "IL", ZipCode = "60062", ActiveAccounts = 1, CurrentAccountsValue = 402_218 },
//                new ClientSearchResultModel() { ClientId = 1, FirstName = "Jason", LastName = "McNamara", StreetAddress = "1108 Rose Ave", City = "Glenview", StateCode = "IL", ZipCode = "61082", ActiveAccounts = 1, CurrentAccountsValue = 188_638 },
//                new ClientSearchResultModel() { ClientId = 1, FirstName = "Kimberly", LastName = "Bauer", StreetAddress = "3534 Pine Meadow Ave", City = "Niles", StateCode = "IL", ZipCode = "61372", ActiveAccounts = 3, CurrentAccountsValue = 763_698 },
//                new ClientSearchResultModel() { ClientId = 1, FirstName = "Ted", LastName = "Thomas", StreetAddress = "1120 Hill Ave", City = "Glenview", StateCode = "IL", ZipCode = "61087", ActiveAccounts = 1, CurrentAccountsValue = 59_123 },
//                new ClientSearchResultModel() { ClientId = 1, FirstName = "Nick", LastName = "Jones", StreetAddress = "4520 Rock Ave", City = "Evanston", StateCode = "IL", ZipCode = "62829", ActiveAccounts = 1, CurrentAccountsValue = 132_356 }
//            };

//            return Ok(results);
//        }

//        // GET: api/ClientSearch/5
//        [HttpGet("{id}", Name = "Get")]
//        public string Get(int id)
//        {
//            return "value";
//        }

//        // POST: api/ClientSearch
//        [HttpPost]
//        public void Post([FromBody] string value)
//        {
//        }

//        // PUT: api/ClientSearch/5
//        [HttpPut("{id}")]
//        public void Put(int id, [FromBody] string value)
//        {
//        }

//        // DELETE: api/ApiWithActions/5
//        [HttpDelete("{id}")]
//        public void Delete(int id)
//        {
//        }
//    }
//}
