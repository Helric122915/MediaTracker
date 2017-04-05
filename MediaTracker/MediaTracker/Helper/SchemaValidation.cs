using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace MediaTracker.Helper
{
    /// <summary>
    /// Checks if the given XML data is valid based on the Metadata Type's corresponding Schema.
    /// </summary>
    public class SchemaValidation
    {
        private Log log;

        public SchemaValidation(Log log)
        {
            this.log = log;
        }

        #region Validate XML
        /// <summary>
        /// Validates the given string of XML Data against the schema of the specified MediaType Parameter. 
        /// </summary>
        /// <param name="xmlData"></param>
        /// <param name="metadata"></param>
        /// <returns></returns>
        public string validate(string xmlData, MediaType media)
        {
            string schemaLocation = "";
            XmlReaderSettings settings = new XmlReaderSettings();
            XmlReader reader = null;
            settings.ValidationType = ValidationType.Schema;

            switch (media)
            { 
                case MediaType.Movie:
                    schemaLocation = "MediaTracker.Schema.MovieSchema.xsd";
                    break;
                case MediaType.VideoGame:
                    schemaLocation = "MediaTracker.Schema.VideoGameSchema.xsd";
                    break;
                case MediaType.Music:
                    schemaLocation = "MediaTracker.Schema.MusicSchema.xsd";
                    break;
            }
                //case metadataType.Device: schemaLocation = "Keithley.Clarius.GUI.Resources.Chooser.Schema.DeviceSchemaV1.xsd"; break;
                //case metadataType.Test: schemaLocation = "Keithley.Clarius.GUI.Resources.Chooser.Schema.TestSchemaV1.xsd"; break;
                //case metadataType.Project: schemaLocation = "Keithley.Clarius.GUI.Resources.Chooser.Schema.ProjectSchemaV1.xsd"; break;
                //case metadataType.Action: schemaLocation = "Keithley.Clarius.GUI.Resources.Chooser.Schema.ActionSchemaV1.xsd"; break;
                //case metadataType.GUIFilter: schemaLocation = "Keithley.Clarius.GUI.Resources.Chooser.Schema.GuiFilterMapSchemaV1.xsd"; break;

            try
            {
                // Retrieves the Assembly Directory and Returns it.
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);

                // Retreives the schema contained in the assembly as a resource.
                Assembly myAssembly = Assembly.ReflectionOnlyLoadFrom(path);

                AssemblyName[] assemblies = myAssembly.GetReferencedAssemblies();

                AssemblyName assembly = assemblies.Single<AssemblyName>(assem => assem.Name == "GUI");

                Assembly guiAssembly = Assembly.Load(assembly);
                //Assembly myAssembly = Assembly.GetExecutingAssembly();

                using (Stream schemaStream = guiAssembly.GetManifestResourceStream(schemaLocation))
                {
                    using (XmlReader schemaReader = XmlReader.Create(schemaStream))
                    {
                        settings.Schemas.Add(null, schemaReader);
                    }
                }
                try
                {
                    // Loads the String with the XML Data and validates it against the loaded schema.
                    using (Stream schemaStream = guiAssembly.GetManifestResourceStream(schemaLocation))
                    {
                        reader = XmlReader.Create(schemaStream, settings);

                        XmlDocument xmld = new XmlDocument();
                        xmld.LoadXml(xmlData);
                        xmld.Schemas.Add(null, reader);
                        xmld.Validate(ValidationCallBack);
                        return "";
                    }
                }
                catch (Exception e)
                {
                    log.handleException(e);
                    return e.Message;
                }
            }
            catch (Exception e)
            {
                log.handleException(e);
                return e.Message;
            }
        }

        /// <summary>
        /// Validates the file at the given file location against the schema of the specified MeidaType Parameter. 
        /// </summary>
        /// <param name="fileLocation"></param>
        /// <param name="metadata"></param>
        /// <param name="isFileLocationString"></param>
        /// <returns></returns>
        public string validate(string fileLocation, MediaType media, bool isFileLocationString)
        {
            string schemaLocation = "";
            XmlReaderSettings settings = new XmlReaderSettings();
            XmlReader reader;

            switch (media)
            {
                case MediaType.Movie:
                    schemaLocation = "MediaTracker.Schema.MovieSchema.xsd";
                    break;
                case MediaType.VideoGame:
                    schemaLocation = "MediaTracker.Schema.VideoGameSchema.xsd";
                    break;
                case MediaType.Music:
                    schemaLocation = "MediaTracker.Schema.MusicSchema.xsd";
                    break;
            }

            try
            {
                // Retreives the schema contained in the assembly as a resource.
                Assembly assembly = Assembly.GetExecutingAssembly();

                //string[] names = assembly.GetManifestResourceNames();
                string[] names = GetType().Assembly.GetManifestResourceNames();

                //Stream stream = assembly.GetManifestResourceStream(names[2]);

                using (Stream schemaStream = assembly.GetManifestResourceStream(names[2]))//assembly.GetManifestResourceStream(schemaLocation))
                {
                    using (XmlReader schemaReader = XmlReader.Create(schemaStream))
                    {
                        settings.Schemas.Add(null, schemaReader);
                    }
                }

                reader = XmlReader.Create(fileLocation, settings);

                try
                {
                    // Loads the String with the XML Data and validates it against the loaded schema.
                    XmlDocument xmld = new XmlDocument();
                    xmld.Load(reader);
                    xmld.Schemas.Add(null, schemaLocation);
                    xmld.Validate(ValidationCallBack);
                    reader.Close();
                    return "";
                }
                catch (Exception e)
                {
                    log.handleException(e);
                    reader.Close();
                    return e.Message;
                }
            }
            catch (Exception e)
            {
                log.handleException(e);
                return e.Message;
            }
        }

        /// <summary>
        /// Reports the error if one is found within the XML data / file based on the schema
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValidationCallBack(object sender, ValidationEventArgs e)
        {
            throw e.Exception;
        }
        #endregion
    }
}