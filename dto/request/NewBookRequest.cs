using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Book.enums;

namespace Book.dto.request
{
    public class NewBookRequest
    {
        public string title { get; set; }
        public string author { get; set; }
        public string publisher { get; set; }
        public string isbn { get; set; }
        public string isni { get; set; }
        public KDC classificationNumber { get; set; }
        public bool? isSeries { get; set; }
        public long? seriesId { get; set; }
        public int? ea {  get; set; }

        public NewBookRequest(string title, string author, string publisher, string isbn, string isni, KDC classificationNumber, bool isSeries, long? seriesId, int? ea)
        {
            this.title = title;
            this.author = author;
            this.publisher = publisher;
            this.isbn = isbn;
            this.isni = isni;
            this.classificationNumber = classificationNumber;
            this.isSeries = isSeries;
            this.seriesId = seriesId;
            this.ea = ea;
        }

        public bool isValid()
        {
            if (isSeries == true && seriesId == null)
            {
                return false;
            }
            if (isSeries == false && seriesId != null)
            {
                return false;
            }
            return true;
        }
    }
}
