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
            var rooms = _dbContext.Rooms.ToList();

            var mappedRooms = _mapper.Map<List<RoomDto>>(rooms);

            foreach (var room in mappedRooms)
            {
                room.DesksNumber = _dbContext.Desks.Where(d => d.RoomId == room.Id).ToList().Count();
            }

            return mappedRooms;
        }
    }
}
