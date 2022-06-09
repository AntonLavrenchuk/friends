import { Component, OnInit } from '@angular/core';
import { Event } from 'src/app/Event/Event';
import { EventService } from '../../services/event.service';
import { ActivatedRoute, Router } from '@angular/router'

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.css']
})
export class EventsComponent implements OnInit {

  events?: Array<Event>;
  selectedEvent?: Event;

  constructor(private eventService : EventService,
    private router:Router) { }

  onClick( selectedEvent:Event): void {
    this.selectedEvent = selectedEvent;
  }

  ngOnInit(): void {
    this.eventService.getEvents().subscribe(events => this.events = events);
  }


}
