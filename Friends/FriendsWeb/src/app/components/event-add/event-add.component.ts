import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { EventService } from 'src/app/services/event.service';
import { Event } from '../../Event/Event';

@Component({
  selector: 'app-event-add',
  templateUrl: './event-add.component.html',
  styleUrls: ['./event-add.component.css']
})
export class EventAddComponent implements OnInit {

  eventToAddForm:FormGroup = new FormGroup ({
    name : new FormControl(''),
    organizatorId : new FormControl(0),
    startDate : new FormControl(new Date()),
    coordinates : new FormControl('')
  });

  eventToAdd:Event = {
    id: 0,
    name: '',
    coordinates: '',
    startDate: new Date(),
    organizatorId: 0,
    membersId: new Array<number>()
  };

  constructor(private service: EventService) { }

  ngOnInit(): void {
  }

  onSubmit() {     

    this.eventToAdd.name = this.eventToAddForm.value['name'] as string;
    this.eventToAdd.coordinates = this.eventToAddForm.value['coordinates'] as string;
    this.eventToAdd.organizatorId = this.eventToAddForm.value['organizatorId'] as number;
    this.eventToAdd.startDate = this.eventToAddForm.value['startDate'] as Date;

    this.service.addEvent(this.eventToAdd);
  }

}
