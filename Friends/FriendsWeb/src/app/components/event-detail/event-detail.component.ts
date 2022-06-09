import { Component, OnInit} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {Event} from '../../Event/Event';
import { EventService } from '../../services/event.service';


@Component({
  selector: 'app-event-detail',
  templateUrl: './event-detail.component.html',
  styleUrls: ['./event-detail.component.css']
})
export class EventDetailComponent implements OnInit {

  event? : Event;
  eventIdFromRoute?: number;

  constructor(private route: ActivatedRoute, private eventsService: EventService) { }

  ngOnInit(): void {
    const routeParams = this.route.snapshot.paramMap;
    this.eventIdFromRoute = Number(routeParams.get('id'));

    this.eventsService.getEvents().subscribe(
      evts => {this.event = evts.find(evt => evt.id === this.eventIdFromRoute)
      console.log('abbbvvhdv');}
    );
  }

  onDelete() {
    this.eventsService.deleteEvent(this.eventIdFromRoute as number);
  }

}
