import { Injectable } from '@angular/core';
import { Event } from '../Event/Event';
import { catchError, Observable, of } from 'rxjs';
import {HttpClient} from '@angular/common/http';
import { GlobalConstants } from '../GlobalConstants';
import { MappingService } from './mapping.service';
import { EventAddModel } from '../models/EventAddModel';
import { EventEditModel } from '../models/EventEditModel';

@Injectable({
  providedIn: 'root'
})
export class EventService {

  getEvents() : Observable<Array<Event>> {
    return this.http.get<Array<Event>>( GlobalConstants.apiURL + '/Events');
  }

  addEvent( evt:Event) : void {

    const eventToAdd = this.mapper.mapToEventAddModel(evt);

    console.log(eventToAdd);
    this.http.post<EventAddModel>(GlobalConstants.apiURL + '/Events', eventToAdd).subscribe(
      
    );
    
  }

  editEvent(id: number, evt: Event):void {
    var evtModel = this.mapper.mapToEventEditModel(evt);

    this.http.put<EventEditModel>(GlobalConstants.apiURL + '/Events/'+ id, evtModel).subscribe();
  }

  deleteEvent( id: number) :void {
    console.log(id);
    this.http.delete(GlobalConstants.apiURL+'/Events/'+id).subscribe();
  }

  constructor(private http: HttpClient,
    private mapper: MappingService) { }
}