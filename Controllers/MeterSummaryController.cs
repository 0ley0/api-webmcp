using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mcpserver.Model;
using mcpserver.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace mcpserver.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MeterSummaryController : ControllerBase
    {
        public readonly IDbContextFactory<PGContext> _dbContextFactory;
        public MeterSummaryController(IDbContextFactory<PGContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        [HttpGet("GetContract")]
        public async Task<ActionResult<IEnumerable<ContractEntity>>> GetContracts()
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                var result = await context.m_contracts
                    .Include(c => c.LotContracts).ToListAsync();



                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("CreateContract")]

        public async Task<ActionResult<ContractEntity>> CreateLotContract([FromBody] ContractEntity contractes)
        {
            if (contractes == null)
            {
                return BadRequest("Lot contract data is null.");
            }

            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                contractes.Timestamp = DateTime.UtcNow.AddHours(7); // Set the timestamp to the current UTC time
                contractes.ContractDate = DateTime.UtcNow.AddHours(7); // Set the contract date to the current UTC time
                await context.m_contracts.AddAsync(contractes);
                await context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetContracts), new ContractEntity { Id = contractes.Id }, contractes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("serach", Name = "GetLotContractlotName")]
        public async Task<ActionResult<IEnumerable<LotContractEntity>>> GetLotContractBylotName([FromQuery] string lot_name)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                var lotContractDb = await context.m_lotcontract
                    .Where(l => l.LotName == lot_name)
                    .ToListAsync();
                var lotContract = lotContractDb
                    .FirstOrDefault();

                return lotContract != null
                    ? Ok(lotContract)
                    : NotFound($"Lot contract with name '{lot_name}' not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetLotContractList")]
        public async Task<ActionResult<IEnumerable<LotContractEntity>>> GetLotContract()
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                var lotContractsDb = await context.m_lotcontract.ToListAsync();

                return Ok(lotContractsDb);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("CreateLotContract")]
        public async Task<ActionResult<LotContractEntity>> CreateLotContract([FromBody] LotContractEntity lotContract)
        {
            if (lotContract == null)
            {
                return BadRequest("Lot contract data is null.");
            }

            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                lotContract.LotDate = DateTime.UtcNow.AddHours(7);
                lotContract.Timestamp = DateTime.UtcNow.AddHours(7);
                await context.m_lotcontract.AddAsync(lotContract);
                await context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetLotContract), new LotContractEntity { Id = lotContract.Id }, lotContract);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetLotIncontract/{id}")]
        public async Task<ActionResult<ContractEntity>> GetLotIncontract(int id)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                var contract = await context.m_contracts
           .Include(c => c.LotContracts)
           .FirstOrDefaultAsync(c => c.Id == id);

                return contract != null
                    ? Ok(contract)
                    : NotFound($"Lot contract with ID '{id}' not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }


}