using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using Newtonsoft.Json.Linq;

namespace DataLayer
{
    public class GlobalFunction
    {
        string payload, service_type, url;
        Page page;


        public async Task<string> GetPostData(string payload, string service_type, string url, Page page, string TripType)
        {
            this.page = page;
            String result = "";
            if (service_type == ODataService.Service_get)
            {
                result = GetODataService(url);
                return result;
            }

            else if (service_type.Equals(ODataService.Service_post))
            {
                await PostDataAsync<string>(payload, url, page, TripType);


            }
            else if (service_type.Equals(ODataService.Service_put))
            {

            }
            return result;

        }

        public async Task<Tuple<T, string>> PostDataAsync<T>(string payload, string url, Page page, string TripType)
        {
            try
            {
                //string uri = $"{Urls.BaseURL}{url}";

                using (HttpClient client = new HttpClient())
                {
                    //fetch csrf
                    var CSRF_TOKEN = string.Empty;
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers
                        .AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", ODataService.username, ODataService.password))));
                    client.DefaultRequestHeaders.Add("X-CSRF-TOKEN", "FETCH");
                    //client.DefaultRequestHeaders.Add("x-smp-appcid", "'" + page.Session["APPCID"] + "'");
                    HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(false);
                    CSRF_TOKEN = response.Headers.GetValues("x-csrf-token").FirstOrDefault();
                    if (CSRF_TOKEN == null || CSRF_TOKEN.Equals(string.Empty))
                        return new Tuple<T, string>(default(T), "CSRF error");

                    client.DefaultRequestHeaders.Remove("X-CSRF-TOKEN");
                    client.DefaultRequestHeaders.Add("X-CSRF-TOKEN", CSRF_TOKEN);
                    client.DefaultRequestHeaders.Add("Accept", "application/xml");
                    //client.DefaultRequestHeaders.Add("x-smp-appcid","'"+page.Session["APPCID"]+"'");

                    HttpContent content = new StringContent(payload);
                    content.Headers.Remove("Content-Type");
                    if (url.Contains("batch"))
                    {
                        content.Headers.Add("Content-Type", "multipart/mixed;boundary=batch");
                    }
                    else
                    {
                        content.Headers.Add("Content-Type", "application/xml");
                    }
                    HttpResponseMessage orderResponse = await client.PostAsync(url + "et_lead_prospect_newSet", content).ConfigureAwait(false);

                    var result = await orderResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

                    string message = result;
                    if (!orderResponse.IsSuccessStatusCode)
                    {
                        //HTTP/1.1 400 Bad Request

                        if (result.ToLower().Contains("HTTP/1.1 400 Bad Request"))
                        {
                            message = "Bad request.Please try again later." + "\n" + result;
                            var errorOutput = result;
                            //var errorMsg = JObject.Parse (errorOutput).SelectToken ("error.message.value").ToString ();
                            return new Tuple<T, string>(default(T), "");
                        }
                        else
                        {
                            message = result;
                        }
                        // showAlert(message, page, TripType);
                        return new Tuple<T, string>(default(T), orderResponse.StatusCode.ToString());
                    }
                    else
                    {
                        // page.On

                        // message = "Plan created succesfully.";
                        // showAlert(message, page, TripType);
                        string message1 = "Thank you for Submission !";
                        //  ScriptManager.RegisterStartupScript(page, page.GetType(), "", "alert('" + message1 + "');window.location='LeadForm.aspx';", true);

                    }

                    //showAlert(message, page);

                    return new Tuple<T, string>(default(T), result);
                    // return null;
                }
            }
            catch (Exception ex)
            {
                //Debug.WriteLine (ex.Message);
                return new Tuple<T, string>(default(T), ex.Message);
            }
        }

        public string GetODataService(string url1)
        {
            try
            {
                var webrequest = (HttpWebRequest)System.Net.WebRequest.Create(url1);
                webrequest.Credentials = new NetworkCredential(ODataService.username, ODataService.password);
                webrequest.Headers["X-CSRF-TOKEN"] = "FETCH";
                webrequest.ContentType = "application/xml";
                webrequest.Accept = "application/json";
                var response = webrequest.GetResponse();

                var reader = new StreamReader(response.GetResponseStream());
                var result = reader.ReadToEnd();

                //    odataCallBack.onSuccess(result);

                return result;


            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }

        }

        public static JArray parseJsonJobLines(string result)
        {
            JArray conversion = null;
            try
            {
                var jObject2 = JObject.Parse(result);
                //dynamic json = JValue.Parse(result.ToString());
                //conversion = (JArray)json.value;
               

                JArray items = (JArray)jObject2["value"];
                for (int i = 0; i < items.Count; i++)
                {
                     ProjectMaster projobj = new ProjectMaster();
                     projobj.Project_Number = (string)jObject2["value"][i]["No"];
                     projobj.Project_Name= (string)jObject2["value"][i]["Description"];
                     projobj.Blocked = (string)jObject2["value"][i]["Blocked"];
                     projobj.Contact_Person = (string)jObject2["value"][i]["Bill_to_Contact_No"];
                     SyncJobs.InsertJobLines(projobj);
                }
               
            }
            catch (Exception e)
            {
            }
            return conversion;
        }

