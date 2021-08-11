using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.API.Entities;
using OnlineShopping.API.Models;
using OnlineShopping.API.Services;

namespace OnlineShopping.API.Controllers
{
    [Route("api/test")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IShopDBService _ShopDBService;

        public MainController(IShopDBService itemService)
        {
            _ShopDBService = itemService;
        }

        [HttpGet("Items")]
        public ActionResult<List<Item>> GetItems([FromQuery] string colname = "", [FromQuery] string colval = "")
        {
            return Ok(_ShopDBService.ReadItems(colname, colval));
        }

        [HttpGet("BusinessPartners")]
        public ActionResult<List<BusinessPartner>> GetBusinessPartners([FromQuery] string colname = "", [FromQuery] string colval = "")
        {
            return  Ok(_ShopDBService.ReadBusinessPartners(colname, colval));
        }

        [HttpPost("Document")]
        public ActionResult<List<BusinessPartner>> AddDocument([FromBody] DocumentDto doc)
        {
            try
            {
                object createdDoc = _ShopDBService.AddDocument(doc);

                int createdDocId = createdDoc is SaleOrder ? ((SaleOrder)createdDoc).ID : ((PurchaseOrder)createdDoc).ID;

                return CreatedAtRoute("GetDocument", new GetDocumentDto { DocId = createdDocId, DocType = doc.BPType }, createdDoc);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpDelete("Document")]
        public ActionResult<bool> DeleteDocument([FromBody] DeleteDocumentDto doc)
        {
            try
            {
                return Ok(_ShopDBService.DeleteDocument(doc));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpGet("Document" , Name = "GetDocument")]
        public ActionResult<bool> GetDocument([FromQuery] GetDocumentDto doc)
        {
            try
            {
                return Ok(_ShopDBService.GetDocument(doc));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpPut("Document")]
        public ActionResult<bool> UpdateDocument([FromBody] UpdateDocumentDto doc)
        {
            try
            {
                return Ok(_ShopDBService.UpdateDocument(doc));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }





    }
}
