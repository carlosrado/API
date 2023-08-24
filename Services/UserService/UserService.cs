using API.Data;
using API.DTOs.FixQty;
using API.DTOs.User;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services.UserService{
    public class UserService : IUserService
    {
        private readonly FordBBDDContext _context;
        private readonly IMapper _mapper;

        public UserService(IMapper mapper, FordBBDDContext context )
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetUserDTO>>> AddUser(AddUserDTO userDTO)
        {
            var serviceResponse = new ServiceResponse<List<GetUserDTO>>();
            var user = _mapper.Map<UserClass>(userDTO);

            // Add the fix object to the context
            _context.Users.Add(user);

            // Save changes to the database
            await _context.SaveChangesAsync();

            // Retrieve the updated list from the database
            var updatedDbUsers = await _context.Users.ToListAsync();

            // Map the updated list to the DTO
            serviceResponse.Data = updatedDbUsers.Select(user => _mapper.Map<GetUserDTO>(user)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUserDTO>>> DeleteUser(int id)
        {
             var serviceResponse = new ServiceResponse<List<GetUserDTO>>();
            try
            {
                // Find the FixQty object with the given id
                var user = _context.Users.FirstOrDefault(f => f.Id == id) ?? throw new Exception($"User with id '{id}' not found");

                // Remove the fix object from the context
                _context.Users.Remove(user);
                
                // Save changes to the database
                await _context.SaveChangesAsync();

                // Retrieve the updated list from the database
                var updatedDbUsers = await _context.Users.ToListAsync();

                // Map the updated list to the DTO
                serviceResponse.Data = updatedDbUsers.Select(user => _mapper.Map<GetUserDTO>(user)).ToList();
            }
            catch (Exception ex)
            {
                // If an exception occurs, set the service response properties accordingly
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUserDTO>>> Get()
        {
             // Retrieve all FixQty objects from the database
            var users = await _context.Users.ToListAsync();
            
            // Create a new service response
            var serviceResponse = new ServiceResponse<List<GetUserDTO>>
            {
                // Map the list of FixQty objects to the DTO and assign it to the response data
                Data = users.Select(user => _mapper.Map<GetUserDTO>(user)).ToList()
            };
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDTO>> GetById(int id)
        {
                        // Retrieve all FixQty objects from the database
            var users = await _context.Users.ToListAsync();

            // Find the FixQty object with the given id
            var user = users.FirstOrDefault(user => user.Id == id);
            
            // Create a new service response
            var serviceResponse = new ServiceResponse<GetUserDTO>{
                // Map the FixQty object to the DTO and assign it to the response data
                Data = _mapper.Map<GetUserDTO>(user) 
            };
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDTO>> UpdateUser(UpdateUserDTO userDTO)
        {
            var serviceResponse = new ServiceResponse<GetUserDTO>();
            try
            {
                // Retrieve the current FixQty object from the database
                var user = await _context.Users.FindAsync(userDTO.Id);

                // Check if the FixQty with the given id exists
                if (user is null)
                {
                    throw new Exception($"User with id '{userDTO.Id}' not found");
                }

                // Update the properties of the FixQty object with the provided values
                _mapper.Map(userDTO, user);

                // Save changes to persist the updates
                await _context.SaveChangesAsync();

                // Map the updated FixQty object to the DTO
                serviceResponse.Data = _mapper.Map<GetUserDTO>(user);
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
