using AutoMapper;
using DeskBookingAPI.Entities;
using DeskBookingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeskBookingAPI.Services
{
    public interface IRoomService
    {
        bool CreateRoom();
        bool DeleteRoom(Room room);
        List<RoomDto> GetAll();
    }

    public class RoomService : IRoomService
    {
        // TODO
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper; 

        public RoomService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public bool CreateRoom()
        {
            _dbContext.Rooms.Add(new Room());
            _dbContext.SaveChanges();

            return true;
        }

        public bool DeleteRoom(Room room)
        {
            _dbContext.Rooms.Remove(room);
            _dbContext.SaveChanges();

            return true;
        }


        public List<RoomDto> GetAll()
        {
            var listOfRooms = new List<RoomDto>();
            var rooms = _dbContext.Rooms.ToList();
            /*foreach (var room in rooms)
            {
                var tempDesks = _dbContext.Desks.Include(e => e.Employee).Where(d => d.RoomId == room.Id).ToList();
                var tempRoom = new RoomDto()
                {
                    RoomId = room.Id,
                    Desks = _mapper.Map<List<DeskDto>>(tempDesks),
                };
                listOfRooms.Add(tempRoom);
            }*/
            return listOfRooms;
        }
    }
}
