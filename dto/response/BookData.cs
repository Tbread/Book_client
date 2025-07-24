using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Book.enums;

namespace Book.dto.response
{
    public class BookData
    {
        public long bookId { get; set; }
        public string title {  get; set; }
        public string author { get; set; }
        public string publisher { get; set; }
        public KDC classificationNumber { get; set; }
        public bool isSeries { get; set; }
        public int ver { get; set; }
        public bool discard { get; set; }
        public string isbn { get; set; }
        public string isni { get; set; }
        public long? seriesId { get; set; }
        public string seriesName { get; set; }
    }
}
