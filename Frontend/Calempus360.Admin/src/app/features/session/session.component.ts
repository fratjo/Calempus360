import { Component } from '@angular/core';
import { FullCalendarModule } from '@fullcalendar/angular';
import { CalendarOptions } from '@fullcalendar/core/index.js';
import dayGridPlugin from '@fullcalendar/daygrid';
import interactionPlugin from '@fullcalendar/interaction';
import timeGridPlugin from '@fullcalendar/timegrid';

@Component({
  selector: 'app-session',
  imports: [FullCalendarModule],
  templateUrl: './session.component.html',
  styleUrl: './session.component.scss',
})
export class SessionComponent {
  calendarOptions: CalendarOptions = {
    initialView: 'timeGridWeek',
    plugins: [dayGridPlugin, timeGridPlugin, interactionPlugin],
    headerToolbar: {
      left: 'prev,next',
      center: 'title',
      right: 'today,timeGridWeek,timeGridDay,dayGridMonth', // user can switch between the two
    },
    views: {
      dayGridMonth: {
        titleFormat: { year: 'numeric', month: 'long' },
      },
      timeGrid: {
        titleFormat: { year: 'numeric', month: 'long', day: 'numeric' },
      },
    },
    nowIndicator: true,
    weekends: false,
    editable: true,
    selectable: true,
    events: [
      {
        // this object will be "parsed" into an Event Object
        title: 'The Title', // a property!
        start: '2025-03-26T10:00:00', // a property!
        end: '2025-03-27T10:00:00', // a property! ** see important note below about 'end' **
      },
      {
        title: 'event 1',
        start: '2025-04-01',
        end: '2025-04-01',
      },
      { title: 'event 2', date: '2025-04-02' },
    ],

    eventClick(arg) {
      alert('Event: ' + arg.event.title);
    },

    eventDragStart(arg) {
      console.log('eventDragStart', arg.event.start);
    },

    eventDragStop(arg) {
      console.log('eventDragStop', arg.event.start);
    },

    eventDrop: function (info) {
      console.log(info.event.start);

      alert(
        info.event.title +
          ' was dropped on ' +
          (info.event.start ? info.event.start : 'an unknown date'),
      );

      if (!confirm('Are you sure about this change?')) {
        info.revert();
      }
    },

    eventResizeStart(arg) {
      console.log('eventResizeStart', arg.event.start);
    },

    eventResizeStop(arg) {
      console.log('eventResizeStop', arg.event.start);
    },
  };

  handleDateClick(arg: any) {
    alert('date click! ' + arg.dateStr);
  }

  toggleWeekends() {
    this.calendarOptions.weekends = !this.calendarOptions.weekends; // toggle the boolean!
  }
}
