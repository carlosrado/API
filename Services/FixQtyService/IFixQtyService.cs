using API.DTOs.FixQty;

namespace API.Services.FixQtyService{
    public interface  IFixQtyService {
        Task<ServiceResponse<List<GetFixQtyDTO>>> Get();
        Task<ServiceResponse<List<GetTempFixQtyDTO>>> GetBetween(DateTime startDate, DateTime endDate);
        Task<ServiceResponse<GetFixQtyDTO>> GetById(int id);
        Task<ServiceResponse<List<GetFixQtyDTO>>> AddFixQty();
        Task<ServiceResponse<GetFixQtyDTO>> UpdateFixQty(UpdateFixQtyDTO fixQty);
        Task<ServiceResponse<List<GetFixQtyDTO>>> DeleteFixQty(int id);

    }
}