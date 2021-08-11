using Microsoft.EntityFrameworkCore;
using OnlineShopping.API.DbContexts;
using OnlineShopping.API.Entities;
using OnlineShopping.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace OnlineShopping.API.Services
{
    public interface IShopDBService
    {
        List<Item> ReadItems(string colName = "",string filterVal = "");
        List<BusinessPartner> ReadBusinessPartners(string colName = "", string filterVal = "");
        object AddDocument(DocumentDto model);
        bool DeleteDocument(DeleteDocumentDto model);
        DocumentInfoDto GetDocument(GetDocumentDto model);
        bool UpdateDocument(UpdateDocumentDto model);

    }
    public class ShopDBService : IShopDBService
    {
        private readonly ShopDbContext _context;

        public ShopDBService(ShopDbContext context)
        {
            _context = context;
        }

        public List<Item> ReadItems(string colName = "", string filterVal = "")
        {
            try
            {
                if (colName == string.Empty) return _context.Items.ToList();

                PropertyInfo info = typeof(Item).GetProperty(colName);
                if (info == null) return new List<Item>();

                string filter = GetBusinessPartnersFilter(info, filterVal);

                List<Item> list = _context.Items.FromSqlRaw("SELECT * FROM dbo.Items where " + filter).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return new List<Item>();
            }           
        }

        public List<BusinessPartner> ReadBusinessPartners(string colName = "", string filterVal = "")
        {
            try
            {
                if (colName == string.Empty) return _context.BusinessPartners.ToList();

                PropertyInfo info = typeof(Item).GetProperty(colName);
                if (info == null) return new List<BusinessPartner>();

                string filter = GetItemFilter(info, filterVal);

                List<BusinessPartner> list = _context.BusinessPartners.FromSqlRaw("SELECT * FROM dbo.BusinessPartners where " + filter).ToList();
                return list;
            }
            catch (Exception ex)
            {
                return new List<BusinessPartner>();
            }
        }

        public object AddDocument(DocumentDto model)
        {
            try
            {
                List<Item> ItemsList = new List<Item>();

                #region validation
                //check if the user exists
                User user = _context.Users.Where(x => x.UserName == model.CreatedBy && x.Active == true).FirstOrDefault();
                if (user == null)
                {
                    throw new Exception("User doesnt exist");
                }
                //check if the BP exists
                BusinessPartner bp = _context.BusinessPartners.Where(x => x.BPCode == model.BPCode && x.Active == true).FirstOrDefault();
                if (bp == null)
                {
                    throw new Exception("BP doesnt exist");
                }
                //validate doc type
                if (model.BPType != "C" && model.BPType != "V" ||
                   model.BPType == "C" && bp.BPTypeId == "V" ||
                   model.BPType == "V" && bp.BPTypeId == "C")
                {
                    throw new Exception("invalid document type");
                }

                //validate items
                if (model.Items == null || model.Items.Count == 0)
                {
                    throw new Exception("Item list cant be empty");
                }
                foreach (var item in model.Items)
                {
                    Item i = _context.Items.Where(x => x.ItemName == item.ItemName && x.Active == true).FirstOrDefault();
                    if (i == null)
                    {
                        throw new Exception("invalid Item. ItemName: " + item.ItemName);
                    }
                    ItemsList.Add(i);
                } 
                #endregion

                if (model.BPType == "C")
                {
                    SaleOrder saleOrder = new SaleOrder();

                    saleOrder.BusinessPartnerId = bp.BPCode;
                    saleOrder.CreateDate = DateTime.Now;
                    saleOrder.LastUpdateDate = null;
                    saleOrder.CreatedBy = user;
                    saleOrder.LastUpdatedBy = null;

                    _context.SaleOrders.Add(saleOrder);

                    //create SalesOrderLines
                    foreach (var item in model.Items)
                    {
                        SaleOrdersLine saleOrdersLine = new SaleOrdersLine();

                        saleOrdersLine.DocID = saleOrder;
                        saleOrdersLine.Itemcode = ItemsList.Where(x => x.ItemName == item.ItemName).First(); 
                        saleOrdersLine.Quantity = item.Quantity;
                        saleOrdersLine.CreateDate = DateTime.Now;
                        saleOrdersLine.LastUpdateDate = null;
                        saleOrdersLine.CreatedBy = user;
                        saleOrdersLine.LastUpdatedBy = null;

                        _context.SaleOrdersLines.Add(saleOrdersLine);

                        if (!string.IsNullOrWhiteSpace(item.Comment))
                        {
                            SaleOrdersLinesComment saleOrdersLinesComment = new SaleOrdersLinesComment();

                            saleOrdersLinesComment.DocID = saleOrder;
                            saleOrdersLinesComment.Lineid = saleOrdersLine;
                            saleOrdersLinesComment.Comment = item.Comment;

                            _context.SaleOrdersLinesComments.Add(saleOrdersLinesComment);
                        }
                    }
                    _context.SaveChanges();
                    return saleOrder;
                }
                else
                {
                    PurchaseOrder purchaseOrder = new PurchaseOrder();

                    purchaseOrder.BusinessPartnerId = bp.BPCode;
                    purchaseOrder.CreateDate = DateTime.Now;
                    purchaseOrder.LastUpdateDate = null;
                    purchaseOrder.CreatedBy = user;
                    purchaseOrder.LastUpdatedBy = null;

                    _context.PurchaseOrders.Add(purchaseOrder);

                    //create PurchaseOrderLines
                    foreach (var item in model.Items)
                    {
                        PurchaseOrdersLine purchaseOrdersLine = new PurchaseOrdersLine();

                        purchaseOrdersLine.DocID = purchaseOrder;
                        purchaseOrdersLine.Itemcode = ItemsList.Where(x => x.ItemName == item.ItemName).First();
                        purchaseOrdersLine.Quantity = item.Quantity;
                        purchaseOrdersLine.CreateDate = DateTime.Now;
                        purchaseOrdersLine.LastUpdateDate = null;
                        purchaseOrdersLine.CreatedBy = user;
                        purchaseOrdersLine.LastUpdatedBy = null;

                        _context.PurchaseOrdersLines.Add(purchaseOrdersLine);
                    }

                    _context.SaveChanges();
                    return purchaseOrder;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteDocument(DeleteDocumentDto model)
        {
            try
            {
                //check if the document exist
                if (model.DocType == "C")//SaleOrder
                {
                    SaleOrder saleOrder = _context.SaleOrders.Find(model.DocId);
                    if(saleOrder == null) { throw new Exception("document doesnt exist"); }
                    List<SaleOrdersLinesComment> commentsList = _context.SaleOrdersLinesComments.Where(x => x.DocID.ID == saleOrder.ID).ToList();
                    List<SaleOrdersLine> saleOrdersLinesList = _context.SaleOrdersLines.Where(x => x.SaleOrderId == saleOrder.ID).ToList();
                    _context.SaleOrdersLinesComments.RemoveRange(commentsList);
                    _context.SaleOrdersLines.RemoveRange(saleOrdersLinesList);
                    _context.SaleOrders.Remove(saleOrder);

                }
                else if (model.DocType == "V")//PurchaseOrder
                {
                    PurchaseOrder purchaseOrder = _context.PurchaseOrders.Find(model.DocId);
                    if (purchaseOrder == null) { throw new Exception("document doesnt exist"); }
                    List<PurchaseOrdersLine> purchaseOrdersLinesList = _context.PurchaseOrdersLines.Where(x => x.PurchaseOrderId == purchaseOrder.ID).ToList();
                    _context.PurchaseOrdersLines.RemoveRange(purchaseOrdersLinesList);
                    _context.PurchaseOrders.Remove(purchaseOrder);


                }
                else { throw new Exception("Invalid Document Type"); }

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }

        public DocumentInfoDto GetDocument(GetDocumentDto model)
        {

            try
            {
                DocumentInfoDto docDto = new DocumentInfoDto();
                if (model.DocType == "C")
                {
                    SaleOrder saleOrder = _context.SaleOrders.Where(x => x.ID == model.DocId).FirstOrDefault();

                    if(saleOrder == null) { throw new Exception("Document isnt exist"); }

                    BusinessPartner businessPartner = _context.BusinessPartners.Where(x => x.BPCode == saleOrder.BusinessPartnerId).First();

                    //docDto.FullName_createdBy = _context.Users.Where(x => x.ID == saleOrder.CreatedBy.ID).First().FullName;
                    docDto.SaleOrder = saleOrder;
                    docDto.BPName = businessPartner.BPName;
                    docDto.BPActive = businessPartner.Active;
                    docDto.SaleOrdersLines = _context.SaleOrdersLines.Where(x => x.SaleOrderId == saleOrder.ID).ToList();
                    docDto.SaleOrdersLinesComments = _context.SaleOrdersLinesComments.Where(x => x.DocID.ID == saleOrder.ID).ToList();

                }
                else if (model.DocType == "V")
                {
                    PurchaseOrder purchaseOrder = _context.PurchaseOrders.Where(x => x.ID == model.DocId).FirstOrDefault();
                    if (purchaseOrder == null) { throw new Exception("Document isnt exist"); }

                    BusinessPartner businessPartner = _context.BusinessPartners.Where(x => x.BPCode == purchaseOrder.BusinessPartnerId).First();

                    docDto.PurchaseOrder = purchaseOrder;
                    docDto.BPName = businessPartner.BPName;
                    docDto.BPActive = businessPartner.Active;
                    docDto.PurchaseOrdersLines = _context.PurchaseOrdersLines.Where(x => x.PurchaseOrderId == purchaseOrder.ID).ToList();


                } else { throw new Exception("Invalid Document type"); }

                return docDto;
            }
            catch (Exception)
            {

                throw;
            }

            

        }

        public bool UpdateDocument(UpdateDocumentDto model)
        {
            try
            {
                //check if the doc exist
                if(model.DocType == "C")
                {
                    SaleOrder saleOrder = _context.SaleOrders.Where(x => x.ID == model.DocId).FirstOrDefault();
                    if(saleOrder == null) { throw new Exception("Document doesnt exist"); }

                    User user = _context.Users.Where(x => x.UserName == model.UserName && x.Active == true).FirstOrDefault();
                    if (user == null) { throw new Exception("Invalid User"); }

                    BusinessPartner businessPartner = _context.BusinessPartners.Where(x => x.BPCode == model.BPCode && x.Active == true).FirstOrDefault();
                    if(businessPartner == null) { throw new Exception("BusinessPartner doesnt exist"); }
                    if(businessPartner.BPTypeId != "C") { throw new Exception("Invalid BusinessPartner"); }

                    //update Quantity
                    foreach (var i in model.Items)
                    {
                        Item itm = _context.Items.Where(x => x.ItemName == i.ItemName).FirstOrDefault();
                        if (itm != null)
                        {
                            SaleOrdersLine saleOrdersLine = _context.SaleOrdersLines.Where(x => x.SaleOrderId == model.DocId && x.ItemId == itm.ItemCode).FirstOrDefault();
                            if (saleOrdersLine != null)
                            {
                                saleOrdersLine.Quantity = i.Quantity;
                                saleOrdersLine.LastUpdateDate = DateTime.Now;
                                saleOrdersLine.LastUpdatedBy = user;
                            }
                        }
                    }

                    //UPDATE BusinessPartner
                    saleOrder.BusinessPartnerId = businessPartner.BPCode;
                    //update User
                    saleOrder.LastUpdateDate = DateTime.Now;
                    saleOrder.LastUpdatedBy = user;

                    _context.SaveChanges();

                }
                else if(model.DocType == "V")
                {
                    PurchaseOrder purchaseOrder  = _context.PurchaseOrders.Where(x => x.ID == model.DocId).FirstOrDefault();
                    if (purchaseOrder == null) { throw new Exception("Document doesnt exist"); }

                    User user = _context.Users.Where(x => x.UserName == model.UserName && x.Active == true).FirstOrDefault();
                    if (user == null) { throw new Exception("Invalid User"); }

                    BusinessPartner businessPartner = _context.BusinessPartners.Where(x => x.BPCode == model.BPCode && x.Active == true).FirstOrDefault();
                    if (businessPartner == null) { throw new Exception("BusinessPartner doesnt exist"); }
                    if (businessPartner.BPTypeId != "V") { throw new Exception("Invalid BusinessPartner"); }

                    //update Quantity
                    foreach (var i in model.Items)
                    {
                        Item itm = _context.Items.Where(x => x.ItemName == i.ItemName).FirstOrDefault();
                        if (itm != null)
                        {
                            PurchaseOrdersLine purchaseOrdersLine = _context.PurchaseOrdersLines.Where(x => x.PurchaseOrderId == model.DocId && x.ItemId == itm.ItemCode).FirstOrDefault();
                            if (purchaseOrdersLine != null)
                            {
                                purchaseOrdersLine.Quantity = i.Quantity;
                                purchaseOrdersLine.LastUpdateDate = DateTime.Now;
                                purchaseOrdersLine.LastUpdatedBy = user;
                            }
                        }
                    }

                    //UPDATE BusinessPartner
                    purchaseOrder.BusinessPartnerId = businessPartner.BPCode;
                    //update User
                    purchaseOrder.LastUpdateDate = DateTime.Now;
                    purchaseOrder.LastUpdatedBy = user;

                    _context.SaveChanges();

                }
                else { throw new Exception("Invalid Document type"); }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        private string GetItemFilter(PropertyInfo info, string filterValue)
        {
            string filterStr = string.Empty;
            switch (info.Name)
            {
                case "ItemCode":
                case "ItemName":
                    filterStr = string.Format("{0} = '{1}'", info.Name, filterValue);
                    break;
                case "Active":
                    filterStr = "Active = " + (Convert.ToBoolean(filterValue) ? "1" : "0");
                    break;

            }
            return filterStr;
        }

        private string GetBusinessPartnersFilter(PropertyInfo info, string filterValue)
        {
            string filterStr = string.Empty;
            switch (info.Name)
            {
                case "BPCode":
                case "BPName":
                case "BPType":
                    filterStr = string.Format("{0} = '{1}'", info.Name, filterValue);
                    break;
                case "Active":
                    filterStr = "Active = " + (Convert.ToBoolean(filterValue) ? "1" : "0");
                    break;

            }
            return filterStr;
        }

    }
}
