using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsoleTest
{
    class Program
    {
        private static readonly HttpClient Client = new HttpClient();
        private const string UserName = "YOUR USER NAME";
        private const string Password = "YOUR PASSWORD";
        private const string Url = "http://localhost/"; 
        private static string _accessToken = string.Empty;

        static void Main(string[] args)
        {
            Init();

            Console.WriteLine(PostPoCreate("XML").Result);

            //Console.WriteLine(PostPoConfirm("JSON").Result);

            //Console.WriteLine(PostT1Amendment("JSON").Result);

            //Console.WriteLine(PostT1Cancel("JSON").Result);

            //Console.WriteLine(PostT2AmendAck("JSON").Result);
            Console.ReadLine();
        }

        public static void Init()
        {
            if (string.IsNullOrEmpty(_accessToken))
            {
                dynamic result = JObject.Parse(Login().Result) ;
                _accessToken = result.access_token;
            }
        }

        public static async Task<string> Login()
        {
            var body = $"grant_type=password&username={UserName}&password={Password}";
            var response = await Client.PostAsync(
                Url+"api/token",
                new StringContent(body, Encoding.UTF8, "application/json"));
            return await response.Content.ReadAsStringAsync();
        } 

        #region PoCreate
        public static async Task<string> PostPoCreate(string fileType)
        {
            var json = PoCreate(fileType);
            //Console.WriteLine(json);
            var uri = Url + "api/Po/Create";
            Console.WriteLine($"Post to {uri}");
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            var response = await Client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
            return await response.Content.ReadAsStringAsync();
        }

        public static string PoCreate(string fileType)
        {
            var file = "Instance File v1_0 Step 1 PO Creation.xml";

            var postbody = new PoCreation
            {
                OrderItemCount = 1,
                OrderQuantity = 100,
                PoNumber = new List<string> { "0123456789" },
                FileType = fileType
            };

            var content = GetContent<PurchaseOrder>(fileType, file, postbody);
            return content;
        }
        #endregion

        #region PoConfirm
        public static async Task<string> PostPoConfirm(string fileType)
        {
            var json = PoConfirm(fileType);
            var uri = Url + "api/T1Po/Confirm";
            Console.WriteLine($"Post to {uri}");
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            var response = await Client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
            return await response.Content.ReadAsStringAsync();
        }

        public static string PoConfirm(string fileType)
        {
            var file = "Instance File v1_0 Step 2 PO Confirmation.xml";

            var postbody = new ApiPoConfirmation
            {
                OrderItemCount = 1,
                OrderQuantity = 100,
                PoNumber = new List<string> { "0123456789" },
                FileType = fileType
            };

            var content = GetContent<PoConfirmation>(fileType, file, postbody);
            return content;
        }
        #endregion

        #region Amendment
        public static async Task<string> PostT1Amendment(string fileType)
        {
            var json = T1Amendment(fileType);
            //Console.WriteLine(json);
            var uri = Url + "api/Po/Amendment";
            Console.WriteLine($"Post to {uri}");
            return await PostRequest(fileType, uri, json);
        }
        public static string T1Amendment(string fileType)
        {
            var file = "Instance File v1_0 Step 3 T1 PO Amendment.xml";

            var postbody = new PoT1Amendment
            {
                OrderItemCount = 1,
                OrderQuantity = 200,
                ModifyCount = 1,
                PoNumber = new List<string> { "0123456789" },
                FileType = fileType
            };

            var content = GetContent<PurchaseOrder>(fileType, file, postbody);
            return content;
        }
        #endregion

        #region Cancel

        public static async Task<string> PostT1Cancel(string fileType)
        {
            var json = T1Cancel(fileType);
            //Console.WriteLine(json);
            return await PostRequest(fileType, "api/Po/Cancel", json);
        }
        public static string T1Cancel(string fileType)
        {
            var file = "Instance File v1_0 Step 7 PO Cancellation.xml";

            var postbody = new ApiPoCancellation
            {
                PoNumber = new List<string> { "0123456789" },
                FileType = fileType
            };

            var content = GetContent<PoCancellation>(fileType, file, postbody);
            return content;
        }

        #endregion

        #region T2AmendmentAcknowledgement
        public static async Task<string> PostT2AmendAck(string fileType)
        {
            var json = T2AmendAck(fileType);
            //Console.WriteLine(json);
            return await PostRequest(fileType, "api/Po/T2AmendmentAcknowledgement", json);
        }
        public static string T2AmendAck(string fileType)
        {
            var file = "Instance File v1_0 Step 6 T2 PO Amendment Acknowledgement.xml";

            var postbody = new ApiPoCancellation
            {
                PoNumber = new List<string> { "0123456789" },
                FileType = fileType
            };

            var content = GetContent<Acknowledgement>(fileType, file, postbody);
            return content;
        }
        #endregion

        #region common
        public static async Task<string> PostRequest(string fileType, string path, string content)
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            var response = await Client.PostAsync(
                Url + path,
                new StringContent(content, Encoding.UTF8, "application/json"));
            return await response.Content.ReadAsStringAsync();
        }

        public static string GetContent<T>(string fileType, string file, dynamic postbody)
        {
            switch (fileType)
            {
                case "JSON":
                {
                    var doc = LoadXml(file);
                    var obj = new List<T> { Tools.XmlToGeneric<T>(doc) };
                    var orderBody = JsonConvert.SerializeObject(obj);
                    postbody.OrderBody = orderBody;
                    postbody.CheckSum = Tools.GetMd5String(orderBody);
                    return JsonConvert.SerializeObject(postbody);
                }
                case "XML":
                {
                    var orderBody = GetBase64FromFile(file);
                    postbody.OrderBody = orderBody;
                    postbody.CheckSum = Tools.GetMd5String(orderBody);
                    return JsonConvert.SerializeObject(postbody);
                }
                default:
                    return null;
            }
        }
        #endregion

        public static XDocument LoadXml(string fileName)
        {
            var rd = XmlReader.Create(fileName);
            return XDocument.Load(rd);
        }

        public static string GetBase64FromFile(string path)
        {
            try
            {
                var data = Encoding.UTF8.GetBytes(File.ReadAllText(path));
                return Convert.ToBase64String(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{MethodBase.GetCurrentMethod().ReflectedType}-{MethodBase.GetCurrentMethod().Name},msg:{ex.Message}");
                return string.Empty;
            }
        }

        public static string GetEnumDescription(Enum value)
        {
            try
            {
                if (value == null) return string.Empty;
                var fi = value.GetType().GetField(value.ToString());
                var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                return attributes.Length > 0 ? attributes[0].Description : value.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return string.Empty;
            }
        }
    }

    public enum EdiErrorList
    {
        [Description("Some properies null or empty")]
        PropertiesNullorEmpty = -14,
        [Description("PoNumber not match")]
        PoNumberNotMatch = -13,
        [Description("Duplicate Order: {0}")]
        DuplicateOrder = -12,
        [Description("Wrong order body content")]
        WrongOrderBody = -11,
        [Description("Null Content")]
        NullContent = -10,
        [Description("Quantity not match")]
        QuantityNotMatch = -9,
        [Description("Count not match")]
        CountNotMatch = -8,
        [Description("Fail on saving content")]
        FailOnSave = -7,
        [Description("JSON convert fail: {0}")]
        JsonConvertFail = -6,
        [Description("XML convert fail.")]
        XmlConvertFail = -5,
        [Description("JSON validate fail: {0}")]
        JsonValidateFail = -4,
        [Description("XML validate fail: {0}")]
        XmlValidateFail = -3,
        [Description("Wrong file type.")]
        WrongFileType = -2,
        [Description("Wrong checksum.")]
        WrongChecksum = -1,
        [Description("Success")]
        Success = 0
    }
}
