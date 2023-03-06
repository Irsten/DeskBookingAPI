using AutoMapper;
using DeskBookingAPI.Entities;
using DeskBookingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeskBookingAPI.Services
{
    public interface IDeskService
    {
        bool CreateDesk(int roomId);
        bool DeleteDesk(Desk desk);
        List<DeskDto> GetAllDesksInRoom(int roomId);
        DeskDto GetDesk(int deskId);
    }

    public class DeskService : IDeskService
    {
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
    }
}
