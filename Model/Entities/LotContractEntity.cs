using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace mcpserver.Model.Entities
{
    public class LotContractEntity
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("lot_name")]
        public string LotName { get; set; }

        [Column("lot_date")]
        public DateTime LotDate { get; set; }

        [Column("timestamp")]
        public DateTime Timestamp { get; set; }

        [Column("ContractEntityId")]
        public int ContractEntityId { get; set; }


    }
}