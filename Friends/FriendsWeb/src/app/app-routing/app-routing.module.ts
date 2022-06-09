import { NgModule } from '@angular/core';
import { EventsComponent } from '../components/events/events.component';
import { RouterModule, Routes } from '@angular/router';
import { EventDetailComponent } from '../components/event-detail/event-detail.component';
import { EventAddComponent } from '../components/event-add/event-add.component';
import { EventEditComponent } from '../components/event-edit/event-edit.component';

const routes: Routes = [
  { path: 'events', component:EventsComponent },
  { path: 'events/:id', component: EventDetailComponent},
  { path: 'add', component: EventAddComponent},
  { path: 'events/:id/edit', component: EventEditComponent}
];

@NgModule({
  declarations: [
  ],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