        public static JArray parseJsonTaskLines(string result)
        {
            JArray conversion = null;
            try
            {
                var jObject2 = JObject.Parse(result);
                //dynamic json = JValue.Parse(result.ToString());
                //conversion = (JArray)json.value;

                JArray items = (JArray)jObject2["value"];
                for (int i = 0; i < items.Count; i++)
                {
                    TaskMaster obj = new TaskMaster();
                    obj.JobNo = (string)jObject2["value"][i]["Job_No"];
                    obj.Project_Number = (string)jObject2["value"][i]["Job_Task_No"];
                    obj.TaskName= (string)jObject2["value"][i]["Description"];
                    SyncTasks.InsertTaskLines(obj);
                }

            }
            catch (Exception e)
            {
            }
            return conversion;
        }

        public static JArray parseJsonResourceLines(string result)
        {
            JArray conversion = null;
            try
            {
                var jObject2 = JObject.Parse(result);
                //dynamic json = JValue.Parse(result.ToString());
                //conversion = (JArray)json.value;
                JArray items = (JArray)jObject2["value"];
                for (int i = 0; i < items.Count; i++)
                {
                    ResourceMaster obj = new ResourceMaster();
                    obj.ResourceNo = (string)jObject2["value"][i]["No"];
                   string username = (string)jObject2["value"][i]["Time_Sheet_Owner_User_ID"];
                   if (!string.IsNullOrEmpty(username))
                   {
                       obj.UserName = username.Replace("SERVPRO\\", "");

                   }
                  
                    obj.Name = (string)jObject2["value"][i]["Name"];
                    bool status = (bool)jObject2["value"][i]["Blocked"];
                    if (!status)
                    {
                        SyncResource.InsertResourceLines(obj);
                    }
                 
                }


            }
            catch (Exception e)
            {
            }
            return conversion;
        }

        public static JArray parseJsonSalesCycleLines(string result)
        {
            JArray conversion = null;
            try
            {
                var jObject2 = JObject.Parse(result);
                //dynamic json = JValue.Parse(result.ToString());
                //conversion = (JArray)json.value;

                JArray items = (JArray)jObject2["value"];
                for (int i = 0; i < items.Count; i++)
                {
                    SalesCyleMaster obj = new SalesCyleMaster();
                    obj.Code = (string)jObject2["value"][i]["Code"];
                    obj.Description = (string)jObject2["value"][i]["Description"];                   
                    SyncSalesCycle.InsertSalecylceLines(obj);
                }
            }
            catch (Exception e)
            {
            }
            return conversion;
        }



        public static string convertDateToSAPFormat(string date)
        {
            try
            {
                DateTime temp = DateTime.ParseExact(date.Split(' ')[0] + "", CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern, CultureInfo.InvariantCulture);

                string str = temp.ToString("yyyy-MM-dd") + "T00:00:00";

                return str + "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }


        public static string convertDateToDotNetFormat(string date, string reqformat)
        {
            string datetime = Convert.ToDateTime(date) + "";
            DateTime temp = DateTime.ParseExact(datetime.Split(' ')[0] + "", CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern, CultureInfo.InvariantCulture);

            string str = temp.ToString(reqformat);

            return str + "";
        }

        public static string convertDateFormat(string date, string reqformat, string currentformat)
        {
            string datetime = Convert.ToDateTime(date) + "";
            DateTime temp = DateTime.ParseExact(datetime.Split(' ')[0] + "", currentformat, CultureInfo.InvariantCulture);

            string str = temp.ToString(reqformat);

            return str + "";
        }






        public static void showAlertCommon(string message, Page page)
        {
            //ScriptManager.RegisterStartupScript(page, page.GetType(), "", "alert('" + message + "');", true);
        }

        public static double checkDoubleValue(string value)
        {
            double temp = 0.00;
            try
            {
                if (value.Trim().ToString().Length > 0)
                {
                    temp = Convert.ToDouble(value);
                }
            }
            catch (Exception ex)
            {
            }

            return temp;

        }

        public static string Base64Encode(string Value)
        {
            var temp = HttpUtility.HtmlEncode(Value);
            return temp;
        }
        public static string ChangeDateFormat(string currentFormat)
        {
            string FinalFormat = "";
            string[] a = currentFormat.Split('-');
            FinalFormat = a[2] + "-" + a[1] + "-" + a[0];
            return FinalFormat;
        }
        //public List<TripHeaderPayload> saveInDB(string data, List<string> blocked_columns)
        //{

        //    List<TripHeaderPayload> list_trip_head = new List<TripHeaderPayload>();
        //    try
        //    {
        //        JArray jArray = GlobalFunction.parseJson(data);
        //        int size = jArray.Count;
        //        for (int i = 0; i < size; i++)
        //        {
        //            JObject object = jArray[i].;
        //            //JArray strings = jArray[i];
        //            //JArray strings = jArray.getJSONObject(i).names();
        //            //for (int j = 0; j < strings.length(); j++)
        //            //{
        //            //    String key = strings.get(j).toString();
        //            //    if (!blocked_columns.contains(key))
        //            //        contentValues.put(key, jArray.getJSONObject(i).get(key).toString());
        //            //}
        //            //long id = insertData(contentValues, table_name);
        //            //contentValues.clear();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        e.printStackTrace();
        //    }

        //    return contentValues;
        //}
    }
}