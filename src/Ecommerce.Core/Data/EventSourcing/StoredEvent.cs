using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Core.Data.EventSourcing
{
    public class StoredEvent
    {
        public StoredEvent(Guid id, string type, DateTime occurrenceDate, string datas)
        {
            Id = id;
            Type = type;
            OccurrenceDate = occurrenceDate;
            Datas = datas;
        }

        public Guid Id { get; private set; }
        public string Type { get; private set; }
        public DateTime OccurrenceDate { get; private set; }
        public string Datas { get; private set; }
    }
}
