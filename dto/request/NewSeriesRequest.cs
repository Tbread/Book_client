using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.dto.request
{
    public class NewSeriesRequest
    {
        public string seriesName {  get; set; }

        public NewSeriesRequest(string seriesName)
        {
            this.seriesName = seriesName;
        }
    }
}
