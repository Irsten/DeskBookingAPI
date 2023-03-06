using AutoMapper;
using DeskBookingAPI.Entities;
using DeskBookingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DeskBookingAPI.Services
{
    public interface IReservationService
    {
        List<ReservationDto> GetAll();
        bool BookDesk(BookingDto dto);
        bool CancelReservation(Reservation reservation);
        bool ChangeDays(ChangeDaysDto dto);
    }

    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ReservationService(ApplicationDbContext dbContext, IMapper mapper)
        {

            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<ReservationDto> GetAll()
        {
            var reservations = _dbContext.Reservations.Include(e => e.Employee).ToList();
            var mappedReservations = _mapper.Map<List<ReservationDto>>(reservations);

            return mappedReservations;
        }
        public bool BookDesk(BookingDto dto)
        {
            var desk = _dbContext.Desks.FirstOrDefault(d => d.Id == dto.DeskId);

            _dbContext.Reservations.Add(new Reservation()
            {
                DeskId = dto.DeskId,
                EmployeeId = dto.EmployeeId,
                BookingDate = dto.BookingDate,
                ExpirationDate = dto.BookingDate.AddDays(dto.BookingDays - 1)
            });

            _dbContext.SaveChanges();

            return true;
        }

        public bool CancelReservation(Reservation reservation)
        {
            _dbContext.Reservations.Remove(reservation);
            _dbContext.SaveChanges();

            return true;
        }

        public bool ChangeDays(ChangeDaysDto dto)
        {
            var reservation = _dbContext.Reservations.FirstOrDefault(r => r.Id == dto.ReservationId);
            reservation.BookingDate = dto.BookingDate;
            reservation.ExpirationDate = dto.BookingDate.AddDays(dto.BookingDays - 1);
            _dbContext.SaveChanges();

            return true;
        }
    }
}
