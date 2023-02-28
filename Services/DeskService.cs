using AutoMapper;
using DeskBookingAPI.Entities;
using DeskBookingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeskBookingAPI.Services
{
    public interface IDeskService
    {
        bool BookDesk(BookingDto dto);
        bool CreateDesk(int roomId);
        bool DeleteDesk(Desk desk);
        List<DeskDto> GetAllDesksInRoom(int roomId);
        DeskDto GetDesk(int deskId);
        bool CancelReservation(Desk desk);
        bool ChangeReservation(Employee employee, Desk desk, Desk selectedDesk, DateTime bookingDate, int bookingDays);
        public bool ChangeDays(BookingDto dto);
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
            _dbContext.Desks.Add(new Desk()
            {
                isAvailable= true,
                RoomId= roomId,
            });
            _dbContext.SaveChanges();

            return true;
        }

        public List<DeskDto> GetAllDesksInRoom(int roomId)
        {
            var desks = _dbContext.Desks.Include(e => e.Employee).Where(d => d.RoomId== roomId).ToList();
            var roomDesks = _mapper.Map<List<DeskDto>>(desks);

            return roomDesks;
        }


        public DeskDto GetDesk(int deskId)
        {
            var desk = _dbContext.Desks.Include(e => e.Employee).FirstOrDefault(d => d.Id == deskId);
            var mappedDesk = _mapper.Map<DeskDto>(desk);

            return mappedDesk;
        }

        public bool DeleteDesk(Desk desk)
        {
            _dbContext.Desks.Remove(desk);
            _dbContext.SaveChanges();

            return true;
        }

        public bool BookDesk(BookingDto dto)
        {
            var desk = _dbContext.Desks.FirstOrDefault(d => d.Id == dto.DeskId);
            desk.BookingDate = dto.BookingDate;
            desk.ExpirationDate = dto.BookingDate.AddDays(dto.BookingDays - 1);
            desk.EmployeeId = dto.EmployeeId;
            desk.isAvailable = false;
            _dbContext.SaveChanges();

            return true;
        }

        public bool CancelReservation(Desk desk)
        {
            desk.BookingDate = null;
            desk.ExpirationDate = null;
            desk.isAvailable = true;
            desk.EmployeeId = null;
            _dbContext.SaveChanges();

            return true;
        }

        public bool ChangeReservation(Employee employee, Desk desk, Desk selectedDesk, DateTime bookingDate, int bookingDays)
        {
            desk.BookingDate = null;
            desk.ExpirationDate = null;
            desk.isAvailable = true;
            desk.EmployeeId = null;

            selectedDesk.BookingDate = bookingDate;
            selectedDesk.ExpirationDate = bookingDate.AddDays(bookingDays - 1);
            selectedDesk.isAvailable = false;
            selectedDesk.EmployeeId = employee.Id;

            _dbContext.SaveChanges();

            return true;
        }

        public bool ChangeDays(BookingDto dto)
        {
            var desk = _dbContext.Desks.FirstOrDefault(d => d.Id == dto.DeskId);
            desk.BookingDate = dto.BookingDate;
            desk.ExpirationDate = dto.BookingDate.AddDays(dto.BookingDays - 1);
            _dbContext.SaveChanges();

            return true;
        }
    }
}
