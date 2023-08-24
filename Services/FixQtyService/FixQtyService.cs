using API.Data;
using API.DTOs.FixQty;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services.FixQtyService{
    public class FixQtyService : IFixQtyService
    {
        private readonly FordBBDDContext _context;
        private readonly IMapper _mapper;

        public FixQtyService(IMapper mapper, FordBBDDContext context )
        {
            _context = context;
            _mapper = mapper;
        }

        // Adds a FixQty object to the database
        public async Task<ServiceResponse<List<GetFixQtyDTO>>> AddFixQty()
        {
            var fix  = new AddFixQtyDTO();
            fix.RandomizeValues();
            var serviceResponse = new ServiceResponse<List<GetFixQtyDTO>>();
            var fixQty = _mapper.Map<FixQtyClass>(fix);

            // Add the fixQty object to the context
            _context.FixQties.Add(fixQty);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Retrieve the updated list from the database
            var updatedDbFixQties = await _context.FixQties.ToListAsync();

            // Map the updated list to the DTO
            serviceResponse.Data = updatedDbFixQties.Select(fixQty => _mapper.Map<GetFixQtyDTO>(fixQty)).ToList();

            return serviceResponse;
        }

        // Deletes a FixQty object from the database based on the provided id
        public async Task<ServiceResponse<List<GetFixQtyDTO>>> DeleteFixQty(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetFixQtyDTO>>();
            try
            {
                // Find the FixQty object with the given id
                var fix = _context.FixQties.FirstOrDefault(f => f.Id == id);
                if (fix == null)
                {
                    throw new Exception($"FixQty with id '{id}' not found");
                }

                // Remove the fix object from the context
                _context.FixQties.Remove(fix);
                
                // Save changes to the database
                await _context.SaveChangesAsync();

                // Retrieve the updated list from the database
                var updatedDbFixQties = await _context.FixQties.ToListAsync();

                // Map the updated list to the DTO
                serviceResponse.Data = updatedDbFixQties.Select(fixQty => _mapper.Map<GetFixQtyDTO>(fixQty)).ToList();
            }
            catch (Exception ex)
            {
                // If an exception occurs, set the service response properties accordingly
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        // Retrieves all FixQty objects from the database
        public async Task<ServiceResponse<List<GetFixQtyDTO>>> Get()
        {
            // Retrieve all FixQty objects from the database
            var dbFixQties = await _context.FixQties.ToListAsync();
            
            // Create a new service response
            var serviceResponse = new ServiceResponse<List<GetFixQtyDTO>>
            {
                // Map the list of FixQty objects to the DTO and assign it to the response data
                Data = dbFixQties.Select(fixQty => _mapper.Map<GetFixQtyDTO>(fixQty)).ToList()
            };
            return serviceResponse;
        }

        // Retrieves a FixQty object from the database based on the provided id
        public async Task<ServiceResponse<GetFixQtyDTO>> GetById(int id)
        {
            // Retrieve all FixQty objects from the database
            var dbFixQties = await _context.FixQties.ToListAsync();

            // Find the FixQty object with the given id
            var fix = dbFixQties.FirstOrDefault(fix => fix.Id == id);
            
            // Create a new service response
            var serviceResponse = new ServiceResponse<GetFixQtyDTO>{
                // Map the FixQty object to the DTO and assign it to the response data
                Data = _mapper.Map<GetFixQtyDTO>(fix) 
            };
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTempFixQtyDTO>>> GetBetween(DateTime startDate, DateTime endDate)
        {

            // Retrieve FixQty objects from the database for the last nDays
            var dbFixQties = await _context.FixQties
                .TemporalBetween(startDate, endDate)
                .OrderBy(f => EF.Property<DateTime>(f, "PeriodStart"))
                .Where(f => EF.Property<DateTime>(f, "PeriodStart") >= startDate && EF.Property<DateTime>(f, "PeriodStart") <= endDate)
                .Select(f => new GetTempFixQtyDTO
                {
                    // Map the other properties directly
                    Id = f.Id,
                    Reference = f.Reference,
                    IdUser = f.IdUser,
                    Responsible = f.Responsible,
                    Reason = f.Reason,
                    ReasonValue = f.ReasonValue,
                    FixQty = f.FixQty,
                    Average = f.Average,
                    ExpirationDate = f.ExpirationDate,
                    DeathDate = f.DeathDate,
                    FixQtyCMMS = f.FixQtyCMMS,
                    PackagingSize = f.PackagingSize,
                    PartStatus = f.PartStatus,
                    Error = f.Error,
                    State = f.State,
                    Comments = f.Comments,

                    // Retrieve PeriodStart and PeriodEnd using EF.Property within the LINQ query
                    PeriodStart = EF.Property<DateTime>(f, "PeriodStart"),
                    PeriodEnd = EF.Property<DateTime>(f, "PeriodEnd")
                })
                .ToListAsync();

            var serviceResponse = new ServiceResponse<List<GetTempFixQtyDTO>>
            {
                Data = dbFixQties, // Assign the mapped DTO list to the response data
            };

            return serviceResponse;
        }
        

        // Updates a FixQty object in the database
        public async Task<ServiceResponse<GetFixQtyDTO>> UpdateFixQty(UpdateFixQtyDTO fixQty)
        {
            var serviceResponse = new ServiceResponse<GetFixQtyDTO>();
            try
            {
                // Retrieve the current FixQty object from the database
                var fix = await _context.FixQties.FindAsync(fixQty.Id);

                // Check if the FixQty with the given id exists
                if (fix is null)
                {
                    throw new Exception($"FixQty with id '{fixQty.Id}' not found");
                }

                // Update the properties of the FixQty object with the provided values
                _mapper.Map(fixQty, fix);

                // Save changes to persist the updates
                await _context.SaveChangesAsync();

                // Map the updated FixQty object to the DTO
                serviceResponse.Data = _mapper.Map<GetFixQtyDTO>(fix);
            }
            catch (Exception ex)
            {
                // If an exception occurs, set the service response properties accordingly
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}
