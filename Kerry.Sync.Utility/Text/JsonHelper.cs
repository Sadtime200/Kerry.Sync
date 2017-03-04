using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;

namespace Kerry.Sync.Utility.Text
{
   public class JsonHelper
    {
        public static string ObjectToJson<T>(T u)
        {
            DataContractJsonSerializer json = new DataContractJsonSerializer(u.GetType());
            string userCredentialStr = String.Empty;

            using (MemoryStream stream = new MemoryStream())
            {
                json.WriteObject(stream, u);
                userCredentialStr = Encoding.UTF8.GetString(stream.ToArray());
            }
            return userCredentialStr;
        }

        /// <summary>
        /// Transfer Json into specified  type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonToObject<T>(String json)
        {

            //return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(ms);
            }
        }


        ///// <summary>
        ///// Transfer current type into Json 
        ///// </summary>
        ///// <typeparam name="T">class type</typeparam>
        ///// <param name="u"></param>
        ///// <returns></returns>
        //public static string ObjectToText<T>(T u)
        //{
        //    DataContractJsonSerializer json = new DataContractJsonSerializer(u.GetType());
        //    string userCredentialStr = String.Empty;

        //    using (MemoryStream stream = new MemoryStream())
        //    {
        //        json.WriteObject(stream, u);
        //        userCredentialStr = Encoding.UTF8.GetString(stream.ToArray());
        //    }
        //    return userCredentialStr;
        //}

        /// <summary>
        ///  Transfer current object to txt file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="u"></param>
        /// <param name="fileName"></param>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public static bool ObjectToText<T>(T u, string path)
        {
            DataContractJsonSerializer json = new DataContractJsonSerializer(u.GetType());
            string userCredentialStr = String.Empty;

            using (MemoryStream stream = new MemoryStream())
            {
                try
                {
                    json.WriteObject(stream, u);
                }
                catch (Exception ex)
                {
                    //continue;
                    throw ex;
                }

                userCredentialStr = Encoding.UTF8.GetString(stream.ToArray());
            }
            return JsonToText(userCredentialStr, path);
        }

        /// <summary>
        /// Save json to local file.
        /// </summary>
        /// <param name="jsonStr"></param>
        public static bool JsonToText(string jsonStr, string fileName, string folderPath)
        {
            bool b = false;
            //string folderPath = System.AppDomain.CurrentDomain.BaseDirectory as string + ConfigurationManager.AppSettings["MasterData"] as string;
            //string folderPath = String.Format("{0}{1}", ConfigurationManager.AppSettings["basePath"].ToString(), ConfigurationManager.AppSettings["MasterData"].ToString());
            //string folderPath = @"D:/K3.5";

            string path = Path.Combine(folderPath + "\\", fileName + ".txt");
            //LogHelper.Info("path : " + path);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(jsonStr);
                return !b;
            }

        }


        public static bool JsonToText(string jsonStr, string path)
        {
            bool b = false;
            //string folderPath = System.AppDomain.CurrentDomain.BaseDirectory as string + ConfigurationManager.AppSettings["MasterData"] as string;
            //string folderPath = String.Format("{0}{1}", ConfigurationManager.AppSettings["basePath"].ToString(), ConfigurationManager.AppSettings["MasterData"].ToString());
            //string folderPath = @"D:/K3.5";

            //string path = Path.Combine(folderPath + "\\", fileName + ".txt");
            //LogHelper.Info("path : " + path);
            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(folderPath);
            //}
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(jsonStr);
                return !b;
            }

        }


        /// <summary>
        /// read local file content , and transfer to json .
        /// </summary>
        /// <param name="path">e.g c:\\1.txt</param>
        /// <returns></returns>
        public static T TextToJson<T>(string path)
        {
            string jsonStr = String.Empty;

            using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
            {
                try
                {
                    jsonStr = sr.ReadToEnd();
                }
                catch (System.OutOfMemoryException ex)
                {
                    throw (ex);
                }
                return JsonToObject<T>(jsonStr);
            }
        }
        public static string TextToStr(string path)
        {
            string jsonStr = String.Empty;

            using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
            {
                try
                {
                    jsonStr = sr.ReadToEnd();
                }
                catch (System.OutOfMemoryException ex)
                {
                    throw (ex);
                }
                return jsonStr;
            }
        }
    }
}

