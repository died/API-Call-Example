using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleTest
{
    #region adidas xml model
    #region Po Creation (Step 1)
    [XmlRoot(ElementName = "PurchaseOrder")]
    public class PurchaseOrder : BaseOrderBody
    {
        [XmlElement(ElementName = "POPurpose")]
        public string PoPurpose { get; set; }
        [XmlElement(ElementName = "originalPOIssueDate")]
        public DateTime OriginalPoIssueDate { get; set; }
        [XmlElement(ElementName = "adidasPODownloadDate")]
        public DateTime AdidasPoDownloadDate { get; set; }
        [XmlElement(ElementName = "currency")]
        public string Currency { get; set; }
        [XmlElement(ElementName = "paymentTerms")]
        public string PaymentTerms { get; set; }
        [XmlElement(ElementName = "actualManufacturer")]
        public ActualManufacturer ActualManufacturer { get; set; }
        [XmlElement(ElementName = "shipTo")]
        public ShipTo ShipTo { get; set; }
        [XmlElement(ElementName = "T1Customer")]
        public string T1Customer { get; set; }
        [XmlElement(ElementName = "ShippingInformation")]
        public ShippingInformation ShippingInformation { get; set; }
        [XmlElement(ElementName = "headerExtension")]
        public string HeaderExtension { get; set; }
        [XmlElement(ElementName = "POLineItem")]
        public List<PoLineItem> PoLineItem { get; set; }
    }
    #endregion

    #region Po Confirmation (Step 2)

    [XmlRoot(ElementName = "POResponseAndAmendment")]
    public class PoConfirmation : BaseOrderBody
    {
        [XmlElement(ElementName = "POLineItem")]
        public List<ConfirmationPoLineItem> PoLineItem { get; set; }
    }
    #endregion

    #region T1 PO Amendment (Step 3)
    //Same as Po Creation (Step 1)
    #endregion

    #region T1 PO Amendment Confirmation (Step 4)
    [XmlRoot(ElementName = "POResponseAndAmendment")]
    public class T1PoAmendmentConfirmation : BaseOrderBody
    {
        [XmlElement(ElementName = "POLineItem")]
        public List<AmendmentConfirmationPoLineItem> PoLineItem { get; set; }
    }
    #endregion

    #region T2 PO Amendment (Step 5)
    [XmlRoot(ElementName = "POResponseAndAmendment")]
    public class T2PoAmendment : BaseOrderBody
    {
        [XmlElement(ElementName = "POLineItem")]
        public List<T2AmendmentPoLineItem> PoLineItem { get; set; }
    }
    #endregion

    #region T2 Po Amendment Acknowledgement (Step 6)
    [XmlRoot(ElementName = "Acknowledgement")]
    public class Acknowledgement : BasePostBody
    {
        [XmlElement(ElementName = "AcknowledgementPurpose")]
        public string AcknowledgementPurpose { get; set; }
    }
    #endregion

    #region PO Cancellation (Step 7)
    [XmlRoot(ElementName = "POCancellation")]
    public class PoCancellation : BasePostBody
    {
        [XmlElement(ElementName = "POCancellationPurpose")]
        public string PoCancellationPurpose { get; set; }
        [XmlElement(ElementName = "originalPOIssueDate")]
        public string OriginalPoIssueDate { get; set; }
    }
    #endregion

    #region Cancellation Confirmation (Step 8)
    //same as T2 Po Amendment Acknowledgement (Step 6)
    #endregion

    #region Shared Class
    [XmlRoot(ElementName = "sender")]
    public class Sender
    {
        [XmlElement(ElementName = "senderName")]
        public string SenderName { get; set; }
        [XmlElement(ElementName = "senderPerson")]
        public string SenderPerson { get; set; }
    }

    [XmlRoot(ElementName = "buyer")]
    public class Buyer
    {
        [XmlElement(ElementName = "buyerName")]
        public string BuyerName { get; set; }
        [XmlElement(ElementName = "buyerAddressLine1")]
        public string BuyerAddressLine1 { get; set; }
        [XmlElement(ElementName = "buyerAddressLine2")]
        public string BuyerAddressLine2 { get; set; }
        [XmlElement(ElementName = "buyerAddressLine3")]
        public string BuyerAddressLine3 { get; set; }
        [XmlElement(ElementName = "buyerAddressLine4")]
        public string BuyerAddressLine4 { get; set; }
        [XmlElement(ElementName = "buyerCity")]
        public string BuyerCity { get; set; }
        [XmlElement(ElementName = "buyerProvince")]
        public string BuyerProvince { get; set; }
        [XmlElement(ElementName = "buyerCountryCode")]
        public string BuyerCountryCode { get; set; }
        [XmlElement(ElementName = "buyerPostalCode")]
        public string BuyerPostalCode { get; set; }
        [XmlElement(ElementName = "buyerContactPerson")]
        public string BuyerContactPerson { get; set; }
        [XmlElement(ElementName = "buyerContactTelNumber")]
        public string BuyerContactTelNumber { get; set; }
        [XmlElement(ElementName = "buyerContactEmail")]
        public string BuyerContactEmail { get; set; }
    }

    [XmlRoot(ElementName = "actualManufacturer")]
    public class ActualManufacturer
    {
        [XmlElement(ElementName = "actualManufacturerCode")]
        public string ActualManufacturerCode { get; set; }
        [XmlElement(ElementName = "actualManufacturerName")]
        public string ActualManufacturerName { get; set; }
        [XmlElement(ElementName = "actualManufacturerAddressLine1")]
        public string ActualManufacturerAddressLine1 { get; set; }
        [XmlElement(ElementName = "actualManufacturerAddressLine2")]
        public string ActualManufacturerAddressLine2 { get; set; }
        [XmlElement(ElementName = "actualManufacturerAddressLine3")]
        public string ActualManufacturerAddressLine3 { get; set; }
        [XmlElement(ElementName = "actualManufacturerAddressLine4")]
        public string ActualManufacturerAddressLine4 { get; set; }
        [XmlElement(ElementName = "actualManufacturerCity")]
        public string ActualManufacturerCity { get; set; }
        [XmlElement(ElementName = "actualManufacturerProvince")]
        public string ActualManufacturerProvince { get; set; }
        [XmlElement(ElementName = "actualManufacturerCountryCode")]
        public string ActualManufacturerCountryCode { get; set; }
        [XmlElement(ElementName = "actualManufacturerPostalCode")]
        public string ActualManufacturerPostalCode { get; set; }
    }

    [XmlRoot(ElementName = "shipTo")]
    public class ShipTo
    {
        [XmlElement(ElementName = "shipToCode")]
        public string ShipToCode { get; set; }
        [XmlElement(ElementName = "shipToName")]
        public string ShipToName { get; set; }
        [XmlElement(ElementName = "shipToAddressLine1")]
        public string ShipToAddressLine1 { get; set; }
        [XmlElement(ElementName = "shipToAddressLine2")]
        public string ShipToAddressLine2 { get; set; }
        [XmlElement(ElementName = "shipToAddressLine3")]
        public string ShipToAddressLine3 { get; set; }
        [XmlElement(ElementName = "shipToAddressLine4")]
        public string ShipToAddressLine4 { get; set; }
        [XmlElement(ElementName = "shipToCity")]
        public string ShipToCity { get; set; }
        [XmlElement(ElementName = "shipToProvince")]
        public string ShipToProvince { get; set; }
        [XmlElement(ElementName = "shipToCountryCode")]
        public string ShipToCountryCode { get; set; }
        [XmlElement(ElementName = "shipToPostalCode")]
        public string ShipToPostalCode { get; set; }
    }

    [XmlRoot(ElementName = "ShippingInformation")]
    public class ShippingInformation
    {
        [XmlElement(ElementName = "forwarder")]
        public string Forwarder { get; set; }
        [XmlElement(ElementName = "incoterm")]
        public string Incoterm { get; set; }
        [XmlElement(ElementName = "shipMode")]
        public string ShipMode { get; set; }
        [XmlElement(ElementName = "countryOfOrigin")]
        public string CountryOfOrigin { get; set; }
        [XmlElement(ElementName = "packingInstruction")]
        public string PackingInstruction { get; set; }
        [XmlElement(ElementName = "shippingInstruction")]
        public string ShippingInstruction { get; set; }
    }

    [XmlRoot(ElementName = "materialDescriptor")]
    public class MaterialDescriptor
    {
        [XmlElement(ElementName = "materialDescription")]
        public string MaterialDescription { get; set; }
        [XmlElement(ElementName = "materialColor")]
        public string MaterialColor { get; set; }
        [XmlElement(ElementName = "sustainableMaterialComposition")]
        public List<string> SustainableMaterialComposition { get; set; }
        [XmlElement(ElementName = "colorMatchingNote")]
        public string ColorMatchingNote { get; set; }
        [XmlElement(ElementName = "materialSectionName")]
        public string MaterialSectionName { get; set; }
        [XmlElement(ElementName = "weightValue")]
        public decimal WeightValue { get; set; }
        [XmlElement(ElementName = "weightUOM")]
        public string WeightUom { get; set; }
        [XmlElement(ElementName = "widthValue")]
        public decimal WidthValue { get; set; }
        [XmlElement(ElementName = "widthUOM")]
        public string WidthUom { get; set; }
        [XmlElement(ElementName = "thicknessValue")]
        public decimal ThicknessValue { get; set; }
        [XmlElement(ElementName = "thicknessUOM")]
        public string ThicknessUom { get; set; }
        [XmlElement(ElementName = "lengthValue")]
        public decimal LengthValue { get; set; }
        [XmlElement(ElementName = "lengthUOM")]
        public string LengthUom { get; set; }
        [XmlElement(ElementName = "heightValue")]
        public decimal HeightValue { get; set; }
        [XmlElement(ElementName = "heightUOM")]
        public string HeightUom { get; set; }
        [XmlElement(ElementName = "size")]
        public string Size { get; set; }
    }

    [XmlRoot(ElementName = "POLineItem")]
    public class PoLineItem : BasePoLineItem
    {
        [XmlElement(ElementName = "changePurposeLineStatus")]
        public string ChangePurposeLineStatus { get; set; }
        [XmlElement(ElementName = "adidasOrderNumber")]
        public string AdidasOrderNumber { get; set; }
        [XmlElement(ElementName = "adidasArticleNumber")]
        public string AdidasArticleNumber { get; set; }
        [XmlElement(ElementName = "adidasWorkingNumber")]
        public string AdidasWorkingNumber { get; set; }
        [XmlElement(ElementName = "adidasRequestDate")]
        public DateTime AdidasRequestDate { get; set; }
        [XmlElement(ElementName = "adidasPlanDate")]
        public DateTime AdidasPlanDate { get; set; }
        [XmlElement(ElementName = "priorityAndOrderType")]
        public string PriorityAndOrderType { get; set; }
        [XmlElement(ElementName = "orderQty")]
        public decimal OrderQty { get; set; }
        [XmlElement(ElementName = "orderUOM")]
        public string OrderUom { get; set; }
        [XmlElement(ElementName = "season")]
        public string Season { get; set; }
        [XmlElement(ElementName = "materialDescriptor")]
        public MaterialDescriptor MaterialDescriptor { get; set; }
        [XmlElement(ElementName = "unitPrice")]
        public decimal UnitPrice { get; set; }
        [XmlElement(ElementName = "lineExtension1")]
        public string LineExtension1 { get; set; }
        [XmlElement(ElementName = "lineExtension2")]
        public string LineExtension2 { get; set; }
        [XmlElement(ElementName = "lineExtension3")]
        public string LineExtension3 { get; set; }
        [XmlElement(ElementName = "lineExtension4")]
        public string LineExtension4 { get; set; }
        [XmlElement(ElementName = "lineExtension5")]
        public string LineExtension5 { get; set; }
    }

    [XmlRoot(ElementName = "POLineItem")]
    public class ConfirmationPoLineItem : BasePoLineItem
    {
        [XmlElement(ElementName = "confirmedDeliveryDate")]
        public DateTime ConfirmedDeliveryDate { get; set; }
        [XmlElement(ElementName = "confirmedDeliveryQty")]
        public decimal ConfirmedDeliveryQty { get; set; }
    }

    public class AmendmentConfirmationPoLineItem : ConfirmationPoLineItem
    {
        [XmlElement(ElementName = "updatedDeliveryDate")]
        public DateTime UpdatedDeliveryDate { get; set; }
        [XmlElement(ElementName = "updatedDeliveryQty")]
        public decimal UpdatedDeliveryQty { get; set; }
    }

    public class T2AmendmentPoLineItem : AmendmentConfirmationPoLineItem
    {
        [XmlElement(ElementName = "lastShipmentUpdatedDeliveryDate")]
        public DateTime LastShipmentUpdatedDeliveryDate { get; set; }
        [XmlElement(ElementName = "lastShipmentUpdatedDeliveryQty")]
        public decimal LastShipmentUpdatedDeliveryQty { get; set; }
    }

    #region Base class
    public class BasePostBody
    {
        [XmlElement(ElementName = "PONumber")]
        public string PoNumber { get; set; }
        [XmlElement(ElementName = "POVersionNumber")]
        public int PoVersionNumber { get; set; }
        [XmlElement(ElementName = "POResponseVersionNumber")]
        public int PoResponseVersionNumber { get; set; }
        [XmlElement(ElementName = "messageGenerationDateTime")]
        public DateTime MessageGenerationDateTime { get; set; }
        [XmlElement(ElementName = "latestUpdateDateTime")]
        public DateTime LatestUpdateDateTime { get; set; }
        [XmlElement(ElementName = "headerRemarks")]
        public string HeaderRemarks { get; set; }
        [XmlElement(ElementName = "sender")]
        public Sender Sender { get; set; }
        [XmlElement(ElementName = "recipient")]
        public string Recipient { get; set; }
        //[XmlElement(ElementName = "buyerName")]
        //public string BuyerName { get; set; }
        [XmlElement(ElementName = "sellerName")]
        public string SellerName { get; set; }
        //[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        //public string Xsi { get; set; }
        [XmlAttribute(AttributeName = "noNamespaceSchemaLocation", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
        public string NoNamespaceSchemaLocation { get; set; }
    }

    public class BaseOrderBody : BasePostBody
    {
        [XmlElement(ElementName = "POResponsePurpose")]
        public string PoResponsePurpose { get; set; }
        [XmlElement(ElementName = "buyer")]
        public Buyer Buyer { get; set; }
    }
    public class BasePoLineItem
    {
        [XmlElement(ElementName = "lineItemNumber")]
        public int LineItemNumber { get; set; }
        [XmlElement(ElementName = "requestDate")]
        public DateTime RequestDate { get; set; }
        [XmlElement(ElementName = "lineRemarks")]
        public string LineRemarks { get; set; }
        [XmlElement(ElementName = "materialReferenceNumber")]
        public string MaterialReferenceNumber { get; set; }
    }
    #endregion

    #endregion
    #endregion

    #region api model
    /// <summary>
    /// Po Creation
    /// </summary>
    public class PoCreation : BasePo
    {
        /// <summary>
        /// Order item's count 
        /// </summary>
        public int OrderItemCount { get; set; }
        /// <summary>
        /// Total order's quantity 
        /// </summary>
        public decimal OrderQuantity { get; set; }
    }

    /// <summary>
    /// Po Confirmation by T2
    /// </summary>
    public class ApiPoConfirmation : PoCreation
    {
    }

    /// <summary>
    /// T1 issued Po Amendment
    /// </summary>
    public class PoT1Amendment : PoCreation
    {
        /// <summary>
        /// How many item are change
        /// </summary>
        public int ModifyCount { get; set; }
    }

    /// <summary>
    /// T1 issued Po Amendment Confirmation by T2
    /// </summary>
    public class PoT1AmendmentConfirmation : PoT1Amendment
    {

    }

    /// <summary>
    /// T2 issued Po Amendment
    /// </summary>
    public class PoT2Amendment : PoT1Amendment
    {

    }

    /// <summary>
    /// T2 issued Po Amendment Confirmation by T1
    /// </summary>
    public class PoT2AmendmentAcknowledgement : BasePo
    {

    }

    /// <summary>
    /// Fill column of PO
    /// </summary>
    public class FullPo
    {
        #region attr
        /// <summary>
        /// If only one order
        /// </summary>
        public List<string> PoNumber { get; set; }
        /// <summary>
        /// Order body's type : XML or JSON
        /// </summary>
        public string FileType { get; set; }
        /// <summary>
        /// Order body
        /// </summary>
        public string OrderBody { get; set; }
        /// <summary>
        /// Order item's count 
        /// </summary>
        public int OrderItemCount { get; set; }
        /// <summary>
        /// Total order's quantity 
        /// </summary>
        public decimal OrderQuantity { get; set; }
        /// <summary>
        /// MD5 for makesure body didn't damage
        /// </summary>
        public string CheckSum { get; set; }
        /// <summary>
        /// How many item are change
        /// </summary>
        public int ModifyCount { get; set; }
        #endregion

        #region convert
        #endregion
        /// <summary>
        /// convert PoCreation
        /// </summary>
        /// <param name="b"></param>
        public static explicit operator FullPo(PoCreation b)  // explicit byte to digit conversion operator
        {
            var d = new FullPo
            {
                CheckSum = b.CheckSum,
                FileType = b.FileType,
                OrderBody = b.OrderBody,
                OrderItemCount = b.OrderItemCount,
                OrderQuantity = b.OrderQuantity,
                PoNumber = b.PoNumber
            }; // explicit conversion
            return d;
        }
        /// <summary>
        /// convert PoT1Amendment
        /// </summary>
        /// <param name="b"></param>
        public static explicit operator FullPo(PoT1Amendment b)  // explicit byte to digit conversion operator
        {
            var d = new FullPo
            {
                CheckSum = b.CheckSum,
                FileType = b.FileType,
                OrderBody = b.OrderBody,
                OrderItemCount = b.OrderItemCount,
                OrderQuantity = b.OrderQuantity,
                PoNumber = b.PoNumber,
                ModifyCount = b.ModifyCount
            }; // explicit conversion
            return d;
        }

        /// <summary>
        /// convert ApiPoT2AmendmentAcknowledgement
        /// </summary>
        /// <param name="b"></param>
        public static explicit operator FullPo(PoT2AmendmentAcknowledgement b)
        {
            var d = new FullPo
            {
                CheckSum = b.CheckSum,
                FileType = b.FileType,
                OrderBody = b.OrderBody,
                PoNumber = b.PoNumber
            };
            return d;
        }
    }

    /// <summary>
    /// Po Cancellation, cancel entire order
    /// </summary>
    public class ApiPoCancellation : BasePo
    {

    }

    /// <summary>
    /// Po Cancellation Confirmation
    /// </summary>
    public class PoCancellationConfirmation : BasePo
    {

    }

    /// <summary>
    /// Base class
    /// </summary>
    public class BasePo
    {
        /// <summary>
        /// If only one order
        /// </summary>
        public List<string> PoNumber { get; set; }
        /// <summary>
        /// Order body's type : XML or JSON
        /// </summary>
        public string FileType { get; set; }
        /// <summary>
        /// Order body
        /// </summary>
        public string OrderBody { get; set; }
        /// <summary>
        /// MD5 for makesure body didn't damage
        /// </summary>
        public string CheckSum { get; set; }
    }
    #endregion
}
