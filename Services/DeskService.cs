using AutoMapper;
using DeskBookingAPI.Entities;
using DeskBookingAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeskBookingAPI.Services
{
    public interface IDeskService
    {
        bool BookDesk(BookingDto dto);
        bool CreateDesk(int roomId);
        bool DeleteDesk(Desk desk);
        List<RoomDeskDto> GetAllDesksInRoom(int roomId);
        Desk GetDesk(int deskId);
        bool CancelBooking();
        bool ChangeBooking();
    }

    public class DeskService : IDeskService
    {
        // TODO
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public DeskService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public bool CreateDesk(int roomId)
        {
            // TODO
            // Only admin
            _dbContext.Desks.Add(new Desk()
            {
                isAvailable= true,
                RoomId= roomId,
            });
            _dbContext.SaveChanges();

            return true;
        }

        public List<RoomDeskDto> GetAllDesksInRoom(int roomId)
        {
            // TODO
            var desks = _dbContext.Desks.Where(d => d.RoomId== roomId).ToList();
            var roomDesks = _mapper.Map<List<RoomDeskDto>>(desks);

            return roomDesks;
        }


        public Desk GetDesk(int deskId)
        {
            // TODO
            var desk = _dbContext.Desks.FirstOrDefault(d => d.Id == deskId); 

            return desk;
        }

        public bool DeleteDesk(Desk desk)
        {
            // TODO
            // Only admin
            _dbContext.Desks.Remove(desk);
            _dbContext.SaveChanges();

            return true;
        }

        public bool BookDesk(BookingDto dto)
        {
            // TODO
            var desk = _dbContext.Desks.FirstOrDefault(d => d.Id == dto.DeskId);
            desk.BookingDate = dto.BookingDate;
            desk.ExpirationDate = dto.BookingDate.AddDays(dto.BookingDays - 1);
            desk.EmployeeId = dto.EmployeeId;
            desk.isAvailable = false;
            _dbContext.SaveChanges();

            return true;
        }

        public bool CancelBooking()
        {
            // TODO
            return true;
        }

        public bool ChangeBooking()
        {
            // TODO
            return true;
        }
    }
}
