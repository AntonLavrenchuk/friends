import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {EventService} from '../../services/event.service';
import {Event} from '../../Event/Event';
import { FormControl, FormGroup } from '@angular/forms';
import { EventEditModel } from 'src/app/models/EventEditModel';
import { MappingService } from 'src/app/services/mapping.service';

@Component({
  selector: 'app-event-edit',
  templateUrl: './event-edit.component.html',
  styleUrls: ['./event-edit.component.css']
})
export class EventEditComponent implements OnInit {

  buttonDisabled: boolean = true;
  eventIdFromRoute?: number;

  private event?: Event;
  eventToEditForm:FormGroup = new FormGroup ({
    name : new FormControl(''),
    startDate : new FormControl(new Date()),
    coordinates : new FormControl('')
  });


  constructor(private route: ActivatedRoute,
    private eventService: EventService,
    private mapper: MappingService) { }

  ngOnInit(): void {
    const routeParams = this.route.snapshot.paramMap;
    this.eventIdFromRoute = Number.parseInt(routeParams.get('id') as string) ;
    
    this.eventService.getEvents().subscribe(
      evts => {
      this.event = evts.find(evt => evt.id === this.eventIdFromRoute);
      console.log(this.event);
      
      this.eventToEditForm.get('name')?.setValue(this.event?.name);
      this.eventToEditForm.get('coordinates')?.setValue(this.event?.coordinates);
      this.eventToEditForm.get('startDate')?.setValue(this.event?.startDate);
    }
    );
  }

  onChange() : void {
    
    console.log(this.eventToEditForm.value);
    console.log(this.event);
    console.log(this.eventToEditForm.value as EventEditModel === this.event);

    if(JSON.stringify(this.eventToEditForm.value) == JSON.stringify(this.event)) {
      this.buttonDisabled = true;
      return;
    }

    if(this.eventToEditForm.value.name !== ''
    || this.eventToEditForm.value.coordinates !== '') {
      this.buttonDisabled = false;
    }
  }

  onSubmit(): void {
    const evt = this.eventToEditForm.value as Event;

    this.eventService.editEvent(this.eventIdFromRoute as number, evt);
  }

}
