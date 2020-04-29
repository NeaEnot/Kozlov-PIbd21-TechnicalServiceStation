using System.Collections.Generic;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;

namespace TechnicalServiceStationBusinessLogic.HelperModels
{
    class PdfRowParameters
    {
        public Table Table { get; set; }

        public List<string> Texts { get; set; }

        public string Style { get; set; }

        public ParagraphAlignment ParagraphAlignment { get; set; }
    }
}
