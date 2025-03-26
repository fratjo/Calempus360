import { Component } from '@angular/core';
import { FullCalendarModule } from '@fullcalendar/angular';
import { CalendarOptions } from '@fullcalendar/core/index.js';
import dayGridPlugin from '@fullcalendar/daygrid';
import interactionPlugin from '@fullcalendar/interaction';

@Component({
  selector: 'app-session',
  imports: [FullCalendarModule],
  templateUrl: './session.component.html',
  styleUrl: './session.component.scss',
})
export class SessionComponent {
  calendarOptions: CalendarOptions = {
    initialView: 'dayGridMonth',
    plugins: [dayGridPlugin, interactionPlugin],
    dateClick: (arg) => this.handleDateClick(arg),
    events: [
      { title: 'event 1', date: '2025-04-01' },
      { title: 'event 2', date: '2025-04-02' },
    ],
  };

  handleDateClick(arg: any) {
    alert('date click! ' + arg.dateStr);
  }
}
