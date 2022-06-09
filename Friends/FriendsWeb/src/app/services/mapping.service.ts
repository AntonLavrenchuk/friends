import { Injectable } from '@angular/core';
import { Event } from '../Event/Event';
import { EventAddModel } from '../models/EventAddModel';
import { EventEditModel } from '../models/EventEditModel';

@Injectable({
  providedIn: 'root'
})
export class MappingService {

  

  constructor() { }

  mapToEventAddModel(evt: Event):EventAddModel {
    const addModel = {
      name: evt.name,
      organizatorId: evt.organizatorId,
      coordinates: evt.coordinates,
      startDate: evt.startDate
    };

    return addModel as EventAddModel;
  }
  
  mapToEventEditModel(evt: Event): EventEditModel {
    const editModel = {
      name: evt.name,
      startDate: evt.startDate,
      coordinates: evt.coordinates
    };

    return editModel as EventEditModel;
  }

}
