using AutoMapper;
using DeskBookingAPI.Entities;
using DeskBookingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeskBookingAPI.Services
{
    public interface IDeskService
    {
        //bool BookDesk(BookingDto dto);
        bool CreateDesk(int roomId);
        bool DeleteDesk(Desk desk);
        List<DeskDto> GetAllDesksInRoom(int roomId);
        DeskDto GetDesk(int deskId);
       // bool CancelReservation(Desk desk);
        //bool ChangeReservation(Employee employee, Desk desk, Desk selectedDesk, DateTime bookingDate, int bookingDays);
       // public bool ChangeDays(BookingDto dto);
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
                RoomId= roomId,
            });
            _dbContext.SaveChanges();

            return true;
        }

        public List<DeskDto> GetAllDesksInRoom(int roomId)
        {
            // TODO
            var desks = _dbContext.Desks.Include(r => r.Reservations).Where(d => d.RoomId == roomId).ToList();
            foreach (var desk in desks)
            {
                foreach (var reservation in desk.Reservations)
                {
                    reservation.Employee = _dbContext.Employees.FirstOrDefault(e => e.Id == reservation.EmployeeId);
                }
            }

            var roomDesks = _mapper.Map<List<DeskDto>>(desks);

            return roomDesks;
        }


        public DeskDto GetDesk(int deskId)
        {
            // TODO
            var desk = _dbContext.Desks.Include(r => r.Reservations).FirstOrDefault(d => d.Id == deskId);
            if (desk.Reservations != null)
            {
                foreach (var reservation in desk.Reservations)
                {
                    reservation.Employee = _dbContext.Employees.FirstOrDefault(e => e.Id == reservation.EmployeeId);
                }
            }

            var mappedDesk = _mapper.Map<DeskDto>(desk);

            return mappedDesk;
        }

        public bool DeleteDesk(Desk desk)
        {
            _dbContext.Desks.Remove(desk);
            _dbContext.SaveChanges();

            return true;
        }

        /*public bool BookDesk(BookingDto dto)
        {
            // TODO
            var desk = _dbContext.Desks.FirstOrDefault(d => d.Id == dto.DeskId);

            _dbContext.Reservations.Add(new Reservation()
            {
                DeskId = dto.DeskId,
                EmployeeId = dto.EmployeeId,
                BookingDate = dto.BookingDate.AddDays(dto.BookingDays - 1),
                ExpirationDate = dto.BookingDate
            }); 
            *//*desk.BookingDate = dto.BookingDate;
            desk.ExpirationDate = dto.BookingDate.AddDays(dto.BookingDays - 1);
            desk.EmployeeId = dto.EmployeeId;
            desk.isAvailable = false;*//*
            _dbContext.SaveChanges();

            return true;
        }

        public bool CancelReservation(Desk desk)
        {
            // TODO
            *//*desk.BookingDate = null;
            desk.ExpirationDate = null;
            desk.isAvailable = true;
            desk.EmployeeId = null;*//*
            _dbContext.SaveChanges();

            return true;
        }

        public bool ChangeReservation(Employee employee, Desk desk, Desk selectedDesk, DateTime bookingDate, int bookingDays)
        {
            // TODO
            *//*desk.BookingDate = null;
            desk.ExpirationDate = null;
            desk.isAvailable = true;
            desk.EmployeeId = null;

            selectedDesk.BookingDate = bookingDate;
            selectedDesk.ExpirationDate = bookingDate.AddDays(bookingDays - 1);
            selectedDesk.isAvailable = false;
            selectedDesk.EmployeeId = employee.Id;*//*

            _dbContext.SaveChanges();

            return true;
        }

        public bool ChangeDays(BookingDto dto)
        {
            // TODO
            var desk = _dbContext.Desks.FirstOrDefault(d => d.Id == dto.DeskId);
           *//* desk.BookingDate = dto.BookingDate;
            desk.ExpirationDate = dto.BookingDate.AddDays(dto.BookingDays - 1);*//*
            _dbContext.SaveChanges();

            return true;
        }*/
    }
}
