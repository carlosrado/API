using API.DTOs.User;

namespace API.Services.UserService{
    public interface  IUserService {
        Task<ServiceResponse<List<GetUserDTO>>> Get();
        Task<ServiceResponse<GetUserDTO>> GetById(int id);
        Task<ServiceResponse<List<GetUserDTO>>> AddUser(AddUserDTO userDTO);
        Task<ServiceResponse<GetUserDTO>> UpdateUser(UpdateUserDTO userDTO);
        Task<ServiceResponse<List<GetUserDTO>>> DeleteUser(int id);

    }
}