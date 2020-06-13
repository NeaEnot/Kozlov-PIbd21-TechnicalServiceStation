using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;
using TechnicalServiceStationBusinessLogic.BindingModels;
using TechnicalServiceStationBusinessLogic.Enums;
using TechnicalServiceStationBusinessLogic.HelperModels;

namespace TechnicalServiceStationBusinessLogic.BusinessLogic
{
    public abstract class BackUpAbstractLogic
    {
        public void SendArchive(UserBindingModel model, BackUpFormat format)
        {
            string folderName = ConfigurationManager.AppSettings["TempFilesPath"] + model.Email;

            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(folderName);

                if (dirInfo.Exists)
                {
                    foreach (FileInfo file in dirInfo.GetFiles())
                    {
                        file.Delete();
                    }
                }
                else
                {
                    Directory.CreateDirectory(folderName);
                }

                string fileName = $"{folderName}.zip";

                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                Assembly assem = GetAssembly();

                var dbsets = GetFullList();

                MethodInfo method = null;
                switch (format)
                {
                    case BackUpFormat.json:
                        method = GetType().BaseType.GetTypeInfo().GetDeclaredMethod("SaveJson");
                        break;
                    case BackUpFormat.xml:
                        method = GetType().BaseType.GetTypeInfo().GetDeclaredMethod("SaveXml");
                        break;
                }
                

                foreach (var set in dbsets)
                {
                    var elem = assem.CreateInstance(set.PropertyType.GenericTypeArguments[0].FullName);
                    MethodInfo generic = method.MakeGenericMethod(elem.GetType());
                    generic.Invoke(this, new object[] { folderName });
                }

                ZipFile.CreateFromDirectory(folderName, fileName);

                dirInfo.Delete(true);

                MailLogic.MailSendAsync(
                    new MailSendInfo
                    {
                        MailAddress = model.Email,
                        Subject = "Резервная копия",
                        Text = "Резервная копия в прикреплённом файле",
                        Attachment = fileName
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SaveJson<T>(string folderName) where T : class, new()
        {
            var records = GetList<T>();
            T obj = new T();
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<T>));

            using (FileStream fs = new FileStream(string.Format("{0}/{1}.json", folderName, obj.GetType().Name), FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, records);
            }
        }

        private void SaveXml<T>(string folderName) where T : class, new()
        {
            var records = GetList<T>();
            T obj = new T();
            XmlSerializer xmlFormatter = new XmlSerializer(typeof(List<T>));

            using (FileStream fs = new FileStream(string.Format("{0}/{1}.xml", folderName, obj.GetType().Name), FileMode.OpenOrCreate))
            {
                xmlFormatter.Serialize(fs, records);
            }
        }

        protected abstract Assembly GetAssembly();

        protected abstract List<PropertyInfo> GetFullList();

        protected abstract List<T> GetList<T>() where T : class, new();
    }
}
