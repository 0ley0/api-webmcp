using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mcpserver.Model.Entities
{
    public class ContractEntity
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("contract_name")]
        public string ContractName { get; set; }

        [Column("customer_name")]
        public string CustomerName { get; set; }

        [Column("contract_date")]
        public DateTime ContractDate { get; set; }

        [Column("timestamp")]
        public DateTime Timestamp { get; set; }
        [NotMapped]
        public List<LotContractEntity> LotContracts { get; set; }


    }
}