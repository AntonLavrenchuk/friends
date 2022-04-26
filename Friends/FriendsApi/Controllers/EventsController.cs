using AutoMapper;
using FriendsApi.Models;
using FriendsData;
using FriendsData.Entities;
using FriendsData.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase, IDisposable
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EventsController(UnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create( EventCreateModel model)
        {

            //TODO: Validation
            if(ModelState is null)
            {
                return BadRequest();
            }

            var evt = _mapper.Map<EventCreateModel, Event>(model);
            var organizator = await _unitOfWork.UsersRepository.GetAsync(evt.OrganizatorId);
            evt.Organizator = organizator;

            await _unitOfWork.EventsRepository.AddAsync(evt);

            await _unitOfWork.Save();

            return Ok(evt);
        }

        [HttpGet]
        public async Task<IEnumerable<EventReadModel>> Read()
        {
            var events = await _unitOfWork.EventsRepository.GetWithInclude();

            return events.Select(evt => _mapper.Map<Event, EventReadModel>(evt));
        }

        [HttpGet("{id:int}")]
        public async Task<EventReadModel> Read(int id)
        {
            var evt = await _unitOfWork.EventsRepository.GetWithInclude(id);

            var result = _mapper.Map<Event, EventReadModel>(evt);

            return result;
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if(id< 0 ||
                ModelState is null)
            {
                return BadRequest();
            }


            await _unitOfWork.EventsRepository.DeleteAsync(id);
            await _unitOfWork.Save();

            var deletedEvent = await _unitOfWork.EventsRepository.GetAsync(id);

            if (deletedEvent is null)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, EventUpdateModel model)
        {
            if(id < 0 ||
                ModelState is null ||
                model is null)
            {
                return BadRequest();
            }

            var oldEvent = await _unitOfWork.EventsRepository.GetAsync(id);

            var updatedEvent = _mapper.Map<EventUpdateModel, Event>(model);
            updatedEvent.Members = oldEvent.Members;
            updatedEvent.Organizator = oldEvent.Organizator;
            updatedEvent.OrganizatorId = oldEvent.OrganizatorId;

            await _unitOfWork.EventsRepository.UpdateAsync(id, updatedEvent);

            await _unitOfWork.Save();

            return Ok();

        }

        [HttpPost("{memberId:int}/{eventId:int}")]
        public async Task<IActionResult> AddMember(int memberId, int eventId)
        {
            var member = await _unitOfWork.UsersRepository.GetAsync(memberId);
            var evt = await _unitOfWork.EventsRepository.GetAsync(eventId);

            if(member is null)
            {
                return NotFound("Member wasn't found");
            }
            if(evt is null)
            {
                return NotFound("Event wasn't found");
            }

            evt.Members.Add(member);
            member.Events.Add(evt);

            await _unitOfWork.Save();
           
            return Ok(evt);
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}
